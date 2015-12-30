var dataForWeixin = {
    appId: "xxxxxxxxx",
    MsgImg: "转发时的图片",
    TLImg: "图片",
    url: "自定义链接",
    title: "自定义标题",
    desc: "自定义描述",
    fakeid: "",
    friendcallback: function () {
        //alert("分享成功！");  
    },
    friendCirclecallback: function () {
        //alert("分享成功！");
    }
};
(function () {
    var onBridgeReady = function () {
       
        //发送给朋友
        WeixinJSBridge.on('menu:share:appmessage', function (argv) {
        
            WeixinJSBridge.invoke('sendAppMessage', {
                //"appid": dataForWeixin.appId,
                "img_url": dataForWeixin.MsgImg,
                "img_width": "120",
                "img_height": "120",
                "link": dataForWeixin.url,
                "desc": dataForWeixin.desc,
                "title": dataForWeixin.title
            }, function (res) {
                //alert(res.err_msg);
                if (res.err_msg =="send_app_msg:ok"||res.err_msg =="send_app_msg:confirm") {
                    dataForWeixin.friendcallback(); 
                }
            });
        });

        function IsAndroid() 
        { 
            var userAgentInfo = navigator.userAgent;
            if (userAgentInfo.indexOf("Android") > 0) { return true; } 
            return false; 
        }

        //发送到朋友圈
        WeixinJSBridge.on('menu:share:timeline', function(argv) {
           //
            if (IsAndroid()) {
                dataForWeixin.friendCirclecallback();
                WeixinJSBridge.invoke('shareTimeline', {
                    "img_url": dataForWeixin.TLImg,
                    "img_width": "120",
                    "img_height": "120",
                    "link": dataForWeixin.url,
                    "desc": dataForWeixin.desc,
                    "title": dataForWeixin.title
                }, function(res) {
                    if(res.err_msg == 'share_timeline:ok'||res.err_msg == 'share_timeline:confirm'){
                       //  dataForWeixin.friendCirclecallback();
                        
                    }
                });
            } else {
                 WeixinJSBridge.invoke('shareTimeline', {
                    "img_url": dataForWeixin.TLImg,
                    "img_width": "120",
                    "img_height": "120",
                    "link": dataForWeixin.url,
                    "desc": dataForWeixin.desc,
                    "title": dataForWeixin.title
                }, function(res) {
                    if(res.err_msg == 'share_timeline:ok'||res.err_msg == 'share_timeline:confirm'){
                        dataForWeixin.friendCirclecallback();
                    }
                });
            }
            
        });
        //分享到微博
        WeixinJSBridge.on('menu:share:weibo', function (argv) {
            WeixinJSBridge.invoke('shareWeibo', {
                "content": dataForWeixin.title,
                "url": dataForWeixin.url
            }, function (res) {
                if (res.err_msg=="share_weibo:ok") {
                    dataForWeixin.callback();
                }
            });
        });
        //分享到facebook
        WeixinJSBridge.on('menu:share:facebook', function (argv) {
            (dataForWeixin.callback)();
            WeixinJSBridge.invoke('shareFB', {
                "img_url": dataForWeixin.TLImg,
                "img_width": "120",
                "img_height": "120",
                "link": dataForWeixin.url,
                "desc": dataForWeixin.desc,
                "title": dataForWeixin.title
            }, function (res) { });
        });
    };
    if (document.addEventListener) {
        document.addEventListener('WeixinJSBridgeReady', onBridgeReady, false);
    } else if (document.attachEvent) {
        document.attachEvent('WeixinJSBridgeReady', onBridgeReady);
        document.attachEvent('onWeixinJSBridgeReady', onBridgeReady);
    }
})();

