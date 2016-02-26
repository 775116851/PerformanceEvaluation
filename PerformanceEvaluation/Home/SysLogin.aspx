<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysLogin.aspx.cs" Inherits="PerformanceEvaluation.PerformanceEvaluation.Home.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>欢迎您登录通联研发中心绩效管理系统</title>
    <%--<link href="../Style/style.css" rel="stylesheet" />--%>
    <link href="../Style/login.css" rel="stylesheet" />
    <script src="../Scripts/IPP.js"></script>
    <script src="../Scripts/jquery-1.7.1.min.js"></script>
    <script type="text/javascript">
        function SetFocus() {
            document.all.txtUserID.focus();
        }
        function keyLogin() {
            if (event.keyCode == 13)   //回车键的键值为13
            {
                
            }
        }
    </script>
</head>
<body onload="SetFocus()">
    <form id="Form1" method="post" runat="server">
        <div id="wrap">
          <div id="Login">
            <div class="box">
              <p class="a01"><img src="../images/loginImg01.jpg" alt="通联研发中心绩效管理系统" /></p>
              <div class="inBox inBox-1">
                <dl class="L_Name">
                  <dt>用户名</dt>
                  <dd>
                      <asp:TextBox ID="txtUserID" runat="server" CssClass="LoginInput01" />
                  </dd>
                </dl>
                <dl class="L_Password">
                  <dt>密&nbsp;&nbsp;&nbsp;码</dt>
                  <dd>
                     <asp:TextBox ID="txtPwd" runat="server"  CssClass="LoginInput01" TextMode="Password" />
                  </dd>
                </dl>
                <dl class="NoBorder">
                  <dt>验证码</dt>
                  <dd>
                       <asp:TextBox ID="txtCode" runat="server" CssClass="LoginInput02" />
                  </dd>
                  <dd class="L_Yzm">
                      <img id="imgVerify" style="height:30px; width:93px; cursor:pointer;" align="absmiddle"  src="VerifyCode.aspx?" alt="看不清？点击更换" onclick="this.src=this.src+'?'" />
                  </dd>
                </dl>
              </div>
              <div class="prompt prompt-1">
                <%-- (<%=SSOCmn.cmn.AppConfig.ServerName %>)&#160;&#160;--%><asp:Label ID="lblMessage" runat="server" EnableViewState="False" />&#160;
            
              </div>
              <dl class="btn-1 btn-2">
                <dt>
       
                    <asp:Button ID="btnLogin" runat="server" CssClass="InputOne4"  Text="登录" OnClick="btnLogin_Click" />
                </dt>
       
                <dd></dd>
              </dl>

            </div>
            <!-- #main--> 
          </div>
          <!--container--> 
        </div>
        <!--#wrap-->
    </form>
</body>
</html>
