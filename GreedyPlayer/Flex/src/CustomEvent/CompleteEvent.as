package CustomEvent
{
	import flash.events.Event;
	import flash.utils.ByteArray;

	public class CompleteEvent extends Event
	{
		private var _data:ByteArray;
		
		public function CompleteEvent(type:String, bubbles:Boolean=false, cancelable:Boolean=false)
		{
			super(type, bubbles, cancelable);
		}
		
		public function get Data():ByteArray
		{
			return this._data;
		}
		public function set Data(data:ByteArray):void
		{
			this._data=data;
		}
	}
}