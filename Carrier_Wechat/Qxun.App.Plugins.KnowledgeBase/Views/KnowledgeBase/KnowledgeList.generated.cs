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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/KnowledgeBase/KnowledgeList.cshtml")]
    public partial class _Views_KnowledgeBase_KnowledgeList_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_KnowledgeBase_KnowledgeList_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\KnowledgeBase\KnowledgeList.cshtml"
  
    Layout = "~/Views/Shared/_Layout.cshtml";
    var ExistRoot = ViewBag.ExistRoot;

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 5 "..\..\Views\KnowledgeBase\KnowledgeList.cshtml"
Write(Html.Partial("_KindEditorResource"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 6 "..\..\Views\KnowledgeBase\KnowledgeList.cshtml"
Write(Html.Partial("_FormResource"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 7 "..\..\Views\KnowledgeBase\KnowledgeList.cshtml"
Write(Html.Partial("_ListResource"));

            
            #line default
            #line hidden
WriteLiteral("\r\n<link");

WriteAttribute("href", Tuple.Create(" href=\"", 201), Tuple.Create("\"", 288)
            
            #line 8 "..\..\Views\KnowledgeBase\KnowledgeList.cshtml"
, Tuple.Create(Tuple.Create("", 208), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Content/css/KnowledgeBase/KnowledgeBase.css?v=")
            
            #line default
            #line hidden
, 208), false)
            
            #line 8 "..\..\Views\KnowledgeBase\KnowledgeList.cshtml"
, Tuple.Create(Tuple.Create("", 272), Tuple.Create<System.Object, System.Int32>(ViewBag.Version
            
            #line default
            #line hidden
, 272), false)
);

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" type=\"text/css\"");

WriteLiteral(" />\r\n<link");

WriteAttribute("href", Tuple.Create(" href=\"", 332), Tuple.Create("\"", 424)
            
            #line 9 "..\..\Views\KnowledgeBase\KnowledgeList.cshtml"
, Tuple.Create(Tuple.Create("", 339), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/jstree/dist/themes/default/style.min.css?v=")
            
            #line default
            #line hidden
, 339), false)
            
            #line 9 "..\..\Views\KnowledgeBase\KnowledgeList.cshtml"
  , Tuple.Create(Tuple.Create("", 408), Tuple.Create<System.Object, System.Int32>(ViewBag.Version
            
            #line default
            #line hidden
, 408), false)
);

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" type=\"text/css\"");

WriteLiteral(" />\r\n<link");

WriteAttribute("href", Tuple.Create(" href=\"", 468), Tuple.Create("\"", 545)
            
            #line 10 "..\..\Views\KnowledgeBase\KnowledgeList.cshtml"
, Tuple.Create(Tuple.Create("", 475), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Content/themes/base/jquery-ui.css?v=")
            
            #line default
            #line hidden
, 475), false)
            
            #line 10 "..\..\Views\KnowledgeBase\KnowledgeList.cshtml"
, Tuple.Create(Tuple.Create("", 529), Tuple.Create<System.Object, System.Int32>(ViewBag.Version
            
            #line default
            #line hidden
, 529), false)
);

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" type=\"text/css\"");

WriteLiteral(" />\r\n<script");

WriteAttribute("src", Tuple.Create(" src=\"", 591), Tuple.Create("\"", 667)
            
            #line 11 "..\..\Views\KnowledgeBase\KnowledgeList.cshtml"
, Tuple.Create(Tuple.Create("", 597), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/jstree/dist/jstree.min.js?v=")
            
            #line default
            #line hidden
, 597), false)
            
            #line 11 "..\..\Views\KnowledgeBase\KnowledgeList.cshtml"
, Tuple.Create(Tuple.Create("", 651), Tuple.Create<System.Object, System.Int32>(ViewBag.Version
            
            #line default
            #line hidden
, 651), false)
);

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral("></script>\r\n<script");

WriteAttribute("src", Tuple.Create(" src=\"", 710), Tuple.Create("\"", 791)
            
            #line 12 "..\..\Views\KnowledgeBase\KnowledgeList.cshtml"
, Tuple.Create(Tuple.Create("", 716), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/KnowledgeBase/KnowledgeBase.js?v=")
            
            #line default
            #line hidden
, 716), false)
            
            #line 12 "..\..\Views\KnowledgeBase\KnowledgeList.cshtml"
