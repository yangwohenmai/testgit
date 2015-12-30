$(function () {
    GetRepositoryCatatoryEntities();
    //getRepositoryArticleList();
    //树型结构
    $('.knowledge-jstree').jstree({
        'core': {
            'data': knowledgeJstreeJson
        },
        "plugins": [
            "contextmenu", "json_data", "types", "wholerow","crrm"
        ],
        "contextmenu": {
            "items": {
                "create": {
                    "label": "添加子目录",
                    "action": function (data) {
                        //清除错误的提示
                        $("#RepositoryCatalog_Title").parents(".form_item").removeClass("form_error");
                        //获取值
                        var ref = $('.knowledge-jstree').jstree(true),
                            ParentId = ref.get_selected(),
                            WeixinPlatId = jQuery.query.get("plat");
                        //填充值，用于保存
                        $("#RepositoryCatalog_Id").val("");
                        $("#RepositoryCatalog_ParentId").val(ParentId);
                        $("#RepositoryCatalog_WeixinPlatId").val(WeixinPlatId);
                        $("#RepositoryCatalog_Title").val("");
                        //显示弹出层
                        $("#konwledgeBaseCatalogEditModal").modal();
                    },
                    "_disabled": function (data) {
                        var ref = $('.knowledge-jstree').jstree(true),
                            currenNode = ref.get_node(ref.get_selected()),
                            allDepartLength = currenNode.parents.length;
                        if (allDepartLength >= 10) {
                            return true;
                        } else {
                            return false;
                        }
                    }
                },
                "addArticle": {
                    "label": "新增条目",
                    "action": function (data) {
                        var ref = $('.knowledge-jstree').jstree(true),
                            cId = ref.get_selected(),
                            Title = ref.get_text(cId),
                            WeixinPlatId = jQuery.query.get("plat");
                        var json = {
                            "Id": "",
                            "Title": "",
                            "UploadTime": "",
                            "Contents": "",
                            "Quantity": 0,
                            "UploadName": "",
                            "CatalogId": cId[0],
                            "WeixinPlatId": WeixinPlatId,
                            "CoverImg": "",
                            "Display": 1,
                            "Abstract":""
                        }
                        setRepositoryArticleMoadalVal(json);
                        $("#RepositoryArticle_CatalogName").val(Title);
                        $(".photo1").attr("src", "");
                        $("#konwledgeBaseArticleEditModal .modal-title").html("新增条目");
                        //显示弹出层
                        $("#konwledgeBaseArticleEditModal").modal();
                    }
                },
                "rename": {
                    "label": "重命名",
                    "action": function (data) {
                        //清楚错误的提示
                        $("#RepositoryCatalog_Title").parents(".form_item").removeClass("form_error");
                        //获取值
                        var ref = $('.knowledge-jstree').jstree(true),
                            Id = ref.get_selected(),
                            Title = ref.get_text(Id),
                            ParentId = ref.get_parent(Id),
                            WeixinPlatId = jQuery.query.get("plat");
                        //填充值，用于保存
                        $("#RepositoryCatalog_Id").val(Id);
                        $("#RepositoryCatalog_WeixinPlatId").val(WeixinPlatId);
                        $("#RepositoryCatalog_Title").val(Title);
                        if (ParentId == "#") {
                            $("#RepositoryCatalog_ParentId").val("diUyZkRDZXFiRUxaYUZSb2Q1U1hDWDROM3ZhUCUyZk5pNm5Z");
                        } else {
                            $("#RepositoryCatalog_ParentId").val(ParentId);
                        }
                        //改变标题并显示弹出层
                        $("#konwledgeBaseCatalogEditModal .modal-title").text("重命名");
                        $("#konwledgeBaseCatalogEditModal").modal();

                    }
                },
                "delete": {
                    "label": "删除",
                    "action": function (data) {
                        if (confirm("确认删除此目录？")) {
                            var ref = $('.knowledge-jstree').jstree(true),
                                id = ref.get_selected();
                            $.post("../KnowledgeBase/DeleteRepositoryCatalogEntityById?id=" + id, function (data) {
                                if (data.IsSuccess) {
                                    if (!id.length) {
                                        return false;
                                    }
                                    ref.delete_node(id);
                                    window.location.reload();
                                } else {
                                    alert(data.ErrorMsg);
                                }
                            });
                        }
                    },
                    "_disabled": function (data) {
                        var ref = $('.knowledge-jstree').jstree(true);
                        var childrenLength = ref.get_children_dom(ref.get_selected()).length;
                        if (childrenLength > 0) {
                            return true;
                        } else {
                            return false;
                        }
                    }
                }
            }
        }
    })
    //加载完毕后，需要选中和展开
    .bind("loaded.jstree", function (e, data) {
        //获取根的Id
        var rootCatalogNodeId = $(".knowledge-jstree li:first").attr("id");
        //打开
        data.instance.open_node(rootCatalogNodeId);
        //选中
        data.instance.select_node(rootCatalogNodeId);
        //初始化数据
        var searcJson = $(".searchValue").val();
        getRepositoryArticleList(rootCatalogNodeId, searcJson);
    })
    .bind('click.jstree', function (e, data) {
        //获取点击的值
        var Id = $(event.target).parents('li').attr('id');
        var eventNodeName = event.target.nodeName;
        //判断是不是点击的展开三角符号和文件夹的图标
        if (eventNodeName == 'INS' || $(event.target).hasClass("jstree-ocl")) {
            return;
        } else {
            var searcJson = $(".searchValue").val();
            getRepositoryArticleList(Id, searcJson);
        }
    });
    //根据条件获取条目列表
    function getRepositoryArticleList(cId,searchJson) {
        $.post("../KnowledgeBase/GetRepositoryArticleEntities", { pageSize: 10, page: 1, searchJson: searchJson, catalogId: cId }, function (data) {
            $("#tableBody").empty();
            if (data.ContentEntity.Body.length > 0) {
                $("#knowledgeBaseArticleTmpl").tmpl(data.ContentEntity.Body).appendTo("#tableBody");
                jQuery.get("../Content/tmpl/foot1.htm", function (repText) {
                    jQuery("#tableFoot").empty();
                    jQuery.template("tmplData", repText);
                    jQuery.tmpl("tmplData", data.ContentEntity.Foot).appendTo("#tableFoot");
                });
            } else {
                $("#tableBody").append("<tr><td colspan='6'>暂无数据！</td></tr>")
            }
        })
    };
    //获取目录列表
    function GetRepositoryCatatoryEntities() {
        $.ajax({
            type: 'POST',
            url: "../KnowledgeBase/GetRepositoryCatatoryEntities",
            data: "",
            async: false,
            success: function (data) {
                if (data.ExtInfo!=null && data.ExtInfo.length > 0) {
                    for (var i = 0; i < data.ExtInfo.length; i++) {
                        var jstreeJson = {
                            id: "",
                            parent: "",
                            text: ""
                        };
                        jstreeJson.id = data.ExtInfo[i].Id;
                        jstreeJson.parent = data.ExtInfo[i].ParentId == "diUyZkRDZXFiRUxaYUZSb2Q1U1hDWDROM3ZhUCUyZk5pNm5Z" ? "#" : data.ExtInfo[i].ParentId;
                        jstreeJson.text = data.ExtInfo[i].Title;
                        knowledgeJstreeJson.push(jstreeJson)
                    }
                }
            }
        });
        //$.post("../KnowledgeBase/GetRepositoryCatatoryEntities", {}, function (data) {

        //})
    };
    //设置添加条目的值
    function setRepositoryArticleMoadalVal(json) {
        $("#RepositoryArticle_Id").val(json.Id);
        $("#RepositoryArticle_Title").val(json.Title);
        $("#RepositoryArticle_CatalogId").val(json.CatalogId);
        $("#RepositoryArticle_WeixinPlatId").val(json.WeixinPlatId);
        $("#RepositoryArticle_Abstract").val(json.Abstract);
        $("#RepositoryArticle_CoverImg").val(json.CoverImg);
        $("#RepositoryArticle_Quantity").val(json.Quantity);
        //$($("input[name=RepositoryArticle_Display]")[0]).attr("checked", "checked");
        //$($("input[name=RepositoryArticle_Display]")[1]).removeAttr("checked", "checked");
        UE.getEditor('RepositoryArticle_Contents').setContent(json.Contents);
    }
    //添加子目录的提交
    $("#btnRepositoryCatalogAddSave").click(function () {
        $("#btnRepositoryCatalogAddSave").attr({ "disabled": "disabled" });
        var json = {
            "Id": $("#RepositoryCatalog_Id").val(),
            "Title": $("#RepositoryCatalog_Title").val(),
            "ParentId": $("#RepositoryCatalog_ParentId").val(),
            "WeixinPlatId": $("#RepositoryCatalog_WeixinPlatId").val()
        }
        //判断部门名称不为空且不能超过50个字
        if (json.Title.replace(/[^\x00-\xFF]/g, '**').length <= 100 && json.Title.replace(/(^\s*)|(\s*$)/g, '') != "") {
            //清除错误的提示
            $("#RepositoryCatalog_Title").parents(".form_item").removeClass("form_error");
            //调用后台方法
            $.post("../KnowledgeBase/InsertOrUpdateRepositoryCatalogEntity", { json:JSON.stringify(json)}, function (data) {
                if (data.IsSuccess) {
                    if (json.ParentId = "diUyZkRDZXFiRUxaYUZSb2Q1U1hDWDROM3ZhUCUyZk5pNm5Z") {
                        window.location.reload();
                    }
                    /*这里是不刷新页面加载数据，暂时后台没有返回*/
                    var ref = $('.knowledge-jstree').jstree(true),
                        sel = ref.get_selected();
                    if (!sel.length) { return false; }
                    sel = sel[0];
                    if (data.ExtInfo == true) {
                        //修改
                        ref.set_text(sel, json.Title);
                    } else {
                        //新增
                        //ref.create_node(sel, { "id": data.ExtInfo, "parent": json.ParentId, "text": json.Title });
                        //GetRepositoryCatatoryEntities();
                        //ref.refresh()
                        window.location.reload();
                    }
                    $("#konwledgeBaseCatalogEditModal").modal("hide");
                    $("#btnRepositoryCatalogAddSave").removeAttr("disabled");
                } else {
                    alert(data.ErrorMsg);
                    $("#btnRepositoryCatalogAddSave").removeAttr("disabled");
                }
            });
        } else {
            $("#RepositoryCatalog_Title").parents(".form_item").addClass("form_error");
            $("#btnRepositoryCatalogAddSave").removeAttr("disabled");
        }
    });
    //添加条目的提交
    $("#btnRepositoryArticleAddSave").click(function () {
        $("#btnRepositoryArticleAddSave").attr({ "disabled": "disabled" });
        var json = {
            "Id": $("#RepositoryArticle_Id").val(),
            "Title": $("#RepositoryArticle_Title").val(),
            "UploadTime": "2015-11-4",
            "Contents": UE.getEditor('RepositoryArticle_Contents').getContent(),
            "Quantity": $("#RepositoryArticle_Quantity").val(),
            "UploadName": "",
            "CatalogId": $("#RepositoryArticle_CatalogId").val(),
            "WeixinPlatId": $("#RepositoryArticle_WeixinPlatId").val(),
            "CoverImg": $("#RepositoryArticle_CoverImg").val(),
            "Display": parseInt($("input[name=RepositoryArticle_Display]:checked").attr("data-state")),
            "Abstract": $("#RepositoryArticle_Abstract").val()
        }
        $.post("../KnowledgeBase/InsertOrUpdateRepositoryAricleEntity", { json: JSON.stringify(json) }, function (data) {
            if (data.IsSuccess) {
                $("#konwledgeBaseArticleEditModal").modal("hide");
                $("#btnRepositoryArticleAddSave").removeAttr("disabled");

                //重新获取数据
                $(".searchValue").val("");
                if (json.Id != "") {
                    var ref = $('.knowledge-jstree').jstree(true),
                        sel = ref.get_selected();
                    getRepositoryArticleList(sel, "");
                } else {
                    getRepositoryArticleList(json.CatalogId, "");
                }
            } else {
                alert(data.ErrorMsg);
                $("#btnRepositoryArticleAddSave").removeAttr("disabled");
            }
        })
    });
    //删除条目
    $("body").on("click", ".btn-del-open", function () {
        var articleId = $(this).attr("item-id");
        if (confirm("确定删除此条目吗？删除后不可恢复！")) {
            $.post("../KnowledgeBase/DeleteRepositoryArticleEntityById", { id: articleId }, function (data) {
                if (data.IsSuccess) {
                    $("tr[id='" + articleId + "']").remove();
                } else {
                    alert(data.ErrorMsg);
                }
            });
        }
    });
    //修改条目
    $("body").on("click", ".btn-detail-open", function () {
        var aId = $(this).attr("item-id");
        $("#konwledgeBaseArticleEditModal .modal-title").html("编辑条目");
        $.post("../KnowledgeBase/GetRepositoryEntityById", { id: aId }, function (data) {
            if (data.IsSuccess) {
                setRepositoryArticleMoadalVal(data.ExtInfo);
                var ref = $('.knowledge-jstree').jstree(true),
                                cId = ref.get_selected(),
                                Title = ref.get_text(data.ExtInfo.CatalogId);
                $("#RepositoryArticle_CatalogName").val(Title);
                $(".photo1").attr("src", data.ExtInfo.CoverImgUrl);
            }
        })
        $("#konwledgeBaseArticleEditModal").modal();
    });
    //查询
    $(".searchBtn").click(function () {
        var ref = $('.knowledge-jstree').jstree(true),
            cId = ref.get_selected();
        var searcJson = $.trim($(".searchValue").val());
        getRepositoryArticleList(cId, searcJson);
    });
    //前一页、后一页,为了防止多次绑定click事件，这里每次都先解绑再绑定
    jQuery("body").undelegate('#tableFoot a:not(a.btn)', 'click').delegate("#tableFoot a:not(a.btn)", "click", function () {
        var page = jQuery(this).attr("page");
        var searchJson = $(".searchValue").val();
        var ref = $('.knowledge-jstree').jstree(true),
                           cId = ref.get_selected();
        $.post("../KnowledgeBase/GetRepositoryArticleEntities", { pageSize: 10, page: page, searchJson: searchJson, catalogId: cId }, function (data) {
            $("#tableBody").empty();
            if (data.ContentEntity.Body.length > 0) {
                $("#knowledgeBaseArticleTmpl").tmpl(data.ContentEntity.Body).appendTo("#tableBody");
                jQuery.get("../Content/tmpl/foot1.htm", function (repText) {
                    jQuery("#tableFoot").empty();
                    jQuery.template("tmplData", repText);
                    jQuery.tmpl("tmplData", data.ContentEntity.Foot).appendTo("#tableFoot");
                });
            } else {
                $("#tableBody").append("<tr><td colspan='6'>暂无数据！</td></tr>")
            }
        })
    });
    //点击添加根目录
    $(".addRootCatalog").click(function () {
        //清除错误的提示
        $("#RepositoryCatalog_Title").parents(".form_item").removeClass("form_error");
        //获取值
        WeixinPlatId = jQuery.query.get("plat");
        //填充值，用于保存
        $("#RepositoryCatalog_Id").val("");
        $("#RepositoryCatalog_ParentId").val("diUyZkRDZXFiRUxaYUZSb2Q1U1hDWDROM3ZhUCUyZk5pNm5Z");
        $("#RepositoryCatalog_WeixinPlatId").val(WeixinPlatId);
        $("#RepositoryCatalog_Title").val("");
        //显示弹出层
        $("#konwledgeBaseCatalogEditModal .modal-title").text("新增根目录");
        $("#konwledgeBaseCatalogEditModal").modal();
    })
    //$(".knowledgeBody_right").resizable({
    //    handles: 'all',
    //    grid: 50,
    //    resize: function (event, ui) {
    //        $(".knowledgeBody_left").css("width", "280px");
    //    }
    //});
});
//转存数据
var knowledgeJstreeJson = []