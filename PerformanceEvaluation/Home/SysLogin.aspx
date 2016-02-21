<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysLogin.aspx.cs" Inherits="PerformanceEvaluation.PerformanceEvaluation.Home.SysLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>欢迎您登录绩效管理平台</title>
    <link href="../Style/style.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.7.1.min.js"></script>
    <script type="text/javascript">
        function SetFocus() {
            document.all.txtUserID.focus();
        }
        function keyLogin() {
            if (event.keyCode == 13)   //回车键的键值为13
            {
                $("#<%=btnLo.ClientID %>").click();//调用登录按钮的登录事件
            }
        }
    </script>
</head>
<body onload="SetFocus()" onkeydown="keyLogin();">
    <form id="Form1" method="post" runat="server">
    <div class="loginbox png">
	    <div class="txtbox">绩效管理平台</div>
        <div class="inputbox">
            <%--<div class="ipb png">
                <asp:TextBox ID="txtOrganID" runat="server" />
            </div>--%>
            <div class="ipc png">
                <%--<input type="text" value="用户名">--%>
                <asp:TextBox ID="txtUserID" runat="server"  />
            </div>
            <div class="ipd png">
                <%--<input type="text" value="密码">--%>
                <asp:TextBox ID="txtPwd" runat="server" TextMode="Password" />
            </div>
            <div>
                <div class="ipe png">
                    <%--<input type="text" value="验证码">--%>
                    <asp:TextBox ID="txtCode" runat="server" />
                </div>
                <%--<img src="../images/yz.jpg">--%>
                <img id="imgVerify" style="height:50px; width:100px; cursor:pointer;" align="absmiddle"  src="VerifyCode.aspx?" alt="看不清？点击更换" onclick="this.src=this.src+'?'" />
                <div style="clear:both;"></div>
            </div>
            <div class="ipbottom"><a href="#" runat="server" id="btnLogin" onserverclick="btnLogin_Click"><img src="../images/login_bottom.png"></a><asp:Label ID="lblMessage" runat="server" EnableViewState="False" /></div>
            <div class="forget"><%--<a href="FindIndex.aspx?Type=SysLogin" target="_blank">忘记密码了？</a>--%><asp:Button ID="btnLo" style="display:none;" runat="server" OnClick="btnLogin_Click" Text="" /></div>
        </div>
    </div>
    </form>
</body>
</html>
