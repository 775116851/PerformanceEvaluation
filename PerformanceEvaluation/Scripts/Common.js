window.onerror = function ()
{
    hideMask();
    $ && $(".mask") && $(".mask").hide();
}

function showMask()
{
    if (window.MaskInterval)
    {
        window.clearInterval(window.MaskInterval);
    }
    window.MaskInterval = window.setInterval(callChildWindowEvent, 1000);
    if ($)
    {
        var mask = $("#divMaskForChildWindow");
        if (mask.length > 0)
        {
            mask.show();
        }
        else
        {
            $("<div id='divMaskForChildWindow' class='mask2'></div>").appendTo($("body"));
        }
        $("#divMaskForChildWindow").height($("body").outerHeight());
    }
    if (window.parent != window)
    {
        window.parent.CurrentChild = window.CurrentChild;
        window.parent.showMask();
    }
}

function hideMask()
{
    $ && $(".mask2") && $(".mask2").hide();
    if (window.MaskInterval)
    {
        window.clearInterval(window.MaskInterval);
        window.MaskInterval = undefined;
    }
}

function callChildWindowEvent()
{
    if (window.CurrentChild && window.CurrentChild.closed)
    {
        hideMask();
        window.CurrentChild = undefined;
    }
}

function showAjaxMask()
{
    $ && $(".mask").height && $(".mask").height($("body").outerHeight());
    $ && $(".mask") && $(".mask").show();
    setTimeout(hideAjaxMask, 60000);
}

function hideAjaxMask()
{
    $ && $(".mask") && $(".mask").hide();
}

//弹出窗口
function openWindow(URL, Width, Height)
{
    window.CurrentChild = window.open(URL, '', 'width=' + Width + ',height=' + Height + ',resizable=1,scrollbars=1,status=no,toolbar=no,location=no,menu=no');
    showMask();
}

//弹出窗口，固定位置left=100,top=100
function openWindowS(URL, Width, Height)
{
    window.CurrentChild = window.open(URL, '', 'width=' + Width + ',height=' + Height + ',resizable=1,scrollbars=1,status=no,toolbar=no,location=no,menu=no,left=60,top=60');
    showMask();
}

//弹出窗口，固定位置left=100,top=100; 固定大小800;600;
function openWindowS2(URL)
{
    openWindowS(URL, 800, 600);
}
function openWindowS3(URL)
{
    openWindowS(URL, 640, 480);
}

//弹出窗口，全屏
function openWindowL(URL)
{

    window.CurrentChild = window.open(URL, '', 'width=screen.width,height=screen.height,scrollbars=1,status=no,toolbar=no,location=no,menu=no,resizable=1');
    showMask();
}

function openModalDialog(URL, width, height)
{
    if (document.all)//ie
    {
        window.showModalDialog(URL, '', "dialogHeight: " + height + "px; dialogWidth: " + width + "px;center:yes;status:no;help:no;");
    }
    else
    {

        window.CurrentChild = window.open(URL, '', 'width=' + width + ',height=' + height + ',scrollbars=1,status=no,toolbar=no,location=no,menu=no,resizable=1');
        showMask();
    }

}

function openModalDialog(URL, width, height, objects)
{
    if (document.all)//ie
    {
        window.showModalDialog(URL, objects, "dialogHeight: " + height + "px; dialogWidth: " + width + "px;center:yes;status:no;help:no;");
    }
    else
    {

        window.CurrentChild = window.open(URL, objects, 'width=' + width + ',height=' + height + ',scrollbars=1,status=no,toolbar=no,location=no,menu=no,resizable=1');
        showMask();
    }
}

function goToPage(url)
{
    window.location = url;
}
//是否是日期
function isDate(value)
{
    if (value == "") return true;

    var values = value.split("-");
    var y, m, d, maxDays = 31;
    if (values.length != 3) return false;

    y = values[0];
    m = values[1];
    d = values[2];

    if (isInteger(m) == false || isInteger(d) == false || isInteger(y) == false) return false;
    if (y.length < 4) return false;
    if ((m < 1) || (m > 12)) return false;
    if (m == 4 || m == 6 || m == 9 || m == 11) maxDays = 30;

    if (m == 2)
    {
        if (y % 4 > 0) maxDays = 28;
        else
            if (y % 100 == 0 && y % 400 > 0) maxDays = 28;
            else maxDays = 29;
    }

    if ((d < 1) || (d > maxDays)) return false;

    return true;

}

