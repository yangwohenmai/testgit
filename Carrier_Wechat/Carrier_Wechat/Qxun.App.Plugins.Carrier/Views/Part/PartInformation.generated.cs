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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Part/PartInformation.cshtml")]
    public partial class _Views_Part_PartInformation_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_Part_PartInformation_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\Part\PartInformation.cshtml"
  
    ViewBag.Title = "WeiPhotoList";
    ViewBag.subTitle = "零件信息管理";
    Layout = "~/Views/Shared/_Layout.cshtml";
    var platid = Request["plat"];
    //var Status = Qxun.UI.Background.App_Code.CreateForm.GetEnumNameByType(typeof(Qxun.App.Plugins.NemakCore.Public.Status), "Status");

            
            #line default
            #line hidden
WriteLiteral("\r\n<script");

WriteAttribute("src", Tuple.Create(" src=\"", 305), Tuple.Create("\"", 370)
            
            #line 8 "..\..\Views\Part\PartInformation.cshtml"
, Tuple.Create(Tuple.Create("", 311), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/jquery.form.js?v=")
            
            #line default
            #line hidden
, 311), false)
            
            #line 8 "..\..\Views\Part\PartInformation.cshtml"
, Tuple.Create(Tuple.Create("", 354), Tuple.Create<System.Object, System.Int32>(ViewBag.Version
            
            #line default
            #line hidden
, 354), false)
);

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral("></script>\r\n<script");

WriteAttribute("src", Tuple.Create(" src=\"", 413), Tuple.Create("\"", 480)
            
            #line 9 "..\..\Views\Part\PartInformation.cshtml"
, Tuple.Create(Tuple.Create("", 419), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/ZeroClipboard.js?v=")
            
            #line default
            #line hidden
, 419), false)
            
            #line 9 "..\..\Views\Part\PartInformation.cshtml"
, Tuple.Create(Tuple.Create("", 464), Tuple.Create<System.Object, System.Int32>(ViewBag.Version
            
            #line default
            #line hidden
, 464), false)
);

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral("></script>\r\n\r\n<div");

WriteLiteral(" class=\"tag mg-bottom-20 clr\"");

WriteLiteral(">   \r\n    <div");

WriteLiteral(" class=\"alert\"");

WriteLiteral(" style=\"width:700px\"");

WriteLiteral("><span");

WriteLiteral(" id=\"notice\"");

WriteLiteral(" style=\"font-weight:bold;\"");

WriteLiteral("></span>&nbsp;&nbsp;&nbsp;&nbsp;<span");

WriteLiteral(" id=\"alert\"");

WriteLiteral(">你好，这是首次登陆,没有历史导入导出记录</span></div>\r\n</div>\r\n");

            
            #line 14 "..\..\Views\Part\PartInformation.cshtml"
Write(Html.Partial("_ListResource"));

            
            #line default
            #line hidden
WriteLiteral("\r\n<link");

WriteAttribute("href", Tuple.Create(" href=\"", 772), Tuple.Create("\"", 871)
            
            #line 15 "..\..\Views\Part\PartInformation.cshtml"
, Tuple.Create(Tuple.Create("", 779), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Content/datetimepicker/bootstrap-datetimepicker.min.css?v=")
            
            #line default
            #line hidden
, 779), false)
            
            #line 15 "..\..\Views\Part\PartInformation.cshtml"
         , Tuple.Create(Tuple.Create("", 855), Tuple.Create<System.Object, System.Int32>(ViewBag.Version
            
            #line default
            #line hidden
, 855), false)
);

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" type=\"text/css\"");

WriteLiteral(" />\r\n<script");

WriteAttribute("src", Tuple.Create(" src=\"", 917), Tuple.Create("\"", 1014)
            
            #line 16 "..\..\Views\Part\PartInformation.cshtml"
, Tuple.Create(Tuple.Create("", 923), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/datetimepicker/bootstrap-datetimepicker.min.js?v=")
            
            #line default
            #line hidden
, 923), false)
            
            #line 16 "..\..\Views\Part\PartInformation.cshtml"
         , Tuple.Create(Tuple.Create("", 998), Tuple.Create<System.Object, System.Int32>(ViewBag.Version
            
            #line default
            #line hidden
, 998), false)
);

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral("></script>\r\n<script");

