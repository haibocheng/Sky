package CustomEvent
{
	import flash.events.Event;

	public class SeekCompleteEvent extends Event
	{
		private var _time:Number;
		private var _fileIndex:Number;
		public function SeekCompleteEvent(type:String, bubbles:Boolean=false, cancelable:Boolean=false)
		{
			super(type, bubbles, cancelable);
		}
		public function get Time():Number
		{
			return this._time;
		}
		public function set Time(time:Number):void
		{
			this._time=time;
		}
		public function get FileIndex():Number
		{
			return this._fileIndex;
		}
		public function set FileIndex(fileIndex:Number):void
		{
			this._fileIndex=fileIndex;
		}
	}
}