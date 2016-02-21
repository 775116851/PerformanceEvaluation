<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="PerformanceEvaluation.PerformanceEvaluation.Home.ChangePassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>修改密码</title>
    <link href="../Style/reset.css" rel="stylesheet" />
    <link href="../Style/index.css" rel="stylesheet" />
    <style type="text/css">
        table {
            width: 100%;
        }

        #table1 tr th {
            width: 50%;
            min-width: 50%;
        }
    </style>
    <script src="../Scripts/jquery-1.7.1.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="Content">
            <div class="Information">
                <h3>修改密码</h3>
                <table id="table1">
                    <tbody>
                        <tr>
                            <th>旧密码：</th>
                            <td>
                               <input id="txtOldPwd" type="password" class="InputOne9" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <th>新密码：</th>
                            <td>
                                 <input id="txtNewPwd" type="password" class="InputOne9" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <th>确认新密码：</th>
                            <td>
                                 <input id="txtConfirmNewPwd" type="password" class="InputOne9" runat="server" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <ul class="ConditionsTwo ConditionsTwo-1">
                <li>
                   <asp:Button runat="server" id="btnSave" Text="保 存" CssClass="InputOne3" OnClick="btnSave_Click"/>
                </li>
                <li>
                    <input type="button" class="InputOne3" id="btnAdd" value="关 闭" onclick="window.close();" />
                </li>
            </ul>
            <div style="text-align:center; padding:10px 0px;">
                <asp:Label runat="server" ID="lblMessage"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
