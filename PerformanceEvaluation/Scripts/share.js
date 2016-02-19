
//var BasePath = ((getRelativePath() == "." || getRelativePath() == "") ? ".." : getRelativePath());
var BasePath = (getRelativePath() == "." ? "" : getRelativePath());
function AjaxWebService(Method, Parm, CallBackMethod, url) {
    //alert(BasePath + "/WebServices/" + url + "/" + Method);
    if (url == "" || typeof (url) == "undefined") url = "GeneralSearch.asmx";
    var CallBackResult = $.ajax({
        type: "POST",
        contentType: "application/json",
        url: BasePath + "/WebServices/" + url + "/" + Method + "?random=" + Math.random(),
        data: Parm,
        dataType: 'json',
        success: function (result) {
            try {
                CallBackMethod(result.d);
            }
            catch (e) {
            }
        },
        error: function (result) {
            //alert(result.responseText);
            //alert("WebService Error");
        },
        complete: function (result) {
            //alert(result.responseText);
        }
    });  
}

function AjaxCallBack(Method, Parm, Sync, Tips) {
    if (typeof (Sync) == "undefined") Sync = false; 
    var CallBackResult = $.ajax({
        async: Sync,
        url: getPageName() + "/" + Method + "?random=" + Math.random(),
        type: 'POST',
        dataType: 'json',
        data: Parm,
        contentType: "application/json; charset=utf-8",
        timeout: 10000,
        beforeSend: function(XMLHttpRequest) {
            if (Tips != "" && typeof (Tips) != "undefined") {
                $("#LoadingPanel").html(Tips);
            }
            else {
                $("#LoadingPanel").html("dealing...");
            }
            $("#LoadingPanel").css("display", "");
        },
        success: function(data) {
            try {
                AjaxCallBackSuccess(Method, data.d);
            }
            catch (e) {
            }
        },
        complete: function(XMLHttpRequest, textStatus) {
            $("#LoadingPanel").css("display", "none");
        },
        error: function(a) {
            alert(a.responseText);
            alert("CallBack Error");
        }

    });
    if (!Sync) {
        return eval("(" + CallBackResult.responseText + ")").d
    }
}

function getPageName() { 
    var thisHREF = document.location.href;
    var tmpUPage = thisHREF.split("?");
    var thisUPage = tmpUPage[0];
    var arr = thisUPage.split('/');
    return arr[arr.length-1];
}

function getRelativePath() {
    //只放在本js文件中 只有在加裁到这个文件时t[t.length-1]指向的才是当前的script标签
    var System = {};
    var t = document.getElementsByTagName("SCRIPT");
    t = (System.scriptElement = t[t.length - 1]).src.replace(/\\/g, "/"); //关键语句
    System.path = (t.lastIndexOf("/") < 0) ? "." : (t.substring(0, t.lastIndexOf("/"))).substring(0, (t.substring(0, t.lastIndexOf("/"))).lastIndexOf("/"));
    return System.path;
}

String.prototype.Trans = function() { return this.replace(/\"/ig, "\\\"").replace(/\'/ig, "\\\'"); };