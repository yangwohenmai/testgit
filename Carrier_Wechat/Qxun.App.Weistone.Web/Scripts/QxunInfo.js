//getAllListOrSearchList(请求的url,页码模板地址,当前页,页大小,平台号,表格的bodyid,表格的footid,tmpl模板id,搜索条件的class名称)
function getAllListOrSearchList(handerurl, footur, page, pagesize, platid, tableBodyID, tableFootID, tmplID, searchClass) {
    //组装json
    if (searchClass != "") {
        var searchJson = "{";
        jQuery("." + searchClass + "").each(function () {
            searchJson += jQuery(this).attr("name") + ":\"" + jQuery(this).val() + "\",";
        })
        searchJson = searchJson.substring(0, searchJson.length - 1) + "}";
    }
    var _options = {};
    _options.action = "list";
    _options.page = 1;
    _options.handlerUrl = handerurl;
    _options.footUrl = footur;
    _options.id = 0;
    _options.pagesize = pagesize;
    loadTmpl();

    //前一页、后一页,为了防止多次绑定click事件，这里每次都先解绑再绑定
    jQuery("body").undelegate('#tableFoot a:not(a.btn)', 'click').delegate("#tableFoot a:not(a.btn)", "click", function () {
        _options.page = jQuery(this).attr("page");
        loadTmpl(_options);
    });



    function loadTmpl() {
        jQuery.ajax({
            url: _options.handlerUrl,
            type: "get",
            dataType: "json",
            data: { page: _options.page, pagesize: _options.pagesize, plat: platid, searchJson: searchJson },
            success: function (repJson) {
                if (repJson.ContentEntity && repJson.ContentEntity.Body) {
                    jQuery("#" + tableBodyID + "").empty();
                    jQuery("#" + tmplID + "").tmpl(repJson.ContentEntity.Body).appendTo("#" + tableBodyID + "");
                    jQuery.get(_options.footUrl, function (repText) {
                        jQuery("#" + tableFootID + "").empty();
                        jQuery.template("tmplData", repText);
                        jQuery.tmpl("tmplData", repJson.ContentEntity.Foot).appendTo("#" + tableFootID + "");
                    });
                }
                else {
                    jQuery("#" + tableBodyID + "").html("暂无数据");
                }
            },
            complete: function (xhr, ts) {
                xhr = null;
            }
        });
    }
}

/// <summary>
/// Form提交
/// </summary>
/// <param name="urlaction">请求的action</param>
/// <param name="dataJson">Form表单数据</param>
/// <param name="returnurl">提交成功后返回的URL</param>
/// <param name="isCustomToken">是否是手动设置的token</param>
/// <returns></returns>
function formSubmitWithReturnUrl(urlaction, dataJson, returnurl, isCustomToken) {
    var header = null;
    if (isCustomToken) {
        //自定义Token
        var tokenName = jQuery("#formTokenName").val();
        header = {
            "formTokenName": tokenName
        };
        header[tokenName] = jQuery("#" + tokenName).val();
    } else {
        //防重复提交用的表单Token
        header = {
            "formToken": jQuery("#formToken").val()
        };
    }
    jQuery.ajax({
        headers: header,
        url: urlaction,
        data: dataJson,
        async: false,
        type: "post",
        cache: false,
        success: function (repJson) {
            if (repJson) {
                if (repJson.IsSuccess) {
                    location.href = returnurl;
                }
                else {
                    alert(repJson.ErrorMsg);
                    return false;
                }
            }
        },
        complete: function (xhr, ts) {
            var newToken = xhr.getResponseHeader("formToken");
            if (newToken != null) {
                jQuery("#formToken").val(newToken);
            }
            else {
                var tokenName = jQuery("#formTokenName").val();
                var customeToken = xhr.getResponseHeader(tokenName);
                if (customeToken != null) {
                    jQuery("#" + tokenName).val(customeToken);
                }
            }
            xhr = null;
        },
        error: function () {
            return false;
        }
    });
}

/// <summary>
/// Form提交
/// </summary>
/// <param name="urlaction">请求的action</param>
/// <param name="dataJson">Form表单数据</param>
/// <returns></returns>
function formSubmitWithNoReturnUrl(urlaction, dataJson) {
    var result;
    jQuery.ajax({
        url: urlaction,
        data: dataJson,
        async: false,
        type: "post",
        cache: false,
        success: function (repJson) {
            if (repJson) {
                if (repJson.IsSuccess) {
                    result = true;
                }
                else {
                    alert(repJson.ErrorMsg);
                    result = false;
                }
            }
        },
        complete: function (xhr, ts) {
            xhr = null;
        },
        error: function () {
            result = false;
        }
    });
    return result;
}