, Tuple.Create(Tuple.Create("", 775), Tuple.Create<System.Object, System.Int32>(ViewBag.Version
            
            #line default
            #line hidden
, 775), false)
);

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral("></script>\r\n<script");

WriteAttribute("src", Tuple.Create(" src=\"", 834), Tuple.Create("\"", 910)
            
            #line 13 "..\..\Views\KnowledgeBase\KnowledgeList.cshtml"
, Tuple.Create(Tuple.Create("", 840), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/ueditor/ueditor.config.js?v=")
            
            #line default
            #line hidden
, 840), false)
            
            #line 13 "..\..\Views\KnowledgeBase\KnowledgeList.cshtml"
, Tuple.Create(Tuple.Create("", 894), Tuple.Create<System.Object, System.Int32>(ViewBag.Version
            
            #line default
            #line hidden
, 894), false)
);

WriteLiteral("></script>\r\n<!-- 编辑器源码文件 -->\r\n<script");

WriteAttribute("src", Tuple.Create(" src=\"", 948), Tuple.Create("\"", 1025)
            
            #line 15 "..\..\Views\KnowledgeBase\KnowledgeList.cshtml"
, Tuple.Create(Tuple.Create("", 954), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/ueditor/ueditor.all.min.js?v=")
            
            #line default
            #line hidden
, 954), false)
            
            #line 15 "..\..\Views\KnowledgeBase\KnowledgeList.cshtml"
, Tuple.Create(Tuple.Create("", 1009), Tuple.Create<System.Object, System.Int32>(ViewBag.Version
            
            #line default
            #line hidden
, 1009), false)
);

WriteLiteral("></script>\r\n<script");

WriteAttribute("src", Tuple.Create(" src=\"", 1045), Tuple.Create("\"", 1112)
            
            #line 16 "..\..\Views\KnowledgeBase\KnowledgeList.cshtml"
, Tuple.Create(Tuple.Create("", 1051), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/ZeroClipboard.js?v=")
            
            #line default
            #line hidden
, 1051), false)
            
            #line 16 "..\..\Views\KnowledgeBase\KnowledgeList.cshtml"
, Tuple.Create(Tuple.Create("", 1096), Tuple.Create<System.Object, System.Int32>(ViewBag.Version
            
            #line default
            #line hidden
, 1096), false)
);

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral("></script>\r\n<div");

WriteLiteral(" class=\"maincontentinner1\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"main_bd\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"knowledgeHearder\"");

WriteLiteral(">\r\n            <span");

WriteLiteral(" style=\"font-size: 18px;\"");

WriteLiteral(">知识库</span>\r\n            <div");

WriteLiteral(" class=\"search\"");

WriteLiteral(" style=\"float: right;height: 100%;line-height: 44px;\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" class=\"searchValue input-large\"");

WriteLiteral(" style=\"height: 26px;border-radius: 5px !important;border: 1px solid rgb(204, 204" +
", 204);padding-left: 5px;\"");

WriteLiteral("/>\r\n                <a");

WriteLiteral(" href=\"javascript:void(0)\"");

WriteLiteral(" class=\"btn btn-primary searchBtn\"");

WriteLiteral(">查询</a>\r\n");

            
            #line 24 "..\..\Views\KnowledgeBase\KnowledgeList.cshtml"
                
            
            #line default
            #line hidden
            
            #line 24 "..\..\Views\KnowledgeBase\KnowledgeList.cshtml"
                 if (ViewBag.CopyUrl != null)
                {

            
            #line default
            #line hidden
WriteLiteral("                    <a");

WriteLiteral(" id=\"btnCopyTo\"");

WriteLiteral(" href=\"javascript:void(0)\"");

WriteLiteral(" target=\"_blank\"");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn btn-primary pull-right\"");

WriteLiteral(" style=\"margin-left: 5px;margin-top: 9px;\"");

WriteLiteral(">\r\n");

WriteLiteral("                        ");

            
            #line 27 "..\..\Views\KnowledgeBase\KnowledgeList.cshtml"
                   Write(Html.Raw("复制知识库链接"));

            
            #line default
            #line hidden
WriteLiteral("\r\n                    </a>\r\n");

            
            #line 29 "..\..\Views\KnowledgeBase\KnowledgeList.cshtml"
                }

            
            #line default
            #line hidden
WriteLiteral("            </div>\r\n        </div>\r\n        <div");

WriteLiteral(" class=\"knowledgeBody\"");

