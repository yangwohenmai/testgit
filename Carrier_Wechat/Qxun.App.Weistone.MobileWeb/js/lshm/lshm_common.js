///
///手机端的列表操作
///
var MobileList = {
    opts:{
        getLsitDataUrl: "",//获取数据的地址
        pageIndex: 1,//页数
        pageSize: 10,//每页几条
        listTmplId: "",//jquery tmpl的Id
        listContainerId: "",//list容器Id
        searchBtnId:"",//搜索按钮的Id
        searchValueCss:"",//搜索框的class
        searchJson: ""//搜索的json
    },
    //初始化
    init: function (opts) {
        $.extend(MobileList.opts, opts);//合并对象
        MobileList.loadListTmpl(MobileList.opts);
        //MobileList.scroll(MobileList.opts);
        MobileList.search(MobileList.opts);
    },
    //加载数据
    loadListTmpl: function (opts) {
        $.ajax({
            url: opts.getLsitDataUrl,
            type: "post",
            data: {
                page: opts.pageIndex,
                pageSize: opts.pageSize,
                searchJson: opts.searchJson
            },
            beforeSend: function () {
                MobileList.loading(true);
            },
            success: function (listData) {
                if (listData.ContentEntity && listData.ContentEntity.Body && listData.ContentEntity.Body.length > 0) {
                    //$("#" + opts.listContainerId + "").empty();
                    $("#" + opts.listContainerId + "").append($("#" + opts.listTmplId + "").tmpl(listData.ContentEntity.Body));
                    if (opts.pageIndex==1) {
                        $('body,html').scrollTop(0);
                    }
                    if (opts.pageIndex == listData.ContentEntity.Foot.Last) {
                        opts.pageIndex = -1;
                    } else {
                        opts.pageIndex = listData.ContentEntity.Foot.Next;
                    }
                }
                else {
                    //MobileList.toast("没有数据了", 4000);
                }
                MobileList.loading(false);
            },
            complete: function (xhr, ts) {
                xhr = null;
            }
        });
    },
    //下拉加载
    scroll: function (opts) {
        $(window).scroll(function() {
            var scrollTop = $(document).scrollTop();
            var documentHeight = $(document).height();
            var windowHeight = $(window).height();
            if (scrollTop >= documentHeight - windowHeight) {
                if (opts.pageIndex != -1) {
                    MobileList.loadListTmpl(opts);
                } else {
                    //MobileList.toast("没有数据了",4000);
                }
            }
        });
    },
    //搜索
    search: function (opts) {
        if ($.trim(opts.searchBtnId) != "") {
            $("#" + opts.searchBtnId).click(function () {
                //获取搜索框的值拼接成json
                if ($.trim(opts.searchValueCss) != "") {
                    var json = {}
                    jQuery("." + opts.searchValueCss + "").each(function () {
                        json[jQuery(this).attr("name")] = jQuery(this).val();
                    });
                    opts.searchJson = MobileList.extendListFunction.extendSearchJosn(json);
                }
                //清空数据并加载数据
                $("#" + opts.listContainerId).empty();
                opts.pageIndex = 1;
                MobileList.loadListTmpl(opts);
            })
        }
    },
    //提示信息
    toast: function (text, delay) {
        var toast = '<div class="toast" style="touch-action: pan-y; -webkit-user-drag: none; -webkit-tap-highlight-color: rgba(0, 0, 0, 0); top: 0px; opacity: 1;">'+text+'</div>';
        $("#toast-container").append(toast);
        setTimeout(function () {
            $("#toast-container").empty();
        },delay)
    },
    //loading的控制（参数：true，false）
    loading: function (onloading) {
        if (onloading) {
            $("#loading").addClass("loading");
            //$("html,body").css("overflow", "hidden");
        } else {
            $("#loading").removeClass("loading");
            //$("html,body").css("overflow", "initial");
        }
    },
    //扩展方法
    extendListFunction:{
        //搜索之前数据处理
        extendSearchJosn:function(searchJson){
            return JSON.stringify(searchJson);
        }
    }
}
///
///手机端的表单操作
///
var MobileForm = {
    opts: {
        submitDataUrl: "",//提交地址
        formElementIds: [],//需要提交的元素Id数组
        submitBtnId: "mobileSubmitBtn",//提交的按钮Id
        formId: "form1",//表单的Id
        //jquery validate的验证规则
        validate: {
            rules: {},
            messages: {}
        },
        checkForm:false,//表单验证结果
        submitSuccessReturnUrl:"",//表单提交成功后跳转的Url

    },
    //初始化
    init: function (opts) {
        $.extend(MobileForm.opts, opts);//合并对象
        MobileForm.validateForm(MobileForm.opts);
        MobileForm.submitForm(MobileForm.opts);
    },
    //提交表单
    submitForm: function (opts) {
        $("#" + opts.submitBtnId).click(function (e) {
            //判断表单验证是否通过
            if (!MobileForm.checkForm(opts)) {
                return false;
            }
            //ajax提交数据
            $.ajax({
                url: opts.submitDataUrl,
                type: "post",
                data: {
                    formDataJson: MobileForm.serializeFormData(opts)
                },
                beforeSend: function () {
                    MobileList.loading(true);
                },
                success: function (formData) {
                    if (formData.IsSuccess) {
                        //MobileList.toast("数据提交成功", 4000);
                        //如果没有配置成功后跳转的url则需要自定义操作
                        if(opts.submitSuccessReturnUrl!=""){
                            location.href=opts.submitSuccessReturnUrl;
                        }
                        MobileForm.extendFormFunction.extendSubmitSuccess(formData);
                    }
                    else {
                        //MobileList.toast(formData.ErrorMsg, 4000);
                        return false;
                    }
                    MobileList.loading(false);
                },
                complete: function (xhr, ts) {
                    MobileList.loading(false);
                    xhr = null;
                },
                error: function () {
                    MobileList.loading(false);
                    return false;
                }
            });
        });
    },
    //序列化表单数据
    serializeFormData: function (opts) {
        var dataJson = {
        };
        for (var i = 0; i < opts.formElementIds.length; i++) {
            var eleType = document.getElementById(opts.formElementIds[i]).tagName;
            switch (eleType) {
                case "INPUT":
                case "SELECT":
                case "TEXTAREA":
                    dataJson[opts.formElementIds[i]] = $(document.getElementById(opts.formElementIds[i])).val();
                    break;
                default:
                     
                    break;
            }
        }
        return MobileForm.extendFormFunction.extendSerializeFormData(JSON.stringify(dataJson));
    },
    //初始化表单验证
    validateForm: function (opts) {
        return opts.checkForm = $("#" + opts.formId).validate(opts.validate);
    },
    //验证表单是否通过
    checkForm: function (opts) {
        var validateStatus = opts.checkForm.checkForm();
        opts.checkForm.showErrors();
        if (validateStatus) {
            validateStatus = MobileForm.extendFormFunction.extendValidateForm();
        } else {
            validateStatus = MobileForm.extendFormFunction.extendValidateForm();
            validateStatus = false;
        }
        return validateStatus;
    },
    //表单扩展方法
    extendFormFunction: {
        //表单数据提交之前做操作
        extendSerializeFormData: function (json) {
            return json;
        },
        //表单额外的验证条件
        extendValidateForm: function () {
            return true;
        },
        //表单提交成功后执行的操作
        extendSubmitSuccess: function (data) {
            return true;
        }
    }
}

//格式化时间字符串（2015-11-20T12:02:45.117----2015-11-20 12:02）
function formatDateString(dateStr) {
    var trimDateStr = $.trim(dateStr);
    var regex = /^(\d{4})-(\d{2})-(\d{2})T(\d{2}):(\d{2}):(\d{2})\.(\d{2,3})$/;
    if (regex.test(trimDateStr)) {
        var arry = trimDateStr.replace("T", " ").split(":");
        arry.pop();
        return arry.join(":");
    } else {
        return "";
    }
}
//获取当前时间(年-月-日)
function getCurrentDate() {
    var myDate = new Date();
    var year = myDate.getFullYear();
    var month = (myDate.getMonth() + 1) < 10 ? "0" + (myDate.getMonth() + 1) : (myDate.getMonth() + 1);
    var day = myDate.getDate() < 10 ? "0" + myDate.getDate() : myDate.getDate();
    var currentDate = year + "-" + month + "-" + day;
    return currentDate;
}