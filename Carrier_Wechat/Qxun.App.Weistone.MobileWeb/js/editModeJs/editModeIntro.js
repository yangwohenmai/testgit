//首先清除掉所有幻灯片里面的连接
$(".editModeIntro a").attr("href", "javascript:void(0)");
//给所有的幻灯片注册编辑事件
$("body").on("click", ".editModeIntro .editModeIntro_link", function (event) {
    var bannerId = $(this).attr("banner_id");
    $(window.parent.document).find("#editModeIntro").show().siblings().hide();
    window.parent.editModeIntro(bannerId);
    event.stopPropagation();
});