WriteLiteral(" style=\"position:relative\"");

WriteLiteral(">\r\n");

            
            #line 33 "..\..\Views\KnowledgeBase\KnowledgeList.cshtml"
            
            
            #line default
            #line hidden
            
            #line 33 "..\..\Views\KnowledgeBase\KnowledgeList.cshtml"
             if (!ExistRoot)
            {

            
            #line default
            #line hidden
WriteLiteral("                <a");

WriteLiteral(" href=\"javascript:void(0)\"");

WriteLiteral(" class=\"btn btn-primary addRootCatalog\"");

WriteLiteral(" style=\"position: absolute;top: 140px;left: 74px;\"");

WriteLiteral(">添加根目录</a>\r\n");

            
            #line 36 "..\..\Views\KnowledgeBase\KnowledgeList.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("            <div");

WriteLiteral(" class=\"knowledge-jstree knowledgeBody_left\"");

WriteLiteral(">\r\n               \r\n            </div>\r\n            <div");

WriteLiteral(" class=\"knowledge-list knowledgeBody_right\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" id=\"konwledgeArticleLsit\"");

WriteLiteral(" style=\"position: relative;\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"dataTables_filter\"");

WriteLiteral(" id=\"dyntable_filter\"");

WriteLiteral(">\r\n                    </div>\r\n                    <table");

WriteLiteral(" id=\"Table1\"");

WriteLiteral(" class=\"table table-bordered responsive dataTable\"");

WriteLiteral(@">
                        <thead>
                            <tr>
                                <th>
                                    条目名
                                </th>
                                <th>
                                    描述
                                </th>
                                <th>
                                    上传人
                                </th>
                                <th>
                                    浏览数
                                </th>
                                <th>
                                    上传时间
                                </th>
                                <th>
                                    操作
                                </th>
                            </tr>
                        </thead>
                        <tbody");

WriteLiteral(" id=\"tableBody\"");

WriteLiteral("></tbody>\r\n                    </table>\r\n                    <div");

WriteLiteral(" class=\"dataTables_info\"");

WriteLiteral(" id=\"Div25\"");

WriteLiteral(" style=\"background: rgb(234, 234, 234);\"");

WriteLiteral(">\r\n                        &nbsp;\r\n                    </div>\r\n                  " +
"  <div");

WriteLiteral(" class=\"dataTables_paginate paging_bootstrap pagination\"");

WriteLiteral(" id=\"Div26\"");

WriteLiteral(">\r\n                        <ul");

WriteLiteral(" id=\"tableFoot\"");

WriteLiteral("></ul>\r\n                    </div>\r\n                </div>\r\n                <div");

WriteLiteral(" id=\"knowledgeArticleEdit\"");

WriteLiteral(">\r\n                    \r\n                </div>\r\n            </div>\r\n        </di" +
"v>\r\n    </div>\r\n</div>\r\n<div");

WriteLiteral(" id=\"konwledgeBaseCatalogEditModal\"");

WriteLiteral(" class=\"modal hide fade in\"");

WriteLiteral(" tabindex=\"-1\"");

WriteLiteral(" role=\"dialog\"");

WriteLiteral(" aria-labelledby=\"myModalLabel\"");

WriteLiteral(" aria-hidden=\"false\"");

WriteLiteral(" style=\"width: 600px; display: none;\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"modal-dialog\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"modal-content\"");

WriteLiteral(">\r\n            <form");

WriteLiteral(" id=\"Form1\"");

WriteLiteral(" method=\"post\"");

WriteLiteral(" class=\"form-horizontal\"");

WriteLiteral(" enctype=\"multipart/form-data\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"modal-header\"");

WriteLiteral(">\r\n                    <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"close\"");

WriteLiteral(" data-dismiss=\"modal\"");

WriteLiteral(" aria-hidden=\"true\"");

WriteLiteral(">\r\n                        ×\r\n                    </button>\r\n                    " +
"<h4");

WriteLiteral(" class=\"modal-title\"");

WriteLiteral(">\r\n                        新增子目录\r\n                    </h4>\r\n                </di" +
"v>\r\n                <div");

WriteLiteral(" class=\"modal-body\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"dialog_content\"");

WriteLiteral(">\r\n                        <div");

WriteLiteral(" class=\"dialog_catalog_name\"");

WriteLiteral(">\r\n                            <div");

WriteLiteral(" class=\"form_item\"");

WriteLiteral(">\r\n                                <label");

WriteLiteral(" for=\"RepositoryCatalog_Title\"");

WriteLiteral(" class=\"form_label\"");

WriteLiteral(" style=\"line-height: 32px;\"");

WriteLiteral(">\r\n                                    目录名称\r\n                                </la" +
"bel>\r\n                                <div");

WriteLiteral(" class=\"form_control\"");

WriteLiteral(">\r\n                                    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" class=\"input_text\"");

WriteLiteral(" id=\"RepositoryCatalog_Id\"");

WriteLiteral(" value=\"\"");

WriteLiteral(">\r\n                                    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" class=\"input_text\"");

WriteLiteral(" id=\"RepositoryCatalog_ParentId\"");

WriteLiteral(" value=\"\"");

WriteLiteral(">\r\n                                    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" class=\"input_text\"");

WriteLiteral(" id=\"RepositoryCatalog_WeixinPlatId\"");

WriteLiteral(" value=\"\"");

WriteLiteral(">\r\n                                    <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"input_text\"");

WriteLiteral(" name=\"RepositoryCatalog_Title\"");

WriteLiteral(" id=\"RepositoryCatalog_Title\"");

WriteLiteral(" placeholder=\"目录名称不为空且不能超过50个字\"");

WriteLiteral(">\r\n                                    <span");

WriteLiteral(" class=\"form_info_block\"");

WriteLiteral(">目录名称不为空且不能超过50个字</span>\r\n                                </div>\r\n               " +
"             </div>\r\n\r\n                        </div>\r\n                    </div" +
">\r\n                </div>\r\n            </form>\r\n            <div");

WriteLiteral(" class=\"modal-footer\"");

WriteLiteral(">\r\n                <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn btn-primary uploadbtn\"");

WriteLiteral(" id=\"btnRepositoryCatalogAddSave\"");

WriteLiteral(">\r\n                    提交\r\n                </button>\r\n                <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn btn-default\"");

WriteLiteral(" data-dismiss=\"modal\"");

WriteLiteral(">\r\n                    取消\r\n                </button>\r\n            </div>\r\n\r\n     " +
"   </div>\r\n    </div>\r\n</div>\r\n<div");

WriteLiteral(" id=\"konwledgeBaseArticleEditModal\"");

WriteLiteral(" class=\"modal hide fade in\"");

WriteLiteral(" tabindex=\"-1\"");

WriteLiteral(" role=\"dialog\"");

WriteLiteral(" aria-labelledby=\"myModalLabel\"");

WriteLiteral(" aria-hidden=\"false\"");

WriteLiteral(" style=\"width: 600px; top: 5%; display: none;\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"modal-dialog\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"modal-content\"");

WriteLiteral(">\r\n            <form");

WriteLiteral(" id=\"Form1\"");

WriteLiteral(" method=\"post\"");

WriteLiteral(" class=\"form-horizontal\"");

WriteLiteral(" enctype=\"multipart/form-data\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"modal-header\"");

WriteLiteral(">\r\n                    <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"close\"");

WriteLiteral(" data-dismiss=\"modal\"");

WriteLiteral(" aria-hidden=\"true\"");

WriteLiteral(">\r\n                        ×\r\n                    </button>\r\n                    " +
"<h4");

WriteLiteral(" class=\"modal-title\"");

WriteLiteral(">\r\n                        新增条目\r\n                    </h4>\r\n                </div" +
">\r\n                <div");

WriteLiteral(" class=\"modal-body\"");

WriteLiteral(" style=\"max-height: 480px;\"");

WriteLiteral(">\r\n                    <input");

WriteLiteral(" id=\"RepositoryArticle_Id\"");

WriteLiteral(" value=\"\"");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" >\r\n                    <input");

WriteLiteral(" id=\"RepositoryArticle_CatalogId\"");

WriteLiteral(" value=\"\"");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(">\r\n                    <input");

WriteLiteral(" id=\"RepositoryArticle_WeixinPlatId\"");

WriteLiteral(" value=\"\"");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(">\r\n                    <input");

WriteLiteral(" id=\"RepositoryArticle_Quantity\"");

WriteLiteral(" value=\"\"");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"form_item\"");