//是否为空
function isEmpty(value)
{
    if (value == null || value.toString() == "")
    {
        return true;
    }
    return false;
}
//是否是整数
function isInteger(value)
{
    var length = value.length;
    var index = 0;
    var NumCodes = '-0123456789';
    var bNum = true;

    if (value.indexOf('-') > 0)
    {
        bNum = false;
    }
    else
    {
        for (index = 0; index < length; index++)
        {
            if (NumCodes.indexOf(value.charAt(index)) == -1)
            {
                bNum = false;
            }
        }
    }

    return bNum;
}


//是否是小数
function isDecimal(value)
{
    var length = value.length;
    var index = 0;
    var NumCodes = '-0123456789.';
    var isOK = true;

    if (value.indexOf('-') > 0)
    {
        isOK = false;
    }
    else
    {
        for (index = 0; index < length; index++)
        {
            if (NumCodes.indexOf(value.charAt(index)) == -1)
            {
                isOK = false;
            }
        }
    }

    return isOK;
}

function getUrlParam(name)
{
    var reg = new RegExp("(^|\\?|&)" + name + "=([^&]*)(\\s|&|$)", "i");
    if (reg.test(window.location.href)) return unescape(RegExp.$2.replace(/\+/g, " "));
}

/**************************************************************************************/
/*************************************数字的验证*****************************************/
/**************************************************************************************/

/**
 * 检查输入的一串字符是否全部是数字
 * 输入:str  字符串
 * 返回:true 或 flase; true表示为数字
 */
function checkNum(str)
{
    return str.match(/\D/) == null;
}


/**
 * 检查输入的一串字符是否为小数
 * 输入:str  字符串
 * 返回:true 或 flase; true表示为小数
 */
function checkDecimal(str)
{
    if (str.match(/^-?\d+(\.\d+)?$/g) == null)
    {
        return false;
    }
    else
    {
        return true;
    }
}

/**
 * 检查输入的一串字符是否为整型数据
 * 输入:str  字符串
 * 返回:true 或 flase; true表示为小数
 */
function checkInteger(str)
{
    if (str.match(/^[-+]?\d*$/) == null)
    {
        return false;
    }
    else
    {
        return true;
    }
}

/**************************************************************************************/
/*************************************字符的验证*****************************************/
/**************************************************************************************/


/**
 * 检查输入的一串字符是否是字符
 * 输入:str  字符串
 * 返回:true 或 flase; true表示为全部为字符 不包含汉字
 */
function checkStr(str)
{
    if (/[^\x00-\xff]/g.test(str))
    {
        return false;
    }
    else
    {
        return true;
    }
}


/**
 * 检查输入的一串字符是否包含汉字
 * 输入:str  字符串
 * 返回:true 或 flase; true表示包含汉字
 */
function checkChinese(str)
{
    if (escape(str).indexOf("%u") != -1)
    {
        return true;
    }
    else
    {
        return false;
    }
}


/**
 * 检查输入的邮箱格式是否正确
 * 输入:str  字符串
 * 返回:true 或 flase; true表示格式正确
 */
function checkEmail(str)
{
    if (str.match(/[A-Za-z0-9_-]+[@](\S*)(net|com|cn|org|cc|tv|[0-9]{1,3})(\S*)/g) == null)
    {
        return false;
    }
    else
    {
        return true;
    }
}


/**
 * 检查输入的手机号码格式是否正确
 * 输入:str  字符串
 * 返回:true 或 flase; true表示格式正确
 */
function checkMobilePhone(str)
{
    if (str.match(/^(?:1[0-9]\d)-?\d{5}(\d{3}|\*{3})$/) == null)
    {
        return false;
    }
    else
    {
        return true;
    }
}


