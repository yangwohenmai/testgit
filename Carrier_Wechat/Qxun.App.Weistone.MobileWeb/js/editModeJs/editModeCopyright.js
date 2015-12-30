//给所有的幻灯片注册编辑事件
$("body").on("click", ".editModeCopyright", function (event) {
    var bannerId = $(this).attr("banner_id");
    $(window.parent.document).find("#editModeCopyright").show().siblings().hide();
    window.parent.editModeCopyright(bannerId);
    event.stopPropagation();
});