using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.IO;
using GreedyToolkit.Media.Flv;
using GreedyToolkit.Aliyun.Oss;

namespace OssDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void btnUpload_Click(object sender, EventArgs e)
        {

            btnUpload.Enabled = false;
            var bucketName = txtBucketName.Text;
            var keyID = txtKeyId.Text;
            var keySecret = txtKeySecret.Text;

            var path = txtFileName.Text;// args[0];
            //var path2 = @"d:\0.dat";
            //Console.WriteLine(path);
            var fileInfo = new FileInfo(path);
            var prefix = Path.GetFileNameWithoutExtension(fileInfo.Name);

            using (var f = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var dealer = new FlvDealer(f);
                var head = dealer.Deal();
                var sengments = dealer.Segments;
                var start = (int)dealer.StartPosition;
                var end = (int)dealer.EndPosition;

                //using (var f2 = File.Open(path2, FileMode.Create, FileAccess.Write))
                //{
                //    f2.Write(head, 0, head.Length);
                //}

                //var section = AliyunSection.GetInstance();
                var client = new OssClient(txtKeyId.Text, txtKeySecret.Text);
                using (var ms = new MemoryStream(head))
                {
                    var headname = prefix + "/0.dat";
                    client.PutObject(new PutObjectRequest(txtBucketName.Text, headname, ms));
                    Console.WriteLine("{0}上传完成", headname);
                }

                f.Position = start;
                var positions = sengments["positions"] as MetaArray;
                var files = sengments["files"] as MetaArray;
                var ext = sengments["extension"].ToString();

                var tasks = new List<Task>();
                for (int i = 0; i < positions.Length; i++)
                {
                    //var bodyname = string.Format("{0}/{1}{2}", prefix, files[i], ext);
                    //client.PutObject(new PutObjectRequest(section.Oss.Bucket, bodyname, f, start, Convert.ToUInt32(positions[i]) - start));
                    //Console.WriteLine("{0}上传完成", bodyname);
                    //start = Convert.ToInt32(positions[i]);

                    var task = new Task(
                        (obj) =>
                        {
                            PutObjectRequest req;
                            string bodyname;

                            var j = (int)obj;
                            var position = Convert.ToInt32(positions[j]);
                            bodyname = string.Format("{0}/{1}{2}", prefix, files[j], ext);
                            var vStart = start;
                            if (j > 0)
                            {
                                vStart = Convert.ToInt32(positions[j - 1]);
                            }

                            req = new PutObjectRequest(
                                 txtBucketName.Text,
                                 bodyname,
                                 File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read),
                                 vStart,
                                Convert.ToUInt32(position - vStart));
                            Console.WriteLine("{0}  {1}  {2}", position, bodyname, vStart);
                            //start = position;

                            var res = client.PutObject(req);
                            Console.WriteLine("{0}上传完成", bodyname);

                        }, i);
                    //tasks.Last().ContinueWith(task);
                    Console.WriteLine(
                        tasks.Count(
                            t =>
                            t.Status == TaskStatus.Running || t.Status == TaskStatus.WaitingToRun
                            || t.Status == TaskStatus.WaitingForActivation));
                    tasks.RemoveAll(t => t.Status == TaskStatus.RanToCompletion);
                    //Console.WriteLine("移除{0}个", tasks.RemoveAll(t => t.Status == TaskStatus.RanToCompletion));
                    if (tasks.Count(t => t.Status == TaskStatus.Running || t.Status == TaskStatus.WaitingToRun || t.Status == TaskStatus.WaitingForActivation) >= 5)
                    {
                        Task.WaitAny(tasks.ToArray());
                    }
                    tasks.Add(task);
                    task.Start();
                }

                Task.WaitAll(tasks.ToArray());
                Console.WriteLine("上传完毕");
                MessageBox.Show("上传完毕", "提示");
                btnUpload.Enabled = true;
            }


            //var info = new FlvInfo(path2);
            //Console.WriteLine(info.Header.ToString());
            //Console.WriteLine("=============================================");
            //foreach (var tag in info.Body.Tags.Where(t => t.TagType == TagType.Meta))
            ////foreach (var tag in info.Body.Tags.Where(t => t.TagType == TagType.Video && ((t as VideoTag).Data as VideoData).Type == FrameType.KeyFrame))
            //{
            //    Console.WriteLine(tag.ToString());
            //    Console.WriteLine(tag.TagType.ToString());
            //    Console.WriteLine(tag.Header.ToString());
            //    if (tag.Data != null)
            //    {
            //        Console.WriteLine(tag.Data.ToString());
            //    }
            //    Console.WriteLine("=============================================");
            //    Console.ReadKey();

            //}
            //}
        }

        private void btnChoseFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "flv files (*.flv)|*.flv";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
                txtFileName.Text = openFileDialog.FileName;
        }

    }
}