WriteAttribute("src", Tuple.Create(" src=\"", 1057), Tuple.Create("\"", 1164)
            
            #line 17 "..\..\Views\Part\PartInformation.cshtml"
, Tuple.Create(Tuple.Create("", 1063), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/datetimepicker/locales/bootstrap-datetimepicker.zh-CN.js?v=")
            
            #line default
            #line hidden
, 1063), false)
            
            #line 17 "..\..\Views\Part\PartInformation.cshtml"
                  , Tuple.Create(Tuple.Create("", 1148), Tuple.Create<System.Object, System.Int32>(ViewBag.Version
            
            #line default
            #line hidden
, 1148), false)
);

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral("></script>\r\n\r\n<div");

WriteLiteral(" class=\"maincontentinner1\"");

WriteLiteral(">\r\n\r\n    <div");

WriteLiteral(" id=\"Div12\"");

WriteLiteral(" class=\"dataTables_wrapper\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" id=\"dyntable_length\"");

WriteLiteral(" class=\"dataTables_length\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"btn-group\"");

WriteLiteral(">\r\n                <label>\r\n                    <a");

WriteLiteral(" href=\"#importMember\"");

WriteLiteral(" class=\"btn btn-primary\"");

WriteLiteral(" data-toggle=\"modal\"");

WriteLiteral(">\r\n                        导入数据\r\n                    </a>\r\n                    <a" +
"");

WriteLiteral(" class=\"btn btn-primary\"");

WriteLiteral(" id=\"btnBatchExport\"");

WriteLiteral(">\r\n                        批量导出\r\n                    </a>\r\n                    <a" +
"");

WriteLiteral(" class=\"btn btn-primary\"");

WriteLiteral(" id=\"btnBatchExportERP\"");

WriteLiteral(">\r\n                        ERP导出\r\n                    </a>\r\n                </lab" +
"el>\r\n            </div>\r\n            <span");

WriteLiteral(" style=\"float:right\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" name=\"PartNumber\"");

WriteLiteral(" id=\"PartNumber\"");

WriteLiteral(" class=\"search\"");

WriteLiteral(" value=\"\"");

WriteLiteral(" style=\"color: gray\"");

WriteLiteral(" placeholder=\"请输入零件号\"");

WriteLiteral(" onkeyup=\"value=value.replace(/[\\\']+/, \'\') \"");

WriteLiteral(">\r\n               \r\n                <button");

WriteLiteral(" class=\"btn m-bottom-10\"");

WriteLiteral(" id=\"btnselect\"");

WriteLiteral(">查询</button>\r\n\r\n            </span>\r\n        </div>\r\n        <div");

WriteLiteral(" class=\"dataTables_filter\"");

WriteLiteral(" id=\"dyntable_filter\"");

WriteLiteral(">\r\n        </div>\r\n        <table");

WriteLiteral(" id=\"Table1\"");

WriteLiteral(" class=\"table table-bordered responsive dataTable\"");

