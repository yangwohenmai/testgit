using Qxun.App.Weistone.Services;
using Qxun.App.Weistone.ViewModels;
using Qxun.Core.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

/// <summary>
/// UploadHandler 的摘要说明
/// </summary>
public class UploadHandler : Handler
{

    public UploadConfig UploadConfig { get; private set; }
    public UploadResult Result { get; private set; }

    public UploadHandler(HttpContext context, UploadConfig config)
        : base(context)
    {
        this.UploadConfig = config;
        this.Result = new UploadResult() { State = UploadState.Unknown };
    }

    public override void Process()
    {
        HttpPostedFile file;
        string picName;
        Image img;
        try
        {
            file = Request.Files[UploadConfig.UploadFieldName];
            img = Image.FromStream(file.InputStream);
            picName = Request["name"];
            if (string.IsNullOrWhiteSpace(picName))
            {
                picName = file.FileName;
            }
            if (!UploadConfig.AllowExtensions.Contains("." + picName.ToLower().Split('.').Last()))
            {
                Result.State = UploadState.TypeNotAllow;
                Result.ErrorMessage = "图像格式不正确，只能上传gif,jpg,jpeg,png类型的图像";
                WriteResult();
                return;
            }
        }
        catch
        {
            Result.State = UploadState.Unknown;
            Result.ErrorMessage = "请选择图像";
            WriteResult();
            return;
        }
        ViewAccount account = Context.Session["user"] as ViewAccount;
        if (account == null)
        {
            Result.State = UploadState.Unknown;
            Result.ErrorMessage = "未知错误";
            WriteResult();
            return;
        }
        ViewWeixinPlat weixinplat = Context.Session["weixinplat"] as ViewWeixinPlat;
        if (weixinplat == null)
        {
            Result.State = UploadState.Unknown;
            Result.ErrorMessage = "未知错误";
            WriteResult();
            return;
        }

        string panorama = Context.Request["panorama"];
        ImgResource res;
        //取出解密之后的AccountId
        string accountId;
        string catelog;//明文
        new ResourceBll().GetUploadInfo(account, weixinplat, out accountId, out catelog);
        if (string.IsNullOrEmpty(panorama))
        {
            res = new ResourceBll().UploadImg(img, accountId, picName, catelog);
        }
        else
        {
            res = new ResourceBll().UploadFullImg(img, accountId, picName, catelog);
        }
        Response.ContentType = "text/html";
        Response.HeaderEncoding = Encoding.UTF8;
        if (res != null && !string.IsNullOrEmpty(res.ImgPath))
        {
            Result.State = UploadState.Success;
            Result.Url = res.ImgPath;
            WriteResult();
            return;
        }
        Result.State = UploadState.Unknown;
        Result.ErrorMessage = "请选择图像";
        WriteResult();
    }

    private void WriteResult()
    {
        this.WriteJson(new
        {
            state = GetStateMessage(Result.State),
            url = Result.Url,
            title = Result.OriginFileName,
            original = Result.OriginFileName,
            error = Result.ErrorMessage
        });
    }

    private string GetStateMessage(UploadState state)
    {
        switch (state)
        {
            case UploadState.Success:
                return "SUCCESS";
            case UploadState.FileAccessError:
                return "文件访问出错，请检查写入权限";
            case UploadState.SizeLimitExceed:
                return "文件大小超出服务器限制";
            case UploadState.TypeNotAllow:
                return "不允许的文件格式";
            case UploadState.NetworkError:
                return "网络错误"; 
        }
        return "未知错误";
    }

    private bool CheckFileType(string filename)
    {
        var fileExtension = Path.GetExtension(filename).ToLower();
        return UploadConfig.AllowExtensions.Select(x => x.ToLower()).Contains(fileExtension);
    }

    private bool CheckFileSize(int size)
    {
        return size < UploadConfig.SizeLimit;
    }
}

public class UploadConfig
{
    /// <summary>
    /// 文件命名规则
    /// </summary>
    public string PathFormat { get; set; }

    /// <summary>
    /// 上传表单域名称
    /// </summary>
    public string UploadFieldName { get; set; }

    /// <summary>
    /// 上传大小限制
    /// </summary>
    public int SizeLimit { get; set; }

    /// <summary>
    /// 上传允许的文件格式
    /// </summary>
    public string[] AllowExtensions { get; set; }

    /// <summary>
    /// 文件是否以 Base64 的形式上传
    /// </summary>
    public bool Base64 { get; set; }

    /// <summary>
    /// Base64 字符串所表示的文件名
    /// </summary>
    public string Base64Filename { get; set; }
}

public class UploadResult
{
    public UploadState State { get; set; }
    public string Url { get; set; }
    public string OriginFileName { get; set; }

    public string ErrorMessage { get; set; }
}

public enum UploadState
{
    Success = 0,
    SizeLimitExceed = -1,
    TypeNotAllow = -2,
    FileAccessError = -3,
    NetworkError = -4,
    Unknown = 1,
}

