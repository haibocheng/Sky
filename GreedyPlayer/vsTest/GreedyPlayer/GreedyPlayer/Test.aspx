<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="GreedyPlayer.Test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GreedyPlayerTest</title>
</head>
<body>
    <script type="text/javascript" src="/Scripts/jquery-1.5.1.js"></script>
    <script type="text/javascript" src="/Scripts/greedyPlayer/swfobject.js"></script>
    <script type="text/javascript" src="/Scripts/greedyPlayer/greedyplayer.js"></script>
    <div id="flashContent">
    </div>
    <input type="button" value="测试" onclick="test()" />
    <script type="text/javascript">
        // For version detection, set to min. required Flash Player version, or 0 (or 0.0.0), for no version detection. 
        //        var swfVersionStr = "11.1.0";
        //        // To use express install, set to playerProductInstall.swf, otherwise the empty string. 
        //        var xiSwfUrlStr = "playerProductInstall.swf";
        //        var flashvars = { url: "http://testmyaudios.oss-cn-hangzhou.aliyuncs.com/334/0.dat" };
        //        var params = {};
        //        params.quality = "high";
        //        params.bgcolor = "#ffffff";
        //        params.allowscriptaccess = "sameDomain";
        //        params.allowfullscreen = "true";
        //        var attributes = {};
        //        attributes.id = "greedyPlayer";
        //        attributes.name = "greedyPlayer";
        //        attributes.align = "middle";
        //        swfobject.embedSWF(
        //                "Scripts/greedyPlayer/GreedyPlayer.swf", "flashContent",
        //                "800", "635",
        //                swfVersionStr, xiSwfUrlStr,
        //                flashvars, params, attributes);
        //        function test() {
        //            var greedyPlayer = getGreedyPlayer("greedyPlayer");
        //            greedyPlayer.play();
        //        }
        //        function getGreedyPlayer(id) {
        //            if (window.document[id]) {
        //                return window.document[id];
        //            } else if (navigator.appName.indexOf("Microsoft") == -1) {
        //                if (document.embeds && document.embeds[id])
        //                    return document.embeds[id];
        //            } else {
        //                return document.getElementById(id);
        //            }
        //        }
        //        var a = new greedyPlayer("flashContent");
        //        a.setup({
        //            file: "http://testmyaudios.oss-cn-hangzhou.aliyuncs.com/334/0.dat",
        //            width: 800,
        //            height: 635
        //        });
        //        a.setgreedyPlayer();
        //        function test() {
        //            a.seek(50);
        //        }
        GreedyPlayer("flashContent").setup({
            //            file: "http://hmtest.oss.aliyuncs.com/151/0.dat",
            file: "http://wenjuntest.oss-cn-hangzhou.aliyuncs.com/vvv/0.dat",
            //            file: url,
            //            file: "http://greedyint-1course.oss.aliyuncs.com/xinketang/video/8f536a29234b4499907c0848474bf8d5/0.dat",
            //            file: "http://greedyint-1course.oss.aliyuncs.com/xinketang/video/00dd6f87e9434cd9a1fa19ed1e17cff7-131031/0.dat",
            width: 800,
            height: 600,
            autoplay: false,
            isShowSlider: true
        });
        //        function test() {
        //            greedyPlayer.play();
        //        }
    </script>
</body>
</html>
