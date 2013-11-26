package CustomEvent
{
	import flash.events.Event;

	public class WaitEvent extends Event
	{
		private var _state:Boolean;
		public function WaitEvent(type:String, bubbles:Boolean=false, cancelable:Boolean=false)
		{
			super(type, bubbles, cancelable);
		}
		public function get State():Boolean
		{
			return this._state;
		}
		public function set State(state:Boolean):void
		{
			this._state=state;
		}
	}
}