/// <summary>
/// Form修改
/// </summary>
/// <param name="urlaction">请求的action</param>
/// <param name="dataJson">Form表单数据</param>
/// <param name="returnurl">提交成功后返回的URL</param>
/// <param name="isCustomToken">是否是手动设置的token</param>
/// <returns></returns>
function formUpdate(urlaction, dataJson, returnurl, isCustomToken) {
    var header = null;
    if (isCustomToken) {
        //自定义Token
        var tokenName = jQuery("#formTokenName").val();
        header = {
            "formTokenName": tokenName
        };
        header[tokenName] = jQuery("#" + tokenName).val();
    } else {
        //防重复提交用的表单Token
        header = {
            "formToken": jQuery("#formToken").val()
        };
    }
    jQuery.ajax({
        headers: header,
        url: urlaction,
        data: dataJson,
        async: false,
        type: "post",
        cache: false,
        success: function (repJson) {
            if (repJson) {
                if (repJson.IsSuccess) {
                    location.href = returnurl;
                }
                else {
                    alert(repJson.ErrorMsg);
                    return false;
                }
            }
        },
        complete: function (xhr, ts) {
            var newToken = xhr.getResponseHeader("formToken");
            if (newToken != null) {
                jQuery("#formToken").val(newToken);
            }
            else {
                var tokenName = jQuery("#formTokenName").val();
                var customeToken = xhr.getResponseHeader(tokenName);
                if (customeToken != null) {
                    jQuery("#" + tokenName).val(customeToken);
                }
            }
            xhr = null;
        },
        error: function () {

        }
    });
}

/// <summary>
/// Form修改
/// </summary>
/// <param name="urlaction">请求的action</param>
/// <param name="dataJson">Form表单数据</param>
/// <returns></returns>
function formUpdateWithNoReturnUrl(urlaction, dataJson) {
    var result;
    jQuery.ajax({
        url: urlaction,
        data: dataJson,
        async: false,
        type: "post",
        cache: false,
        success: function (repJson) {
            if (repJson) {
                if (repJson.IsSuccess) {
                    result = true;
                }
                else {
                    alert(repJson.ErrorMsg);
                    result = false;
                }
            }
        },
        complete: function (xhr, ts) {
            xhr = null;
        },
        error: function () {
            result = false;
        }
    });
    return result;
}

//ids表示要删除的id集合，platid表示当前页面的url参数名称，ajaxurl表示请求的地址，confirmmsg表示删除前提示信息
//注：一定要给tr一个id，格式：tr+id，如tr12
function DeleteByIdandPlatid(ids, platid, ajaxurl, confirmmsg) {
    if (confirm(confirmmsg)) {
        platid = jQuery.query.get(platid);
        jQuery.ajax({
            url: ajaxurl,
            type: "post",
            data: {
                "ids": ids,
                plat: platid
            },
            success: function (repText) {
                if (repText.IsSuccess) {
                    if (ids.indexOf(',') > 0) {
                        var item = ids.split(',');
                        for (var i = 0; i < item.length; i++) {
                            var trid = "#tr" + item[i];
                            jQuery(trid).fadeOut(function () {
                                jQuery(trid).remove();
                            });
                        }
                    }
                    else {
                        var trid = "#tr" + ids;
                        jQuery(trid).fadeOut(function () {
                            jQuery(trid).remove();
                        });
                    }
                }
                else {
                    alert(repText.ErrorMsg);
                }
            },
            complete: function (xhr, ts) {
                xhr = null;
            }
        });
    }
}

//直接点删除按钮
//ids表示要删除的id集合，platid表示当前页面的url参数名称，ajaxurl表示请求的地址，confirmmsg表示删除前提示信息
function SingleDelete(id, platid, ajaxurl, confirmmsg) {
    DeleteByIdandPlatid(id, platid, ajaxurl, confirmmsg);
}

//多选删除
//name表示复选框的name值，platid页面url参数，ajaxurl表示请求的地址，confirmmsg表示删除前提示信息
function MultipleDelete(name, platid, ajaxurl, confirmmsg) {
    var ids = "";
    jQuery("input[name='" + name + "']:checkbox:checked").each(function () {
        ids += jQuery(this).val() + ",";
    });
    ids = ids.substring(0, ids.length - 1);
    if (ids.length <= 0) {
        alert("请至少选择一条记录");
    } else {
        DeleteByIdandPlatid(ids, platid, ajaxurl, confirmmsg);
    }
}


//根据id删除
function DeleteById(id, urlaction) {
    if (confirm("确定删除吗？")) {
        jQuery.ajax({
            url: urlaction,
            type: "post",
            data: {
                id: id,
                plat: jQuery.query.get("plat")
            },
            success: function (repText) {
                if (repText.IsSuccess) {
                    var trid = "#tr" + id;
                    jQuery(trid).fadeOut(function () {
                        jQuery(trid).remove();
                    });
                }
            },
            complete: function (xhr, ts) {
                xhr = null;
            }
        });
    }
}

