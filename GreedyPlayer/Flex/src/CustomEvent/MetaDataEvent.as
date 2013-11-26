package CustomEvent
{
	import flash.events.Event;
	
	import mx.collections.ArrayCollection;

	public class MetaDataEvent extends Event
	{
//		private var _totalTime:String;
//		private var _times:Array;
//		private var _filepositions:Array;
//		private var _segmenta:Array;
//		private var _length:uint;
//		private var _width:uint;
//		private var _height:uint;
		
//		public function OnMetaDataEvent()
//		{
//		}
		private var _info:Object;
		public function MetaDataEvent(type:String, bubbles:Boolean=false, cancelable:Boolean=false)
		{
			super(type, bubbles, cancelable);
		}
		
		public function get Info():Object
		{
			return this._info;
		}
		public function set Info(info:Object):void
		{
			this._info=info;
		}
//		public function get TotalTime():String
//		{
//			return this._totalTime;
//		}
//		public function set TotalTime(totalTime:String):void
//		{
//			this._totalTime=totalTime;
//		}
//		
//		public function get Times():Array
//		{
//			return this._times;
//		}
//		public function set Times(times:Array):void
//		{
//			this._times=times;
//		}
//		
//		public function get Filepositions():Array
//		{
//			return this._filepositions;
//		}
//		public function set Filepositions(filepositions:Array):void
//		{
//			this._filepositions=filepositions;
//		}
//		
//		public function get Segmenta():Array
//		{
//			return this._segmenta;
//		}
//		public function set Segmenta(segmenta:Array):void
//		{
//			this._segmenta=segmenta;
//		}
//		
//		public function get Length():uint
//		{
//			return this._length;
//		}
//		public function set Length(length:uint):void
//		{
//			this._length=length;
//		}
//		
//		public function get Width():uint
//		{
//			return this._width;
//		}
//		public function set Width(_width:uint):void
//		{
//			this._width=_width;
//		}
//		public function get Height():uint
//		{
//			return this._height;
//		}
//		public function set Height(height:uint):void
//		{
//			this._height=height;
//		}
//		
	}
}