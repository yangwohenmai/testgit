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
    
    #line 1 "..\..\Views\MessageBoard\MessageBoardInfo.cshtml"
    using Qxun.App.Plugins.CarrierWechat.ViewModels;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/MessageBoard/MessageBoardInfo.cshtml")]
    public partial class _Views_MessageBoard_MessageBoardInfo_cshtml : System.Web.Mvc.WebViewPage<ViewMessageBoard>
    {
        public _Views_MessageBoard_MessageBoardInfo_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 3 "..\..\Views\MessageBoard\MessageBoardInfo.cshtml"
  
    ViewBag.subTitle = "留言详情";
    Layout = "~/Views/Shared/_Layout.cshtml";
    var formTitle = "留言详情";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 8 "..\..\Views\MessageBoard\MessageBoardInfo.cshtml"
Write(Html.Partial("_FormResource"));

            
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

            
            #line 14 "..\..\Views\MessageBoard\MessageBoardInfo.cshtml"
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

            
            #line 18 "..\..\Views\MessageBoard\MessageBoardInfo.cshtml"
                                                    Write(formTitle);

            
            #line default
            #line hidden
WriteLiteral("\r\n                            </h3>\r\n                        </div>\r\n            " +
"            <div");

WriteLiteral(" class=\"span2\"");

WriteLiteral(">\r\n                        </div>\r\n                    </div>\r\n                  " +
"  <div");

WriteLiteral(" class=\"box-content\"");

WriteLiteral(" id=\"\"");

WriteLiteral(">\r\n                        <form");

WriteLiteral(" action=\"\"");

WriteLiteral(" method=\"post\"");

WriteLiteral(" class=\"form-horizontal form-validate\"");

WriteLiteral(" id=\"form1\"");

WriteLiteral(" name=\"form1\"");

WriteLiteral(">\r\n                            <div");

WriteLiteral(" class=\"control-group\"");

WriteLiteral(">\r\n                                <label");

WriteLiteral(" class=\"control-label\"");

WriteLiteral(">\r\n                                    客户姓名：\r\n                                </l" +
"abel>\r\n                                <div");

WriteLiteral(" class=\"controls\"");

WriteLiteral(">\r\n                                    <label>");

            
            #line 31 "..\..\Views\MessageBoard\MessageBoardInfo.cshtml"
                                      Write(Model.CustomerName);

            
            #line default
            #line hidden
WriteLiteral("</label>\r\n                                </div>\r\n                            </d" +
"iv>\r\n                            <div");

WriteLiteral(" class=\"control-group\"");

WriteLiteral(">\r\n                                <label");

WriteLiteral(" class=\"control-label\"");

WriteLiteral(">\r\n                                    手机号：\r\n                                </la" +
"bel>\r\n                                <div");

WriteLiteral(" class=\"controls\"");

WriteLiteral(">\r\n                                    <label>");

            
            #line 39 "..\..\Views\MessageBoard\MessageBoardInfo.cshtml"
                                      Write(Model.TelPhone);

            
            #line default
            #line hidden
WriteLiteral("</label>\r\n                                </div>\r\n                            </d" +
"iv>\r\n                            <div");

WriteLiteral(" class=\"control-group\"");

WriteLiteral(">\r\n                                <label");

WriteLiteral(" class=\"control-label\"");

WriteLiteral(">\r\n                                    留言标题：\r\n                                </l" +
"abel>\r\n                                <div");

WriteLiteral(" class=\"controls\"");

WriteLiteral(">\r\n                                    <label>");

            
            #line 47 "..\..\Views\MessageBoard\MessageBoardInfo.cshtml"
                                      Write(Model.MessageTitle);

            
            #line default
            #line hidden
WriteLiteral("</label>\r\n                                </div>\r\n                            </d" +
"iv>\r\n                            <div");

WriteLiteral(" class=\"control-group\"");

WriteLiteral(">\r\n                                <label");

WriteLiteral(" class=\"control-label\"");

WriteLiteral(">\r\n                                    留言内容：\r\n                                </l" +
"abel>\r\n                                <div");

WriteLiteral(" class=\"controls\"");

WriteLiteral(">\r\n                                    <label>");

            
            #line 55 "..\..\Views\MessageBoard\MessageBoardInfo.cshtml"
                                      Write(Model.MessageContent);

            
            #line default
            #line hidden
WriteLiteral("</label>\r\n                                </div>\r\n                            </d" +
"iv>\r\n\r\n                            <div");

WriteLiteral(" class=\"control-group\"");

WriteLiteral(">\r\n                                <label");

WriteLiteral(" class=\"control-label\"");

WriteLiteral(">\r\n                                    留言时间：\r\n                                </l" +
"abel>\r\n                                <div");

WriteLiteral(" class=\"controls\"");

WriteLiteral(">\r\n                                    <label>");

            
            #line 64 "..\..\Views\MessageBoard\MessageBoardInfo.cshtml"
                                      Write(Model.MessageDate);

            
            #line default
            #line hidden
WriteLiteral("</label>\r\n                                </div>\r\n                            </d" +
"iv>\r\n                            <div");

WriteLiteral(" class=\"control-group\"");

WriteLiteral(">\r\n                                <label");

WriteLiteral(" class=\"control-label\"");

WriteLiteral(">\r\n                                    回复内容：\r\n                                </l" +
"abel>\r\n                                <div");

WriteLiteral(" class=\"controls\"");

WriteLiteral(">\r\n                                    <textarea");

WriteLiteral(" id=\"ReplyContent\"");

WriteLiteral(">");

            
            #line 72 "..\..\Views\MessageBoard\MessageBoardInfo.cshtml"
                                                           Write(Model.ReplyContent);

            
            #line default
            #line hidden
WriteLiteral("</textarea>\r\n                                </div>\r\n                            " +
"</div>\r\n                            <div");

WriteLiteral(" class=\"control-group\"");

WriteLiteral(" style=\"padding-left: 158px;\"");

WriteLiteral(">\r\n                                <a");

WriteLiteral(" class=\"btn btn-primary\"");

WriteLiteral(" id=\"replyBtn\"");

WriteLiteral(" href=\"javascript:void(0)\"");

WriteLiteral(@">回复</a>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<script>
    $(function () {
        $(""#replyBtn"").click(function () {
            var replyContent = $(""#ReplyContent"").val();
            var id = jQuery.query.get(""id"");
            $.post(""");

            
            #line 90 "..\..\Views\MessageBoard\MessageBoardInfo.cshtml"
               Write(Url.Action("UpdateMessageBoardEntity", "MessageBoard"));

            
            #line default
            #line hidden
            
            #line 90 "..\..\Views\MessageBoard\MessageBoardInfo.cshtml"
                                                                      Write(Html.Raw(@ViewBag.oQueryString));

            
            #line default
            #line hidden
WriteLiteral("\", { id: id, replyContent: replyContent }, function (data) {\r\n                if " +
"(data.IsSuccess) {\r\n                    location.href = \"");

            
            #line 92 "..\..\Views\MessageBoard\MessageBoardInfo.cshtml"
                                Write(Url.Action("MessageBoardList", "MessageBoard"));

            
            #line default
            #line hidden
            
            #line 92 "..\..\Views\MessageBoard\MessageBoardInfo.cshtml"
                                                                               Write(Html.Raw(@ViewBag.oQueryString));

            
            #line default
            #line hidden
WriteLiteral("\";\r\n                } else {\r\n                    alert(data.ErrorMsg)\r\n         " +
"       }\r\n            })\r\n        })\r\n    })\r\n</script>\r\n");

        }
    }
}
#pragma warning restore 1591