/**
 * 检查输入的固定电话号码是否正确
 * 输入:str  字符串
 * 返回:true 或 flase; true表示格式正确
 */
function checkTelephone(str)
{
    if (str.match(/^(([0\+]\d{2,3}-)?(0\d{2,3})-)(\d{7,8})(-(\d{3,}))?$/) == null)
    {
        return false;
    }
    else
    {
        return true;
    }
}

/**
 * 检查QQ的格式是否正确
 * 输入:str  字符串
 *  返回:true 或 flase; true表示格式正确
 */
function checkQQ(str)
{
    if (str.match(/^\d{5,10}$/) == null)
    {
        return false;
    }
    else
    {
        return true;
    }
}

/**
 * 检查输入的身份证号是否正确
 * 输入:str  字符串
 *  返回:true 或 flase; true表示格式正确
 */
function checkCard(str)
{
    //15位数身份证正则表达式
    var arg1 = /^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$/;
    //18位数身份证正则表达式
    var arg2 = /^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])((\d{4})|\d{3}[A-Z])$/;
    if (str.match(arg1) == null && str.match(arg2) == null)
    {
        return false;
    }
    else
    {
        return true;
    }
}

/**
 * 检查输入的IP地址是否正确
 * 输入:str  字符串
 *  返回:true 或 flase; true表示格式正确
 */
function checkIP(str)
{
    var arg = /^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$/;
    if (str.match(arg) == null)
    {
        return false;
    }
    else
    {
        return true;
    }
}

/**
 * 检查输入的URL地址是否正确
 * 输入:str  字符串
 *  返回:true 或 flase; true表示格式正确
 */
function checkURL(str)
{
    if (str.match(/(http[s]?|ftp):\/\/[^\/\.]+?\..+\w$/i) == null)
    {
        return false
    }
    else
    {
        return true;
    }
}

/**
 * 检查输入的字符是否具有特殊字符
 * 输入:str  字符串
 * 返回:true 或 flase; true表示包含特殊字符
 * 主要用于注册信息的时候验证
 */
function checkQuote(str)
{
    var items = new Array("~", "`", "!", "@", "#", "$", "%", "^", "&", "*", "{", "}", "[", "]", "(", ")");
    items.push(":", ";", "'", "|", "\\", "<", ">", "?", "/", "<<", ">>", "||", "//");
    items.push("admin", "administrators", "administrator", "管理员", "系统管理员");
    items.push("select", "delete", "update", "insert", "create", "drop", "alter", "trancate");
    str = str.toLowerCase();
    for (var i = 0; i < items.length; i++)
    {
        if (str.indexOf(items[i]) >= 0)
        {
            return true;
        }
    }
    return false;
}


/**************************************************************************************/
/*************************************时间的验证*****************************************/
/**************************************************************************************/

/**
 * 检查日期格式是否正确
 * 输入:str  字符串
 * 返回:true 或 flase; true表示格式正确
 * 注意：此处不能验证中文日期格式
 * 验证短日期（2007-06-05）
 */
function checkDate(str)
{
    //var value=str.match(/((^((1[8-9]\d{2})|([2-9]\d{3}))(-)(10|12|0?[13578])(-)(3[01]|[12][0-9]|0?[1-9])$)|(^((1[8-9]\d{2})|([2-9]\d{3}))(-)(11|0?[469])(-)(30|[12][0-9]|0?[1-9])$)|(^((1[8-9]\d{2})|([2-9]\d{3}))(-)(0?2)(-)(2[0-8]|1[0-9]|0?[1-9])$)|(^([2468][048]00)(-)(0?2)(-)(29)$)|(^([3579][26]00)(-)(0?2)(-)(29)$)|(^([1][89][0][48])(-)(0?2)(-)(29)$)|(^([2-9][0-9][0][48])(-)(0?2)(-)(29)$)|(^([1][89][2468][048])(-)(0?2)(-)(29)$)|(^([2-9][0-9][2468][048])(-)(0?2)(-)(29)$)|(^([1][89][13579][26])(-)(0?2)(-)(29)$)|(^([2-9][0-9][13579][26])(-)(0?2)(-)(29)$))/);
    var value = str.match(/^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2})$/);
    if (value == null)
    {
        return false;
    }
    else
    {
        var date = new Date(value[1], value[3] - 1, value[4]);
        return (date.getFullYear() == value[1] && (date.getMonth() + 1) == value[3] && date.getDate() == value[4]);
    }
}

