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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Mobile/Views/RepositoryArticle/MobileRepositoryArticleList.cshtml")]
    public partial class _Mobile_Views_RepositoryArticle_MobileRepositoryArticleList_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Mobile_Views_RepositoryArticle_MobileRepositoryArticleList_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Mobile\Views\RepositoryArticle\MobileRepositoryArticleList.cshtml"
  
    Layout = "~/mobile/Views/Shared/_Layout.cshtml";
    ViewBag.Title = "产品知识库";

            
            #line default
            #line hidden
WriteLiteral(@"
<style>
    .seacrInput {
        width: 57%;
        height: 34px;
        float: left;
        border-radius: 5px;
        border: 0;
        margin-right: 5%;
        margin-left: 5px;
        padding-left: 5px;
    }
    #mobileSearchBtn {
        width:30%;
        background:#221715;
        color:white;
    }
</style>
    <!-- #CSS -->
<link");

WriteLiteral(" type=\"text/css\"");

WriteLiteral(" rel=\"stylesheet\"");

WriteAttribute("href", Tuple.Create(" href=\"", 492), Tuple.Create("\"", 534)
            
            #line 23 "..\..\Mobile\Views\RepositoryArticle\MobileRepositoryArticleList.cshtml"
, Tuple.Create(Tuple.Create("", 499), Tuple.Create<System.Object, System.Int32>(Url.Content("~/css/LSHM/lshm.css")
            
            #line default
            #line hidden
, 499), false)
);

WriteLiteral(" media=\"screen,projection\"");

WriteLiteral(" />\r\n<!-- #SCRIPT -->\r\n<script");

WriteAttribute("src", Tuple.Create(" src=\"", 591), Tuple.Create("\"", 636)
            
            #line 25 "..\..\Mobile\Views\RepositoryArticle\MobileRepositoryArticleList.cshtml"
, Tuple.Create(Tuple.Create("", 597), Tuple.Create<System.Object, System.Int32>(Url.Content("~/js/jquery.tmpl.min.js")
            
            #line default
            #line hidden
, 597), false)
);

WriteLiteral("></script>\r\n<script");

WriteAttribute("src", Tuple.Create(" src=\"", 656), Tuple.Create("\"", 698)
            
            #line 26 "..\..\Mobile\Views\RepositoryArticle\MobileRepositoryArticleList.cshtml"
, Tuple.Create(Tuple.Create("", 662), Tuple.Create<System.Object, System.Int32>(Url.Content("~/js/Jquery.Query.js")
            
            #line default
            #line hidden
, 662), false)
);

WriteLiteral("></script>\r\n<script");

WriteAttribute("src", Tuple.Create(" src=\"", 718), Tuple.Create("\"", 764)
            
            #line 27 "..\..\Mobile\Views\RepositoryArticle\MobileRepositoryArticleList.cshtml"
, Tuple.Create(Tuple.Create("", 724), Tuple.Create<System.Object, System.Int32>(Url.Content("~/js/lshm/lshm_common.js")
            
            #line default
            #line hidden
, 724), false)
);

WriteLiteral("></script>\r\n<div");

WriteLiteral(" class=\"lshm_container\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"lshm_listSearch\"");

WriteLiteral(">\r\n        <input");

WriteLiteral(" class=\"seacrInput\"");

WriteLiteral(" name=\"searchContent\"");

WriteLiteral(" placeholder=\"请输入至少2个及以上的关键词\"");

WriteLiteral(" />\r\n        <a");

WriteLiteral(" class=\"btn btn_block\"");

WriteLiteral(" id=\"mobileSearchBtn\"");

WriteLiteral(" href=\"javascript:void(0)\"");

WriteLiteral(">搜　索</a>\r\n    </div>\r\n    <div");

WriteLiteral(" class=\"lshm_list\"");

WriteLiteral(">\r\n        <ul");

WriteLiteral(" class=\"lshm_list_ul\"");

WriteLiteral(" id=\"repositoryArticleList\"");

WriteLiteral("></ul>\r\n    </div>\r\n</div>\r\n<script");

WriteLiteral(" type=\"text/x-jquery-tmpl\"");

WriteLiteral(" id=\"repositoryArticleListTmpl\"");

WriteLiteral(">\r\n    <li class=\"lshm_list_li\">\r\n        <a class=\"list_link\" href=\"");

            
            #line 39 "..\..\Mobile\Views\RepositoryArticle\MobileRepositoryArticleList.cshtml"
                              Write(Url.Action("MobileRepositoryArticleDetail", "RepositoryArticle"));

            
            #line default
            #line hidden
            
            #line 39 "..\..\Mobile\Views\RepositoryArticle\MobileRepositoryArticleList.cshtml"
                                                                                               Write(Html.Raw(ViewBag.oQueryString));

            
            #line default
            #line hidden
WriteLiteral(@"&aid=${Id}"">
            <div class=""list_content"">
                <div class=""list_detail"">
                    <div class=""list_detail_row"" style=""font-size: 1.4em;color: rgb(0, 0, 0);"">${Title}</div>
                    <div class=""list_detail_row"">${Abstract}</div>
                    <div class=""list_detail_row"" style=""text-align: right;"">浏览数：${Quantity}次&nbsp;&nbsp;&nbsp;${UploadTime.split(""T"")[0]}</div>
                </div>
            </div>
        </a>
    </li>
</script>
<script>
    $(function () {
        var opts = {
            getLsitDataUrl: """);

            
            #line 53 "..\..\Mobile\Views\RepositoryArticle\MobileRepositoryArticleList.cshtml"
                        Write(Url.Action("GetRepositoryArticleEntities", "RepositoryArticle"));

            
            #line default
            #line hidden
            
            #line 53 "..\..\Mobile\Views\RepositoryArticle\MobileRepositoryArticleList.cshtml"
                                                                                        Write(Html.Raw(ViewBag.oQueryString));

            
            #line default
            #line hidden
WriteLiteral(@""",
            pageIndex: 1,
            pageSize: 10,
            listTmplId: ""repositoryArticleListTmpl"",
            listContainerId: ""repositoryArticleList"",

            searchBtnId: ""mobileSearchBtn"",//搜索按钮的Id
            searchValueCss: ""seacrInput""//搜索框的class
        }
        MobileList.init(opts);
        //搜索value特殊处理
        MobileList.extendListFunction.extendSearchJosn = function (searchJson) {
            return searchJson[""searchContent""];
        }
    });
</script>");

        }
    }
}
#pragma warning restore 1591
