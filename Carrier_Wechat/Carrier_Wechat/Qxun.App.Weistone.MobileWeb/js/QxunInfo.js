var keyStr = "_k_s_";
var formClass = {
    config: {
        //实体名与属性名的分隔字符
        seperate: '_k_s_',
        //实体类型标识
        entityType: '[object Object]',
        //表单提交按钮ID
        submitId: 'bsubmit',
        //表单ID
        formId: 'form1'
    },
    opts: {
        //获取初始化表单数据URL
        getFormDataUrl: "",
        //提交表单数据URL
        postFormDataUrl: "",
        //提交成功后跳转URL
        returnUrl: "",
        //表单主键ID
        primaryKeyId: 0,
        //表单验证参数
        validateOpts: {}
    },
    validate: null,
    init: function (opts) {
        formClass.opts = opts;
        validate = $("#" + formClass.config.formId).validate(formClass.opts.validateOpts);
        if (formClass.opts.primaryKeyId > 0) {
            formClass.getFormData();
        }
        $("#" + formClass.config.submitId).click(function (e) {
            $.event.trigger({
                type: "afterFormSubmit"
            });
            if (!formClass.checkForm(validate)) {
                return;
            }
            $("#" + formClass.config.submitId).attr('disabled', 'disabled');
            formClass.formSubmit();
            $("#" + formClass.config.submitId).removeAttr("disabled");
        });
    },
    //序列化表单提交数据
    serializeForm: function () {
        var result = $("form").serializeArray();
        var entity = new Object();
        jQuery.each(result, function (i, field) {
            if (field.name.indexOf(keyStr) >= 0) {
                var names = field.name.split(keyStr);
                if (entity[names[0]] == undefined) {
                    entity[names[0]] = new Object();
                }
                if (names.length <= 2) {
                    entity[names[0]][names[1]] = field.value;
                } else {
                    formClass.serializeFormRecursion(entity[names[0]], field.name.substring(field.name.indexOf(keyStr) + keyStr.length), field.value);
                }
            } else {
                entity[field.name] = field.value;
            }
        });

        var jsonStr = JSON.stringify(entity);
        jsonStr = formClass.extFunction.serializeFormFun(jsonStr);
        return jsonStr;
    },
    serializeFormRecursion: function (entity, name, value) {
        if (name.indexOf(keyStr) >= 0) {
            var names = name.split(keyStr);
            if (entity[names[0]] == undefined) {
                entity[names[0]] = new Object();
            }
            if (names.length <= 2) {
                entity[names[0]][names[1]] = value;
            } else {
                formClass.serializeFormRecursion(entity[names[0]], name.substring(name.indexOf(keyStr) + keyStr.length), value);
            }
        } else {
            entity[name] = value;
        }
    },
    //验证表单
    checkForm: function (validate) {
        var b = validate.checkForm();
        validate.showErrors();
        if (b == true) {
            b = formClass.extFunction.validateFun();
        }
        else {
            b = formClass.extFunction.validateFun();
            b = false;
        }
        return b;
    },
    //初始化表单数据(修改)
    getFormData: function () {
        jQuery.getJSON(formClass.opts.getFormDataUrl, function (jData) {
            if (jData.ExtInfo) {
                jQuery.each(jData.ExtInfo, function (key, value) {
                    var s = Object.prototype.toString.call((value));
                    if (s == keyType) {
                        jQuery.each(value, function (key1, value1) {
                            jQuery("#" + key + keyStr + key1).val(value1);
                            formClass.getFormRecursion((key + keyStr + key1), value1);
                        });
                    } else {
                        jQuery("#" + key).val(value);
                    }
                });
                $.event.trigger({
                    type: "afterFormInit"
                });
                formClass.extFunction.extIniting(jData.ExtInfo);
                $.event.trigger({
                    type: "afterMapInit"
                });
            }
        })
    },
    getFormRecursion: function (skey, data) {
        var sData = Object.prototype.toString.call((data));
        if (sData == keyType) {
            jQuery.each(data, function (key, value) {
                var s = Object.prototype.toString.call((value));
                if (s == keyType) {
                    jQuery.each(value, function (key1, value1) {
                        jQuery("#" + skey + keyStr + key + keyStr + key1).val(value1);
                        formClass.getFormRecursion(value);
                    });
                } else {
                    jQuery("#" + skey + keyStr + key).val(value);
                }
            });
        }
    },
    //Form提交
    formSubmit: function () {
        jQuery.ajax({
            url: formClass.opts.postFormDataUrl,
            data: formClass.serializeForm(),
            async: false,
            type: "post",
            cache: false,
            success: function (repJson) {
                if (repJson) {
                    if (repJson.IsSuccess) {
                        if (formClass.opts.returnUrl === undefined || formClass.opts.returnUrl === "") {
                            window.location.reload();
                        } 
                        else if (repJson.ExtInfo != undefined || repJson.ExtInfo != null) {
                            formClass.extFunction.extSubmitForm(repJson);
                        }
                        else {
                            location.href = formClass.opts.returnUrl;
                        }
                    }
                    else {
                        if (repJson.ErrorMsg != undefined) {
                            alert(repJson.ErrorMsg);
                        } else {
                            alert("未知错误");
                        }
                        return false;
                    }
                }
            },
            complete: function (xhr, ts) {
                var newToken = xhr.getResponseHeader("@Html.Raw(CreateForm.FormTokenName)");
                if (newToken != null) {
                    jQuery("#@Html.Raw(CreateForm.FormTokenName)").val(newToken);
                }
                xhr = null;
            },
            error: function () {
                return false;
            }
        });
    },
    extFunction: {
        //自定义表单验证方法
        validateFun: function () {
            return true;
        },
        //自定义Json方法
        serializeFormFun: function (json) {
            return json;
        },
        extInitForm: function () {

        },
        extSubmitForm: function (data) {
            location.href = formClass.opts.returnUrl;
        },
        //页面控件初始化完成之后需要单独处理某个动作时使用
        extIniting: function (data) {

        }
    }
};
//单独验证手机号码、电话号码格式（支持手机号码，3-4位区号，7-8位直播号码，1－4位分机号）
function VerifyPhoneNum(num) {
    return /(^(((13[0-9]|14[0-9]|15[0-9]|17[0-9]|18[0-9])\d{8})|((9|8|5|6)\d{7})|(\d{7,8})|(\d{4}|\d{3})-?(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)/.test(num);
}

//var Qxun = {
////    alert: function (buttonPrompt) {
////        buttonPrompt = buttonPrompt || '成功';
////        $('<div>').simpledialog2({
////            mode: 'button',
////            headerText: '消息提示',
////            headerClose: false,
////            buttonPrompt: buttonPrompt,
////            buttons: {
////                '确定': {
////                    click: function () {

////                    },
////                    icon: ""
////                }
////            }
////        });
////    },
//    confirm: function (buttonPrompt) {
//        buttonPrompt = buttonPrompt || '成功';
//        $('<div>').simpledialog2({
//            mode: 'button',
//            headerText: '消息提示',
//            headerClose: false,
//            buttonPrompt: buttonPrompt,
//            buttons: {
//                '确定' : {
//                    click: function() {
//                            
//                    }
//                },
//                '取消' : {
//                    click: function() {
//                            
//                    },
//                    icon: "delete",
//                    theme: "c"
//                }
//            }
//        });
//    }

//};

//function alert(buttonPrompt) {
//    buttonPrompt = buttonPrompt || '成功';
//    $('<div>').simpledialog2({
//        mode: 'button',
//        headerText: '消息提示',
//        headerClose: false,
//        buttonPrompt: buttonPrompt,
//        buttons: {
//            '确定': {
//                click: function () {

//                },
//                icon: ""
//            }
//        }
//    });
//}