WriteLiteral(">\r\n                        <label");

WriteLiteral(" class=\"form_label\"");

WriteLiteral(">\r\n                            所属目录\r\n                        </label>\r\n          " +
"              <div");

WriteLiteral(" class=\"form_control\"");

WriteLiteral(">\r\n                            <input");

WriteLiteral(" id=\"RepositoryArticle_CatalogName\"");

WriteLiteral(" type=\"text\"");

WriteLiteral("  class=\"input_text js_partial_require\"");

WriteLiteral(" placeholder=\"\"");

WriteLiteral(" readonly=\"readonly\"");

WriteLiteral(" value=\"\"");

WriteLiteral(">\r\n                        </div>\r\n                    </div>\r\n                  " +
"  <div");

WriteLiteral(" class=\"form_item\"");

WriteLiteral(">\r\n                        <label");

WriteLiteral(" class=\"form_label\"");

WriteLiteral(">\r\n                            条目名\r\n                        </label>\r\n           " +
"             <div");

WriteLiteral(" class=\"form_control\"");

WriteLiteral(">\r\n                            <input");

WriteLiteral(" id=\"RepositoryArticle_Title\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"input_text js_partial_require\"");

WriteLiteral(" placeholder=\"\"");

WriteLiteral(" value=\"\"");

