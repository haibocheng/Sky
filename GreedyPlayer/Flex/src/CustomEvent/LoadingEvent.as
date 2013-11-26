package CustomEvent
{
	import flash.events.Event;
	import flash.utils.ByteArray;

	public class LoadingEvent extends Event
	{
		private var _data:ByteArray;
		private var _totalData:Number;
		private var _loaderData:Number;
		
		public function LoadingEvent(type:String, bubbles:Boolean=false, cancelable:Boolean=false)
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
		public function get TotalData():Number
		{
			return this._totalData;
		}
		public function set TotalData(totalData:Number):void
		{
			this._totalData=totalData;
		}
		public function get LoaderData():Number
		{
			return this._loaderData;
		}
		public function set LoaderData(loaderData:Number):void
		{
			 this._loaderData=loaderData;
		}
	}
}