 function getUrlByImgId(id,className,url) {
     if (IsIdHasValue(id)) {
        jQuery.ajax({
	        url: url,
	        data: {
	            imgId: id
	        },
	        type: "get",
	        dataType: "json",
	        cache: false,
	        async: false,
	        success: function (jData) {
	            if (jData && IsIdHasValue(jData.ResourceId)) {
	                jQuery("." + className + "").attr("src", jData.ResourceUrl);
	            } else {
	                jQuery("." + className + "").attr("src", "");
	            }
	        }
	    });
    } else {
	    jQuery("." + className + "").attr("src", "");
    }
};