WriteLiteral(">\r\n                        </div>\r\n                    </div>\r\n                  " +
"  <div");

WriteLiteral(" class=\"form_item\"");

WriteLiteral(">\r\n                        <label");

WriteLiteral(" class=\"form_label\"");

WriteLiteral(">\r\n                            封面图片\r\n                        </label>\r\n          " +
"              <div");

WriteLiteral(" class=\"form_control\"");

WriteLiteral(">\r\n");

WriteLiteral("                            ");

            
            #line 165 "..\..\Views\KnowledgeBase\KnowledgeList.cshtml"
                       Write(Html.Partial("UploadImagePartial", new ViewDataDictionary(ViewData) { { "count", "1" }, { "picHidden", "RepositoryArticle_CoverImg" }, { "tip", "" }, { "isShow", true } }));

            
            #line default
            #line hidden
WriteLiteral("\r\n                        </div>\r\n                    </div>\r\n                   " +
" <div");

WriteLiteral(" class=\"form_item\"");

WriteLiteral(">\r\n                        <label");

WriteLiteral(" class=\"form_label\"");

WriteLiteral(">\r\n                            摘要\r\n                        </label>\r\n            " +
"            <div");

WriteLiteral(" class=\"form_control\"");

WriteLiteral(">\r\n                                <textarea");

WriteLiteral(" id=\"RepositoryArticle_Abstract\"");

WriteLiteral(" class=\"input_text js_partial_require\"");

WriteLiteral("></textarea>\r\n                        </div>\r\n                    </div>\r\n       " +
"             <div");

WriteLiteral(" class=\"form_item\"");

WriteLiteral(">\r\n                        <label");

WriteLiteral(" class=\"form_label\"");

WriteLiteral(">\r\n                            是否显示封面\r\n                        </label>\r\n        " +
"                <div");

WriteLiteral(" class=\"form_control\"");

WriteLiteral(">\r\n                            <input");

WriteLiteral(" name=\"RepositoryArticle_Display\"");

WriteLiteral(" type=\"radio\"");

WriteLiteral(" class=\"input_text js_partial_require\"");

WriteLiteral(" data-state=\"1\"");

WriteLiteral(" checked=\"checked\"");

WriteLiteral("> 是 \r\n                            <input");

WriteLiteral(" name=\"RepositoryArticle_Display\"");

WriteLiteral(" type=\"radio\"");

WriteLiteral(" class=\"input_text js_partial_require\"");

WriteLiteral(" data-state=\"0\"");

WriteLiteral(" placeholder=\"\"");

WriteLiteral(" value=\"\"");

WriteLiteral("> 否 \r\n                        </div>\r\n                    </div>\r\n               " +
"     <div");

WriteLiteral(" class=\"form_item\"");

WriteLiteral(">\r\n                        <label");

WriteLiteral(" class=\"form_label\"");

WriteLiteral(">\r\n                            正文\r\n                        </label>\r\n            " +
"            <div");

WriteLiteral(" class=\"form_control\"");

WriteLiteral(" style=\" margin-left: 0;\"");

WriteLiteral(">\r\n                            <script");

WriteLiteral(" id=\"RepositoryArticle_Contents\"");

WriteLiteral(" name=\"content\"");

WriteLiteral(" type=\"text/plain\"");