/**
 * 检查时间格式是否正确
 * 输入:str  字符串
 * 返回:true 或 flase; true表示格式正确
 * 验证时间(10:57:10)
 */
function checkTime(str)
{
    var value = str.match(/^(\d{1,2})(:)?(\d{1,2})\2(\d{1,2})$/)
    if (value == null)
    {
        return false;
    }
    else
    {
        if (value[1] > 24 || value[3] > 60 || value[4] > 60)
        {
            return false
        }
        else
        {
            return true;
        }
    }
}

/**
 * 检查全日期时间格式是否正确
 * 输入:str  字符串
 * 返回:true 或 flase; true表示格式正确
 * (2007-06-05 10:57:10)
 */
function checkFullTime(str)
{
    //var value = str.match(/^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2}) (\d{1,2}):(\d{1,2}):(\d{1,2})$/);
    var value = str.match(/^(?:19|20)[0-9][0-9]-(?:(?:0[1-9])|(?:1[0-2]))-(?:(?:[0-2][1-9])|(?:[1-3][0-1])) (?:(?:[0-2][0-3])|(?:[0-1][0-9])):[0-5][0-9]:[0-5][0-9]$/);
    if (value == null)
    {
        return false;
    }
    else
    {
        //var date = new Date(checkFullTime[1], checkFullTime[3] - 1, checkFullTime[4], checkFullTime[5], checkFullTime[6], checkFullTime[7]);
        //return (date.getFullYear() == value[1] && (date.getMonth() + 1) == value[3] && date.getDate() == value[4] && date.getHours() == value[5] && date.getMinutes() == value[6] && date.getSeconds() == value[7]);
        return true;
    }

}




/**************************************************************************************/
/************************************身份证号码的验证*************************************/
/**************************************************************************************/

/**  
 * 身份证15位编码规则：dddddd yymmdd xx p
 * dddddd：地区码
 * yymmdd: 出生年月日
 * xx: 顺序类编码，无法确定
 * p: 性别，奇数为男，偶数为女
 * <p />
 * 身份证18位编码规则：dddddd yyyymmdd xxx y
 * dddddd：地区码
 * yyyymmdd: 出生年月日
 * xxx:顺序类编码，无法确定，奇数为男，偶数为女
 * y: 校验码，该位数值可通过前17位计算获得
 * <p />
 * 18位号码加权因子为(从右到左) Wi = [ 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2,1 ]
 * 验证位 Y = [ 1, 0, 10, 9, 8, 7, 6, 5, 4, 3, 2 ]
 * 校验位计算公式：Y_P = mod( ∑(Ai×Wi),11 )
 * i为身份证号码从右往左数的 2...18 位; Y_P为脚丫校验码所在校验码数组位置
 *
 */
