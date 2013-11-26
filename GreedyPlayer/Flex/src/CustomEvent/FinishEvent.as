package CustomEvent
{
	import flash.events.Event;

	public class FinishEvent extends Event
	{
		public function FinishEvent(type:String, bubbles:Boolean=false, cancelable:Boolean=false)
		{
			super(type, bubbles, cancelable);
		}
	}
}