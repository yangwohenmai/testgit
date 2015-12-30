//全局变量用来判断是查看详情状态下还是在全选时的操作（"Handle"表示批量操作模式，为空则表示详情模式）
var ContactsHandleOrDetail="";
//用来存放列表的数据，点击全选的时候就不用再去请求后台
var StaticContactsData;
//获取成员列表的方法
function getContactsList(searchJson, pageIndex, pageSize,departId) {
        $('#member_list').html('<img style="margin: 0 auto;display: block;" src="../Content/Images/Contacts/ico_loading_white_967e3eed.gif" />');
        departId = departId || "";
        pageIndex = pageIndex || 1; //默认为第一页
        pageSize = pageSize || 10; //默认为10条数据
        var defaultSearchJson = '{"Name":"","UserId":"","DepartId":"","TagId":"","Status":0 }';
        searchJson = searchJson || defaultSearchJson;
        $.get("../Contacts/GetUserQYAllPageList", { pageIndex: pageIndex, pageSize: pageSize, searchJson: searchJson,departId:departId }, function (result) {
            if (result.ContentEntity.Body.length>0) {
                $("#member_list").empty();
                $("#Contacts_list_tmpl").tmpl(result.ContentEntity.Body).appendTo("#member_list");
                jQuery.get("../Content/tmpl/foot1.htm", function (repText) {
                    jQuery("#tableFoot").empty();
                    jQuery.template("tmplData", repText);
                    jQuery.tmpl("tmplData", result.ContentEntity.Foot).appendTo("#tableFoot");
                });
                StaticContactsData = result.ContentEntity.Body;
            } else {
                $("#member_list").html("<div style='text-align:center'>没有成员</div>");
                jQuery.get("../Content/tmpl/foot1.htm", function (repText) {
                    jQuery("#tableFoot").empty();
                    jQuery.template("tmplData", repText);
                    jQuery.tmpl("tmplData", result.ContentEntity.Foot).appendTo("#tableFoot");
                });
                StaticContactsData = result.ContentEntity.Body;
            }
        });
    }
