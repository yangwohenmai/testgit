    //表单处理类
var formClass = {
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
    init: function (opts) {
        formClass.getFormData(opts);
    },
    //初始化表单数据(修改)
    getFormData: function (opts) {
        jQuery.getJSON(opts.getFormDataUrl, function (jData) {
            formClass.extFunction.extIniting(jData);
        })
    },
    extFunction: {
        //页面控件初始化完成之后需要单独处理某个动作时使用
        extIniting: function (data) {

        }
    }
};
