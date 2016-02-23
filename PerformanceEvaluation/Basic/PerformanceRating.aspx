<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PerformanceRating.aspx.cs" Inherits="PerformanceEvaluation.PerformanceEvaluation.Basic.PerformanceRating" %>

<%@ Register Src="~/UC/DropDown.ascx" TagPrefix="uc" TagName="DropDown" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>绩效评分</title>
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
            $("#ListDiv").load("../Basic/PerformanceRating_Ajax.aspx?PageIndex=" + CurrentPageIndex, JsonObj, function (a, b, c) {
                $("#ListDiv").fadeIn();
            });
        }
        var CurrentPageIndex = 1;
        var PreJson; //记录之前的查询值，用于翻页
        function Search() {
            CurrentPageIndex = 1
            LoadList($("[sname='forminput'],#txtPersonName,#ddlPF_ddlEnum").serializeObject());
            PreJson = $("[sname='forminput'],#txtPersonName,#ddlPF_ddlEnum").serializeObject();
        }
        function goFilterPager() {
            if (PreJson == null) {
                PreJson = $("[sname='forminput'],#txtPersonName,#ddlPF_ddlEnum").serializeObject();
            }
            LoadList(PreJson);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="Content">
            <div class="Information">
                <h3>绩效评分</h3>
                <table id="table1">
                    <tbody>
                        <tr>
                            <th>员工名称</th>
                            <td><asp:TextBox ID="txtPersonName" CssClass="InputOne9" runat="server" sname="forminput"></asp:TextBox></td>
                            <th>是否已评分</th>
                            <td colspan="3"><uc:DropDown runat="server" ID="ddlPF" /></td>
                        </tr>
                        <tr>
                            <td colspan="4"><asp:Label ID="lblMsg" runat="server" /></td>
                        </tr>
                    </tbody>
                </table>
                <ul class="ConditionsTwo ConditionsTwo-1">
                    <li>
                        <input type="button" class="InputOne3" id="btnQuery" value="查询" onclick="Search()" />
                    </li>
                    <li>
                        <asp:Button ID="btnSave" runat="server" Text="加权汇总" class="InputOne3" OnClick="btnSave_Click"/>
                    </li>
                </ul>
            </div>
            <%--<table id="Table3" cellspacing="1" cellpadding="2" width="80%" align="center">
                <tr class="tblrow">
                    <td align="left" colspan="4" style="padding: 0px;">
                            
                    </td>
                </tr>
            </table>--%>
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
    <div class="mask" style="display: none;">
        <div class="ajaxWaiting"></div>
    </div>
</body>
</html>
<script type="text/javascript">
    $(function () {
        $("#<%=btnSave.ClientID %>").click(function () {
            if (confirm("您确定要进行加权汇总操作么，这将删除当期的加权汇总数据？") == true) {
                return true;
            }
            return false;
        });
    })
</script>