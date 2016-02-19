
//弹出窗口
function openWindow(URL,Width,Height) { 
window.open(URL,'','width='+Width+',height='+Height+',resizable=1,scrollbars=0,status=no,toolbar=no,location=no,menu=no');
}

//弹出窗口，固定位置left=100,top=100
function openWindowS(URL,Width,Height) { 
window.open(URL,'','width='+Width+',height='+Height+',resizable=1,scrollbars=1,status=no,toolbar=no,location=no,menu=no,left=60,top=60');
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
	window.open(URL,'','width=screen.width,height=screen.height,scrollbars=1,status=no,toolbar=no,location=no,menu=no,resizable=1');
}

function openModalDialog(URL,width,height)
{
   window.showModalDialog(URL,'','width='+width+',height='+height+',scrollbars=1,status=no,toolbar=no,location=no,menu=no,resizable=1');

}

function openModalDialog(URL, width, height ,title) {

    window.showModalDialog(URL, title, 'width=' + width + ',height=' + height + ',scrollbars=1,status=no,toolbar=no,location=no,menu=no,resizable=1');

}

function goToPage(url)
{
   window.location=url;
}
//是否是日期
function isDate(value)
{
    if (value=="") return true;
    
    var values=value.split("-");
    var y,m,d,maxDays=31;
    if (values.length!=3) return false;            
    
    y=values[0];
    m=values[1];
    d=values[2];             

   if (isInteger(m)==false || isInteger(d)==false || isInteger(y)==false)  return false;             
   if (y.length < 4)  return false;  
   if ((m<1) || (m>12))  return false; 
   if (m==4 || m==6 || m==9 || m==11) maxDays = 30;  
   
   if (m==2) 
   {  
        if (y % 4 > 0)   maxDays = 28;  
        else 
            if (y % 100 == 0 && y % 400 > 0) maxDays = 28;  
            else maxDays = 29;  
   }  
   
   if  ((d<1) || (d>maxDays))  return false;   
   
   return true;                
    
}

//是否为空
function isEmpty(value) 
{           
    if(value == null || value.toString() == "") 
    {			   	            
        return true;
    }
        return false;
}
//是否是整数
function isInteger(value)
{   
    var length=value.length;
    var index=0;
    var NumCodes='-0123456789';
    var bNum=true;
    
    if(value.indexOf('-')>0)  
    {
         bNum=false;
    }
    else
    {    
         for(index=0;index<length;index++)
         {
            if (NumCodes.indexOf(value.charAt(index))==-1)
	         {
 		          bNum=false;
	         }      
         }
    }
          
    return bNum ;	
}   


//是否是小数
function isDecimal(value)
{   
    var length=value.length;
    var index=0;
    var NumCodes='-0123456789.';
    var isOK=true;
    
    if(value.indexOf('-')>0)  
    {
         isOK=false;
    }
    else
    {    
         for(index=0;index<length;index++)
         {
            if (NumCodes.indexOf(value.charAt(index))==-1)
	         {
 		          isOK=false;
	         }      
         }
    }
          
    return isOK ;	
}

function fillSelect(data, controlName, addBlank) {
    try {
        var val = $(controlName).val();
        $(controlName).empty();

        if (addBlank) {
            $(controlName).prepend("<option value='-9999'>--请选择--</option>");
        }
        for (var i = 0; i < data.length; i++) {
            $(controlName).append("<option value='" + data[i].Value + "'>" + data[i].Name + "</option>");
        }
        if (val) {
            $(controlName).val(val);
        }
    }
    catch (f) {
        throw "绑定控件出错";
    }
}