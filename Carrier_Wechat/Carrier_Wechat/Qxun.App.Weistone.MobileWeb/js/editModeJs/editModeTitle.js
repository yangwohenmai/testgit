//首先清除掉所有幻灯片里面的连接
$(".editModeTitle a").attr("href", "javascript:void(0)");
//给所有的幻灯片注册编辑事件
$("body").on("click", ".editModeTitle .editModeTitle_link", function (event) {    
    $(window.parent.document).find("#editModeTitle").show().siblings().hide();
    window.parent.editModeTitle();
    event.stopPropagation();
});