WriteLiteral(@">
            <thead>
                <tr>
                    <th>
                        零件号
                    </th>
                    <th>
                        零件名称
                    </th>
                    <th>
                        零件价格
                    </th> 
                    <th>
                        操作
                    </th>                 
                </tr>
            </thead>
            <tbody");

WriteLiteral(" id=\"tableBody\"");

WriteLiteral("></tbody>\r\n        </table>\r\n        <div");

WriteLiteral(" class=\"dataTables_info\"");

WriteLiteral(" id=\"Div25\"");

WriteLiteral(">\r\n            &nbsp;\r\n        </div>\r\n        <div");

WriteLiteral(" class=\"dataTables_paginate paging_bootstrap pagination\"");

WriteLiteral(" id=\"Div26\"");

WriteLiteral(">\r\n            <ul");

WriteLiteral(" id=\"tableFoot\"");

WriteLiteral("></ul>\r\n        </div>\r\n    </div>\r\n    <br>\r\n    <br>\r\n</div>\r\n<!--列表模板-->\r\n\r\n<s" +
"cript");

WriteLiteral(" type=\"text/x-jquery-tmpl\"");

WriteLiteral(" id=\"replylist\"");

WriteLiteral(@">

    <tr class=""gradeX odd"" id=""tr${PartId}"">
        <td class="" "">
            ${PartNumber}
        </td>
        <td class="" "">
            ${PartName}
        </td>
        <td class="" "">
           ￥${Price}
        </td>
        <td class="" "">
            ");

WriteLiteral("\r\n            <a href=\"");

            
            #line 90 "..\..\Views\Part\PartInformation.cshtml"
                Write(Url.Action("EditPartInfo", "Part"));

            
            #line default
            #line hidden
            
            #line 90 "..\..\Views\Part\PartInformation.cshtml"
                                                   Write(Html.Raw(@ViewBag.oQueryString));

            
            #line default
            #line hidden
WriteLiteral("&id=${PartId}\" item-id=\"${PartId}\" class=\"handleIcon btn-detail-open\" title=\"编辑\">" +
"</a>\r\n        </td>\r\n    </tr>\r\n    <div id=\"dateail_${PartId}\"></div>\r\n\r\n</scri" +
"pt>\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n    var plat = jQuery.query.get(\"plat\");\r\n    var opts = {\r\n        getListUrl" +
": \'");

            
            #line 99 "..\..\Views\Part\PartInformation.cshtml"
                Write(Url.Action("GetPartInformationPageList", "Part"));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n        footUrl: \'");

            
            #line 100 "..\..\Views\Part\PartInformation.cshtml"
             Write(Url.Content("~/Content/tmpl/foot1.htm"));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n        tableBodyId: \"tableBody\",\r\n        tableFootId: \"tableFoot\",\r\n       " +
" replyListId: \"replylist\",\r\n        pageSize: 15,\r\n        ");

WriteLiteral(@"
        itemId: ""item-id"",
        cssClass: "".btn-del-open"",
        plat: plat,
        checkBox: """"
    };
    $(function () {
        //初始化列表数据
        listClass.init(opts);
        //初始化导入记录
        init();
        //搜索
        $(""#btnselect"").click(function () {
            var entity = new Object();
            entity.PartNumber = $(""#PartNumber"").val();
            var searchJson = JSON.stringify(entity);
            opts.getListUrl = '");

            
            #line 121 "..\..\Views\Part\PartInformation.cshtml"
                          Write(Url.Action("GetPartInformationPageList", "Part"));

            
            #line default
            #line hidden
WriteLiteral(@"?searchJson=' + searchJson;
            listClass.init(opts);

        });

        $(""#btnBatchExport"").click(function () {
            var PartNumber = $('#PartNumber').val();
            //var txtCompany = $('#txtCompany').val();
            //var txtName = $('#txtName').val();
            //var date = $(""#date"").val();
            location.href = '");

            
            #line 131 "..\..\Views\Part\PartInformation.cshtml"
                        Write(Url.Action("ExportPartinfoToExcel", "Part"));

            
            #line default
            #line hidden
WriteLiteral(@"?PartNumber=' + PartNumber;
            return false;
        });

        $(""#btnBatchExportERP"").click(function () {
            var PartNumber = $('#PartNumber').val();
            //var txtCompany = $('#txtCompany').val();
            //var txtName = $('#txtName').val();
            //var date = $(""#date"").val();
            ");

WriteLiteral("\r\n            location.href = \'");

            
            #line 141 "..\..\Views\Part\PartInformation.cshtml"
                        Write(Url.Action("ExportPartinfoToExcelERP", "Part"));

            
            #line default
            #line hidden
WriteLiteral("?PartNumber=\' + PartNumber;\r\n            return false;\r\n        });\r\n\r\n    })\r\n\r\n" +
"\r\n\r\n\r\n\r\n\r\n    function init() {\r\n        $.post(\"");

            
            #line 153 "..\..\Views\Part\PartInformation.cshtml"
           Write(Url.Action("Record", "Part"));

            
            #line default
            #line hidden
            
            #line 153 "..\..\Views\Part\PartInformation.cshtml"
                                        Write(Html.Raw(ViewBag.oQueryString));

            
            #line default
            #line hidden
WriteLiteral(@""", {}, function (data) {
            //  if (data.IsSuccess) {
            //alert(""注册成功"");
            if (data.AllUploadRecord != null) {
                var success = data.SuccessRecord;
                var fail = data.FailRecord;
                var time = formatDate(data.UploadTime).split(""."")[0];
                var all = data.AllUploadRecord;
                var a = document.getElementById(""alert"");
                var b = document.getElementById(""notice"");
                a.innerHTML = ""上次历史上传成功"" + success + ""条，失败"" + fail + ""条，总共上传"" + all + ""条，上传时间:"" + time + "",现有总数据为：");

            
            #line 163 "..\..\Views\Part\PartInformation.cshtml"
                                                                                                             Write(ViewBag.TotalCount);

            
            #line default
            #line hidden
WriteLiteral("\";\r\n                a.innerHTML = \"上次上传时间为：\" + time + \"，总共上传\" + all + \"条数据，其中成功\" " +
"+ success + \"条，失败\" + fail + \"条。\"\r\n                b.innerHTML = \"现有总数据条数为：");

            
            #line 165 "..\..\Views\Part\PartInformation.cshtml"
                                   Write(ViewBag.TotalCount);

            
            #line default
            #line hidden
WriteLiteral(" 条。         \"\r\n            }\r\n        });\r\n    }\r\n\r\n</script>\r\n\r\n<!--数据加载的提示-->\r\n" +
"\r\n<div");

WriteLiteral(" id=\"load\"");

WriteLiteral(" class=\"tipsClass\"");

WriteLiteral("></div>\r\n<div");

WriteLiteral(" id=\"importMember\"");

WriteLiteral(" class=\"modal hide fade\"");

WriteLiteral(" tabindex=\"-1\"");

WriteLiteral(" role=\"dialog\"");

WriteLiteral(" aria-labelledby=\"myModalLabel\"");

WriteLiteral("\r\n     aria-hidden=\"false\"");

WriteLiteral(" style=\"display: none;\"");

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

WriteLiteral(">\r\n                        批量导入零件信息\r\n                    </h4>\r\n                <" +
"/div>\r\n                <div");

WriteLiteral(" class=\"modal-body\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"alert alert-info\"");

WriteLiteral(">\r\n                        文件大小不要超过1M，数据量小于5000条！\r\n                    </div>\r\n  " +
"                  <a");

WriteAttribute("href", Tuple.Create(" href=\"", 7761), Tuple.Create("\"", 7811)
            
            #line 192 "..\..\Views\Part\PartInformation.cshtml"
, Tuple.Create(Tuple.Create("", 7768), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Content/tmpl/PartList.csv")
            
            #line default
            #line hidden
, 7768), false)
);

WriteLiteral(" target=\"_blank\"");

WriteLiteral(" class=\"green\"");

WriteLiteral(">\r\n                        下载内容格式模板文件PartList.csv\r\n                    </a>\r\n    " +
"                <div");

WriteLiteral(" style=\"padding-bottom: 14px;font: 16px verdana;\"");

WriteLiteral(">\r\n                        上传文件：\r\n                        <span");

WriteLiteral(" style=\"vertical-align: middle;margin-right: 14px;margin-left: 12px;font-size: 16" +
"px;\"");

WriteLiteral(">\r\n                            <input");

WriteLiteral(" type=\"file\"");

WriteLiteral(" id=\"file\"");

WriteLiteral(" name=\"file1\"");

WriteLiteral(" value=\"上传\"");

WriteLiteral(" style=\" font-size: 16px; /* vertical-align: middle; */ padding-top: 3px; line-he" +
"ight: 20px; height: 28px;\"");

WriteLiteral(" />\r\n                        </span>\r\n                    </div>\r\n\r\n             " +
"       <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"weiXinUserId\"");

WriteLiteral(" />\r\n                    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"groupID\"");

WriteLiteral(" />\r\n                </div>\r\n                <div");

WriteLiteral(" class=\"modal-footer\"");

WriteLiteral(">\r\n                    <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn btn-default\"");

WriteLiteral(" data-dismiss=\"modal\"");

WriteLiteral(">\r\n                        取消\r\n                    </button>\r\n                   " +
" <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn btn-primary uploadbtn\"");

WriteLiteral(" ");

WriteLiteral(">\r\n                        提交\r\n                    </button>\r\n                </d" +
"iv>\r\n            </form>\r\n        </div>\r\n    </div>\r\n</div>\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
    $(function () {
        $("".uploadbtn"").click(function () {
            var fileName = $(""#file"").val();
            var fileLenght = fileName.length;
            var fileEnd = fileName.substring(fileLenght - 4, fileLenght);
            if (fileEnd == "".csv"") {
                jQuery(""#Form1"").ajaxSubmit({
                    url: '");

            
            #line 225 "..\..\Views\Part\PartInformation.cshtml"
                     Write(Url.Action("ImportPartListByFile", "Part"));

            
            #line default
            #line hidden
WriteLiteral(@"',
                    // data: { vipType: vipType },
                    success: function (responseTexts) {
                        //  var responseText = JSON.parse(responseTexts);
                        if (responseTexts.IsSuccess) {
                            alert(""程序正在后台上传,请稍后刷新查看上传记录!"");
                            $('#importMember').modal('hide');
                        }

                        else {
                            $('#importMember').modal('hide');
                            location.reload();
                        }
                    },
                    complete: function (xhr, ts) { xhr = null },
                    error: function (content) { alert(""文件格式错误！""); }
                })
            } else {
                alert(""请上传.cvs格式的文档"")
            }
        })
    })
</script>");

        }
    }
}
#pragma warning restore 1591
