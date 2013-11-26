package Util
{
	import flash.utils.ByteArray;

	public class Help
	{
		public function Help()
		{
		}
		//从byteArray读取指定字节
		public static function cutByteArray(byteArrayBase:ByteArray,byteArrayBasePosition:uint, newByteArrayOffset:uint=0, readLength:uint=0):ByteArray
		{
			var bytes:ByteArray = new ByteArray();
			byteArrayBase.position=byteArrayBasePosition;
			byteArrayBase.readBytes(bytes,0,readLength);
			byteArrayBase.position=0;
			return bytes;
		}
		public static function joinByteArrays(p1:ByteArray,p2:ByteArray):ByteArray
		{
			var bytes:ByteArray=new ByteArray();
			var head:ByteArray=p1;
			var bodyPart:ByteArray=p2;
			head.position=0;
			head.readBytes(bytes,0,head.length);
			bodyPart.position=0;
			bodyPart.readBytes(bytes,head.length,bodyPart.length);
			return bytes;
		}
		//拼接byteArray
		public static function joinByteArray(byteArrays:Vector.<ByteArray>):ByteArray
		{
			var bytes:ByteArray=new ByteArray();
			byteArrays[0].position=0;
			byteArrays[0].readBytes(bytes,0,byteArrays[0].length);
			for(var i:int=1;i<byteArrays.length;i++)
			{
				byteArrays[i].position=0;
				byteArrays[i].readBytes(bytes,byteArrays[i-1].length,byteArrays[i].length);
			}
			return bytes;
		}
		//时间转换
		public static function timeFormat(second:uint):String
		{
			var time:String ="";
			if (second >= 24 * 3600) {
				//time += parseInt(second / 24 * 3600) + '天';
				time+=Math.floor(second / 24 * 3600).toString() + ':';
				second %= 24 * 3600;
			}
			if (second >= 3600) {
				//time += parseInt(second / 3600) + '小时';
				time += Math.floor(second / 3600) + ':';
				second %= 3600;
			}
			if (second >= 60) {
				//        time += parseInt(second / 60) + '分钟';
				time +=Math.floor(second / 60) + ':';
				second %= 60;
			}
			if (second >= 0) {
				//time += second + '秒';
				time += second;
			}
			var arr:Array=time.split(":");
			var day:String="",h:String="",min:String="",sec:String="";
			var str:String="";
			if(arr.length==1)
			{
				if(!arr[0])
				{
					sec="00";
				}
				else
				{
					sec=arr[0]<10?"0"+arr[0]:arr[0];
				}
				str="00:00:"+sec;
			}
			if(arr.length==2)
			{
				min=arr[0]<10?"0"+arr[0]:arr[0];
				sec=arr[1]<10?"0"+arr[1]:(arr[1]>0?arr[1]:"00");
				str="00:"+min+":"+sec;
			}
			if(arr.length==3)
			{
				h=arr[0]<10?"0"+arr[0]:arr[0];
				min=arr[1]<10?"0"+arr[1]:arr[1];
				sec=arr[2]<10?"0"+arr[2]:arr[2];
				str=h+":"+min+":"+sec;
			}
			if(arr.length==4)
			{
				day=arr[0]<10?"0"+arr[0]:arr[0];
				h=arr[1]<10?"0"+arr[1]:arr[1];
				min=arr[2]<10?"0"+arr[2]:arr[2];
				sec=arr[3]<10?"0"+arr[3]:arr[3];
				str=h+":"+min+":"+sec;
			}
			return str;
		}
	}
}