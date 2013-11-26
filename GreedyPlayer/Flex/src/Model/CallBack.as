package Model
{
	public class CallBack
	{
		public function CallBack()
		{
		}
		private var _functionName:String;
		private var _closure:Function;
		public function get FunctionName():String
		{
			return this._functionName;
		}
		public function set FunctionName(functionName:String):void
		{
			this._functionName=functionName;
		}
		public function get Closure():Function
		{
			return this._closure;
		}
		public function set Closure(closure:Function):void
		{
			this._closure=closure;
		}
	}
}