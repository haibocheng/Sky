package Core
{
	import mx.core.UIComponent;

	public class PlayerOptions
	{
		private var _uiComponent:UIComponent;
		private var _url:String;
		private var _width:uint;
		private var _height:uint;
		private var _autoplay:Boolean=false;
		private var _isShowSlider:Boolean=false;
		public function PlayerOptions()
		{
		}
		public function get UiComponent():UIComponent
		{
			return this._uiComponent;
		}
		public function set UiComponent(uiComponent:UIComponent):void
		{
			this._uiComponent=uiComponent;
		}
		public function get Url():String
		{
			return this._url;
		}
		public function set Url(url:String):void
		{
			this._url=url;
		}
		public function get Width():uint
		{
			return this._width;
		}
		public function set Width(width:uint):void
		{
			this._width=width;
		}
		public function get Height():uint
		{
			return this._height;
		}
		public function set Height(height:uint):void
		{
			this._height=height;
		}
		public function get Autoplay():Boolean
		{
			return this._autoplay;
		}
		public function set Autoplay(autoplay:Boolean):void
		{
			this._autoplay=autoplay;
		}
		public function get IsShowSlider():Boolean
		{
			return this._isShowSlider;
		}
		public function set IsShowSlider(isShowSlider:Boolean):void
		{
			this._isShowSlider=isShowSlider;
		}
		
	}
}