//首先清除掉所有幻灯片里面的连接
$(".editModeBanner a").attr("href", "javascript:void(0)");
//给所有的幻灯片注册编辑事件
$("body").on("click", ".editModeBanner .editModeBanner_link", function (event) {
    var bannerId = $(this).attr("banner_id");
    $(window.parent.document).find("#editModeBanner").show().siblings().hide();
    if (bannerId == "diUyZkRDZXFiRUxaYUZSb2Q1U1hDWDRFNDhQd3FobzY3YQ..") {//判断是否为新增(id=-1)
        $(window.parent.document).find("[name='img1']").attr("src", "");
        $(window.parent.document).find("#BannerId").val("");
        $(window.parent.document).find(".photo1").attr("src", "");
        $(window.parent.document).find("#ResourceId").val("");
        $(window.parent.document).find(':input', '#editModeBannerFormId')
         .not(':button, :submit, :reset, :hidden')
         .val('')
         .removeAttr('checked')
         .removeAttr('selected');
        $(window.parent.document).find(".tab_hasno").click();
        bannerId = "";
    }
    window.parent.editModeBanner(bannerId);    
    event.stopPropagation();
});