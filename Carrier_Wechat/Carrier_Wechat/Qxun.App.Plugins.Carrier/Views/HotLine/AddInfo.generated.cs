﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18408
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ASP
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    
    #line 2 "..\..\Views\HotLine\AddInfo.cshtml"
    using Qxun.UI.Background.App_Code;
    
    #line default
    #line hidden
    
    #line 1 "..\..\Views\HotLine\AddInfo.cshtml"
    using Qxun.UI.Background.Config;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/HotLine/AddInfo.cshtml")]
    public partial class _Views_HotLine_AddInfo_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_HotLine_AddInfo_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 3 "..\..\Views\HotLine\AddInfo.cshtml"
   ViewBag.Title = "新增呼叫单";
 ViewBag.subTitle = "新增呼叫单";
 Layout = "~/Views/Shared/_Layout.cshtml";
 //如果要上传原尺寸图像，请将本参数赋值为"panorama"
 ViewData["panorama"] = "panorama";
 var platid = Request["plat"];
 var formTitle = "新增呼叫单";
 var type = Request["type"];
 string content = "", value = "";

 ViewBag.formlines = new FormLineConfig[] {
       new FormLineConfig(){desc = "呼叫单编号",name = "TicketNumber",
           type = "input-text", tips = "<span class='maroon'>*</span>请填写呼叫单编号",vaildType = "required,maxlength:32",inputContent="",functionName="onkeyup",functionStr="value=value.replace(/[^\\d]/g,'')"},
       new FormLineConfig(){desc = "客户姓名",name = "CustomerName",
            type = "input-text", tips = "<span class='maroon'>*</span> 请填写客户名称",vaildType = "required,maxlength:32",inputContent="",functionStr=""} ,
       new FormLineConfig(){desc = "来电号码",name = "CallNumber",
            type = "input-text", tips = "<span class='maroon'>*</span> 请填写来电号码。",vaildType = "required",inputContent="",functionName="onkeyup",functionStr=""} ,
       new FormLineConfig(){desc = "来电日期",name = "CallDate",
            type = "input-text", tips = "<span class='maroon'>*</span> 请填写来电日期。",vaildType = "required",inputContent="",functionName="onkeyup",functionStr=""} ,
       new FormLineConfig(){desc = "来电时间",name = "CallTime",
            type = "input-text", tips = "<span class='maroon'>*</span> 请填写来电时间。",vaildType = "required",inputContent="",functionName="onkeyup",functionStr=""} ,
       new FormLineConfig(){desc = "呼叫单来源",name = "TicketFrom",
            type = "input-text", tips = "<span class='maroon'>*</span> 请填写呼叫单来源。",vaildType = "required",inputContent="",functionName="onkeyup",functionStr=""} ,
       new FormLineConfig(){desc = "来电目的",name = "CallFor",
            type = "input-text", tips = "<span class='maroon'>*</span> 请填写来电目的。",vaildType = "required",inputContent="",functionName="onkeyup",functionStr=""} ,
       new FormLineConfig(){desc = "支持工程师",name = "SupportEngineer",
            type = "input-text", tips = "<span class='maroon'>*</span> 请填写支持工程师。",vaildType = "required",inputContent="",functionName="onkeyup",functionStr=""} ,
       new FormLineConfig(){desc = "状态",name = "Status",
            type = "input-text", tips = "<span class='maroon'>*</span> 选择状态。",vaildType = "required",inputContent="",functionName="onkeyup",functionStr=""} ,
   };
 ViewBag.formlines1 = new FormLineConfig[]{
     new FormLineConfig(){desc = "问题描述",name = "Description",
            type = "input-text", tips = "<span class='maroon'>*</span> 请填写问题描述。",vaildType = "required",inputContent="",functionName="onkeyup",functionStr=""} ,
     new FormLineConfig(){desc = "地区",name = "Location",
            type = "input-text", tips = "<span class='maroon'>*</span> 请填写地区。",vaildType = "required",inputContent="",functionName="onkeyup",functionStr=""} ,
     new FormLineConfig(){desc = "车牌号",name = "PlateNumber",
            type = "input-text", tips = "<span class='maroon'>*</span> 请填写车牌号。",vaildType = "required",inputContent="",functionName="onkeyup",functionStr=""} ,
     new FormLineConfig(){desc = "机组序列号",name = "UnitNumber",
            type = "input-text", tips = "<span class='maroon'>*</span> 请填写机组序列号。",vaildType = "required",inputContent="",functionName="onkeyup",functionStr=""} ,
     new FormLineConfig(){desc = "反馈日期",name = "FeedBackDate",
            type = "input-text", tips = "<span class='maroon'>*</span> 请填写反馈日期。",vaildType = "required",inputContent="",functionName="onkeyup",functionStr=""} ,
     new FormLineConfig(){desc = "反馈时间",name = "FeedBackTime",
            type = "input-text", tips = "<span class='maroon'>*</span> 请填写反馈时间。",vaildType = "required",inputContent="",functionName="onkeyup",functionStr=""} ,
     new FormLineConfig(){desc = "车子归属地",name = "CarOwnership",
            type = "input-text", tips = "<span class='maroon'>*</span> 请填写车子归属地。",vaildType = "required",inputContent="",functionName="onkeyup",functionStr=""} ,
     new FormLineConfig(){desc = "来电客户公司",name = "CustomerCompany",
            type = "input-text", tips = "<span class='maroon'>*</span> 请填写来电客户公司。",vaildType = "required",inputContent="",functionName="onkeyup",functionStr=""} ,
     new FormLineConfig(){desc = "报修机组类型",name = "UnitType",
            type = "input-text", tips = "<span class='maroon'>*</span> 请填写报修机组类型。",vaildType = "required",inputContent="",functionName="onkeyup",functionStr=""} ,
     new FormLineConfig(){desc = "维修服务站",name = "ServiceStation",
            type = "input-text", tips = "<span class='maroon'>*</span> 请填写维修服务站。",vaildType = "required",inputContent="",functionName="onkeyup",functionStr=""} ,
     new FormLineConfig(){desc = "报修事宜",name = "RepairInfo",
            type = "input-text", tips = "<span class='maroon'>*</span> 请填写报修事宜。",vaildType = "required",inputContent="",functionName="onkeyup",functionStr=""} 
 };
 ViewBag.formlines2 = new FormLineConfig[]{
     new FormLineConfig(){desc = "受理服务站",name = "AcceptStation",
            type = "input-text", tips = "<span class='maroon'>*</span> 请填写。",vaildType = "required",inputContent="",functionName="onkeyup",functionStr=""} ,
     new FormLineConfig(){desc = "受理情况",name = "AcceptOrNot",
            type = "input-text", tips = "<span class='maroon'>*</span> 请填写地区。",vaildType = "required",inputContent="",functionName="onkeyup",functionStr=""} ,
     new FormLineConfig(){desc = "是否在4小时内联系用户",name = "ContactIn4OrNot",
            type = "input-text", tips = "<span class='maroon'>*</span> 请填写车牌号。",vaildType = "required",inputContent="",functionName="onkeyup",functionStr=""} ,
     new FormLineConfig(){desc = "回访时间",name = "ReturnTime",
            type = "input-text", tips = "<span class='maroon'>*</span> 请填写机组序列号。",vaildType = "required",inputContent="",functionName="onkeyup",functionStr=""} ,
     new FormLineConfig(){desc = "是否解决故障",name = "SolveOrNot",
            type = "input-text", tips = "<span class='maroon'>*</span> 请填写反馈日期。",vaildType = "required",inputContent="",functionName="onkeyup",functionStr=""} ,
     new FormLineConfig(){desc = "是否解答疑问",name = "AnswerOrNot",
            type = "input-text", tips = "<span class='maroon'>*</span> 请填写反馈时间。",vaildType = "required",inputContent="",functionName="onkeyup",functionStr=""} ,
     new FormLineConfig(){desc = "是否提供信息（销售）",name = "GiveInfoOrNotforSales",
            type = "input-text", tips = "<span class='maroon'>*</span> 请填写车子归属地。",vaildType = "required",inputContent="",functionName="onkeyup",functionStr=""} ,
     new FormLineConfig(){desc = "是否解答问题（销售）",name = "AnswerOrNotforSales",
            type = "input-text", tips = "<span class='maroon'>*</span> 请填写报修事宜。",vaildType = "required",inputContent="",functionName="onkeyup",functionStr=""} ,
     new FormLineConfig(){desc = "建议",name = "Suggest",
            type = "input-text", tips = "<span class='maroon'>*</span> 请填写来电客户公司。",vaildType = "required",inputContent="",functionName="onkeyup",functionStr=""} ,
     new FormLineConfig(){desc = "评分",name = "Score",
            type = "input-text", tips = "<span class='maroon'>*</span> 请填写报修机组类型。",vaildType = "required",inputContent="",functionName="onkeyup",functionStr=""} ,
     new FormLineConfig(){desc = "处理结果",name = "Result",
            type = "input-text", tips = "<span class='maroon'>*</span> 请填写维修服务站。",vaildType = "required",inputContent="",functionName="onkeyup",functionStr=""} 
 };
 ViewBag.formsecondlines = new FormLineConfig[]{
       new FormLineConfig(){desc = "",name = "TicketId",
            type = "input-hidden", tips = "",vaildType = "",inputContent = "",inputValue=""},
       //new FormLineConfig(){desc = "",name = "Description",
       //     type = "input-hidden", tips = "",vaildType = "",inputContent = "",inputValue=""},
       //new FormLineConfig(){desc = "",name = "Location",
       //     type = "input-hidden", tips = "",vaildType = "",inputContent = "",inputValue=""},
       //new FormLineConfig(){desc = "",name = "PlateNumber",
       //     type = "input-hidden", tips = "",vaildType = "",inputContent = "",inputValue=""},
       //new FormLineConfig(){desc = "",name = "UnitNumber",
       //     type = "input-hidden", tips = "",vaildType = "",inputContent = "",inputValue=""},
       //new FormLineConfig(){desc = "",name = "FeedBackDate",
       //     type = "input-hidden", tips = "",vaildType = "",inputContent = "",inputValue=null},
       //new FormLineConfig(){desc = "",name = "FeedBackTime",
       //     type = "input-hidden", tips = "",vaildType = "",inputContent = "",inputValue=null},
       //new FormLineConfig(){desc = "",name = "CarOwnership",
       //     type = "input-hidden", tips = "",vaildType = "",inputContent = "",inputValue=""},
       //new FormLineConfig(){desc = "",name = "CustomerCompany",
       //     type = "input-hidden", tips = "",vaildType = "",inputContent = "",inputValue=""},
       //new FormLineConfig(){desc = "",name = "UnitType",
       //     type = "input-hidden", tips = "",vaildType = "",inputContent = "",inputValue=""},
       //new FormLineConfig(){desc = "",name = "ServiceStation",
       //     type = "input-hidden", tips = "",vaildType = "",inputContent = "",inputValue=""},
       //new FormLineConfig(){desc = "",name = "RepairInfo",
       //     type = "input-hidden", tips = "",vaildType = "",inputContent = "",inputValue=""},
       //new FormLineConfig(){desc = "",name = "AcceptStation",
       //     type = "input-hidden", tips = "",vaildType = "",inputContent = "",inputValue=""},
       //new FormLineConfig(){desc = "",name = "AcceptOrNot",
       //     type = "input-hidden", tips = "",vaildType = "",inputContent = "",inputValue=""},
       //new FormLineConfig(){desc = "",name = "ContactIn4OrNot",
       //     type = "input-hidden", tips = "",vaildType = "",inputContent = "",inputValue=""},
       //new FormLineConfig(){desc = "",name = "ReturnTime",
       //     type = "input-hidden", tips = "",vaildType = "",inputContent = "",inputValue=null},
       //new FormLineConfig(){desc = "",name = "SolveOrNot",
       //     type = "input-hidden", tips = "",vaildType = "",inputContent = "",inputValue=""},
       //new FormLineConfig(){desc = "",name = "AnswerOrNot",
       //     type = "input-hidden", tips = "",vaildType = "",inputContent = "",inputValue=""},
       //new FormLineConfig(){desc = "",name = "GiveInfoOrNotforSales",
       //     type = "input-hidden", tips = "",vaildType = "",inputContent = "",inputValue=""},
       //new FormLineConfig(){desc = "",name = "Suggest",
       //     type = "input-hidden", tips = "",vaildType = "",inputContent = "",inputValue=""},
       //new FormLineConfig(){desc = "",name = "Score",
       //     type = "input-hidden", tips = "",vaildType = "",inputContent = "",inputValue=""},
       //new FormLineConfig(){desc = "",name = "Result",
       //     type = "input-hidden", tips = "",vaildType = "",inputContent = "",inputValue=""},
       //new FormLineConfig(){desc = "",name = "AnswerOrNotforSales",
       //     type = "input-hidden", tips = "",vaildType = "",inputContent = "",inputValue=""},
       new FormLineConfig(){desc = "",name = "WeixinPlatId",
            type = "input-hidden", tips = "",vaildType = "",inputContent = "",inputValue= Request.QueryString["plat"]}
   };

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 132 "..\..\Views\HotLine\AddInfo.cshtml"
Write(Html.Partial("_FormResource"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 133 "..\..\Views\HotLine\AddInfo.cshtml"
Write(Html.Partial("_KindEditorResource"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 134 "..\..\Views\HotLine\AddInfo.cshtml"
Write(Html.Partial("_DateTimePickerResource", new ViewDataDictionary() { { "className", "form_datetime" } }));

            
            #line default
            #line hidden
WriteLiteral("\r\n<div");

WriteLiteral(" id=\"main\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"container-fluid\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"row-fluid\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"span12\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"box\"");

WriteLiteral(">\r\n");

WriteLiteral("                    ");

            
            #line 140 "..\..\Views\HotLine\AddInfo.cshtml"
               Write(Html.Partial("GoBackButton"));

            
            #line default
            #line hidden
WriteLiteral("\r\n                    <div");

WriteLiteral(" class=\"box-title\"");

WriteLiteral(">\r\n                        <div");

WriteLiteral(" class=\"span10\"");

WriteLiteral(">\r\n                            <h3>\r\n                                <i");

WriteLiteral(" class=\"icon-edit\"");

WriteLiteral("></i>");

            
            #line 144 "..\..\Views\HotLine\AddInfo.cshtml"
                                                    Write(formTitle);

            
            #line default
            #line hidden
WriteLiteral("\r\n                            </h3>\r\n                        </div>\r\n            " +
"            <div");

WriteLiteral(" class=\"span2\"");

WriteLiteral(">\r\n                        </div>\r\n                    </div>\r\n                  " +
"  <div");

WriteLiteral(" class=\"box-content\"");

WriteLiteral(">\r\n                        <form");

WriteLiteral(" action=\"\"");

WriteLiteral(" method=\"post\"");

WriteLiteral(" class=\"form-horizontal form-validate\"");

WriteLiteral(" id=\"form1\"");

WriteLiteral(" name=\"form1\"");

WriteLiteral(">\r\n");

WriteLiteral("                            ");

            
            #line 152 "..\..\Views\HotLine\AddInfo.cshtml"
                        Write(new CreateForm().CreateFormBody(ViewBag.formlines));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 153 "..\..\Views\HotLine\AddInfo.cshtml"
                            
            
            #line default
            #line hidden
            
            #line 153 "..\..\Views\HotLine\AddInfo.cshtml"
                             if (type == "change")
                            {

            
            #line default
            #line hidden
WriteLiteral("                                <div");

WriteLiteral(" style=\"color:black;text-align:center;font-size:36px;width:40%;height:40px\"");

WriteLiteral(">\r\n                                    热线回访\r\n                                </di" +
"v>\r\n");

WriteLiteral("                                <div");

WriteLiteral(" style=\"width:40%;height:2px;margin-bottom:20px;margin-top:10px; background-color" +
":#D5D5D5;overflow:hidden;\"");

WriteLiteral("></div>\r\n");

            
            #line 159 "..\..\Views\HotLine\AddInfo.cshtml"
                                
            
            #line default
            #line hidden
            
            #line 159 "..\..\Views\HotLine\AddInfo.cshtml"
                            Write(new CreateForm().CreateFormBody(ViewBag.formlines1, false));

            
            #line default
            #line hidden
            
            #line 159 "..\..\Views\HotLine\AddInfo.cshtml"
                                                                                             
                                //@(new CreateForm().CreateFormBody(ViewBag.formlines1, false))
                            }

            
            #line default
            #line hidden
WriteLiteral("                            ");

            
            #line 162 "..\..\Views\HotLine\AddInfo.cshtml"
                             if (type == "repair")
                            {
                                
            
            #line default
            #line hidden
            
            #line 164 "..\..\Views\HotLine\AddInfo.cshtml"
                            Write(new CreateForm().CreateFormBody(ViewBag.formlines1, false));

            
            #line default
            #line hidden
            
            #line 164 "..\..\Views\HotLine\AddInfo.cshtml"
                                                                                             

            
            #line default
            #line hidden
WriteLiteral("                                <div");

WriteLiteral(" style=\"color:black;text-align:center;font-size:36px;width:40%;height:40px\"");

WriteLiteral(">\r\n                                    热线回访\r\n                                </di" +
"v>\r\n");

WriteLiteral("                                <div");

WriteLiteral(" style=\"width:40%;height:2px;margin-bottom:20px;margin-top:10px; background-color" +
":#D5D5D5;overflow:hidden;\"");

WriteLiteral(">\r\n                                </div>\r\n");

            
            #line 170 "..\..\Views\HotLine\AddInfo.cshtml"
                                
            
            #line default
            #line hidden
            
            #line 170 "..\..\Views\HotLine\AddInfo.cshtml"
                            Write(new CreateForm().CreateFormBody(ViewBag.formlines2, false));

            
            #line default
            #line hidden
            
            #line 170 "..\..\Views\HotLine\AddInfo.cshtml"
                                                                                             
                            }

            
            #line default
            #line hidden
WriteLiteral("                            ");

            
            #line 172 "..\..\Views\HotLine\AddInfo.cshtml"
                        Write(new CreateForm().CreateFormBody(ViewBag.formsecondlines, false));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                            ");

            
            #line 173 "..\..\Views\HotLine\AddInfo.cshtml"
                       Write(Html.Partial("CreateFormFoot"));

            
            #line default
            #line hidden
WriteLiteral("\r\n                        </form>\r\n                    </div>\r\n                </" +
"div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n    var id = jQuery.query.get(\"id\");\r\n    var opts = {\r\n        primaryKeyId: " +
"id,\r\n        getFormDataUrl: \'");

            
            #line 185 "..\..\Views\HotLine\AddInfo.cshtml"
                    Write(Url.Action("GetPartInformationEntityById", "HotLine"));

            
            #line default
            #line hidden
WriteLiteral("?id=\' + id,\r\n        ");

WriteLiteral("\r\n        postFormDataUrl: \'");

            
            #line 187 "..\..\Views\HotLine\AddInfo.cshtml"
                     Write(Url.Action("InsertOrUpdateHotLineEntity", "HotLine"));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n        returnUrl: \'");

            
            #line 188 "..\..\Views\HotLine\AddInfo.cshtml"
               Write(Url.Action("HotLineInfo", "HotLine"));

            
            #line default
            #line hidden
            
            #line 188 "..\..\Views\HotLine\AddInfo.cshtml"
                                                    Write(Html.Raw(@ViewBag.oQueryString));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n        validateOpts: {\r\n            rules: {\r\n");

WriteLiteral("                ");

            
            #line 191 "..\..\Views\HotLine\AddInfo.cshtml"
            Write(new CreateForm().CreateFormValid(ViewBag.formlines));

            
            #line default
            #line hidden
WriteLiteral(@"
            }
        }
    };

    //formClass.extFunction.validateFun = function () {
    //    if (imgEntity.ResourceUrl === undefined || imgEntity.ResourceUrl === """") {
    //        imgEntity.showError(false, ""请上传零件图片"");
    //        return false;
    //    }
    //    else {
    //        imgEntity.showError(true);
    //        return true;
    //    }
    //};

    formClass.extFunction.serializeFormFun = function (json) {
        var jsondata = { ""json"": json, ""plat"": jQuery.query.get(""plat"") };
        return jsondata;
    };
    $(function () {
        formClass.init(opts);
    });
</script>
");

        }
    }
}
#pragma warning restore 1591
