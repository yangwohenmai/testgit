/**
标签的树型结构
**/
$(function () {
    //加载标签的树型结构
    $('#contact-tag-list')
        .jstree({
            "core": {
                "animation": 0,
                "check_callback": true,
                'data': { 'url': '../Contacts/GetTagAllList' },
                "themes": {
//                    "name":"tag",
//                    "url":"../Scripts/jstree/dist/themes/tag/style.css",
                    "dots": false,
                    "icons": true
                },
            },
            "types" : {
              "default" : {
                "icon" : "icon_tag icon_tag_blue"
              },
              "demo" : {
                "icon" : "glyphicon glyphicon-ok"
              }
            },
            "plugins": [
                "contextmenu", "themes","search","types","wholerow"
            ],
            "contextmenu": {
                "items": {
                    "rename": {
                        "label": "重命名",
                        "action": function(data) {
                            //清除错误信息
                            $("#tagName").parents(".form_item").removeClass("form_error");
                            //获取值
                            var ref = $('#contact-tag-list').jstree(true),
                                id = ref.get_selected(),
                                text = ref.get_text(id),
                                isLock = ref.get_node(id).li_attr['data_IsLock'],
                                accountId = ref.get_node(id).li_attr['data_AccountId'];
                                qyTag = ref.get_node(id).li_attr['qyTag'];
                            //填充值，用于保存
                            $("#tagId").val(id);
                            $("#tagName").val(text);
                            $("#isLock").val(isLock);
                            $("#tagAccountId").val(accountId);
                            $("#qyTag").val(qyTag);
                            //修改弹出层名称并显示
                            $("#__dialog__TAGDialog__ .modal-title").text("重命名");
                            $("#__dialog__TAGDialog__").modal();
                        }
                    },
                    "删除": {
                        "label": "删除",
                        "action": function(data) {
                            if (confirm("确认删除标签？\n只是删除此标签，不会影响标签包含的成员")) {
                                var ref = $('#contact-tag-list').jstree(true),
                                    id = ref.get_selected();
                                $.post("../Contacts/DeleteTagEntity?id=" + id, function(data) {
                                    if (data.IsSuccess) {
                                        if (!id.length) {
                                            return false;
                                        }
                                        ref.delete_node(id);
                                        $("#contact-tag-list li").each(function () {
                                            if ($(this).attr("data_islock") == "true") {
                                                $(this).addClass("jstree-lock");
                                            }
                                        });
                                    } else {
                                        alert(data.ErrorMsg);
                                    }
                                });
                            }
                        }
                    },
                    "解锁": {
                        "label": function(data) {
                            var ref = $('#contact-tag-list').jstree(true),
                                id = ref.get_selected(),
                                isLock = ref.get_node(id).li_attr['data_IsLock'];
                            if (isLock) {
                                return "解锁";
                            } else {
                                return "加锁";
                            }
                        },
                        "action": function(data) {
                            var ref = $('#contact-tag-list').jstree(true),
                                id = ref.get_selected(),
                                isLock = ref.get_node(id).li_attr['data_IsLock'] == true ? false : true; //加锁解锁取相反的值

                            $.post("../Contacts/TagIsLock?id=" + id, { IsLock: isLock }, function(json) {
                                if (json.IsSuccess) {
                                    if (isLock) {
                                        ref.get_node(id).li_attr['data_IsLock'] = true;
                                        $("#contact-tag-list li[id='" + id + "']").addClass("jstree-lock");
                                    } else {
                                        ref.get_node(id).li_attr['data_IsLock'] = false;
                                        $("#contact-tag-list li[id='" + id + "']").removeClass("jstree-lock");
                                    }
                                }
                                else {
                                    alert(json.ErrorMsg);
                                }
                            });
                        }
                    },
                    "ID": {
                        "label": function(data) {
                            var ref = $('#contact-tag-list').jstree(true),
                                id = ref.get_selected();
                            return "ID：" + id;
                        },
                        "_disabled": true
                    }
                }
            }
        })
        //加载完毕后，需要选中和展开
        .bind("loaded.jstree", function(e, data) {
            //选中第一个
            data.instance.select_node($("#contact-tag-list li:first").attr("id"));
            //如果是锁定的 就加一个锁的小图标
            $("#contact-tag-list li").each(function() {
                if ($(this).attr("data_islock")=="true") {
                    $(this).addClass("jstree-lock");   
                }
            });
        })
        .bind("create_node.jstree", function(e, data) {
            $("#contact-tag-list li").each(function() {
                if ($(this).attr("data_islock")=="true") {
                    $(this).addClass("jstree-lock");   
                }
            });
        })
        //标签的点击事件
        .bind('click.jstree', function(e, data) {
            //关闭详情 弹出层
            $(".opp_result").animate({ "right": "-370px" }, 500).hide();
            //退出批量操作的模式
            ContactsHandleOrDetail = "";
            //获取当前点击的Id
            var currentClickTagNodeId = $(event.srcElement).parents('li').attr('qytag');
            var eventNodeName = event.srcElement.nodeName;
            if (eventNodeName == 'INS'|| $(event.srcElement).hasClass("jstree-ocl")) {
                return;
            } else {
                getContactsList('{"Name":"","UserId":"","DepartId":"","TagId":"\'' + currentClickTagNodeId + '\'","Status":0 }', 1, 10);
                //改变标题的值
                var ref = $("#contact-tag-list").jstree(true),
                    selectedId = ref.get_selected(),
                text = ref.get_node(selectedId).text;
                $(".bar_right_text-title.js_menu_title").text(text);
            }
        });
    //添加标签
    $("body").on("click", ".js_tag_create", function() {
        //填充初始值并显示层，用于保存
        $("#tagId").val();
        $("#isLock").val("true");
        $("#tagName").val("");
        $("#__dialog__TAGDialog__").modal();
    });
    //标签的添加，编辑保存
    $("#btnTagAddSave").click(function() {
        //获取需要的值
        var tagId=$("#tagId").val();
        var tagName = $("#tagName").val();
        var isLock = $("#isLock").val();
        var accountAccountId = $("#tagAccountId").val();
        var qyTag = $("#qyTag").val();
        var json = {
            "TagId":tagId,
            "TagName":tagName,
            "IsLock":isLock,
            "AccountId": accountAccountId,
            "qyTag": qyTag == "" ? 0 : qyTag
        };
        //判断标签名称不为空且不能超过32个字
        if (tagName.replace(/[^\x00-\xFF]/g, '**').length <= 64 && tagName.replace(/(^\s*)|(\s*$)/g, '') != "") {
            //清除错误信息
            $("#tagName").parents(".form_item").removeClass("form_error");
            //调用后台方法
            $.post("../Contacts/InsertOrUpdateTagEntity", { json:JSON.stringify(json),id:tagId}, function (data) {
                if (data.IsSuccess) {
                    var ref = $('#contact-tag-list').jstree(true);
                    if (tagId != "") {
                        var  sel = ref.get_selected();
                        if (!sel.length) { return false; }
                        sel = sel[0];
                        //修改
                        ref.set_text(sel, data.ExtInfo.TagName);
                    } else {
                        //新增
                        ref.create_node("#", { id: data.ExtInfo.QYTag, text: data.ExtInfo.TagName, li_attr: { data_IsLock: data.ExtInfo.IsLock, data_AccountId: data.ExtInfo.AccountId, qyTag: data.ExtInfo.QYTag } });
                    }
                    $("#__dialog__TAGDialog__").modal("hide");
                } else {
                    alert(data.ErrorMsg);
                }
            });
        } else {
            $("#tagName").parents(".form_item").addClass("form_error");
        }
    });
});