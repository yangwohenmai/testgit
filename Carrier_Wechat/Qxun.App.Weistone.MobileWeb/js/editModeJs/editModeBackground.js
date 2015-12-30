//给所有的背景注册编辑事件
$("body").on("click", ".editModeBackground", function (event) {    
    $(window.parent.document).find("#editModeBackground").show().siblings().hide();
    window.parent.editModeBackground();
    event.stopPropagation();
});