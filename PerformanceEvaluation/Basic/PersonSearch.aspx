<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="PersonSearch.aspx.cs" Inherits="PerformanceEvaluation.PerformanceEvaluation.Basic.PersonSearch" %>
<%@ Register Src="~/UC/DropDown.ascx" TagPrefix="uc" TagName="DropDown" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>员工信息查询</title>
    <link href="../Style/index.css" rel="stylesheet" />
    <link href="../Style/reset.css" rel="stylesheet" />
    <link href="../Style/pager.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.7.1.min.js"></script>
    <script src="../Scripts/json.js"></script>
    <script src="../Scripts/Common.js"></script>
    <script src="../Scripts/share.js"></script>
    <script src="../Scripts/jquery.pager.js"></script>
    <style type="text/css">
        table {
            width: 100%;
        }

        #table1 tr th {
            width: 100px;
            min-width: 100px;
        }
    </style>
    <script type="text/javascript">
        function LoadList(JsonObj) {
            $("#lblMsg").text("")
            $("#ListDiv").html("<table class='table' width='100%' height='180px' border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr><td height='180px' style='text-align:center; padding-top:50px;'><img src='../images/pager/spinner.gif'>数据载入中,请稍候...&nbsp;</td></tr></table>");
            $("#ListDiv").load("../Basic/PersonSearch_Ajax.aspx?PageIndex=" + CurrentPageIndex, JsonObj, function (a, b, c) {
                $("#ListDiv").fadeIn();
            });
        }
        var CurrentPageIndex = 1;
        var PreJson; //记录之前的查询值，用于翻页
        function Search() {
            CurrentPageIndex = 1
            LoadList($("[sname='forminput'],#txtPersonName,#ddlStatus_ddlEnum,#ddlPersonType").serializeObject());
            PreJson = $("[sname='forminput'],#txtPersonName,#ddlStatus_ddlEnum,#ddlPersonType").serializeObject();
        }
        function goFilterPager() {
            if (PreJson == null) {
                PreJson = $("[sname='forminput'],#txtPersonName,#ddlStatus_ddlEnum,#ddlPersonType").serializeObject();
            }
            LoadList(PreJson);
        }
        function Add() {
            openWindow("../Basic/PersonSearchOpt.aspx", 950, 600);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="Content">
            <div class="Information">
                <h3>员工信息查询</h3>
                <table id="table1">
                    <tbody>
                        <tr>
                            <th>所属机构</th>
                            <td><asp:DropDownList ID="ddlOrgan" sname="forminput" runat="server"></asp:DropDownList>
                                <asp:DropDownList ID="ddlClass" sname="forminput" runat="server"></asp:DropDownList>
                                <asp:HiddenField ID="hidClassSysNo" runat="server" Value="-9999" />
                            </td>
                            <th>员工类型</th>
                            <td><asp:DropDownList ID="ddlPersonType" sname="forminput" runat="server"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <th>员工姓名</th>
                            <td><asp:TextBox ID="txtPersonName" CssClass="InputOne9" runat="server" sname="forminput"></asp:TextBox></td>
                            <th>员工状态</th>
                            <td colspan="3"><uc:DropDown runat="server" ID="ddlStatus" /></td>
                        </tr>
                        <tr>
                            <td colspan="4"><asp:Label ID="lblMsg" runat="server" /></td>
                        </tr>
                    </tbody>
                </table>
                <ul class="ConditionsTwo ConditionsTwo-2">
                    <li>
                        <input type="button" class="InputOne3" id="btnQuery" value="查询" onclick="Search()" />
                    </li>
                    <li>
                        <input type="button" class="InputOne3" id="btnAdd" runat="server" onclick="Add()" value="添加" />
                    </li>
                    <li>
                        <input type="button" class="InputOne3" id="btExport" runat="server" value="导 出" onserverclick="btExport_Click"/>
                    </li>
                </ul>
            </div>
            <div id="ListDiv">
                <table class="table" width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td height="180px" style="text-align: center; padding-top: 40px;">&#160;                               
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
<script type="text/javascript">
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

        $("#<%=ddlClass.ClientID %>").change(function () {
            var pOrgan = $("#<%=ddlOrgan.ClientID %>").val();
            var pClass = $("#<%=ddlClass.ClientID %>").val();
            $("#hidClassSysNo").val(pClass);
        });

        function OrganChange_Callback(result) {
            for (var i = 0; i < result.length; i++) {
                $("#<%=ddlClass.ClientID%>").append(("<option value='" + result[i][0] + "'>" + result[i][1] + "</option>"));
            }
        }
    });
</script>