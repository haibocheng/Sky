package Core
{
	import CustomEvent.FinishEvent;
	import CustomEvent.MetaDataEvent;
	import CustomEvent.WaitEvent;
	
	import Util.BrowserInterface;
	import Util.Help;
	
	import flash.events.EventDispatcher;
	import flash.events.NetStatusEvent;
	import flash.net.NetConnection;
	import flash.net.NetStream;
	import flash.net.NetStreamAppendBytesAction;
	import flash.utils.ByteArray;
	
	import mx.messaging.AbstractConsumer;

	public class Netstream extends EventDispatcher
	{
		private var _netStream:NetStream;
		private var _netConnection:NetConnection;
		private var _tags:Array=new Array();
		private var _ba:ByteArray;
		private var _seekBa:ByteArray;
		public  const  OnWait:String="onWait";
//		private var _onMetaDataCallBack:Function;
		//公开的函数
		public  const  OnMetaData:String="onMetaData";
		public  const  OnFinish:String="onFinish";
//		private var FinishCallBack：Function;
		public function Netstream(ba:ByteArray)
		{
//			this._onMetaDataCallBack=onMetaDataCallBack;
			_ba=ba;
			init();
		}
		private function init():void
		{  
//			_ns = new NetStream(nc);
//			var netclient:Object=new Object();
//			_ns.client =netclient;
//			//			_video.attachNetStream(_ns);
//			_ns.play(null);
//			_ns.appendBytes(_ba);
			
			_netConnection = new NetConnection();
			_netConnection.connect(null);
			_netStream = new NetStream(_netConnection);
			var netclient:Object=new Object();
			_netStream.client = netclient;
			_netStream.client.onMetaData=onMetaDataHandler;
			_netStream.bufferTime = 5;
			_netStream.addEventListener(NetStatusEvent.NET_STATUS,netStatusHandler);
			_netStream.play(null);
			_netStream.appendBytes(_ba);
		}
		private function onMetaDataHandler(infoObject:Object):void
		{
			for (var propName:String in infoObject) {
				if (propName == "keyframes")
				{
					var kfObject:Object = infoObject[propName];
					var timeArr:Array=kfObject["times"];
					var byteArr:Array=kfObject["filepositions"];
					for(var i:int=0;i<timeArr.length;i++)
					{
						var tagPos:int = byteArr[i];//Read the tag size;
						var timestamp:Number = timeArr[i];//read the timestamp;
						_tags.push({timestamp:timestamp,tagPos:tagPos});
						
					}
				}
			}
			var e:MetaDataEvent=new MetaDataEvent(OnMetaData);
			e.Info=infoObject;
			dispatchEvent(e);
		}
		private function netStatusHandler(event:NetStatusEvent):void
		{
			switch (event.info.code) {
				
				case "NetStream.Seek.Notify" :
					//					event.info.
					_netStream.appendBytesAction(NetStreamAppendBytesAction.RESET_SEEK);
//					_ba.position = _seekPos;
//					var bytes:ByteArray = new ByteArray();
//					_ba.readBytes(_seekBa);
					_netStream.appendBytes(_seekBa);
					var ew:WaitEvent=new WaitEvent(OnWait);
					ew.State=true;
					//			e.TotalData=event.TotalData
					dispatchEvent(ew);
					
//					_netStream.resume();
					break;
				case "NetStream.Play.Start":
					//					UpdataTimeBar();
					break;
				case "NetStream.Buffer.Empty":
					//					UpdataTimeBar();
//					if(FinishCallBack!=null)
//						FinishCallBack();
					var e:FinishEvent=new FinishEvent(OnFinish);
					dispatchEvent(e);
					break;
			}
		}
		//开始
		public  function start():void
		{
//			load();
		}
		//暂停
		public  function pause():void
		{
			_netStream.pause();
		}
		//停止
		public  function stop():void
		{
			_netStream.close();
//			this._params.UiComponent.removeChild(_video);
		}
		//取消
		public function dispose():void
		{
			_netStream.dispose();
		}	
		//继续
		public  function resume():void
		{
			_netStream.resume();
		}
		//seek
		public  function seek(seektime:Number,ba:ByteArray):void
		{
			_seekBa=ba;
			_netStream.seek(seektime);
			
		}
		//播放
		public  function play(ba:ByteArray):void
		{
//			_netStream.play(null);
			_netStream.appendBytes(ba);
		}
		//获取当前播放时间
		public function getCurrentTime():Number
		{
			return _netStream.time;
		}
		//获取总共时间
		public function getBytesTotal():Number
		{
			return _netStream.bytesTotal;
		}
		//获取当前加载的byte
		public function getBytesLoader():Number
		{
			return _netStream.bytesLoaded;
		}
		public function getNetStream():NetStream
		{
			return _netStream;
		}

	}
}