var Wi = [7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2, 1];// 加权因子   
var ValideCode = [1, 0, 10, 9, 8, 7, 6, 5, 4, 3, 2];// 身份证验证位值.10代表X   
function IdCardValidate(idCard)
{
    idCard = trim(idCard.replace(/ /g, ""));
    if (idCard.length == 15)
    {
        return isValidityBrithBy15IdCard(idCard);
    }
    else
        if (idCard.length == 18)
        {
            var a_idCard = idCard.split("");// 得到身份证数组   
            if (isValidityBrithBy18IdCard(idCard) && isTrueValidateCodeBy18IdCard(a_idCard))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
}

/**  
 * 判断身份证号码为18位时最后的验证位是否正确
 * @param a_idCard 身份证号码数组
 * @return
 */
function isTrueValidateCodeBy18IdCard(a_idCard)
{
    var sum = 0; // 声明加权求和变量   
    if (a_idCard[17].toLowerCase() == 'x')
    {
        a_idCard[17] = 10;// 将最后位为x的验证码替换为10方便后续操作   
    }
    for (var i = 0; i < 17; i++)
    {
        sum += Wi[i] * a_idCard[i];// 加权求和   
    }
    valCodePosition = sum % 11;// 得到验证码所位置   
    if (a_idCard[17] == ValideCode[valCodePosition])
    {
        return true;
    }
    else
    {
        return false;
    }
}

/**  
 * 通过身份证判断是男是女
 * @param idCard 15/18位身份证号码
 * @return 'female'-女、'male'-男
 */
function maleOrFemalByIdCard(idCard)
{
    idCard = trim(idCard.replace(/ /g, ""));// 对身份证号码做处理。包括字符间有空格。   
    if (idCard.length == 15)
    {
        if (idCard.substring(14, 15) % 2 == 0)
        {
            return 'female';
        }
        else
        {
            return 'male';
        }
    }
    else
        if (idCard.length == 18)
        {
            if (idCard.substring(14, 17) % 2 == 0)
            {
                return 'female';
            }
            else
            {
                return 'male';
            }
        }
        else
        {
            return null;
        }
}

/**  
 * 验证18位数身份证号码中的生日是否是有效生日
 * @param idCard 18位书身份证字符串
 * @return
 */
function isValidityBrithBy18IdCard(idCard18)
{
    var year = idCard18.substring(6, 10);
    var month = idCard18.substring(10, 12);
    var day = idCard18.substring(12, 14);
    var temp_date = new Date(year, parseFloat(month) - 1, parseFloat(day));
    // 这里用getFullYear()获取年份，避免千年虫问题   
    if (temp_date.getFullYear() != parseFloat(year) ||
    temp_date.getMonth() != parseFloat(month) - 1 ||
    temp_date.getDate() != parseFloat(day))
    {
        return false;
    }
    else
    {
        return true;
    }
}

/**  
 * 验证15位数身份证号码中的生日是否是有效生日
 * @param idCard15 15位书身份证字符串
 * @return
 */
function isValidityBrithBy15IdCard(idCard15)
{
    var year = idCard15.substring(6, 8);
    var month = idCard15.substring(8, 10);
    var day = idCard15.substring(10, 12);
    var temp_date = new Date(year, parseFloat(month) - 1, parseFloat(day));
    // 对于老身份证中的你年龄则不需考虑千年虫问题而使用getYear()方法   
    if (temp_date.getYear() != parseFloat(year) ||
    temp_date.getMonth() != parseFloat(month) - 1 ||
    temp_date.getDate() != parseFloat(day))
    {
        return false;
    }
    else
    {
        return true;
    }
}

//去掉字符串头尾空格   
if (typeof (String.prototype.trim) !== "function")
{
    String.prototype.trim = function ()
    {
        return this.replace(/(^\s*)|(\s*$)/g, "");
    }
}

function checkLength(text, max)
{
    if (text.trim() && text.trim().length <= max)
        return true;
    else
        return false;
}

/////////输入事件相关:onkeyup,onpaste等
///禁止输入非数字
function noEnterNonNumer(obj)
{
    obj.value = obj.value.trim().replace(/[\D]/g, '')
}
///字符数大于上限则禁止继续输入
function noEnterAnyMore(obj, max)
{
    if (!checkLength(obj.value.trim(), max))
        obj.value = obj.value.trim().substr(0, max);
}
///自定义正则表达式的禁止输入
function noEnterByExp(obj, exp)
{
    obj.value = obj.value.trim().replace(exp, '')
    return;
}
function noEnterNonDecimal(obj)
{
    //先把非数字的都替换掉，除了数字和.
    obj.value = obj.value.replace(/[^\d.]/g, "");
    //必须保证第一个为数字而不是.
    obj.value = obj.value.replace(/^\./g, "");
    //保证只有出现一个.而没有多个.
    obj.value = obj.value.replace(/\.{2,}/g, ".");
    //保证.只出现一次，而不能出现两次以上
    obj.value = obj.value.replace(".", "$#$").replace(/\./g, "").replace("$#$", ".");
}