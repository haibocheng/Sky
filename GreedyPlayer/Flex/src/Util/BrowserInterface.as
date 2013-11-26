package Util
{
	import Model.CallBack;
	
	import flash.external.ExternalInterface;
	
	import spark.components.List;

	//浏览器公开接口
	public class BrowserInterface
	{
		public function BrowserInterface()
		{
		}
		//调用浏览器、js的方法
		public static function call(functionName:String, ... arguments):void
		{
			ExternalInterface.call(functionName,  arguments);
		}
		//提供js可调用的方法
		public static function callback(functionName:String, closure:Function):void
		{
			ExternalInterface.addCallback(functionName,closure);  
		}
		//注册js可调用方法
		public static function registCallBack(parameter:CallBack):void
		{
			callback(parameter.FunctionName,parameter.Closure);
		}
	}
}