WriteLiteral(" width=\"100%\"");

WriteLiteral(">\r\n                            </script>\r\n                        </div>\r\n       " +
"             </div>\r\n                </div>\r\n                <div");

WriteLiteral(" class=\"modal-footer\"");

WriteLiteral(">\r\n                    <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn btn-primary uploadbtn\"");

WriteLiteral(" id=\"btnRepositoryArticleAddSave\"");

WriteLiteral(" style=\"width: 150px;\"");

WriteLiteral(">\r\n                        保存\r\n                    </button>\r\n                   " +
" <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn btn-default\"");

WriteLiteral(" data-dismiss=\"modal\"");

WriteLiteral(">\r\n                        取消\r\n                    </button>\r\n                </d" +
"iv>\r\n            </form>\r\n        </div>\r\n    </div>\r\n</div>\r\n<script");

WriteLiteral(" type=\"text/x-jquery-tmpl\"");

WriteLiteral(" id=\"knowledgeBaseArticleTmpl\"");

WriteLiteral(@">
    <tr class=""gradeX odd"" id=""${Id}"">
        <td class=""a_Title"">
            ${Title}
        </td>
        <td class=""a_Abstract"">
            ${Abstract}
        </td>
        <td class=""a_UploadName"">
            ${UploadName}
        </td>
        <td class=""a_Quantity"">
            ${Quantity}
        </td>
        <td class=""a_UploadTime"">
            ${formatDate(UploadTime).split('.')[0]}
        </td>
        <td class="" "">
            <a href=""javascript:void(0)"" item-id=""${Id}"" class=""handleIcon btn-detail-open"" title=""编辑""></a>
            <a href=""javascript:void(0)"" item-id=""${Id}"" class=""handleIcon btn-del-open"" title=""删除""></a>
        </td>
    </tr>
    <div id=""dateail_${id}""></div>
</script>
<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n    $(function () {\r\n        if ($(\"#btnCopyTo\").length > 0) {\r\n            Ze" +
"roClipboard.setMoviePath(\'");

            
            #line 234 "..\..\Views\KnowledgeBase\KnowledgeList.cshtml"
                                   Write(Url.Content("~/Scripts/ZeroClipboard.swf?v="));

            
            #line default
            #line hidden
            
            #line 234 "..\..\Views\KnowledgeBase\KnowledgeList.cshtml"
                                                                                 Write(ViewBag.Version);

            
            #line default
            #line hidden
WriteLiteral("\');\r\n            var clip = new ZeroClipboard.Client(); // 新建一个对象\r\n            cl" +
"ip.setHandCursor(true); // 设置鼠标为手型\r\n            clip.setText(\'");

            
            #line 237 "..\..\Views\KnowledgeBase\KnowledgeList.cshtml"
                     Write(ViewBag.CopyUrl);

            
            #line default
            #line hidden
WriteLiteral(@"'.replace(/&amp;/g, ""&"")); // 设置要复制的文本。
            clip.glue(""btnCopyTo""); // 和上一句位置不可调换
            clip.addEventListener(""complete"", function () {
                alert(""链接已成功复制到剪贴板，右键选择粘贴或按键盘快捷键“Ctrl+V“即可使用"");
            });
        }
    });
</script>
<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
    var editorTEXTInitContent = """";
    var editorTEXT = UE.getEditor('RepositoryArticle_Contents', {
        catchRemoteImageEnable: false,
        initialFrameHeight: 300,
        toolbars: [['fullscreen', 'source', ""fontsize"", ""|"", ""blockquote"", ""horizontal"", ""|"", ""removeformat"", ""link"", ""unlink"", ""|"",""insertimage"", ""insertmusic"", ""insertaudio"", ""insertcard""], [""bold"", ""italic"", ""underline"", ""forecolor"", ""backcolor"", ""|"", ""justifyleft"", ""justifycenter"", ""justifyright"", ""|"", ""rowspacingtop"", ""rowspacingbottom"", ""lineheight"", ""|"", ""insertorderedlist"", ""insertunorderedlist"", ""|"", ""imagenone"", ""imageleft"", ""imageright"", ""imagecenter""]]

    });
</script>
<!--数据加载的提示-->
<div");

WriteLiteral(" id=\"load\"");

WriteLiteral(" class=\"tipsClass\"");

WriteLiteral("></div>");

        }
    }
}
#pragma warning restore 1591
