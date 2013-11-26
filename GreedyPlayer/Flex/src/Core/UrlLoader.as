package Core
{
	import CustomEvent.CompleteEvent;
	import CustomEvent.LoadingEvent;
	
	import Util.BrowserInterface;
	
	import flash.events.DataEvent;
	import flash.events.Event;
	import flash.events.EventDispatcher;
	import flash.events.ProgressEvent;
	import flash.events.SecurityErrorEvent;
	import flash.external.ExternalInterface;
	import flash.net.URLLoader;
	import flash.net.URLLoaderDataFormat;
	import flash.net.URLRequest;
	import flash.utils.ByteArray;
	
	import mx.core.ComponentDescriptor;

	public class UrlLoader extends EventDispatcher
	{
		public  const  OnComplete:String="onMetaData";
		public  const  OnProgress:String="onProgress";
		//私有变量
		private var _url:String;
//		private var _completeCallBack:Function;
		//开放函数
//		public var WaitForLoade:Function;
		public function UrlLoader(url:String)
		{
			this._url=url;
//			this._completeCallBack=completeCallBack;
			init();
		}
		private function init():void
		{
			var loader:URLLoader = new URLLoader();
			loader.dataFormat=URLLoaderDataFormat.BINARY ;
//			addListeners(loader);
			loader.addEventListener(Event.COMPLETE, completeHandler);
			loader.addEventListener(ProgressEvent.PROGRESS, progressHandler);
			loader.addEventListener(SecurityErrorEvent.SECURITY_ERROR, securityErrorHandler);
			var request:URLRequest = new URLRequest(this._url);
			loader.load(request);
			//等待事件
//			if(this.WaitForLoade)
//				WaitForLoade(true);
			
		}
		private function completeHandler(event:Event):void {
			var _ba:ByteArray= event.target.data as ByteArray;
			var e:CompleteEvent =new CompleteEvent(OnComplete);
			e.Data=_ba;
			dispatchEvent(e);
//			_completeCallBack(_ba);
//			if(this.WaitForLoade)
//				WaitForLoade(false);
		}
		//加載進度
		private function progressHandler(event:ProgressEvent):void {
//			OnProgressCallBack(event.bytesLoaded,event.bytesTotal);
			var e:LoadingEvent=new LoadingEvent(OnProgress);
			e.LoaderData=event.bytesLoaded;
			e.TotalData=event.bytesTotal;
			dispatchEvent(e);
		}
		//沙箱错误
		private function securityErrorHandler(event:SecurityErrorEvent):void {
			BrowserInterface.call("console.log",  event.text);
		}
	}
}