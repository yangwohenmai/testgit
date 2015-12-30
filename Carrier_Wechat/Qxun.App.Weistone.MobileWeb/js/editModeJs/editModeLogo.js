//给所有的幻灯片注册编辑事件
$("body").on("click", ".editModeLogo", function (event) {    
    $(window.parent.document).find("#editModeLogo").show().siblings().hide();
    window.parent.editModeLogo();
    event.stopPropagation();
});