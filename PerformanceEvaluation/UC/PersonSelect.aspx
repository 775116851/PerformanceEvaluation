<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonSelect.aspx.cs" Inherits="PerformanceEvaluation.PerformanceEvaluation.UC.PersonSelect" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="DropDown.ascx" TagName="DropDown" TagPrefix="uc" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>员工查询</title>
    <link href="../Style/index.css" rel="stylesheet" />
    <link href="../Style/reset.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.7.1.min.js"></script>
    <style type="text/css">
       table {
            width: 100%;
        }
            table th{
                 white-space:nowrap;
            }
        #table1 th {
            min-width:60px;
        }
    </style>
    <script type="text/javascript">
        function trClick(id) {
            var Selected = [];
            var tr = $("#" + id);
            var trid = tr.attr("uid");
            var hidden = jQuery("[sname='hidden'][uid='" + trid + "']");
            $(hidden[0]).val(trid);
            var value = hidden.attr("svalue").split(',');
            var item = { "PersonSysNo": value[0], "PersonName": value[1], "OrganName": value[2], "FunctionInfo": value[3], "TypeName": value[4] };
            Selected.push(item);
            window.parent.PersonInfo = Selected;
            window.parent.SelectPerson();
        }

        function Confirm() {
            window.parent.PersonInfo = Selected;
            window.parent.SelectPerson();

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="Content">
            <div class="Information">
                <table id="table1">
                    <tbody>
                        <tr>
                            <th>员工编号</th>
                            <td>
                                <asp:TextBox ID="txtPersonSysNo" runat="server" CssClass="InputOne9"></asp:TextBox>
                            </td>
                            <th>员工名称</th>
                            <td>
                                 <asp:TextBox ID="txtPersonName" runat="server" CssClass="InputOne9"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <ul class="ConditionsTwo ConditionsTwo-1">
                <li>
                    <asp:Button ID="btnSearch" CssClass="InputOne3" runat="server" Text="查询" OnClick="btnSearch_Click"></asp:Button>
                </li>
            </ul>
            <table id="Table3" cellspacing="1" cellpadding="2" width="80%" align="center">
                <tr>
                    <td align="left" style="padding: 0px;">
                        <asp:Label ID="lblMessage" runat="server" EnableViewState="False"></asp:Label>
                    </td>
                </tr>
            </table>
            <div class="DingDan" >
                <table id="datatable" width="80%" class="DingDanTable listView">
                    <tr>
                        <th >编号</th>
                        <th>员工姓名</th>
                        <th >二级部</th>
                        <th >职能室</th>
                        <th >员工类型</th>
                    </tr>
                    <asp:Repeater ID="Rep1" runat="server" >
                        <ItemTemplate>
                            <tr id='trid<%#Eval("SysNo") %>' uid='<%#Eval("SysNo") %>' ondblclick="trClick(this.id);">
                                <td id='tr<%#Eval("SysNo") %>td1' uid='<%#Eval("SysNo") %>'>
                                    <input id='SysNo<%#Eval("SysNo") %>' type="hidden" uid='<%#Eval("SysNo") %>' value="" svalue='<%#Eval("SysNo") %>,<%#Eval("Name") %>,<%#Eval("OrganName") %>,<%#Eval("FunctionInfo") %>,<%#Eval("TypeName") %>' sname="hidden" /><%#Eval("SysNo") %>
                                </td>
                                <td><asp:Label ID="Label2" runat="server" Text='<%# Eval("Name") %>'></asp:Label></td>
                                <td ><asp:Label ID="lblProductID" runat="server" Text='<%# Eval("OrganName") %>'></asp:Label></td>
                                <td><asp:Label ID="Label1" runat="server" Text='<%# Eval("FunctionInfo") %>'></asp:Label></td>
                                <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("TypeName") %>'></asp:Label></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td colspan="5">
                            <webdiyer:AspNetPager ID="AspNetPager1" AlwaysShow="true" PageSize="20" runat="server" Font-Size="Medium" FirstPageText="首页" LastPageText="尾页"
                                            OnPageChanged="AspNetPager1_PageChanged" NextPageText="下一页"  PrevPageText="上一页" ShowPageIndexBox="Never" >
                            </webdiyer:AspNetPager>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
