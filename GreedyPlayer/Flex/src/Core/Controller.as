package Core
{
	import CustomEvent.CompleteEvent;
	import CustomEvent.FinishEvent;
	import CustomEvent.LoadingEvent;
	import CustomEvent.MetaDataEvent;
	import CustomEvent.SeekCompleteEvent;
	import CustomEvent.WaitEvent;
	
	import Util.BrowserInterface;
	import Util.Help;
	
	import flash.events.Event;
	import flash.events.EventDispatcher;
	import flash.media.Video;
	import flash.net.NetStream;
	import flash.utils.ByteArray;
	
	import flashx.textLayout.elements.BreakElement;
	
	import mx.controls.List;
	import mx.utils.NameUtil;

	public class Controller extends EventDispatcher
	{
		public const LeftTime:Number=30;
		private var _params:PlayerOptions;
		private var _loaderCounter:uint=0;
		private var _head:ByteArray;
		private var _file0:ByteArray;
		private var _file1:ByteArray;
//		private var _nsHead:Netstream;
		private var _playTime:Number;
		private var _seekPos:Number;
		private var _seekTime:Number;
		private var _isCompleteByteLenth:uint=0;
		private var _isAutoLoad:Boolean;
		private var _ns:Netstream;
		private var _ba:ByteArray=new ByteArray();
//		private var _baList:Vector.<ByteArray>=new Vector.<ByteArray>();
		private var _baHead:ByteArray;
		private var _urlStream:UrlStream;
		private var _urlLoader:UrlLoader;
		private var _extension:String;
		private var _files:Array;
		private var _urlForebody:String;
		private var _positionTimes:Array=new Array();
		private var _positions:Array;
		private var _times:Array;
		private var _filepositins:Array;
		private var _currentTime:Number;
		private var _urlStreamFlg:Boolean=true;
		private var _urlStreamIncreaseLength:uint=0;
		private var _buffer:ByteArray=new ByteArray();
		private var _fileIsOK:Array=new Array();
		private var _video:Video;
		private var _totalTime:uint;
		private var _fileIndex:uint=0;
		private var _isNext:Boolean=true;
		private var _width:Number;
		private var _height:Number;
		public  const  OnComplete:String="onComplete";
		public  const  OnProgress:String="onProgress";
		public  const  OnSeek:String="onSeek";
		public  const  OnFinish:String="onFinish";
		public  const  OnWait:String="onWait";
		public function Controller(params:PlayerOptions)
		{
			_params=params;
			init();
		}
		private function init():void
		{
			//下载0号文件
			_urlForebody=_params.Url.substring(0,_params.Url.lastIndexOf("/"));
			var urlLoader:UrlLoader = new UrlLoader(_params.Url);
			urlLoader.addEventListener(urlLoader.OnComplete,urlLoaderCompleteHandle);
		}
		//0号文件下载完毕
		private function urlLoaderCompleteHandle(evenet:CompleteEvent):void
		{
			_file0=evenet.Data;
			//将0号文件加载到ns中
			_ns=new Netstream(_file0);
			_ns.addEventListener(_ns.OnMetaData,headMetaDataHandle);
			_ns.addEventListener(_ns.OnFinish,finishHandle);

		}
		//跳转用到的头部（0号文件+预备关键帧（AVC））
		private function urlLoaderCompleteHandle0(evenet:CompleteEvent):void
		{
			_file1=evenet.Data;
			var bTemp:ByteArray=new ByteArray();
			//针对AVC格式文件的处理（H263和H264（AVC）公用）
			for(var i:Number=0;i<79;i++)
			{
				bTemp.writeByte(_file1[i]);
			}
			bTemp.position=0;
			_baHead=Help.joinByteArrays(_file0,bTemp);
			
		}
		//播放完毕
		private function finishHandle(event:FinishEvent):void
		{
			var e:FinishEvent=new FinishEvent(OnFinish);
			dispatchEvent(e);
		}
		//播放等待完毕
		private function waitHandle(event:WaitEvent):void
		{
			var e:WaitEvent=new WaitEvent(_ns.OnWait);
			e.State=event.State;
			//			e.TotalData=event.TotalData
			dispatchEvent(e);
		}
		//头部加载完毕
		private function headMetaDataHandle(event:MetaDataEvent):void
		{
			
			
			
			for (var propName:String in event.Info) {
				if(propName=="segments")
				{
					for(var name:String in event.Info[propName] )
					{
//						trace(a);
//						trace(event.Info[propName][a])
						if(name.toLowerCase()=="extension")
						{
							_extension=event.Info[propName][name];
							continue;
						}
						if(name.toLowerCase()=="positions")
						{
							_positions=event.Info[propName][name];
							continue;
						}
						if(name.toLowerCase()=="files")
						{
							_files=event.Info[propName][name];
						}
					}
					continue;
				}
				if(propName=="keyframes")
				{
					for(var key:String in event.Info[propName])
					{
						
						if(key.toLowerCase()=="times")
						{
							_times=event.Info[propName][key];
							continue;
						}
						if(key.toLowerCase()=="filepositions")
						{
							_filepositins=event.Info[propName][key];
							continue;
						}
					}
				}
			}
			//加载第一个文件
			var urlLoader0:UrlLoader = new UrlLoader(_urlForebody+"/"+"1"+_extension);
			urlLoader0.addEventListener(urlLoader0.OnComplete,urlLoaderCompleteHandle0);
			//生成截断点对应的时间数组
			var fileopsitionIndex:int=0;
			for(var i:int=0;i<_filepositins.length;i++)
			{
				if(_filepositins[i]==_positions[fileopsitionIndex])
				{
					_positionTimes.push(_times[i]);
					_fileIsOK.push(false);
					fileopsitionIndex++;
				}
			}
			_positionTimes.push(_times[_filepositins.length-1]);
//			_nsHead.dispose();
			_ba.position=_filepositins[0];
			_width=event.Info["width"];
			_height=event.Info["height"];
			var tempWidth:Number=this._params.UiComponent.width;
			var tempHeight:Number=this._params.UiComponent.height;
			var videoWidth:Number;
			var videoHeight:Number;
			var rateW:Number=tempWidth/_width;
			var rateH:Number=tempHeight/_height;
			var rate:Number=1;
			if (tempWidth ==0 && tempHeight==0){
				rate = 1;
			}else if (tempWidth==0){//
				if (rateH<1) rate = rateH;
			}else if (videoHeight==0){
				if (rateW<1) rate = rateW;
			}else if (rateW<1 || rateH<1){
				rate = (rateW<=rateH?rateW:rateH);
			}
			else if(rateW>1 && rateH>1)
			{
				rate=(rateW<=rateH?rateW:rateH);
			}
//			if (rate<1){
				_width = _width * rate;
				_height = _height * rate;
//			}
			_video=new Video(_width,_height);
			this._params.UiComponent.width=_width;
			this._params.UiComponent.height=_height;
			this._params.UiComponent.addChild(_video);
//			总的时长
			_totalTime=event.Info["duration"];
			_video.attachNetStream(_ns.getNetStream());
			var e:MetaDataEvent =new MetaDataEvent(OnComplete);
			var obj:Object=new Object();
			obj.duration=_totalTime;
			obj.positionTimes=_positionTimes;
			obj.filesize=event.Info["filesize"];
			obj.height=event.Info["height"];
			obj.width=event.Info["width"];
			e.Info=obj;
			dispatchEvent(e);
		}
		//播放
		public function play( isAutoLoad:Boolean,time:Number=0):void
		{
			
			var fileIndex:uint=0;
			_playTime=time;
			_isAutoLoad=isAutoLoad;
			for(var i:int=0;i<_positionTimes.length;i++)
			{
				if(time<=_positionTimes[i]-LeftTime&&_urlStreamFlg)
				{
					fileIndex=i;
					break;
				}
			}
			_fileIndex=fileIndex;
			if(time>=_positionTimes[fileIndex]-LeftTime&&_positionTimes[fileIndex]-LeftTime>0&&_urlStreamFlg)
			{
				
//				if(_fileIsOK[_fileIndex+1])
//				{
//					
//					seek(time);
//				}
//				else
//				{
					_urlStream=new UrlStream(_urlForebody+"/"+_files[_fileIndex+1]+_extension);
					_urlStream.addEventListener(_urlStream.OnProgress,urlStreamProgressHandler);
					_fileIndex++;
//				}
			}
			else
			{
				_urlStream=new UrlStream(_urlForebody+"/"+_files[_fileIndex]+_extension);
				_urlStream.addEventListener(_urlStream.OnProgress,urlStreamProgressHandler);
			}
		}
		//按需下载（顺序下载）
		private function urlStreamProgressHandler(event:LoadingEvent):void
		{
			_ba.writeBytes(event.Data);
			_buffer.writeBytes(event.Data);
//			_urlStreamIncreaseLength+=event.Data.length;
			_ns.play(event.Data);
			var e:LoadingEvent=new LoadingEvent(OnProgress);
			e.LoaderData=_ba.length;
			e.TotalData=event.TotalData
			dispatchEvent(e);
			if(_buffer.length>=event.TotalData)
			{
				_buffer.clear();
				_fileIsOK[_fileIndex]=true;
				_urlStreamFlg=true;
			}
			
		}
		//从指定位置获取文件
		private function getSeekByte(seekPos:Number):ByteArray
		{
//			if(seekPos<_ba.length)
//			{
//				_ns.appendBytesAction(NetStreamAppendBytesAction.RESET_SEEK);
				_ba.position = seekPos;
				var bytes:ByteArray = new ByteArray();
				_ba.readBytes(bytes);
				return bytes;
//			}
		}
		//跳转
		public function seek(time:Number):void
		{
			var cTime:Number= 0;
			var pTime:Number= 0;
			var seekPos:Number=0;
			var ba:ByteArray=new ByteArray();
			var seekTime:Number=0;
			for (var i:int=1; i<_times.length; i++)
			{
				cTime  = _times[i];
				pTime=_times[i-1];
				
				if(pTime < time)
				{
					if(time>=_times[_times.length-1])
					{
						seekPos=_filepositins[_filepositins.length-1];
						if(seekPos<_ba.length)
						{
							ba=getSeekByte(seekPos);
							_seekTime=_times[_times.length-1];
							_ns.seek(_seekTime,ba);
						}
						else
						{
							_seekTime=_times[_times.length-1];
							_seekPos=seekPos;
							changeBaPosition(seekPos);
//							_ba.position=seekPos;
							stop();
							replaceNs();
							playSeek(_seekTime);
//							_ns.resume();
						}
//						seekCallBack(_tags[_tags.length-1].timestamp);
						break;
					}
					else
					{
						if(time < cTime){
							seekPos=_filepositins[i-1];
							if(seekPos<_ba.length)
							{
								ba=getSeekByte(seekPos);
								ba.position=0
								_seekTime=_times[i-1];
								if(_fileIsOK[getFileIndex(_seekTime-LeftTime)])
								{
									
									_ns.seek(_seekTime,ba);
								}
								else
								{
									changeBaPosition(seekPos);
									_seekPos=seekPos;
									
									_seekTime=_times[i-1];
									//								_seekTime=seekTime;
									stop();
									replaceNs();
									playSeek(_seekTime);
//									_ns.resume();
								}
//								for(var x:int=0;x<5;x++)
//								{
//									trace(ba.readByte());
//								}
								
							}
							else
							{
//								_ba.position=seekPos;
								changeBaPosition(seekPos);
								_seekPos=seekPos;
								
								_seekTime=_times[i-1];
//								_seekTime=seekTime;
								stop();
								replaceNs();
								playSeek(_seekTime);
//								_ns.resume();
//								_ns.getNetStream().play(null);
//								_ns.play(_head);
//								play(false,seekTime);
							}
							break;
						}
					}
				}
			}
			var e:SeekCompleteEvent=new SeekCompleteEvent(OnSeek);
			e.Time=_seekTime;
			e.FileIndex=getFileIndex(_seekTime);
			dispatchEvent(e);
		}
		private function changeBaPosition(seekPos:Number):void
		{
			for(var i:int=0;i<_positions.length;i++)
			{
				if(seekPos<_positions[i])
				{
					_ba.position=_positions[i-1];
					break;
				}
			}
		}
		private function playSeek(seekTime:Number):void
		{
			
		
			var fileIndex:int=getFileIndex(seekTime-LeftTime);
			_fileIndex=fileIndex;
			_urlLoader=new UrlLoader(_urlForebody+"/"+_files[fileIndex]+_extension);
			_urlLoader.addEventListener(_urlLoader.OnComplete,urlLoaderCompleteHandleSeek);
			_urlLoader.addEventListener(_urlLoader.OnProgress,urlLoaderProgressHandler);
			var e:WaitEvent=new WaitEvent(_ns.OnWait);
			e.State=false;
			//			e.TotalData=event.TotalData
			dispatchEvent(e);
		}
		private function urlLoaderCompleteHandleSeek(event:CompleteEvent):void
		{
			
//			_ns.play(event.Data);
//			_urlStreamFlg=true;
			_ba.writeBytes(event.Data);
			_fileIsOK[_fileIndex]=true;
			var e:LoadingEvent=new LoadingEvent(OnProgress);
			e.LoaderData=_ba.length;
			//			e.TotalData=event.TotalData
			dispatchEvent(e);
//			_ba.position=_seekPos;
//			var byte
			_ns.seek(_seekTime,getSeekByte(_seekPos));
		}
		//url进度控制，暂时没用到
		private function urlLoaderProgressHandler(event:LoadingEvent):void
		{
			
//			var e:LoadingEvent=new LoadingEvent(OnProgress);
//			e.LoaderData=_ba.length+event.LoaderData;
////			e.TotalData=event.TotalData
//			dispatchEvent(e);
//			if(_ba.length>=event.TotalData)
//			{
//				_urlStreamFlg=true;
//			}
		}
		//获取文件编号
		private function getFileIndex(time:Number):int
		{
			var fileIndex:int=0;
			for(var i:int=0;i<_positionTimes.length;i++)
			{
				if(time<=_positionTimes[i]-LeftTime&&_urlStreamFlg)
				{
					fileIndex=i;
					break;
				}
			}
			return fileIndex;
		}
		//暂停
		public function pause():void
		{
			_ns.pause();
		}
		//继续
		public function resume():void
		{
			_ns.resume();
		}
		//下载下一步
		public function next(tiem:Number):void
		{
			
		}
		//获取当前播放时间
		public function getCurrentTime():uint
		{
			return _ns.getCurrentTime();
		}
		public function stop():void
		{
			_ns.stop();
		}
		//重新初始化播放器
		public function replaceNs():void
		{
//			var baTemp:ByteArray=new ByteArray();
//			baTemp.writeBytes(_baHead);
//			_ba.position=_filepositins[0];
//			_ba.readBytes(baTemp,_filepositins[0]);
			this._params.UiComponent.removeChild(_video);
			_ns=new Netstream(_baHead);
//			_ns.pause();
			_ns.addEventListener(_ns.OnFinish,finishHandle);
			_ns.addEventListener(_ns.OnWait,waitHandle);
			_video=new Video(_width,_height);
			this._params.UiComponent.addChild(_video);
			_video.attachNetStream(_ns.getNetStream());
//			_ns.addEventListener(_ns.OnMetaData,headMetaDataHandle);
		}
		//
	}
}