//字符串截取
function SubContent(content) {
    if (content.length >= 10) {
        return content.substring(0, 10) + "...";
    }
    else {
        return content;
    }
}

//json时间转换
//将DATE转化成字符串
function formatDate(cellval) {
    try {
//        var date = new Date(parseInt(cellval.replace("/Date(", "").replace(")/", ""), 10));
//        var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
//        var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
//        var hour = date.getHours() < 10 ? "0" + date.getHours() : date.getHours();
//        var minutes = date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes();
//        var seconds = date.getSeconds() < 10 ? "0" + date.getSeconds() : date.getSeconds();
//        return date.getFullYear() + "-" + month + "-" + currentDate + " " + hour + ":" + minutes + ":" + seconds;
          return cellval.replace("T", " ");
    } catch (e) {
        return "";
    }
}
//表单元素序列化，封装成Json字符串,otherParamters是需要封装的其他元素如Img(由于serializeArray方法无法获取Img、Button等元素，故还需手动把Img元素传进来)，
function getJsonStr(otherParamters) {
    var jsonStr = '{';
    x = $("form").serializeArray();
    $.each(x, function (i, field) {
        jsonStr += '"' + field.name + '":' + '"' + field.value + '",';
    });
    jsonStr += otherParamters + '}';
    return jsonStr;
}
var keyStr = '_k_s_';
var keyType = '[object Object]';
//初始化表单(修改)
function getFormById(url) {
    jQuery.getJSON(url, function (jData) {
        if (jData.ExtInfo) {
            jQuery.each(jData.ExtInfo, function (key, value) {
                var s = Object.prototype.toString.call((value));
                if (s == keyType) {
                    jQuery.each(value, function (key1, value1) {
                        jQuery("#" + key + keyStr + key1).val(value1);
                    });
                } else {
                    jQuery("#" + key).val(value);
                }
            });
            $.event.trigger({
                type: "afterFormInit"
            });
        }
    });
}

function IsExist(urlaction, platid, abc) {
    jQuery.ajax({
        url: urlaction,
        type: "post",
        data: {
            plat: platid
        },
        success: function (repText) {
            abc(repText);
        },
        complete: function (xhr, ts) {
            xhr = null;
        }
    });
}

//单独验证手机号码、电话号码格式（支持手机号码，3-4位区号，7-8位直播号码，1－4位分机号）
function VerifyPhoneNum(num) {
    return /(^(((13[0-9]|14[0-9]|15[0-9]|17[0-9]|18[0-9])\d{8})|((9|8|5|6)\d{7})|(\d{7,8})|(\d{4}|\d{3})-?(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)/.test(num);
}

//输入字符统计：0/30
//content：输入内容的文本框的Id，count：统计数量
function inputCountStatistics(content, count) {
    var text = $("#" + content + "").val();
    if (text) {
        var counter = text.length;
        $("#" + count + "").text(counter);
    }
    else {
        $("#" + count + "").text(0);
    }
}

//去掉两端空格
function trim(s) {
    return s.replace(/(^\s*)|(\s*$)/g, '');
}

/********************************************************************************************************************/
/***************************************************旧版本***********************************************************/
/********************************************************************************************************************/
/// <summary>
/// 获取列表
/// </summary>
/// <param name="handerurl">请求的action</param>
/// <param name="footur">尾部URL</param>
/// <returns></returns>
function getList(handerurl, footur, page, pagesize, platid) {
    var _options = {};
    _options.action = "list";
    _options.page = 1;
    _options.handlerUrl = handerurl;
    _options.footUrl = footur;
    _options.id = 0;
    _options.pagesize = pagesize;
    loadTmpl();

    //前一页、后一页,为了防止多次绑定click事件，这里每次都先解绑再绑定
    jQuery("body").undelegate('#tableFoot a:not(a.btn)', 'click').delegate("#tableFoot a:not(a.btn)", "click", function () {
        _options.page = jQuery(this).attr("page");
        loadTmpl(_options);
    });


    function loadTmpl() {
        jQuery.ajax({
            url: _options.handlerUrl,
            type: "get",
            dataType: "json",
            data: { page: _options.page, pagesize: _options.pagesize, plat: platid
            },
            success: function (repJson) {
                if (repJson.ContentEntity && repJson.ContentEntity.Body) {
                    jQuery("#tableBody").empty();
                    jQuery("#replylist").tmpl(repJson.ContentEntity.Body).appendTo("#tableBody");
                    jQuery.get(_options.footUrl, function (repText) {
                        jQuery("#tableFoot").empty();
                        jQuery.template("tmplData", repText);
                        jQuery.tmpl("tmplData", repJson.ContentEntity.Foot).appendTo("#tableFoot");
                    });
                }
                else {
                    jQuery("#tableBody").html("暂无数据");
                }
            },
            complete: function (xhr, ts) {
                xhr = null;
            }
        });
    }
    
}
