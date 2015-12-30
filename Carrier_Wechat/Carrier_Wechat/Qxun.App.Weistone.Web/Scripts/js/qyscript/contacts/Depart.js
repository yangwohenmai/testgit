/**
树型组织架构
**/
$(function () {
    //树型组织结构
    $('#contact-org')
        .jstree({
            "core": {
                "animation": 0,
                "check_callback": true,
                'data': { 'url': '../Contacts/GetPartyList' }
            },
            "types": {
                "#": {
                    "max_children": 1,
                    "max_depth": 4,
                    "valid_children": ["root"]
                },
                "root": {
                    "icon": "/static/3.0.8/assets/images/tree_icon.png",
                    "valid_children": ["default"]
                },
                "default": {
                    "valid_children": ["default", "file"]
                },
                "file": {
                    "icon": "glyphicon glyphicon-file",
                    "valid_children": []
                }
            },
            "plugins": [
                "contextmenu", "json_data", "search", "wholerow"
            ],
            "contextmenu": {
                "items": {
                    "create": {
                        "label": "添加子部门",
                        "action": function (data) {
                            //清除错误的提示
                            $("#departmentName").parents(".form_item").removeClass("form_error");
                            //获取值
                            var ref = $('#contact-org').jstree(true),
                                id = ref.get_selected(),
                                order = ref.get_node(id).li_attr['data_Order'],
                                accountId = ref.get_node(id).li_attr['data_AccountId'];
                            qyDepartment = ref.get_node(id).li_attr['qyDepartment'];
                            //填充值，用于保存
                            $("#departmentId").val();
                            $("#departmentName").val("");
                            $("#departmentParentId").val(id);
                            $("#departmentOrderId").val(order);
                            $("#accountId").val(accountId);
                            $("#qyDepartment").val(qyDepartment);
                            //显示弹出层
                            $("#__dialog__MNDialog__").modal();
                        },
                        "_disabled": function (data) {
                            var ref = $('#contact-org').jstree(true),
                                currenNode = ref.get_node(ref.get_selected()),
                                allDepartLength = currenNode.parents.length;
                            if (allDepartLength >= 10) {
                                return true;
                            } else {
                                return false;
                            }
                        }
                    },
                    "rename": {
                        "label": "重命名",
                        "action": function (data) {
                            //清楚错误的提示
                            $("#departmentName").parents(".form_item").removeClass("form_error");
                            //获取值
                            var ref = $('#contact-org').jstree(true),
                                id = ref.get_selected(),
                                text = ref.get_text(id),
                                parentId = ref.get_parent(id),
                                order = ref.get_node(id).li_attr['data_Order'],
                                accountId = ref.get_node(id).li_attr['data_AccountId'];
                            qyDepartment = ref.get_node(id).li_attr['qyDepartment'];
                            //填充值，用于保存
                            $("#departmentId").val(id);
                            $("#departmentName").val(text);
                            $("#departmentOrderId").val(order);
                            $("#accountId").val(accountId);
                            $("#qyDepartment").val(qyDepartment);
                            if (parentId == "#") {
                                $("#departmentParentId").val(0);
                            } else {
                                $("#departmentParentId").val(parentId);
                            }
                            //改变标题并显示弹出层
                            $("#__dialog__MNDialog__ .modal-title").text("重命名");
                            $("#__dialog__MNDialog__").modal();

                        }
                    },
                    "删除": {
                        "label": "删除",
                        "action": function (data) {
                            if (confirm("确认删除部门？\n没有子部门且没有成员的部门才可以被删除。")) {
                                var ref = $('#contact-org').jstree(true),
                                    id = ref.get_selected();
                                $.post("../Contacts/DelDepartMent?id=" + id, function (data) {
                                    if (data.IsSuccess) {
                                        if (!id.length) {
                                            return false;
                                        }
                                        ref.delete_node(id);
                                    } else {
                                        alert(data.ErrorMsg);
                                    }
                                });
                            }
                        },
                        "_disabled": function () {
                            var ref = $('#contact-org').jstree(true);
                            var childrenLength = ref.get_children_dom(ref.get_selected()).length;
                            if (childrenLength > 0) {
                                return true;
                            } else {
                                return false;
                            }
                        }
                    },
                    "ID": {
                        "label": function (data) {
                            var ref = $('#contact-org').jstree(true),
                                id = ref.get_selected();
                            return "ID：" + id;
                        },
                        "_disabled": true
                    }
                }
            },
        })
        //加载完毕后，需要选中和展开
        .bind("loaded.jstree", function (e, data) {
            //获取根的Id
            var rootDepartNodeId = $("#contact-org li:first").attr("id");
            //打开
            data.instance.open_node(rootDepartNodeId);
            //选中
            data.instance.select_node(rootDepartNodeId);
            //初始化数据
            getContactsList('{"Name":"","UserId":"","DepartId":"\'' + rootDepartNodeId + '\'","TagId":"","Status":0 }', 1, 10, rootDepartNodeId);
            getStatesContactsCountByDepartyId(rootDepartNodeId);
        })
        .bind('click.jstree', function (e, data) {
            //关闭详情 弹出层
            $(".opp_result").animate({ "right": "-370px" }, 500).hide();
            //退出批量操作的模式
            ContactsHandleOrDetail = "";
            //获取点击的值
            var currentClickDepartNodeId = $(event.srcElement).parents('li').attr('id');
            var eventNodeName = event.srcElement.nodeName;
            //判断是不是点击的展开三角符号和文件夹的图标
            if (eventNodeName == 'INS' || $(event.srcElement).hasClass("jstree-ocl")) {
                return;
            } else {
                getContactsList('{"Name":"","UserId":"","DepartId":"\'' + currentClickDepartNodeId + '\'","TagId":"","Status":0 }', 1, 10, currentClickDepartNodeId);
                getStatesContactsCountByDepartyId(currentClickDepartNodeId);
            }
        })
        //这里是拖拽的事件
        .on('move_node.jstree', function (e, data) {
            console.info(data);
            jQuery.post("modulemng/dndmodule",
                {
                    id: data.node.id,
                    parent: data.parent,
                    position: data.position
                },
                function (data, status) {
                    alert("Data: " + data + "\nStatus: " + status);
                }, 'json');
        });
    //新建部门的保存
    $("#btnDepartmentAddSave").click(function () {
        //获取需要的值
        var id = $("#departmentId").val();
        var name = $("#departmentName").val();
        var parentId = $("#departmentParentId").val();
        var order = $("#departmentOrderId").val();
        var accountAccountId = $("#accountId").val();
        var qyDepartment = $("#qyDepartment").val();
        var json = {
            "DepartmentId": id,
            "Name": name,
            "ParentId": parentId,
            "Order": order,
            "AccountId": accountAccountId,
            "QYDepartment": qyDepartment,
        };
        //判断部门名称不为空且不能超过32个字
        if (name.replace(/[^\x00-\xFF]/g, '**').length <= 64 && name.replace(/(^\s*)|(\s*$)/g, '') != "") {
            //清楚错误的提示
            $("#departmentName").parents(".form_item").removeClass("form_error");
            //调用后台方法
            $.post("../Contacts/InsertOrUpdateDepartmentEntity", { json: JSON.stringify(json), id: id }, function (data) {
                if (data.IsSuccess) {
                    /*这里是不刷新页面加载数据，暂时后台没有返回*/
                    var ref = $('#contact-org').jstree(true),
                        sel = ref.get_selected();
                    if (!sel.length) { return false; }
                    sel = sel[0];
                    if (id != "") {
                        //修改
                        ref.set_text(sel, data.ExtInfo.Name);
                    } else {
                        //新增
                        ref.create_node(sel, {id: data.ExtInfo.QYDepartment, text: data.ExtInfo.Name, li_attr: {qyDepartment: data.ExtInfo.QYDepartment, data_Order: data.ExtInfo.Order, data_AccountId: data.ExtInfo.AccountId } });
                    }
                    $("#__dialog__MNDialog__").modal("hide");
                } else {
                    alert(data.ErrorMsg);
                }
            });
        } else {
            $("#departmentName").parents(".form_item").addClass("form_error");
        }
    });
});