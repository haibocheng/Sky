var greedyPlayer = {
    _greedyPlayer: null,
    _id: null,
    _onLoadCallBack: null,
    _onFihishCallBack: null,
    _onReadyCallBack: null,
    _autoplay: false,
    _isShowSlider: false,
    setup: function (obj) {
        if ((typeof obj.autoplay) == "boolean") {
            this._autoplay = obj.autoplay;
        }
        if ((typeof obj.isShowSlider) == "boolean") {
            this._isShowSlider = obj.isShowSlider;
        }
        var swfVersionStr = "11.1.0";
        this._onLoadCallBack = obj.onLoad;
        this._onFihishCallBack = obj.onFinish;
        this._onReadyCallBack = obj.onReady;
        //设置成全局函数，flash只能调用全局函数
        window['onFinish'] = obj.onFinish;
        window['onReady'] = obj.onReady;
        // To use express install, set to playerProductInstall.swf, otherwise the empty string. 
        var xiSwfUrlStr = "playerProductInstall.swf";
        var flashvars = { url: obj.file, autoplay: this._autoplay, isShowSlider: this._isShowSlider, width: obj.width, height: obj.height };
        var params = {};
        params.quality = "high";
        params.bgcolor = "#ffffff";
        params.allowscriptaccess = "sameDomain";
        params.allowfullscreen = "true";
        var attributes = {};
        attributes.id = this._id;
        attributes.name = this._id;
        attributes.align = "middle";
        swfobject.embedSWF(
                "/Scripts/greedyPlayer/GreedyPlayer.swf", this._id,
                obj.width, obj.height,
                swfVersionStr, xiSwfUrlStr,
                flashvars, params, attributes);
    },
    //    _onLoad: function () {
    //        //        greedyPlayer._greedyPlayer = getGreedyPlayer(greedyPlayer._id);
    //        //        if (greedyPlayer._autoplay) {

    //        //            setTimeout(function () {
    //        //                greedyPlayer._greedyPlayer.play();
    //        //            }, 3000);
    //        //        }
    //        greedyPlayer._onLoadCallBack();
    //    },
    //    play: function () {
    //        this._greedyPlayer.play();
    //    },
    //    seek: function (time) {
    //        this._greedyPlayer.seek(time);
    //    },
    //    pause: function () {
    //        this._greedyPlayer.pause();
    //    },
    setId: function (id) {
        this._id = id;
    }
}
function getGreedyPlayer(id) {
    if (window.document[id]) {
        return window.document[id];
    } else if (navigator.appName.indexOf("Microsoft") == -1) {
        if (document.embeds && document.embeds[id])
            return document.embeds[id];
    } else {
        return document.getElementById(id);
    }
}
function GreedyPlayer(id) {
    if (!getGreedyPlayer(id)) {
        greedyPlayer.setId(id);
        return greedyPlayer;
    }
    else {
        return getGreedyPlayer(id);
    }

}