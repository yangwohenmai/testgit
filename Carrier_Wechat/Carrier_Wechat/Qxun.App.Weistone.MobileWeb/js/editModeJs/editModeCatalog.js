//首先清除掉所有分类里面的连接
$(".editModeCatalog a").attr("href", "javascript:void(0)");
//给所有的分类注册编辑事件
$("body").on("click", ".editModeCatalog .editModeCatalog_link", function (event) {
    var catalogId = $(this).attr("catalog_id");
    $(window.parent.document).find("#editModeCategory").show().siblings().hide();
    if (catalogId == "diUyZkRDZXFiRUxaYUZSb2Q1U1hDWDRFNDhQd3FobzY3YQ..") {//判断是否为新增(id=-1)
        $(window.parent.document).find("[name='img0']").attr("src", "");
        $(window.parent.document).find("#MicWebsiteCatalogId").val("");
        $(window.parent.document).find(".photo0").attr("src", "");
        $(window.parent.document).find("#CoverResource").val("");
        $(window.parent.document).find(':input', '#editModeCategoryFormId')
            .not(':button, :submit, :reset, :hidden, :radio')
            .val('')
         .removeAttr('checked')
         .removeAttr('selected');
        $(window.parent.document).find(".tab_hasno").click();
        catalogId = "";
    }
    window.parent.editModeCategory(catalogId);
    event.stopPropagation();
});