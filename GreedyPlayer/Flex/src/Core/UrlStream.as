package Core
{
	import CustomEvent.LoadingEvent;
	
	import Util.BrowserInterface;
	
	import flash.events.Event;
	import flash.events.EventDispatcher;
	import flash.events.IOErrorEvent;
	import flash.events.ProgressEvent;
	import flash.net.URLRequest;
	import flash.net.URLStream;
	import flash.utils.ByteArray;

	public class UrlStream extends EventDispatcher
	{
		private var _url:String;
		private var _progressCallBack:Function;
		private var _urlStream:URLStream;
		private var _buffer:ByteArray = new ByteArray();
		public  const  OnProgress:String="onProgress";
		public const OnComplete:String="onComplete";
		
		public function UrlStream(url:String)
		{
			this._url=url;
			init();
//			this._progressCallBack=progressCallBack;
		}
		private function init():void
		{
			_urlStream = new URLStream();
			var request:URLRequest = new URLRequest(_url);
			_urlStream.addEventListener(IOErrorEvent.IO_ERROR, errorHandler);
			_urlStream.addEventListener(ProgressEvent.PROGRESS, progressHandler);
//			_urlStream.addEventListener(Event.COMPLETE,completeHandler);
			_urlStream.load(request);
		}
		private function errorHandler(event:IOErrorEvent):void
		{
			BrowserInterface.call("console.log",  event.text);
		}
		private function progressHandler(event:ProgressEvent):void
		{
			_urlStream.readBytes(_buffer,0,_urlStream.bytesAvailable);
			_buffer.position = 0;
			if(_buffer.bytesAvailable)
			{
//				this._progressCallBack(event.bytesLoaded,event.bytesTotal,_buffer);
				var e:LoadingEvent=new LoadingEvent(OnProgress);
				e.Data=_buffer;
				e.TotalData=event.bytesTotal;
				dispatchEvent(e);
				_buffer.clear();
			}
		}
//		private function completeHandler():void
//		{
//			var e:Event=new Event(OnComplete);
//			dispatchEvent(e);
//		}
	}
}