//首先清除掉所有幻灯片里面的连接
$(".editModePhone a").attr("href", "javascript:void(0)");
//给所有的幻灯片注册编辑事件
$("body").on("click", ".editModePhone .editModePhone_link", function (event) {
    $(window.parent.document).find("#editModePhone").show().siblings().hide();
    window.parent.editModePhone();    
    event.stopPropagation();
});