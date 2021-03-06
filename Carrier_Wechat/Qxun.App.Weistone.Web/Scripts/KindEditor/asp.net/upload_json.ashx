﻿<%@ WebHandler Language="C#" Class="Upload" %>

/**
 * KindEditor ASP.NET
 *
 * 本ASP.NET程序是演示程序，建议不要直接在实际项目中使用。
 * 如果您确定直接使用本程序，使用之前请仔细确认相关安全设置。
 *
 */

using System;
using System.Collections;
using System.Web;
using System.IO;
using System.Globalization;
using LitJson;
using Qxun.Core.Common;
using System.Drawing;
using Qxun.App.Weistone.Services;
using Qxun.App.Weistone.ViewModels;
using System.Web.SessionState;

public class Upload : IHttpHandler, IRequiresSessionState
{

    private HttpContext context;

    public void ProcessRequest(HttpContext context)
    {
        
        ViewAccount entity = null;
        if (context.Session["user"] != null)
        {
            entity = (ViewAccount)context.Session["user"];
        }
        else
        {
            entity = new ViewAccount();
        }

        //定义允许上传的文件扩展名
        Hashtable extTable = new Hashtable();
        extTable.Add("image", "gif,jpg,jpeg,png,bmp");
        extTable.Add("flash", "swf,flv");
        extTable.Add("media", "swf,flv,mp3,wav,wma,wmv,mid,avi,mpg,asf,rm,rmvb");
        extTable.Add("file", "doc,docx,xls,xlsx,ppt,htm,html,txt,zip,rar,gz,bz2");

        //最大文件大小
        int maxSize = 1000000;
        this.context = context;
        HttpPostedFile imgFile = context.Request.Files["imgFile"];

        String dirName = context.Request.QueryString["dir"];
        if (String.IsNullOrEmpty(dirName))
        {
            dirName = "image";
        }
        if (!extTable.ContainsKey(dirName))
        {
            showError("目录名不正确。");
        }

        String fileName = imgFile.FileName;
        String fileExt = Path.GetExtension(fileName).ToLower();
        if (imgFile.InputStream.Length > maxSize)
        {
            showError("上传文件大小超过限制。");
        }

        if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(((String)extTable[dirName]).Split(','), fileExt.Substring(1).ToLower()) == -1)
        {
            showError("上传文件扩展名是不允许的扩展名。\n只允许" + ((String)extTable[dirName]) + "格式。");
        }
        Image img = Image.FromStream(imgFile.InputStream);
        ResourceBll imgFiles = new ResourceBll();
        ViewAccount account = context.Session["user"] as ViewAccount;
        if (account == null)
        {
            return;
        }

        ViewWeixinPlat weixinplat = context.Session["weixinplat"] as ViewWeixinPlat;
        if (weixinplat == null)
        {
            return;
        }
            
        
        string panorama = context.Request["panorama"];
        ImgResource res = new ImgResource();
        //取出解密之后的AccountId
        string accountId;
        string catelog;//明文
        new ResourceBll().GetUploadInfo(account, weixinplat, out accountId, out catelog);
        if (string.IsNullOrEmpty(panorama))
        {
            res = imgFiles.UploadImg(img, accountId, imgFile.FileName, catelog);
        }
        else
        {
            res = imgFiles.UploadFullImg(img, accountId, imgFile.FileName, catelog);
        }
        Hashtable hash = new Hashtable();
        hash["error"] = 0;
        hash["url"] = res.ImgPath;
        hash["imgID"] = res.ImgId;
        context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
        context.Response.Write(JsonMapper.ToJson(hash));
        context.Response.End();
    }

    private void showError(string message)
    {
        Hashtable hash = new Hashtable();
        hash["error"] = 1;
        hash["message"] = message;
        context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
        context.Response.Write(JsonMapper.ToJson(hash));
        context.Response.End();
    }

    public bool IsReusable
    {
        get
        {
            return true;
        }
    }
}
