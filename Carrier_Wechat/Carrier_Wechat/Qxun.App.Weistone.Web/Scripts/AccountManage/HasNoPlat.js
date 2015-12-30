var platId = jQuery.query.get("plat");
if (IsIdHasValue(platId)) {
}
else {
    // 没有平台就把上方的其他的链接设置成不能点
    $("#homeBigTitle").parent().attr("href", "#");
    $("#homeBigTitle").attr("id", "setBigTitle");
    $("#setUpBigTitle").parent().attr("href", "#");
    $("#setUpBigTitle").attr("id", "setBigTitle");
    $("#functionBigTitle").parent().attr("href", "#");
    $("#functionBigTitle").attr("id", "setBigTitle");
    $("#manageBigTitle").parent().attr("href", "#");
    $("#manageBigTitle").attr("id", "setBigTitle");
}