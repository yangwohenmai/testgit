﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18444
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
    
    #line 2 "..\..\Views\Dealer\EditDealerInfo.cshtml"
    using Qxun.UI.Background.App_Code;
    
    #line default
    #line hidden
    
    #line 1 "..\..\Views\Dealer\EditDealerInfo.cshtml"
    using Qxun.UI.Background.Config;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Dealer/EditDealerInfo.cshtml")]
    public partial class _Views_Dealer_EditDealerInfo_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_Dealer_EditDealerInfo_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 3 "..\..\Views\Dealer\EditDealerInfo.cshtml"
   ViewBag.Title = "编辑经销商信息";
 ViewBag.subTitle = "授权经销商管理";
 Layout = "~/Views/Shared/_Layout.cshtml";
 //如果要上传原尺寸图像，请将本参数赋值为"panorama"
 ViewData["panorama"] = "panorama";
 var platid = Request["plat"];
 var formTitle = "编辑经销商信息";
 string content = "", value = "";

 ViewBag.formlines = new FormLineConfig[] {
       new FormLineConfig(){desc = "BPCode",name = "BPCode",
           type = "input-text", tips = "<span class='maroon'>*</span>请填写BP编号Code",vaildType = "required,maxlength:32",inputContent="",functionName="onkeyup",functionStr="value=value.replace(/[^\\d]/g,'')"},
       //new FormLineConfig(){desc = "ERPCode",name = "ServerSiteCode",
       //     type = "input-text", tips = "<span class='maroon'>*</span> 请填写ERP代码",vaildType = "required,maxlength:32",inputContent="",functionStr=""} ,
       new FormLineConfig(){desc = "站点名称",name = "SiteName",
            type = "input-text", tips = "<span class='maroon'>*</span> 请填写站点名称",vaildType = "required",inputContent="",functionName="onkeyup",functionStr=""} ,
       new FormLineConfig(){desc = "站点负责人",name = "SiteManager",
            type = "input-text", tips = "<span class='maroon'>*</span> 请填写站点名称",vaildType = "required",inputContent="",functionName="onkeyup",functionStr=""} ,
       new FormLineConfig(){desc = "联系电话",name = "Tel",
            type = "input-text", tips = "<span class='maroon'>*</span> 请填写站点名称",vaildType = "required",inputContent="",functionName="onkeyup",functionStr=""} ,
       new FormLineConfig(){desc = "站点地址",name = "SiteAddress",
            type = "input-text", tips = "<span class='maroon'>*</span> 请填写站点名称",vaildType = "required",inputContent="",functionName="onkeyup",functionStr=""} ,
       new FormLineConfig(){desc = "成立日期",name = "EstablishmentTime",
            type = "input-text", tips = "<span class='maroon'>*</span> 请填写站点名称",vaildType = "required",inputContent="",functionName="onkeyup",functionStr=""} ,
       new FormLineConfig(){desc = "公司名称",name = "CompanyName",
            type = "input-text", tips = "<span class='maroon'>*</span> 请填写站点名称",vaildType = "required",inputContent="",functionName="onkeyup",functionStr=""} ,
       new FormLineConfig(){desc = "类型",name = "SiteType",
            type = "input-text", tips = "<span class='maroon'>*</span> 请填写站点名称",vaildType = "required",inputContent="",functionName="onkeyup",functionStr=""} ,
       new FormLineConfig(){desc = "级别",name = "SiteGrade",
            type = "input-text", tips = "<span class='maroon'>*</span> 请填写站点名称",vaildType = "required",inputContent="",functionName="onkeyup",functionStr=""} ,
       new FormLineConfig(){desc = "区域服务经理",name = "LocalManager",
            type = "input-text", tips = "<span class='maroon'>*</span> 请填写站点名称",vaildType = "required",inputContent="",functionName="onkeyup",functionStr=""} ,
       
   };
 ViewBag.formsecondlines = new FormLineConfig[]{
       new FormLineConfig(){desc = "",name = "DealerId",
            type = "input-hidden", tips = "",vaildType = "",inputContent = "",inputValue=""},
       new FormLineConfig(){desc = "",name = "OpenId",
            type = "input-hidden", tips = "",vaildType = "",inputContent = "",inputValue=""},
       new FormLineConfig(){desc = "",name = "WeixinPlatId",
            type = "input-hidden", tips = "",vaildType = "",inputContent = "",inputValue= Request.QueryString["plat"]}
   };

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 46 "..\..\Views\Dealer\EditDealerInfo.cshtml"
Write(Html.Partial("_FormResource"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 47 "..\..\Views\Dealer\EditDealerInfo.cshtml"
Write(Html.Partial("_KindEditorResource"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 48 "..\..\Views\Dealer\EditDealerInfo.cshtml"
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

            
            #line 54 "..\..\Views\Dealer\EditDealerInfo.cshtml"
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

            
            #line 58 "..\..\Views\Dealer\EditDealerInfo.cshtml"
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

            
            #line 66 "..\..\Views\Dealer\EditDealerInfo.cshtml"
                        Write(new CreateForm().CreateFormBody(ViewBag.formlines));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");

WriteLiteral("                            ");

            
            #line 68 "..\..\Views\Dealer\EditDealerInfo.cshtml"
                        Write(new CreateForm().CreateFormBody(ViewBag.formsecondlines, false));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                            ");

            
            #line 69 "..\..\Views\Dealer\EditDealerInfo.cshtml"
                       Write(Html.Partial("CreateFormFoot"));

            
            #line default
            #line hidden
WriteLiteral("\r\n                        </form>\r\n                    </div>\r\n                </" +
"div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n    var id = jQuery.query.get(\"id\");\r\n    var opts = {\r\n        primaryKeyId: " +
"id,\r\n        getFormDataUrl: \'");

            
            #line 81 "..\..\Views\Dealer\EditDealerInfo.cshtml"
                    Write(Url.Action("GetDealerEntityById", "Dealer"));

            
            #line default
            #line hidden
WriteLiteral("?id=\' + id,\r\n        postFormDataUrl: \'");

            
            #line 82 "..\..\Views\Dealer\EditDealerInfo.cshtml"
                     Write(Url.Action("InsertOrUpdateDealerEntity", "Dealer"));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n        returnUrl: \'");

            
            #line 83 "..\..\Views\Dealer\EditDealerInfo.cshtml"
               Write(Url.Action("DealerInfo", "Dealer"));

            
            #line default
            #line hidden
            
            #line 83 "..\..\Views\Dealer\EditDealerInfo.cshtml"
                                                  Write(Html.Raw(@ViewBag.oQueryString));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n        validateOpts: {\r\n            rules: {\r\n");

WriteLiteral("                ");

            
            #line 86 "..\..\Views\Dealer\EditDealerInfo.cshtml"
            Write(new CreateForm().CreateFormValid(ViewBag.formlines));

            
            #line default
            #line hidden
WriteLiteral(@"
            }
        }
    };

    

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
