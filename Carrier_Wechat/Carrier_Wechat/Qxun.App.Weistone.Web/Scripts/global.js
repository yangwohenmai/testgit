
//判断ID字符串是否大于0
function IsIdHasValue(id)
{
    return !(id === null) && id != "diUyZkRDZXFiRUxaYUZSb2Q1U1hDWDROM3ZhUCUyZk5pNm5Z" && id != "diUyZkRDZXFiRUxaYUZSb2Q1U1hDWDRHdE1mdUowM3EwQg.." && id != ""
}
//验证url
function RegUrl(value) {
    return /^(https?|ftp):\/\/(((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:)*@)?(((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]))|((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?)(:\d*)?)(\/((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)+(\/(([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)*)*)?)?(\?((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|[\uE000-\uF8FF]|\/|\?)*)?(\#((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|\/|\?)*)?$/i.test(value);
}



var count = 0;

function QueryCondition() {
    this.SortType = 0;
    this.SortField = "";
    this.Condition = [];
    this.Page = 1;
}


//用户
function AccountEntity() {
    this.AccountId = '';
    this.LoginName = '';
    this.NickName = '';
    this.Password = '';
    this.Email = '';
    this.QQ = null;
    this.Phone = null;
    this.Provice = '';
    this.City = '';
    this.EndTime = '';
}

//公共帐号
function WeixinPlatEntity() {
    this.WeixinPlatId = '';
    this.WeixinName = '';
    this.WeixinOriginalId = '';
    this.WeixinNum = '';
    this.Token = '';
    this.Url = '';
    this.Email = '';
    this.CreateTime = '1990-1-1';
    this.PicUrl = '';
}



/// 规则
function PlatRuleEntity1() {
    this.PlatRuleId = '';
    this.KeyWord = '';
    this.IsMatched = false;
    this.ResponseType = '';
    this.ResponseId = '';
    this.WeixinPlatId = '';
}

/// 单图文回复
function SinglePicTextEntity1() {
    this.SinglePicTextId = '';
    this.createTime = '1990-1-1';
    this.Title = '';
    this.LinkUrlOrId = '';
    this.LinkType = '';
    //this.ResourceResourceId = '';
}

/// 多图文回复
function MultiplePicTextEntity1() {
    this.MultiplePicTextId = '';
    this.createTime = '1990-1-1';
    this.Title = '';
    this.LinkUrlOrId = '';
    this.LinkType = '';
    this.ParentId = '';
    this.ResourceResourceId = '';
}

/// 微网站
function TinyWebSiteEntity1() {
    this.TinyWebSiteId = '';
    this.Content = '';
}

/// 资源(微网站资源)
function ResourceEntities1() {
    this.ResourceId = '';
    this.ResourceType = '';
    this.ResourceUrl = '';
    this.createTime = '1990-1-1';
    this.IsInnerResource = false;
}
var arrayObj = new Array();


/// 微网站资源
function TinyWebSiteResource1() {
    this.ResourceEntities = arrayObj;
    this.TinyWebSiteEntity = TinyWebSiteEntity1();
}

/// 单图文/资源
function SinglePicTextResource1() {
    this.SinglePicTextEntity = SinglePicTextEntity1();
    this.TinyWebSiteResource = TinyWebSiteResource1();
}

//单图文内容
function SinglePicTextPlatRuleEntity1() {
    this.PlatRule = PlatRuleEntity1();
    this.SinglePicTextResource = SinglePicTextResource1();
}

function MultiplePicTextResource1() {
    this.MultiplePicTextEntity = MultiplePicTextEntity1();
    this.TinyWebSiteResource = TinyWebSiteResource1();
}

//多图文内容
function MultiplePicTextPlatRuleEntity1() {
    this.PlatKeyWordEntity = PlatRuleEntity1();
    this.MultiplePicTextResources = arrayObj;
}

//自定义菜单
function ButtonEntity() {
    this.ButtonGroupId = '';
    this.ParentId = '';
    this.ButtonName = '';
    this.OrderBy = 0;
    this.WeixinPlatId = '';
}

var arrayButton = new Array();
function ButtonGroupItem() {
    this.ButtonGroupEntity = ButtonEntity();
    this.ChildrenButtonGroup = arrayButton;
}

//楼盘介绍
function HouseEstateEntity() {
    this.HouseEstateId = '';
    this.Title = '';
    this.AparmentTopPicResource = '';
    this.Longitude = 0;
    this.Latitude = 0;
    this.HouseDesc = '';
    this.ProjectDesc = '';
    this.TrafficDesc = '';
    this.EstateTopPicResource = '';
    this.CreateTime = '1990-1-1';
    this.AreaAddress = '';
    this.HomePagePicResources = '';
    this.AppointmentId = null;
}

//户型
function HouseApartMentEntity() {
    this.HouseApartmentId = '';
    this.ApartmentName = '';
    this.Floor = 0;
    this.Area = 0;
    this.RoomNum = 0;
    this.HallNum = 0;
    this.OrderBy = 0;
    this.AparmentDesc = '';
    this.PicResourceIds = '';
    this.SubHouseEstateId = '';
    this.WeixinPlatId = '';
}

//专家印象
function HouseExpertCommentsEntity() {
    this.HouseExpertCommentsId = '';
    this.ExpertName = '';
    this.ExpertDuty = '';
    this.ExpertPicResource = '';
    this.ExpertDesc = '';
    this.Comments = '';
    this.CreateTime = '1990-1-1';
    this.OrderBy = 0;
    this.WeixinPlatId = '';
    this.Title = '';
}

//房友印象
function HouseImpressionEntity() {
    this.HouseImpressionId = '';
    this.Title = '';
    this.OrderBy = 0;
    this.ImpressionNum = '';
    this.IsDisplay = false;
    this.WeixinPlatId = '';
    this.CreateTime = '1990-1-1';
}

//相册
function HousePhotoEntity() {
    this.HousePhotoId = '';
    this.PhotoName = '';
    this.OrderBy = 0;
    this.PhotoDesc = '';
    this.PhotoResourceIds = '';
    this.WeixinPlatId = '';
    this.CreateTime = '1990-1-1';
}

//子楼盘
function SubHouseEstateEntity() {
    this.SubHouseEstateId = '';
    this.SubHouseName = '';
    this.OrderBy = 0;
    this.SubDesc = '';
    this.WeixinPlatId = '';
}

//全景图

function HouseApartmentPanoramaEntity() {
    this.HouseApartmentPanoramaId = '';
    this.PanoramaResourceIds = '';
    this.HouseApartmentHouseApartmentId = '';
    this.PanoramaTitle = '';
}

//预定
function ReservationEntity() {
    this.ReservationId = '';
    this.Title = '';
    this.ReservaAddress = '';
    this.suggestId = '';
    this.lng = 0.0;
    this.lat = 0.0;
    this.Phone = '';
    this.OrderTopImg = '';
    this.OrderDetail = '';
    this.RenameOrder = '';
    this.RenameOrderDesc = '';
    this.RenameOrderPhone = '';
    this.LimitType = '';
    this.LimitContent = '';
    this.LinkManName = '';
    this.LinkManContent = '';
    this.LinkPhoneName = '';
    this.LinkPhoneContent = '';
    this.IsDisplayLinkMan = false;
    this.IsDisplayLinkPhone = false;
    this.WeixinPlatId = '';
    this.ReservationState = 1;
    this.FormCustomCode = '';
    this.ListCustomCode = '';
    this.VoteResultCustomCode = '';
    this.ReservationContentEntities = [];
    this.ReservationType = 1;
    this.UserLimitTimes = 0;
    this.IsAutoSuccess = false;
}
//预定 订单内容设置
function ReservationContentEntity() {
    this.ReservationContentId = '';
    this.Name = '';
    this.Content = '';
    this.IsRequired = false;
    this.Order = 0;
    this.InputType = '';
    this.ReservationId = '';
}
function Select() {
    this.Id = "";
    this.Value = "";
}

function LinkReplyResult() {
    this.LinkType = "";
    this.LinkResult = "";
}

function AuthorityConfigEntity() {
    this.ControllerConfig = [];
    this.ActionConfig = [];
}

//积分策略
function MembershipIntegralEntity() {
    this.MembershipIntegralId = '';
    this.CardInstruction = '';
    this.IntegralInstruction = '';
    this.NextClearTime = '';
    this.ClearTimes = '';
    this.ClearPeriod = '';
    this.WeixinPlatId = '';
    this.SignedOnContinueDays = 0;
    this.MembershipIntegralItemEntitities = [];
}
//策略名称项
function ViewMembershipIntegralItemSet() {
    this.MembershipIntegralItemSetId = '';
    this.IntegralType = '';
    this.Integral = 0;
    this.MembershipIntegralId = '';
    this.IntegralUpline = 0;
}