//获取不同部门中的成员数量
function getStatesContactsCountByDepartyId(departId) {
    $.get("../Contacts/GetUserQYStatusCount", {departId:'\''+departId+'\''}, function(result) {
            $(".dropdown.drop_page_title > a .dropdown-toggle-text").html("全部成员<span class='gray'>("+result.totalCount+")</span>");
            $(".dropdown.drop_page_title ul li[value=0] .gray").text("("+result.totalCount+")");
            $(".dropdown.drop_page_title ul li[value=1] .gray").text("("+result.Subscription+")");
            $(".dropdown.drop_page_title ul li[value=4] .gray").text("("+result.UnSubscription+")");
            $(".dropdown.drop_page_title ul li[value=2] .gray").text("("+result.Freeze+")");
    });
}
$(function () {
    //同步
    $(document).on("click", ".btn-SynchronizeQYUser", function () {
        $.post("../Contacts/SynchronizeQYUser", function (jData) {
            alert(jData.ErrorMsg);
        });
    });

    $(document).on("click", "#js_upload_file", function () {
        alert("sd");
    });

    //前一页、后一页,为了防止多次绑定click事件，这里每次都先解绑再绑定
    jQuery("body").undelegate('#tableFoot a:not(a.btn)', 'click').delegate("#tableFoot a:not(a.btn)", "click", function () {
        var page = jQuery(this).attr("page");
        if ($(".tab_filetype_items li .active").text() == "员工") {
            var searchValue = $("#token-input-").val();
            getContactsList('{"Name":"' + searchValue + '","UserId":"' + searchValue + '","DepartId":"","TagId":"","Status":0 }', page, 10);
        }
        //判断当前操作是部门还是标签
        else if ($("#contact-org").is(":visible")) {
            var ref = $("#contact-org").jstree(true),
                selectedDepartId = ref.get_selected().toString();
                getContactsList('{"Name":"","UserId":"","DepartId":"\''+selectedDepartId+'\'","TagId":"","Status":0 }', page, 10, selectedDepartId);
        }
        else {
            var ref = $("#contact-tag-list").jstree(true),
                selectedTagId = ref.get_selected().toString();
                getContactsList('{"Name":"","UserId":"","DepartId":"","TagId":"\''+selectedTagId+'\'","Status":0 }', page, 10);
        }
    });
    //改变详细弹出层的高度
    $(".opp_result").css("height", $(window).height()+"px");
    //允许部门结构和标签的层可以拉伸改变大小
    $( ".js_party_tree.tree_box" ).resizable({
      resize: function( event, ui ) {
          $(".js_member_list.tree_list").css("left",ui.size.width);
          $(".bar_left.toolbar_search_add_box").css("width",ui.size.width-20);
      },
      maxWidth:440,
      minWidth:260,
      maxHeight:585,
      minHeight:585,
      grid: [ 20, 0 ]
    });


    //================================================================标签中的一些操作==========================================================start*/
    //添加成员中成员列表(公用)  参数说明（departId：部门ID，pageIndex：页数，appendOrAppendTo[append,appendTo]：插入还是追加，用于延时加载）
    function getContactsListInAddTag(departId,pageIndex,appendOrAppendTo) {
        pageIndex = pageIndex || 1;
        appendOrAppendTo = appendOrAppendTo || "appendTo";
        //需要调用后台方法获取到成员的列表
        var searchJson = '{"Name":"","UserId":"","DepartId":"\'' + departId + '\'","TagId":"","Status":0 }';
        $.get("../Contacts/GetUserQYAllPageList", { pageIndex: pageIndex, pageSize: 20, searchJson: searchJson }, function(result) {
            if (result.ContentEntity.Body.length > 0) {
                if(appendOrAppendTo=="appendTo") {
                    $("#tag_Addcontacts_listview_list").empty();
                    //填充数据
                    $("#Contacts_Add_Tag_list_tmpl").tmpl(result.ContentEntity.Body).appendTo("#tag_Addcontacts_listview_list");
                }else {
                    $("#tag_Addcontacts_listview_list").append($("#Contacts_Add_Tag_list_tmpl").tmpl(result.ContentEntity.Body));
                }
                //寻找相同项并选中
                $("#__dialog__addContacts__Dialog__ .mod-lozenges__item.token-input-token").each(function() {
                    var id = $(this).attr("data-id");
                    for (var i = 0; i < result.ContentEntity.Body.length; i++) {
                        if (id == result.ContentEntity.Body[i].UserQYId) {
                            $("#tag_Addcontacts_listview_list .mod-tree-people__people-item[data-id="+id+"]").addClass("mod-tree-people__people-item_selected")
                                .find("input[type=checkbox]").prop("checked",true);
                            break;
                        }
                    }
                });
            }else {
                $("#tag_Addcontacts_listview_list").empty();
            }
        });
    }
    //点击标签中的添加成员
    $("body").on("click", ".js_menu_tag_member", function () {
        //加载组织架构
        if ($.jstree.reference("#js_uins_list_org") != null) {
            $('#js_uins_list_org').jstree().destroy();
        }
        $("#js_uins_list_org").jstree({
            "core": {
                "animation": 0,
                "check_callback": true,
                'data': { 'url': '../Contacts/GetPartyList' }
            },
            "plugins": [
                    "wholerow"
                ]
        })
            //加载完毕后，需要选中和展开
            .bind("loaded.jstree", function(e, data) {
                //获取root的Id
                var ref = $("#js_uins_list_org").jstree(true),
                rootDepartId = $("#js_uins_list_org li:first").attr("id");
                ref.open_node(rootDepartId);//打开root分类
                ref.select_node(rootDepartId); //选择第一个
                getContactsListInAddTag(rootDepartId);
            });
        $("#tag_Addcontacts_listview_list div").each(function () {
            $(this).removeClass("mod-tree-people__people-item_selected");
            $(this).find("input[type=checkbox]").prop("checked",false);
        });
        $("#__dialog__addContacts__Dialog__ .mod-tree-people__input ul .mod-lozenges__item").remove();
        $("#__dialog__addContacts__Dialog__").modal();
    });
    //点击部门获取部门下的成员列表
    $("#js_uins_list_org").on("click", ".jstree-node a", function(e) {
        var eventNodeName = event.srcElement.nodeName;
        if (eventNodeName == 'INS') {
            return;
        } else {
            scrollTimes = 1;
            //选择的id值
            //alert($(event.srcElement).parents('li').attr('id'));
            var departId = $(event.srcElement).parents('li').attr('id');
            getContactsListInAddTag(departId);
        }
    });
    //列表滚动加载事件
    var scrollTimes = 1;
    $("#tag_Addcontacts_listview_list").scroll(function() {
        var height = $("#tag_Addcontacts_listview_list").height();
        var scrollTop = $("#tag_Addcontacts_listview_list").scrollTop();
        var ref = $("#js_uins_list_org").jstree(true),
            departId = ref.get_selected().toString();
        //820是这个层总高度，包含不可见部分，算法（每行的行高*总共多少行）41*20
        if((820*scrollTimes)-(height+scrollTop)<100) {
            scrollTimes += 1;
            getContactsListInAddTag(departId,scrollTimes,"append");
        }
    });
    //点击成员列表
    $("#tag_Addcontacts_listview_list").on("click", ".mod-tree-people__people-item", function() {
        if ($(this).hasClass("mod-tree-people__people-item_selected")) {
            //取消选中
            $(this).removeClass("mod-tree-people__people-item_selected").find("input[type=checkbox]").prop("checked",false);
            //移除
            $("#__dialog__addContacts__Dialog__ .token-input-list.input_text .mod-lozenges__item[data-id=" + $(this).attr("data-id").replace(/\./g, "\\.") + "]").remove();
        } else {
            //选中
            $(this).addClass("mod-tree-people__people-item_selected").find("input[type=checkbox]").prop("checked","checked");
            //添加
            var liImg = '<li class="mod-lozenges__item token-input-token" data-id='+$(this).attr("data-id")+'>'
            + ' <span class="mod-lozenges__icon">'
            + ' <img class="mod-lozenges__icon-avatar" style="height: 24px;width: 24px;" src='+$(this).find(".mod-tree-people__people-avatar").attr("src")+' alt="">'
            + '</span>'
            + '<span class="mod-lozenges__text">'+$(this).find(".mod-tree-people__people-name").text()+'</span>'
            + ' <span class="token-input-delete-token mod-lozenges__close"><i class="mod-lozenges__close-icon"></i></span>'
            + '</li>';
            $("#__dialog__addContacts__Dialog__ .token-input-list.input_text").prepend(liImg);
            $("#token-input-js_party_list").css("width", "1px");
        }
        
    });
    //删除成员
    $("#__dialog__addContacts__Dialog__,#__dialog__memberEditorView__").on("click", ".mod-lozenges__close-icon", function() {
        //取消选中
        $("#tag_Addcontacts_listview_list .mod-tree-people__people-item[data-id=" + $(this).parents("li").attr("data-id").replace(/\./g, "\\.") + "]").removeClass("mod-tree-people__people-item_selected")
            .find("input[type=checkbox]").prop("checked",false);
        $(this).parents("li").remove();
    });
    //快速查询,并显示出来
    $("#token-input-js_tag_list")
        .focus(function() {
            $(this).css("width", "50px");
            if($(this).val()=="") {
                $("#token-input-dropdown-searching_box").show().find("#token-input-dropdown-hint").show();
            }
        })
        .keyup(function() {
            if($(this).val()!="") {
            var searchJson = '{"Name":"'+$(this).val()+'","UserId":"","DepartId":"","TagId":"","Status":0 }';
                $.get("../Contacts/GetUserQYAllPageList", { pageIndex: 1, pageSize: 20, searchJson: searchJson }, function(result) {
                    if (result.ContentEntity.Body.length > 0) {
                        StaticContactsData = result.ContentEntity.Body;
                        $("#tag_search_contacts_list_result").empty();
                        $("#tag_search_contacts_list_tmpl").tmpl(result.ContentEntity.Body).appendTo("#tag_search_contacts_list_result");
                        $("#token-input-dropdown-searching_box #token-input-dropdown-result").show().siblings().hide();
                    } else {
                        $("#tag_search_contacts_list_result").empty().html("<p>无搜索结果</p>");
                        $("#token-input-dropdown-searching_box #token-input-dropdown-result").show().siblings().hide();
                    }
                });
            }
        })
        .blur(function() {
            $(this).val("");
            $(this).css("width", "1px");
            setTimeout(function() {
                $("#token-input-dropdown-searching_box").hide().find("#token-input-dropdown-hint").show().siblings().hide();
            }, 250);
        });
    $("#__dialog__addContacts__Dialog__ .token-input-list.input_text").click(function() {
        $("#token-input-js_tag_list").focus();
    });
    //搜索列表中点击成员
    $("#token-input-dropdown-searching_box").on("click", ".token-input-dropdown-item2", function() {
        //添加
            var liImg = '<li class="mod-lozenges__item token-input-token" data-id='+$(this).attr("data-id")+'>'
            + ' <span class="mod-lozenges__icon">'
            + ' <img class="mod-lozenges__icon-avatar" src='+$(this).find("img").attr("src")+' alt="">'
            + '</span>'
            + '<span class="mod-lozenges__text">'+$(this).find("span").text()+'</span>'
            + ' <span class="token-input-delete-token mod-lozenges__close"><i class="mod-lozenges__close-icon"></i></span>'
            + '</li>';
            $("#__dialog__addContacts__Dialog__ .token-input-list.input_text").prepend(liImg);
            $("#token-input-js_party_list").css("width", "1px");
            $("#tag_Addcontacts_listview_list .mod-tree-people__people-item[data-id=" + $(this).attr("data-id") + "]").addClass("mod-tree-people__people-item_selected")
                .find("input[type=checkbox]").prop("checked", true);
    });
    //点击保存成员（批量保存）
    $("#btnTagAddContactsSave").click(function() {
        var ref = $("#contact-tag-list").jstree(true),
            tagId = ref.get_selected().toString(),
            qyTag = $("#contact-tag-list #" + ref.get_selected()).attr("qytag");
        var contactIds = [];
        $("#__dialog__addContacts__Dialog__ .mod-lozenges__item.token-input-token").each(function() {
            contactIds.push($(this).attr("data-id"));
        });
        if (contactIds.length > 0) {
            $.post("../Contacts/AddUserTag", { ids: contactIds.join(","), tagId: '\''+tagId+'\'' }, function(result) {
                if (result.IsSuccess) {
                    getContactsList('{"Name":"","UserId":"","DepartId":"","TagId":"\'' + qyTag + '\'","Status":0 }', 1, 10);
                    $("#__dialog__addContacts__Dialog__").modal("hide");
                } else {
                    alert(result.ErrorMsg);
                }
            });
        } else {
            alert("对不起！你没有选择成员！");
        }
    });
    //删除标签成员
    $(".js_batch_del_tagContacts").click(function() {
        var ref = $("#contact-tag-list").jstree(true),
            tagId = ref.get_selected().toString(),
            qyTag = $("#contact-tag-list #" + ref.get_selected()).attr("qytag");
        var ids = [];
        $(".opp_result .choosed_content > ul> div > li").each(function() {
            ids.push($(this).attr("uin"));
        });
        $.post("../Contacts/RemoveUserTag", { tagId: '\''+tagId+'\'', ids: ids.join(",") }, function(result) {
            if (result.IsSuccess) {
                getContactsList('{"Name":"","UserId":"","DepartId":"","TagId":"\'' + qyTag.toString() + '\'","Status":0 }', parseInt($("#tableFoot .active .number").text()), 10);
                 //关闭详情 弹出层
                $(".opp_result").animate({ "right": "-370px" }, 500).hide();
            } else {
                alert(result.ErrorMsg);
            }
        });
    });
    //================================================================标签中的一些操作==========================================================end*/


    //===============================================批量导入成员================================================================================start*/
    //批量导入中的下一步
    $("#step_next").click(function () {
        var inviteResult;
        if ($("#invite").attr("checked") == "checked") {
            inviteResult = true;
        } else {
            inviteResult = false;
        }
        jQuery("#FormUoLoad").ajaxSubmit({
            url: '../Contacts/ImportQYUsers',
            data: {
                invite: inviteResult
            },
            success: function (responseTexts) {
                var responseText = JSON.parse(responseTexts);
                if (!responseText.IsSuccess) {
                    alert(responseText.ErrorMsg);
                }
                else {
                    $('#m2').modal('hide');
                    location.reload();
                }
            },
            complete: function (xhr, ts) {
                xhr = null;
            },
            error: function (content) {
                alert("出错了")
            }
        });
        ////选择第二步的导航
        //$("#__dialog__memberImportView__ .nav_box").find("li:eq(1)").addClass("on").siblings().removeClass("on");
        ////显示第二步的层，隐藏其他的层
        //$("#step_two_content").show().siblings(".step_content").hide();
        ////隐藏下一步的操作按钮
        //$("#__dialog__memberImportView__ .modal-footer").hide();

    });
    //点击导入
    $(".js_menu_import").click(function () {
        $("#step_one_content").show().siblings(".step_content").hide();
        $("#__dialog__memberImportView__ .modal-footer").show();
    });
    //导出模板
    $("#EmportTemplate").click(function () {
        location.href = '../Contacts/EmportTemplate';
    });
    //===============================================批量导入成员================================================================================end*/
    
    
    /*==================================================================成员批量操作=============================================================start*/
    //详情中的移动和批量操作中的移动公共方法
    function loadDepartJstreeDialog() {
        if ($.jstree.reference("#departmentMoveDialog") != null) {
            $('#departmentMoveDialog').jstree().destroy();
        }
        $('#departmentMoveDialog')
            .jstree({
                "core": {
                    "animation": 0,
                    "check_callback": true,
                    'data': { 'url': '../Contacts/GetPartyList' },
                },
                "plugins": [
                    "wholerow"
                ]
            })
            //加载完毕后，需要选中和展开
            .bind("loaded.jstree", function(e, data) {
                //选中第一个
                data.instance.select_node($("#departmentMoveDialog li:first").attr("id"));
                //打开一级分类
                data.instance.open_node($("#departmentMoveDialog li:first").attr("id"));
            });
        $("#__dialog__moveContacts__Dialog__").modal();
    }
    //详情中的邀请关注和批量操作中的邀请
    function InviteMember(id, confirmMsg) {
        if (confirm(confirmMsg)) {
            $.post("../Contacts/InviteMember", { id: id }, function (result) {
                if (result.IsSuccess) {
                        alert("邀请成功！");
                        //window.location.reload();
                    } else {
                        alert("非常抱歉！您的申请未能成功发送，请重试！");
                        //window.location.reload();
                    }
            });
        }
    }
    //调用邀请关注接口
    function inviteAttention(json, confirmMsg) {
        if (confirm(confirmMsg)) {
            $.post("../Contacts/InviteAttention", { json: json }, function (result) {
                if (result.substring(0, 4) == "true") {
                    alert("邮件发送成功！");
                    //window.location.reload();
                } else {
                    alert("非常抱歉！您的申请未能成功发送，请重试！");
                    //window.location.reload();
                }
            });
        }
    }
//点击全选checkBox
    $(".js_checkbox_all").click(function () {
        //全选与反选
        $("#member_list input[type=checkbox][class='js_checkbox checkbox']").prop("checked", jQuery(this).prop('checked'));
        if (jQuery(this).prop('checked')) {
            //现在处于批量操作模式
            ContactsHandleOrDetail = "Handle";
            //修改标题
            $(".opp_result .choosed_header .choosed_mainTitle").html("批量选择(<span class='js_member_count'>" + $("#member_list").children().length + "</span>)");
            //添加列表
            $(".opp_result .member_avator_list").empty();
            $("#Contacts_batchHandle_tmpl").tmpl(StaticContactsData).appendTo(".opp_result .member_avator_list");
            //把所有行都选择selected
            $("#member_list li").addClass("checked");
            //显示层
            $(".opp_result").show().animate({ "right": 0 }, 500);
        } else {
            //现在退出批量操作模式
            ContactsHandleOrDetail = "";
            //隐藏层
            $(".opp_result").animate({ "right": "-370px" }, 500).hide();
            $("#member_list li").removeClass("checked");
        }
        //显示操作按钮
        if($("#contact-org").is(":visible")) {
            $("#member_tool_handle").show().siblings(".js_member_toolbar").hide();
        }else {
            $("#member_tool_tag").show().siblings(".js_member_toolbar").hide();
        }
    });
    //批量操作层中的单个删除图标（头像旁边）
    $(".opp_result .member_avator_list").on("click", ".js_deselect", function () {
        //移除
        $(this).parent().parent().remove();
        //移除详情的框
        $("#memberPopover").remove();
        //去掉列表中相应的选中状态checked
        $("#member_list li[uin='" + $(this).parent().attr("uin") + "']").find("input").removeAttr("checked");
        $("#member_list li[uin='" + $(this).parent().attr("uin") + "']").removeClass("checked");
        //改变相应的批量选择的成员数量
        var newCount = parseInt($(".js_member_count").text()) - 1;
        $(".js_member_count").text(newCount);
        //判断是不是已经都删除了
        if ($(".opp_result .member_avator_list").children().length == 0) {
            //结束了批量操作模式，回到详情模式，如果现在点击应该显示详情页面
            ContactsHandleOrDetail = "";
            //关闭窗口
            $(".opp_result").animate({ "right": "-370px" }, 500).hide();
            $("#member_list li").removeClass("checked");
            //移除全选
            $(".js_checkbox_all").removeAttr("checked");
        }
    });
    //单选，列表前面的checkbox
    $("#member_list").on("click", ".js_checkbox", function (e) {
        $(this).parents("li").removeClass("selected");
        if ($(this).prop('checked')) {
            if ($("#member_list input[class='js_checkbox checkbox']:checked").length == 1 && ContactsHandleOrDetail == "") {
                ContactsHandleOrDetail = "Handle";
                //修改标题
                $(".opp_result .choosed_header .choosed_mainTitle").html("批量选择(<span class='js_member_count'>" + $("#member_list input[class='js_checkbox checkbox']:checked").length + "</span>)");
                //添加列表
                $(".opp_result .member_avator_list").empty();
                //添加选择状态
                $(this).parents("li").addClass("checked");
                //在操作层中加入相应的成员
                var contactsHandle = '<div>'
                    + '<li class="member_avator_item js_member_avatarlabel" uin=' + $(this).parents("li").attr("uin") + '>'
                    + '<input type="hidden" class="c_userid" value="' + $(this).parents("li").find(".account").text() + '" />'
                    + '<input type="hidden" class="c_email" value="'+$(this).parents("li").find(".mail").text()+'" />'
                    + '<a class="icon icon_delete js_deselect" href="javascript:;"></a>'
                    + '<img height="60" width="60" src=' + $(this).parents("li").find("img").attr("src") + '>'
                    + '<!--<p class="e_name">costa</p>-->'
                    + '<p class="c_name">' + $(this).parents("li").find(".js_username").text() + '</p>'
                    + '</li>'
                    + '</div>';
                $(".opp_result .member_avator_list").append(contactsHandle);
                //显示层
                $(".opp_result").show().animate({ "right": 0 }, 500);
            } else {
                //添加选择状态
                $(this).parents("li").addClass("checked");
                //在操作层中加入相应的成员
                var contactsHandle = '<div>'
                    + '<li class="member_avator_item js_member_avatarlabel" uin=' + $(this).parents("li").attr("uin") + '>'
                    + '<input type="hidden" class="c_userid" value="' + $(this).parents("li").find(".account").text() + '" />'
                    + '<input type="hidden" class="c_email" value="'+$(this).parents("li").find(".mail").text()+'" />'
                    + '<a class="icon icon_delete js_deselect" href="javascript:;"></a>'
                    + '<img height="60" width="60" src=' + $(this).parents("li").find("img").attr("src") + '>'
                    + '<!--<p class="e_name">costa</p>-->'
                    + '<p class="c_name">' + $(this).parents("li").find(".js_username").text() + '</p>'
                    + '</li>'
                    + '</div>';
                $(".opp_result .member_avator_list").append(contactsHandle);
                //改变相应的批量选择的成员数量
                var newCount = parseInt($(".js_member_count").text()) + 1;
                $(".js_member_count").text(newCount);
            }
        } else {
            //如果一个都没有选择，则退出批量操作模式,否则进入详情模式
            if ($("#member_list input[class='js_checkbox checkbox']:checked").length == 0) {
                ContactsHandleOrDetail = "";
                $(this).parents("li").removeClass("checked");
                //隐藏层
                $(".opp_result").animate({ "right": "-370px" }, 500).hide();
            } else {
                $(".opp_result .member_avator_list li[uin='" + $(this).parents("li").attr("uin") + "']").parent().remove();
                $(this).parents("li").removeClass("checked");
                //改变相应的批量选择的成员数量
                var newCount = parseInt($(".js_member_count").text()) - 1;
                $(".js_member_count").text(newCount);
            }
        }
        e.stopPropagation();
        //显示操作按钮
        if($("#contact-org").is(":visible")) {
            $("#member_tool_handle").show().siblings(".js_member_toolbar").hide();
        }else {
            $("#member_tool_tag").show().siblings(".js_member_toolbar").hide();
        }
    });
    //鼠标放上去的效果
    $("body").on({
        mouseenter: function () {
            var this_top = $(this).offset().top + 10;
            var this_left = $(this).offset().left - 330;
            var uin = $(this).attr("uin");
            for (var i = 0; i < StaticContactsData.length; i++) {
                if (StaticContactsData[i].UserQYId == uin) {
                    $("#Contacts_deatail_float_tmpl").tmpl(StaticContactsData[i]).appendTo("body");
                    $("#memberPopover").css({ "top": this_top + "px", "left": this_left + "px" });
                    break;
                }
            }
        },
        mouseleave: function () {
            $("#memberPopover").remove();
        }
    }, ".js_member_avatarlabel");
    
    //批量删除
    $(".js_batch_del").click(function() {
        var users = [];
        $(".member_avator_list.js_member_list li input.c_userid").each(function () {
            users.push($(this).val());
        });
        $.get("../Contacts/DeleteByUserIds", { userIds: users.join(',') }, function (result) {
            if (result.IsSuccess)
            {
                //删除选中行
                $(".js_member_list .js_list_member.checked").remove();
                //现在退出批量操作模式
                ContactsHandleOrDetail = "";
                //隐藏更多的弹出层
                $(".js_menu_list.menu_list").hide();
                //关闭批量选择框
                $(".opp_result").animate({ "right": "-370px", "display": "none" }, 500);
                $(".opp_result").hide();
                //重新加载数据
                rootDepartId = $("#contact-org li:first").attr("id");
                getContactsList('{"Name":"","UserId":"","DepartId":"\'' + rootDepartId + '\'","TagId":"","UserQYId":"" }', 1, 10, rootDepartId);
                getStatesContactsCountByDepartyId(rootDepartId);
            }
            else
            {
                alert(result.ErrorMsg);
            }
        })
    });
    //批量移动
    $(".js_batch_move").click(function() {
        loadDepartJstreeDialog();
    });
    //批量邀请关注
    $(".js_batch_invite").click(function() {
        var noEmail = "";
        var allLength = $(".opp_result .choosed_content ul li").length;
        var Recipients = [];
        $(".opp_result .choosed_content ul li").each(function() {
            var email = $(this).find(".c_email").val();
            if (email=="") {
               noEmail+=$(this).find(".c_name").text()+"\n\n";
            } else {
                Recipients.push(email);
            }
        });
        var json = '{ QYName: "哥带你飞", QYCode: "http://liuhaiyang.qxuninfo.com/qy/Upload/35/20141126/fc45cf0c-f6ab-4f5c-bd66-cdad51a31f5b.jpg",Recipients:"'+Recipients.join(",")+'" }';
        var confirmMsg = "";
        if (noEmail=="") {
            confirmMsg = "是否通过邮件发送二维码，邀请所选的" + allLength + "位成员关注？";
        } else {
            confirmMsg = "是否通过邮件发送二维码，邀请所选的"+allLength+"位成员关注？\n\n"+noEmail+"未设置邮箱，无法发送";
        }
        inviteAttention(json,confirmMsg);
    });
    /*==================================================================成员批量操作=============================================================end*/


    /*==================================================================搜索框中的一些操作=============================================================start*/
    //组织结构与标签的切换
    //切换到结构
    $("body").on("click", ".js_menu_party", function () {
        $("#contact-org").show().siblings().hide();
        $(".contact_view").find(".js_toolbar.tool_bar").remove();
        $(".contact_view").prepend($("#toolbar_search_add_box_init_tmpl").html());
        //获取数据
        var ref = $("#contact-org").jstree(true);
        var departId = ref.get_selected().toString();
        getContactsList('{"Name":"","UserId":"","DepartId":"\''+departId+'\'","TagId":"","Status":0 }', 1, 10,departId);
        //隐藏详细信息的层并清空并结束操作模式
        $(".opp_result").animate({ "right": "-370px" }, 500).hide();
        $(".opp_result .member_avator_list js_member_list").empty();
        ContactsHandleOrDetail = "";
        getStatesContactsCountByDepartyId(departId);
    });
    //切换到标签
    $("body").on("click", ".js_menu_tag", function () {
        $("#contact-tag").show().siblings().hide();
        $(".contact_view").find(".js_toolbar.tool_bar").remove();
        $(".contact_view").prepend($("#toolbar_search_add_box_tag_tmpl").html());
        //获取数据
        var ref = $("#contact-tag-list").jstree(true);
        var tagId = $("#contact-tag-list #" + ref.get_selected()).attr("qytag");
        getContactsList('{"Name":"","UserId":"","DepartId":"","TagId":"\''+tagId+'\'","Status":0 }', 1, 10);
        //改变标题的值
        var text = ref.get_node(tagId).text;
        $(".bar_right_text-title.js_menu_title").text(text);
        //隐藏详细信息的层并清空并结束操作模式
        $(".opp_result").animate({ "right": "-370px" }, 500).hide();
        $(".opp_result .member_avator_list js_member_list").empty();
        ContactsHandleOrDetail = "";
    });
    /*==================================================================搜索框中的一些操作=============================================================end*/


    /*==================================================================搜索框中的一些操作=============================================================start*/
    //初始化搜索框的样子
    $(".contact_view").prepend($("#toolbar_search_add_box_init_tmpl").html());
    //点击搜索的图标
    $("body").on("click", ".js_menu_search", function () {
        //把搜索框显示出来
        $(".js_menu_search_box").css("display", "block");
        //把焦点放在input上，如果失去焦点则隐藏并把文本清空
        $("#token-input-").focus().val("");
    });
    //失去焦点
    $("body").on({
        blur: function () {
            //这里是判断如果是搜索结果列表
            if ($(".bar_right_search").has("#token-input-").length == 0) {
                //隐藏搜索层
                $(".js_menu_search_box").css("display", "none");
            }
        },
        focus: function () {
            //从后台请求数据，并通过模板绑定在下拉框中
            if ($("#token-input-").val() != "") {
                //这里是判断如果是搜索结果列表
                if ($(".bar_right_search").has("#token-input-").length == 0) {
                    //显示搜索弹出列表层
                    $(".menu_avatar_wrap").css({ "display": "block", "position": "absolute", "top": "278px", "left": "" + $("#token-input-").offset().left + "px", "width": "240px", "z-index": "999" });
                } else {
                    //显示搜索弹出列表层
                    $(".menu_avatar_wrap").css({ "display": "block", "position": "absolute", "top": "278px", "left": "" + $("#token-input-").offset().left + "px", "width": "435px", "z-index": "999" });
                }
            }
        },
        keyup: function () {
            //从后台请求数据，并通过模板绑定在下拉框中
            if ($("#token-input-").val() != "") {
                //把获取到的数据填充到ul中(只显示前5条数据)
                var searchJson = '{"Name":"' + $("#token-input-").val() + '","UserId":"' + $("#token-input-").val() + '","DepartId":"","TagId":"","Status":0}';
                $.get("../Contacts/GetUserQYAllPageList", { pageIndex: 1, pageSize: 5, searchJson: searchJson }, function (result) {
                    //先清空li，除最后两个（其他的都是动态加载进来的）
                    $("#token-input-dropdown-result ul .contacts_menu_avatar_item").remove();
                    $("#token-input-dropdown-result ul").prepend($("#Contacts_search_menu_list").tmpl(result.ContentEntity.Body));
                    StaticContactsData = result.ContentEntity.Body;
                });
                //这里是判断如果是搜索结果列表
                if ($(".bar_right_search").has("#token-input-").length == 0) {
                    //显示搜索弹出列表层
                    $(".menu_avatar_wrap").css({ "display": "block", "position": "absolute", "top": "278px", "left": "" + $("#token-input-").offset().left + "px", "width": "240px", "z-index": "999" });
                } else {
                    //显示搜索弹出列表层
                    $(".menu_avatar_wrap").css({ "display": "block", "position": "absolute", "top": "278px", "left": "" + $("#token-input-").offset().left + "px", "width": "435px", "z-index": "999" });
                }
                //把搜索框的值赋值到b标签中
                $(".menu_avatar_content .menu_avatar_title_more b").text($("#token-input-").val());
            }
        },
        keydown: function (e) {
            var curKey = e.which;
            if (curKey == 13) {
            }
        }
    }, "#token-input-");
    //点击除搜索弹出层以外的地方都隐藏
    $("body").not(".menu_avatar_wrap").click(function () {
        if (!$(event.target).hasClass("js_menu_search_input") && !$(event.target).hasClass("icon_magnifying")) {
            $(".menu_avatar_wrap").css("display", "none");
        }
    });
    //点击搜索列表
    var org = false;
    var tag = false;
    var org_li = false;
    var tag_li = false;
    $("body").on("click", ".menu_avatar_item", function () {
        //搜索框中的值
        var searchValue = $("#token-input-").val();
        //获取点击的类型（成员、组织、标签）
        var searchType = $(this).attr("search-type");
        //alert(searchType);
        //把列表的层换个样式，让其margin-left=0，这样左边的导航被遮住
        $(".contact_box").removeClass("contact_box").addClass("contact_search_box");
        $(".js_member_list.tree_list").addClass("tree_list_search_person");
        //把搜索层进行改变，加载别的层，以显示出不同的样子
        $(".contact_view").find(".js_toolbar.tool_bar").remove();
        $(".contact_view").prepend($("#toolbar_search_add_box_result_tmpl").html());
        $("#token-input-").val(searchValue);
        $(".tab_filetype_items li a").removeClass("active");
        //清空列表信息和分页
        $("#member_list").empty();
        $("#tableFoot").empty();
        switch (searchType) {
            //组织架构                                                                     
            case "org":
                $(".tab_filetype_items li[value=1] a").addClass("active");
                $(".js_member_list.tree_list").removeClass("tree_list_search_person");
                $("#contact-org").show().siblings().hide();
                //搜索
                if (org) { clearTimeout(org); }
                org = setTimeout(function () {
                    var ref = $("#contact-org").jstree(true);
                    ref.search(searchValue);
                     var firstSearchId=$('#contact-org .jstree-search:first').parent().attr("id");
                    //选中第一个组织架构
                    ref.deselect_all(); //取消所有的选中
                    ref.select_node(firstSearchId); //把跟设置为选中
                    getContactsList('{"Name":"","UserId":"","DepartId":"\''+firstSearchId+'\'","TagId":"","Status":0 }', 1, 10,firstSearchId);
                }, 250);
                break;
            //标签                                                              
            case "tag":
                $(".tab_filetype_items li[value=0] a").addClass("active");
                $(".js_member_list.tree_list").removeClass("tree_list_search_person");
                $("#contact-tag").show().siblings().hide();
                //搜索
                if (tag) { clearTimeout(tag); }
                tag = setTimeout(function () {
                    var ref = $("#contact-tag-list").jstree(true);
                    ref.search(searchValue);
                     var firstSearchId=$('#contact-tag-list .jstree-search:first').parent().attr("id");
                    //选中第一个组织架构
                    ref.deselect_all(); //取消所有的选中
                    ref.select_node(firstSearchId); //把跟设置为选中
                    getContactsList('{"Name":"","UserId":"","DepartId":"","TagId":"\''+firstSearchId+'\'","Status":0 }', 1, 10);
                }, 250);
                break;
            //成员                      
            case "contacts":
                $(".tab_filetype_items li[value=2] a").addClass("active");
                $.get("../Contacts/GetUserQYEntityById?id=" + $(this).attr("data-id"), function (result) {
                    $("#token-input-").val(result.Name);
                    $("#member_list").empty();
                    $("#Contacts_list_tmpl").tmpl(result).appendTo("#member_list");
                    $("#tableFoot").empty();
                });
                break;
        }
    });
    //点击回退按钮
    $("body").on("click", ".btn_back", function () {
        //把搜索层恢复到初始化的样子
        $(".contact_view").find(".js_toolbar.tool_bar").remove();
        $(".contact_view").prepend($("#toolbar_search_add_box_init_tmpl").html());
        //把列表页恢复到初始化的样子
        $(".contact_search_box").removeClass("contact_search_box").addClass("contact_box");
        $(".js_member_list.tree_list").removeClass("tree_list_search_person");
        //清除搜索状态
        $('#contact-org').jstree(true).clear_search();
        $('#contact-tag-list').jstree(true).clear_search();
        //重新加载数据
        rootDepartId = $("#contact-org li:first").attr("id");
        getContactsList('{"Name":"","UserId":"","DepartId":"\''+rootDepartId+'\'","TagId":"","UserQYId":"" }', 1, 10,rootDepartId);
        getStatesContactsCountByDepartyId(rootDepartId); 
        //选中第一个组织架构
        var ref = $("#contact-org").jstree(true);
        ref.deselect_all(); //取消所有的选中
        ref.select_node(rootDepartId); //把跟设置为选中
    });
    //点击列表上的下拉框进行搜索（全部、关注、未关注、禁用）
    $("body").on("click", ".dropdown-menu.drop_member li", function () {
        //得到状态值，传到后台查询
        var value = $(this).attr("value");
        //得到部门的ID
        var ref = $("#contact-org").jstree(true),
        departId=ref.get_selected().toString();
        //修改相应的文字显示信息
        $(".filter .dropdown-toggle-text").html($(this).find("a").html());
        getContactsList('{"Name":"","UserId":"","DepartId":"\''+departId+'\'","TagId":"","Status":"'+value+'" }', 1, 10,departId);
    });
    //搜索列表的tab切换(0表示标签，1表示结构，2表示成员)
    $("body").on("click", ".tab_filetype_items li", function () {
        var value = $(this).attr("value");
        var searchValue = $("#token-input-").val();
        //清空列表信息
        $("#member_list").empty();
        //切换选中的状态
        $(".tab_filetype_items li a").removeClass("active");
        $(this).children("a").addClass("active");
        switch (value) {
            case "0":
                $(".js_member_list.tree_list").removeClass("tree_list_search_person");
                $("#contact-tag").show().siblings().hide();
                //搜索
                if (org_li) { clearTimeout(org_li); }
                org_li = setTimeout(function () {
                    $('#contact-org').jstree(true).search(searchValue);
                }, 250);
                break;
            case "1":
                $(".js_member_list.tree_list").removeClass("tree_list_search_person");
                $("#contact-org").show().siblings().hide();
                //搜索
                if (tag_li) { clearTimeout(tag_li); }
                tag_li = setTimeout(function () {
                    $('#contact-tag-list').jstree(true).search(searchValue);
                }, 250);
                break;
            case "2":
                $(".js_member_list.tree_list").addClass("tree_list_search_person");
                getContactsList('{"Name":"'+searchValue+'","UserId":"'+searchValue+'","DepartId":"","TagId":"","Status":0 }', 1, 10);
                break;
        }
    });
    /*==================================================================搜索框中的一些操作=============================================================end*/


    /*==================================================================成员列表中包含的一些操作=============================================================start*/
    //点击成员查看详情
    $("#member_list").on("click", ".js_list_member", function (e) {
        //隐藏更多的弹出层
        $(".js_menu_list.menu_list").hide();
        //判断是不是出于全选的和单选的状态
        if (ContactsHandleOrDetail == "Handle") {
            //判断是否被选择，checked
            if ($(this).hasClass("checked")) {
                //移除选择的状态
                $(this).removeClass("checked");
                //移除checkbox的选中
                $(this).find("input").removeAttr("checked");
                //在操作层中移除相应的成员
                $(".opp_result .member_avator_list li[uin='" + $(this).attr("uin") + "']").parent().remove();
                //改变相应的批量选择的成员数量
                var newCount = parseInt($(".js_member_count").text()) - 1;
                $(".js_member_count").text(newCount);
                //判断是不是已经都删除了
                if ($(".opp_result .member_avator_list").children().length == 0) {
                    //结束了批量操作模式，回到详情模式，如果现在点击应该显示详情页面
                    ContactsHandleOrDetail = "";
                    //关闭窗口
                    $(".opp_result").animate({ "right": "-370px" }, 500).hide();
                    $("#member_list li").removeClass("checked");
                    //移除全选
                    $(".js_checkbox_all").removeAttr("checked");
                }
            } else {
                //添加选择的状态
                $(this).addClass("checked");
                //添加checkbox的选中
                $(this).find("input").prop("checked", true);
                //在操作层中加入相应的成员
                var contactsHandle = '<div>'
                    + '<li class="member_avator_item js_member_avatarlabel" uin=' + $(this).attr("uin") + '>'
                    + '<input type="hidden" class="c_userid" value="' + $(this).find(".account").text() + '" />'
                    + '<input type="hidden" class="c_email" value="'+$(this).find(".mail").text()+'" />'
                    + '<a class="icon icon_delete js_deselect" href="javascript:;"></a>'
                    + '<img height="60" width="60" src=' + $(this).find("img").attr("src") + '>'
                    + '<!--<p class="e_name">costa</p>-->'
                    + '<p class="c_name">' + $(this).find(".js_username").text() + '</p>'
                    + '</li>'
                    + '</div>';
                $(".opp_result .member_avator_list").append(contactsHandle);
                //改变相应的批量选择的成员数量
                var newCount = parseInt($(".js_member_count").text()) + 1;
                $(".js_member_count").text(newCount);
            }
        } else {
            //修改标题名称
            $(".choosed_mainTitle").text("成员资料");
            //如果已经弹出了，再次点击就关闭
            if ($(this).hasClass("selected")) {
                $(".opp_result").animate({ "right": "-370px", "display": "none" }, 500);
                $(".opp_result").hide();
                $(this).removeClass("selected");
            } else {
                //添加selected的class
                $("#member_list li").removeClass("selected");
                $(this).addClass("selected").siblings();
                //显示详细的层
                $(".opp_result").show();
                $(".opp_result").animate({ "right": 0 }, 500);
                //获取成员信息
                var uin = $(this).attr("uin");
                var departId = "";
                if ($("#contact-org").is(":visible"))
                {
                    var ref = $("#contact-org").jstree(true);
                        departId = ref.get_selected().toString();
                }
                $.get("../Contacts/GetUserQYEntityById?id=" + uin,{departId:departId}, function (result) {
                    $(".opp_result .member_avator_list").empty();
                    $("#Contacts_detail_tmpl").tmpl(result).appendTo(".opp_result .member_avator_list");
                    //判断是否置顶
                    if (result.IsTop) {
                        $(".js_setTop").text("取消置顶");
                    } else {
                        $(".js_setTop").text("置顶");
                    }
                    //判断是否禁用
                    if (result.Enable == 1) {
                        $(".js_enable").text("禁用");
                    } else {
                        $(".js_enable").text("启用");
                    }
                    //判断是否可用置顶按钮（非本部门成员不能置顶）
                    var ref = $("#contact-org").jstree(true),
                        departId = ref.get_selected().toString();
                    var departmentArry = result.Department.split(",");
                    if (departmentArry.indexOf('\''+departId+'\'')>-1) {
                        $(".js_setTop").removeAttr("disabled");
                    } else {
                        $(".js_setTop").attr("disabled",true);
                    }
                    //判断是否已经关注（显示邀请关注按钮）
                    if (result.Status==1) {
                        $(".js_invite").attr("disabled",true);
                    } else {
                        $(".js_invite").removeAttr("disabled");
                    }
                });
                //显示操作按钮（判断是在标签中还是在部门中）
                if($("#contact-org").is(":visible")) {
                    $("#member_tool_detail").show().siblings(".js_member_toolbar").hide();
                }else {
                    $("#member_tool_tag").show().siblings(".js_member_toolbar").hide();
                }
            }
        }
        e.stopPropagation();
    });
    //详情弹出的关闭按钮
    $(".js_close").click(function () {
        //如果是批量操作的模式
        if (ContactsHandleOrDetail == "Handle") {
            //去掉所有的选择状态
            $("#member_list li").removeClass("checked");
            $("#member_list input[class='js_checkbox checkbox']").removeAttr("checked");
            //这个是全选的
            $(".js_checkbox_all").removeAttr("checked");
            //回到详情模式
            ContactsHandleOrDetail = "";
        }
        //隐藏更多的弹出层
        $(".js_menu_list.menu_list").hide();
        $(".opp_result").animate({ "right": "-370px", "display": "none" }, 500);
        $(".opp_result").hide();
    });
    //详情弹出的顶置
    $(".js_setTop").click(function () {
        var id = $("#UserQYId").val(),
            isTop = $("#IsTop").val() == "true" ? false : true;
            var ref=$("#contact-org").jstree(true),
            departId=ref.get_selected().toString();
        $.post("../Contacts/IsTop", { id: id, isTop: isTop,departId:departId }, function (result) {
            if (result.IsSuccess) {
                if (isTop) {
                    //修改文字和隐藏域的值
                    $(".js_setTop").text("取消顶置");
                    $("#IsTop").val(isTop);
                    $("#member_list li[uin='" + id + "']").addClass("pintop");
                    $("#member_list").prepend($("#member_list li[uin='" + id + "']").parent());
                } else {
                    //修改文字
                    $("#IsTop").val(isTop);
                    $(".js_setTop").text("顶置");
                    $("#member_list li[uin='" + id + "']").removeClass("pintop");
                    $("#member_list .pintop:last").parent().after($("#member_list li[uin='" + id + "']").parent());
                }
            } else {
                alert(result.ErrorMsg);
            }
        });
    });
    //详情弹出的修改
    $(".js_update").click(function () {
        loadAddOrEditDialog();
        fillContactsInfo();
    });
    //详情弹出的邀请关注
    $(".js_invite").click(function() {
        var confirmMsg = "是否发送邀请关注请求？";
        InviteMember($("#UserQYId").val(), confirmMsg);
    });
    //详情弹出的禁用
    $(".js_enable").click(function () {
        var id = $("#UserQYId").val(),
            isEnable = $("#Enable").val() == "0" ? "Forbidden" : "Normal";
        if (isEnable == "Normal") {
            if (confirm("你确定要禁用" + $(".c_name").text() + "吗？")) {
                $.post("../Contacts/IsEnable", { id: id, isEnable: "Forbidden" }, function (result) {
                    if (result.IsSuccess) {
                        //修改文字和隐藏域的值
                        $(".js_enable").text("启用");
                        $("#Enable").val("1");
                        //修改状态
                        $(".opp_result .cc_header .freeze").text("启用");
                        //修改列表中的值
                        $("#member_list li[uin='" + id.replace(/\./g, "\\.") + "'] .state").html('<span class="icon icon_lock" style="vertical-align:-1px;" title="已启用"></span>');
                        $(".opp_result .cc_header .freeze").text("已禁用");
                    } else {
                        alert(result.ErrorMsg);
                    }
                });
            }
        } else {
            $.post("../Contacts/IsEnable", { id: id, isEnable: "Normal" }, function (result) {
                if (result.IsSuccess) {
                    //修改文字
                    $(".js_enable").text("禁用");
                    $("#Enable").val("0");
                    //修改状态
                    var status = $("#Status").val();
                    var text = "";
                    var statusIcon = "";
                    if (status == 1) {
                        text = "已关注";
                        statusIcon = '<span class="icon icon_questionmask" style="vertical-align:-1px;" title="已关注"></span>';
                    } else if (status == 4) {
                        text = "未关注";
                        statusIcon = '<span class="icon icon_questionmask" style="vertical-align:-1px;" title="未关注"></span>';
                    } else {
                        text = "已禁用";
                        statusIcon = '<span class="icon icon_lock" style="vertical-align:-1px;" title="已禁用"></span>';
                    }
                    //修改列表中的值
                    $(".opp_result .cc_header .freeze").text(text);
                } else {
                    alert(result.ErrorMsg);
                }
            });
        }
    });
    //详情弹出的更多
    $(".js_menu").click(function () {
        if ($(".js_menu_list.menu_list").is(":visible")) {
            $(".js_menu_list.menu_list").hide();
        } else {
            $(".js_menu_list.menu_list").show();
        }
    });
    //详情弹出的更多中的移动
    $(".js_move").click(function () {
        $(".js_menu_list.menu_list").hide();
        loadDepartJstreeDialog();
    });
    //移动中的保存按钮
    $("#btnMoveContactsSave").click(function() {
        var ref = $('#departmentMoveDialog').jstree(true);
        var departId = ref.get_selected().toString();
        var ids = [];
        $(".member_avator_list.js_member_list div > li").each(function() {
            ids.push($(this).attr("uin"));
        });
        $.post("../Contacts/MoveDepartment", {departId:'\''+departId+'\'',ids: ids.join(",")}, function(result) {
            if (result.IsSuccess) {
                var refJstree = $("#contact-org").jstree(true),
                        SelecteddepartId = refJstree.get_selected();
                        getContactsList('{"Name":"","UserId":"","DepartId":"\''+SelecteddepartId+'\'","TagId":"","Status":0 }', parseInt($("#tableFoot .active .number").text()), 10,SelecteddepartId);
                $("#__dialog__moveContacts__Dialog__").modal("hide");
                //关闭详情 弹出层
                $(".opp_result").animate({ "right": "-370px" }, 500).hide();
                ContactsHandleOrDetail = "";
            } else {
                alert(result.ErrorMsg);
            }
        });
    });
    //详情弹出的更多中的日志
    $(".js_review").click(function () {

    });
    //详情弹出的更多中的删除
    $(".js_delete").click(function () {
        if (confirm("你确定要删除所选的1位成员吗？\n你不能撤销此操作。")) {
            var userId = $(".member_avator_list.js_member_list li .detail p.e_name").text();
            $.get("../Contacts/DeleteByUserId", { userId: userId }, function (result) {
                if (result.IsSuccess) {
                    //删除选中行
                    $(".js_member_list .js_list_member.checked").remove();
                    //隐藏更多的弹出层
                    $(".js_menu_list.menu_list").hide();
                    //关闭批量选择框
                    $(".opp_result").animate({ "right": "-370px", "display": "none" }, 500);
                    $(".opp_result").hide();
                    //重新加载数据
                    rootDepartId = $("#contact-org li:first").attr("id");
                    getContactsList('{"Name":"","UserId":"","DepartId":"\'' + rootDepartId + '\'","TagId":"","UserQYId":"" }', 1, 10, rootDepartId);
                    getStatesContactsCountByDepartyId(rootDepartId);
                }
                else {
                    alert(result.ErrorMsg);
                }
            })
        }
    });
    /*==================================================================成员列表中包含的一些操作=============================================================end*/


    /*==================================================================新增成员弹出层中的一些js操作==========================================================start*/
    //新增成员和修改成员的弹出层的公共方法
    function loadAddOrEditDialog() {
        //先清空扩展属性里面的值
        $("#__dialog__memberEditorView__ .extattr_item").remove();
        //获取扩展属性的列表并进行tmpl绑定
        $.ajax({
            type: "get",
            url: "../Contacts/GetPropertiesList",
            async: false,
            success: function(result) {
                $("#__dialog__memberEditorView__ .modal-body").append($("#Contacts_Add_Extattr_FormInput_tmpl").tmpl(result.ExtInfo));
            }
        });
//        $.get("../Contacts/GetPropertiesList", function (result) {
//            $("#__dialog__memberEditorView__ .modal-body").append($("#Contacts_Add_Extattr_FormInput_tmpl").tmpl(result));
//        });
        //加载组织结构的树型结构
        if ($.jstree.reference(".memberDialog_selectTree #token-input-dropdown-hint") != null) {
            $('.memberDialog_selectTree #token-input-dropdown-hint').jstree().destroy();
        }
        $('.memberDialog_selectTree #token-input-dropdown-hint').jstree({
            "core": {
                "animation": 0,
                "check_callback": true,
                'data': { 'url': '../Contacts/GetPartyList' }
            },
            "plugins": [
                    "wholerow"
                ]
        })
        .bind("loaded.jstree", function(e, data) {
            //打开一级分类
            data.instance.open_node($(".memberDialog_selectTree #token-input-dropdown-hint li:first").attr("id"));
        });
        //显示弹出层
        $("#__dialog__memberEditorView__").modal();
    }
    //点击新建成员，弹出层
    $("body").on("click", ".js_menu_create", function () {
        clearAddContactsInput();
        loadAddOrEditDialog();
    });
    //新增成员的保存
    $("#btnContactsAdd").click(function () {
        getAddContactsData(true);
    });
    //点击部门文本框，弹出下拉的组织架构列表
    $("#__dialog__memberEditorView__ .token-input-list").click(function () {
        if ($(this).hasClass("token-input-focused")) {
            $(".token-input-dropdown.memberDialog_selectTree").css({ "display": "none" });
            $('.memberDialog_selectTree #token-input-dropdown-hint').hide();
            $(this).removeClass("token-input-focused");
        } else {
            $("#token-input-js_party_list").focus();
            $(this).addClass("token-input-focused");
            $(".token-input-dropdown.memberDialog_selectTree").css({ "display": "block", "position": "absolute", "width": "398px", "height": "250px", "z-index": "9999", "margin-top": "-21px" });
            $('.memberDialog_selectTree #token-input-dropdown-hint').show();
        }
    });
    //选择下拉框中的组织名称
    $("body").on("click", ".token-input-dropdown.memberDialog_selectTree .jstree-node a", function (e) {
        var treeList = '<li class="mod-lozenges__item token-input-token" data-id=' + $(this).parent().attr("id") + '>'
            + '<span class="mod-lozenges__icon">'
            + '<i class="mod-lozenges__icon-org"></i>'
            + '</span>'
            + '<span class="mod-lozenges__text">' + $(this).text() + '</span>'
            + '<span class="token-input-delete-token mod-lozenges__close">'
            + '<i class="mod-lozenges__close-icon"></i>'
            + '</span>'
            + '</li>';

        $(".token-input-list.input_text").prepend(treeList);
        $("#token-input-js_party_list").css("width", "1px");
        e.stopPropagation();
    });
    //保存并添加下一条数据
    $("#btnContactsAddAndNext").click(function () {
        getAddContactsData();
    });
    //清空新增成员的文本框
    function clearAddContactsInput() {
        //清空所有的文本框
        $("#__dialog__memberEditorView__ input").val("");
        //accountId
        $("#__dialog__memberEditorView__ #js_accountid").val();
        //成员ID
        $("#__dialog__memberEditorView__ #js_id").val();
        //成员状态
        $("#__dialog__memberEditorView__ #js_status").val("UnSubscription");
        //是否启用或禁用
        $("#__dialog__memberEditorView__ #js_enable").val("Normal");
        //是否置顶
        $("#__dialog__memberEditorView__ #js_isTop").val("false");
        //成员标签
        $("#__dialog__memberEditorView__ #js_Tags").val("");
        //清空图片路径
        $("#__dialog__memberEditorView__ .photo").attr("src", "");
        //清空所在部门的框
        $("#__dialog__memberEditorView__ .mod-lozenges__item").remove();
        //把userid和微信id设置为可读
        $("#__dialog__memberEditorView__ #js_userid").removeAttr("readonly");
        //成员微信号
        $("#__dialog__memberEditorView__ #js_wechat").removeAttr("readonly");
        //去掉验证的信息（红色边框和字体）
        $("#__dialog__memberEditorView__ #js_name").parents(".form_item").removeClass("form_error");
        $("#__dialog__memberEditorView__ #js_userid").parents(".form_item").removeClass("form_error");
        $("#js_partial").parents(".form_item").removeClass("form_error");
        $("#js_mobile").parents(".form_item").removeClass("form_error");
        $("#js_mobile").next().hide();
        $("#js_email").parents(".form_item").removeClass("form_error");
        $("#js_email").next().hide();
        $("#js_party_list").parents(".form_item").removeClass("form_error");
        //隐藏部门下拉框的层
        $(".token-input-dropdown.memberDialog_selectTree").css({ "display": "none" });
        $('.memberDialog_selectTree #token-input-dropdown-hint').hide();
    }
    //提交数据进行保存(saveType用来判断是直接保存还是继续添加下一条，执行不同的方法)
    function getAddContactsData(saveType) {
        var isPassValidate = true;
        //accountId
        var js_accountid = $("#accountId").val();
        //成员ID
        var js_id = $("#__dialog__memberEditorView__ #js_id").val();
        //成员姓名
        var js_name = $("#__dialog__memberEditorView__ #js_name").val();
        //成员帐号
        var js_userid = $("#__dialog__memberEditorView__ #js_userid").val();
        //成员微信号
        var js_wechat = $("#__dialog__memberEditorView__ #js_wechat").val();
        //成员头像ID
        var js_imgid = $("#__dialog__memberEditorView__ .photo").attr("src");
        //成员手机
        var js_mobile = $("#__dialog__memberEditorView__ #js_mobile").val();
        //成员邮箱
        var js_email = $("#__dialog__memberEditorView__ #js_email").val();
        //成员状态
        var js_status=$("#__dialog__memberEditorView__ #js_status").val();
        //是否启用或禁用
        var js_enable=$("#__dialog__memberEditorView__ #js_enable").val();
        //是否置顶
        var js_isTop = $("#__dialog__memberEditorView__ #js_isTop").val();
        //成员标签
        var js_Tags = $("#__dialog__memberEditorView__ #js_Tags").val();
        //成员部门
        var partyListArry = [];
        $("#__dialog__memberEditorView__ .mod-lozenges__item").each(function () {
            partyListArry.push('\''+$(this).attr("data-id")+'\'');
        });
        //成员职位
        var js_position = $("#__dialog__memberEditorView__ #js_position").val();
        //成员扩展属性
        var contactsExtattrArry = [];
        $($("#__dialog__memberEditorView__ .js_exValue")).each(function (i, value) {
            //var contactsExtattrEntity = {
            //    ExtattrName: $(this).attr("name"),
            //    ExtattrValue: $(this).val(),
            //    Id: $(this).attr("data-id")
            //};
            var contactsExtattrEntity = {
                ExtattrName: $(this).attr("name"),
                ExtattrValue: $(this).val(),
                Id: $(this).attr("data-id")
            };
            contactsExtattrArry.push(contactsExtattrEntity);
        });
        //提交前需要进行验证
        //姓名不为空
        if (js_name.replace(/(^\s*)|(\s*&)/g, "") == "") {
            $("#__dialog__memberEditorView__ #js_name").parents(".form_item").addClass("form_error");
            isPassValidate = false;
        } else {
            $("#__dialog__memberEditorView__ #js_name").parents(".form_item").removeClass("form_error");
        }
        //帐号不为中文帐号不为空
        if (/[\u4E00-\u9FA5]/g.test(js_userid) || js_userid.replace(/(^\s*)|(\s*$)/g, "") == "") {
            $("#__dialog__memberEditorView__ #js_userid").parents(".form_item").addClass("form_error");
            isPassValidate = false;
        } else {
            $("#__dialog__memberEditorView__ #js_userid").parents(".form_item").removeClass("form_error");
        }
        //身份信息不能同时为空
        if (js_wechat.replace(/(^\s*)|(\s*&)/g, "") == "" && js_mobile.replace(/(^\s*)|(\s*&)/g, "") == "" && js_email.replace(/(^\s*)|(\s*&)/g, "") == "") {
            $("#js_partial").parents(".form_item").addClass("form_error");
            isPassValidate = false;
        } else {
            $("#js_partial").parents(".form_item").removeClass("form_error");
            if (js_mobile.replace(/(^\s*)|(\s*&)/g, "") != "") {
                if (!/^[0-9]*$/.test(js_mobile)) {
                    $("#js_mobile").parents(".form_item").addClass("form_error");
                    $("#js_mobile").next().show();
                    isPassValidate = false;
                } else {
                    $("#js_mobile").parents(".form_item").removeClass("form_error");
                    $("#js_mobile").next().hide();
                }
            }
            if(js_email.replace(/(^\s*)|(\s*&)/g, "") != "") {
                if (!/[\w!#$%&'*+/=?^_`{|}~-]+(?:\.[\w!#$%&'*+/=?^_`{|}~-]+)*@(?:[\w](?:[\w-]*[\w])?\.)+[\w](?:[\w-]*[\w])?/.test(js_email)) {
                    $("#js_email").parents(".form_item").addClass("form_error");
                    $("#js_email").next().show();
                    isPassValidate = false;
                } else {
                    $("#js_email").parents(".form_item").removeClass("form_error");
                    $("#js_email").next().hide();
                }
            }
        }
        //部门必填
        if (partyListArry.length<=0) {
            $("#js_party_list").parents(".form_item").addClass("form_error");
            isPassValidate = false;
        }else {
            $("#js_party_list").parents(".form_item").removeClass("form_error");
        }
        //只有都通过才提交
        if (isPassValidate) {
            var contactsAddEntity = {
                "UserQYId": js_id,
                "UserId": js_userid,
                "Email": js_email,
                "Avatar": js_imgid,
                "Mobile": js_mobile,
                "Position": js_position,
                "Name": js_name,
                "Gender": "Male",
                "Tel": "",
                "Enable": js_enable,
                "Status": js_status,
                "Tags": "",
                "IsTop": js_isTop,
                "TopTime": "",
                "WeixinId": js_wechat,
                "AccountId": js_accountid,
                "Extattr": JSON.stringify(contactsExtattrArry),
                "Department": partyListArry.join(","),
                "Tags": js_Tags
            };
            //alert(JSON.stringify(contactsAddEntity));
            $.post("../Contacts/InsertOrUpdateUserQYEntity", { json: JSON.stringify(contactsAddEntity) }, function (result) {
                if (result.IsSuccess) {
                    alert("成功");
                    if (saveType) {
                        $("#__dialog__memberEditorView__").modal("hide");
                        var ref = $("#contact-org").jstree(true),
                            departId = ref.get_selected();
                            getContactsList('{"Name":"","UserId":"","DepartId":"\''+departId+'\'","TagId":"","Status":0 }', parseInt($("#tableFoot .active .number").text()), 10,departId);
                        //关闭窗口
                        $(".opp_result").animate({ "right": "-370px" }, 500).hide();
                    } else {
                        clearAddContactsInput();
                    }
                } else {
                    alert(result.ErrorMsg);
                }
            });
        }
    }
    //修改时填充数据
    function fillContactsInfo() {
        clearAddContactsInput();
        var uin = $("#UserQYId").val();
        for (var i = 0; i < StaticContactsData.length; i++) {
                if (StaticContactsData[i].UserQYId == uin) {
                    //accountId
                    $("#__dialog__memberEditorView__ #js_accountid").val(StaticContactsData[i].AccountId);
                    //成员ID
                    $("#__dialog__memberEditorView__ #js_id").val(StaticContactsData[i].UserQYId);
                    //成员状态
                    if (StaticContactsData[i].Status==1) {
                        $("#__dialog__memberEditorView__ #js_status").val("Subscription");
                    }else if (StaticContactsData[i].Status==2) {
                        $("#__dialog__memberEditorView__ #js_status").val("Freeze");
                    } else if (StaticContactsData[i].Status==4) {
                        $("#__dialog__memberEditorView__ #js_status").val("UnSubscription");
                    }
                    //是否启用或禁用
                    if (StaticContactsData[i].Enable==0) {
                        $("#__dialog__memberEditorView__ #js_enable").val("Forbidden");
                    }else if (StaticContactsData[i].Enable==1) {
                        $("#__dialog__memberEditorView__ #js_enable").val("Normal");
                    }
                    //是否置顶
                    $("#__dialog__memberEditorView__ #js_isTop").val(StaticContactsData[i].IsTop);
                    //成员姓名
                    $("#__dialog__memberEditorView__ #js_name").val(StaticContactsData[i].Name);
                    //成员帐号
                    $("#__dialog__memberEditorView__ #js_userid").val(StaticContactsData[i].UserId).attr("readonly","true");
                    //成员微信号
                    if (StaticContactsData[i].WeixinId != ""){
                        $("#__dialog__memberEditorView__ #js_wechat").val(StaticContactsData[i].WeixinId).attr("readonly","true");
                    }
                    //成员头像ID
                    $("#__dialog__memberEditorView__ .photo").attr("src",StaticContactsData[i].Avatar);
                    //成员手机
                    $("#__dialog__memberEditorView__ #js_mobile").val(StaticContactsData[i].Mobile);
                    //成员邮箱
                    $("#__dialog__memberEditorView__ #js_email").val(StaticContactsData[i].Email);
                    //成员职位
                    $("#__dialog__memberEditorView__ #js_position").val(StaticContactsData[i].Position);
                    //成员标签
                    $("#__dialog__memberEditorView__ #js_Tags").val(StaticContactsData[i].Tags);
                    //成员部门
                    $(".token-input-list.input_text").prepend($("#Contacts_Edit_Department_tmpl").tmpl(StaticContactsData[i].DepartmentList));
                    //扩展属性
                    $("#__dialog__memberEditorView__ .js_exValue").each(function () {
                        var thisExtattrInput = $(this);
                        var extattrId = thisExtattrInput.attr("data-id");
                        var extattrName = thisExtattrInput.attr("name");
                        var extattr = JSON.parse(StaticContactsData[i].Extattr);
                        $.each(extattr,function (index,item) {
                            if (extattrName == item.ExtattrName) {
                                thisExtattrInput.val(item.ExtattrValue);
                            }                            
                        })
                    });
                }
            }
    }
    /*==================================================================新增成员弹出层中的一些js操作==========================================================end*/


    /*==================================================================成员属性=============================================================================start*/
    //点击设置成员属性,弹出层
    $("body").on("click", ".js_menu_attr", function () {
        //先清空
        $("#__dialog__corpextAttrView__ .js_attrItem").remove();
        //插入数据
        $.get("../Contacts/GetPropertiesList", function (result) {
            $("#__dialog__corpextAttrView__ .mod-contact-field-list__item_add").before($("#Contacts_Extattr_List_tmpl").tmpl(result.ExtInfo));
        });
        //显示弹出层
        $("#__dialog__corpextAttrView__").modal();
    });
    //点击添加成员属性
    $(".js_addAttr").click(function () {
        var js_addAttr_tr = '<div class="mod-contact-field-list__item js_attrItem js_addAttrItem" data-id="0">'
            + '<input type="checkbox" class="mod-contact-field-list__checkbox" checked="checked">'
            + '<input type="text" class="input_text mod-contact-field-list__input">'
            + '<span class="ui-d-n mod-contact-field-list__error-text js_error">属性命名不超过8个汉字</span>'
            + '<div class="mod-contact-field-list__delete hide js_del">'
            + '<span class="mod-contact-field-list__delete-icon" data-id="0"></span>'
            + '</div>'
            + '</div>';
        if ($(".js_addAttrItem").length <= 0) {
            //添加新的一行进行编辑
            $(".mod-contact-field-list__item_add").before(js_addAttr_tr);
        }
        //聚焦
        $(".mod-contact-field-list__input").focus();
        //失去焦点
        $(".mod-contact-field-list__input").blur(function () {
            if ($(this).val().replace(/(^\s*)|(\s*$)/g, "") != "") {
                var thisInput = $(this);
                //判断字符串的长度
                if ($(this).val().replace(/[^\x00-\xFF]/g, '**').length > 16) {
                    alert("属性名不超过8个汉字");
                    thisInput.focus();
                } else {
                    var Id = thisInput.parent().attr("data-id");
                    if (Id == "0") {
                        Id = "";
                    }
                    var attrEntity = {
                        "MemberPropertiesId": Id,
                        "PropertiesName": thisInput.val(),
                        "IsUse": thisInput.prev().prop("checked")
                    };
                    $.post("../Contacts/InsertOrUpdateMemberProperties", { json: JSON.stringify(attrEntity) }, function (result) {
                        if (result.IsSuccess) {
                            //修改整个层的id
                            thisInput.parent().attr("data-id", result.ExtInfo).removeClass("js_addAttrItem");
                            //修改删除按钮上的id，以便继续操作能是真实有效的
                            thisInput.parent().find(".mod-contact-field-list__delete-icon").attr("data-id", result.ExtInfo);
                            //然后把input替换成span
                            thisInput.replaceWith("<span class='attrName'>" + thisInput.val() + "<span>");
                        } else {
                            alert(result.ErrorMsg);
                        }
                    });
                }
            } else {
                $(this).parent().remove();
            }
        }).keydown(function (e) {
            var curKey = e.which;
            if (curKey == 13) {
                if ($(this).val().replace(/(^\s*)|(\s*$)/g, "") != "") {
                    $(this).parent().removeClass("js_addAttrItem");
                    $(this).replaceWith($(this).val());
                }
            }
        });
    });
    //删除属性
    $("body").on("click", ".mod-contact-field-list__delete-icon", function () {
        var id = $(this).attr("data-id");
        if (confirm("删除属性将删除该属性对应的所有内容，请谨慎操作。\n\n确认删除该属性？")) {
            $.post("../Contacts/DeleteMemberPropertiesEntity?id=" + id, function (result) {
                if (result.IsSuccess) {
                    $("#__dialog__corpextAttrView__ .js_attrItem[data-id=" + id.replace(/\./g, "\\.") + "]").remove();
                } else {
                    alert(result.ErrorMsg);
                }
            });
        }
    });
    //点击选择checkbox调用修改方法
    $("#__dialog__corpextAttrView__").on("click", ".mod-contact-field-list__checkbox", function () {
        var attrEntity = {
            "MemberPropertiesId": $(this).parent().attr("data-id"),
            "PropertiesName": $(this).next().text(),
            "IsUse": $(this).prop("checked")
        };
        $.post("../Contacts/InsertOrUpdateMemberProperties", { json: JSON.stringify(attrEntity) }, function (result) {
            if (result.IsSuccess) {
                //alert("成功");
            } else {
                alert(result.ErrorMsg);
            }
        });
    });
    /*==================================================================成员属性===============================================================================end*/
});