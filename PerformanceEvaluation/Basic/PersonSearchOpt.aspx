<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="PersonSearchOpt.aspx.cs" Inherits="PerformanceEvaluation.PerformanceEvaluation.Basic.PersonSearchOpt" %>
<%@ Register Src="~/UC/DropDown.ascx" TagPrefix="uc" TagName="DropDown" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>新增/修改员工信息</title>
    <link href="../Style/index.css" rel="stylesheet" />
    <link href="../Style/reset.css" rel="stylesheet" />
    <script src="../Scripts/json.js"></script>
    <script src="../Scripts/Common.js"></script>
    <script src="../Scripts/share.js"></script>
    <script src="../Scripts/jquery-1.7.1.min.js"></script>
    <script src="../Scripts/jquery-ui-1.8.20.min.js"></script>
    <script src="../Scripts/DatePicker/WdatePicker.js"></script>
    <script src="../Scripts/jquery-ui-1.10.4.custom.min.js"></script>
    <link href="../Style/jquery-ui-1.10.4.custom.min.css" rel="stylesheet" />
    <style type="text/css">
        table {
            width: 100%;
        }

        #table1 tr th {
            width: 100px;
            min-width: 100px;
        }
        .ff {
            word-break:keep-all;
            white-space:nowrap;
            overflow:hidden;
            text-overflow:ellipsis;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="Content">
            <div class="Information">
                <h3>新增/修改员工信息</h3>
                <table id="Table1" cellspacing="1" cellpadding="2" border="0" align="center" width="80%">
                    <tbody>
                        <tr>
                            <th><span>*</span>姓名</th>
                            <td><asp:TextBox ID="txtPersonName" runat="server" class="InputOne9" MaxLength="20"></asp:TextBox></td>
                            <th><span>*</span>所属机构</th>
                            <td><asp:DropDownList ID="ddlOrgan" sname="forminput" runat="server"></asp:DropDownList>
                                <asp:DropDownList ID="ddlClass" sname="forminput" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th><span>*</span>员工类型</th>
                            <td><asp:DropDownList ID="ddlPersonType" sname="forminput" runat="server"></asp:DropDownList></td>
                            <th>上级编号</th>
                            <td>
                                <input type="text" style="width:150px;" runat="server" class="InputOne9" id="txtParentPersonName" readonly="true" />
                                <asp:HiddenField ID="hdnParentPersonSysNo" runat="server" />
			                    <input type="button" id="lnkSearch" runat="server" value="查询..." />  
                                <%--<asp:Label ID="lblParentPersonSysNo" runat="server"></asp:Label>--%>
                            </td>
                        </tr>
                        <tr>
                            <th>技能类别</th>
                            <td><asp:TextBox ID="txtSkillCategory" runat="server" class="InputOne9" MaxLength="20"></asp:TextBox></td>
                            <th>性别</th>
                            <td><uc:DropDown ID="ddlGender" runat="server" /></td>
                        </tr>
                        <tr>
                            <th>手机号</th>
                            <td><asp:TextBox ID="txtMobilePhone" runat="server" class="InputOne9" MaxLength="20"></asp:TextBox></td>
                            <th>电话号码</th>
                            <td><asp:TextBox ID="txtTelPhone" runat="server" class="InputOne9" MaxLength="20"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th>QQ号</th>
                            <td><asp:TextBox ID="txtQQ" runat="server" class="InputOne9" MaxLength="20"></asp:TextBox></td>
                            <th>邮件</th>
                            <td><asp:TextBox ID="txtEmail" runat="server" class="InputOne9" MaxLength="20"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th>出生日期</th>
                            <td><input autocomplete="off" type="text" id="textBirthDate" runat="server" class="InputOne9" onclick="WdatePicker()" /></td>
                            <th>入职日期</th>
                            <td><input autocomplete="off" type="text" id="textEntryDate" runat="server" class="InputOne9" onclick="WdatePicker()" /></td>
                        </tr>
                        <tr>
                            <th><span>*</span>是否允许登录</th>
                            <td><uc:DropDown ID="ddlIsLogin" runat="server" /></td>
                            <th><span>*</span>状态</th>
                            <td><uc:DropDown ID="ddlStatus" runat="server" /></td>
                        </tr>
                        <tr>
                            <th>备注</th>
                            <td colspan="3"><asp:TextBox ID="txtNote" runat="server" class="InputOne9" Rows="5" TextMode="MultiLine" Height="72px" Width="660px" ></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th></th>
                            <td colspan="3">
                                <asp:Label ID="lblMsg" runat="server" EnableViewState="False"></asp:Label>
                            </td>
                        </tr>
                    </tbody>
                </table>

                <ul class="ConditionsTwo ConditionsTwo-4">
                    <li>
                        <asp:Button ID="btnSave" runat="server" Text="保 存"  Style="width: 70px;" class="InputOne3"></asp:Button>
                    </li>
                </ul>

            </div>
        </div>
        <div id="divFrame" style="display: none; width: 830px; height: 500px"></div>
    </form>
</body>
</html>
<script type="text/javascript">
    var PersonInfo = [];
    $(function () {
        $("#<%=ddlOrgan.ClientID %>").change(function () {
            var pOrgan = $("#<%=ddlOrgan.ClientID %>").val();
            var pClass = $("#<%=ddlClass.ClientID %>").val();
            $("#<%=ddlClass.ClientID%>").empty();
            $("#<%=ddlClass.ClientID%>").append(("<option value='" +<%=PerformanceEvaluation.Cmn.AppConst.IntNull%> +"'>--- 全部 ---</option>"));
            if (pOrgan != "<%=PerformanceEvaluation.Cmn.AppConst.IntNull%>") {
                AjaxWebService("GetClassList", "{organSysNo:'" + pOrgan + "'}", OrganChange_Callback);
            }
        });

        function OrganChange_Callback(result) {
            for (var i = 0; i < result.length; i++) {
                $("#<%=ddlClass.ClientID%>").append(("<option value='" + result[i][0] + "'>" + result[i][1] + "</option>"));
            }
        }

        $("#lnkSearch").click(function () {
            var srcUrl = "";
            srcUrl = "../UC/PersonSelect.aspx?random=" + Math.random();
            var html = "<iframe  id='frame'  style='width:800px;height:500px' ></iframe>";
            $("#divFrame").html(html);
            $("#frame").attr("src", srcUrl);
            $("#divFrame").dialog({
                autoOpen: true, modal: true, height: 600,
                width: 830, resizable: false, title: "员工查询"
            });
        });

        
    });

    function SelectPerson() {
        $("#divFrame").dialog("close");
        if (PersonInfo.length > 0 && PersonInfo.length <= 1) {
            var col = PersonInfo[0];
            var PersonSysNo = col.PersonSysNo;
            var PersonName = col.PersonName;
            var OrganName = col.OrganName;
            var FunctionInfo = col.FunctionInfo;
            var TypeName = col.TypeName;
            $("#txtParentPersonName").val(PersonName);
            $("#hdnParentPersonSysNo").val(PersonSysNo);
        }
    }
</script>