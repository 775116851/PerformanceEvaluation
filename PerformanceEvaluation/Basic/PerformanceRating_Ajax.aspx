﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PerformanceRating_Ajax.aspx.cs" Inherits="PerformanceEvaluation.PerformanceEvaluation.Basic.PerformanceRating_Ajax" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript">
        var show = { per: 10, index: { n: "首页", m: "first" }, pre: { n: "上一页", m: "prev" }, next: { n: "下一页", m: "next" }, last: { n: "尾页", m: "last"} };
        PageClick = function(pageclickednumber) {
            CurrentPageIndex = pageclickednumber;
            goFilterPager();
        }

        $(document).ready(function() {
            $("#pager").pager({ pagenumber: <%=PageIndex %>, pagecount: <%=MaxPages %>, buttonClickCallback: PageClick, show: show });
            $("#datatable tr").mouseover(function() {
                $(this).addClass("over");
            }).mouseout(function() {
                $(this).removeClass("over");
            });
            $("#datatable tr").click(function() {
                $.each($("#datatable tr"), function(i, n) {
                    $(n).removeClass("selected");
                });
                $(this).addClass("selected");
            });
        });

    </script>
</head>
<body>
    <div style="text-align: left; padding: 5px auto 5px 10px; margin-bottom: 5px; font-size: 12px; margin-top: 5px; margin-left: 5px;">
        <span>共 <font style="color: Blue;">
            <%=PageCount %></font> 条,第 <font style="color: Blue;">
                <%=PageIndex %></font> / <font style="color: Blue;">
                    <%=MaxPages %></font> 页</span>
    </div>
    <table id="datatable" class="DingDanTable listView" cellspacing="0" cellpadding="2" style="width: 100%; border-collapse: collapse; border-top: solid 1px #bbb;">
        <tr>
            <th style="width: 30px; text-align: center">编号
            </th>
            <th style="width: 30px;text-align: center;padding-left: 5px;">绩效周期
            </th>
            <th style="width: 40px; text-align: center;padding-left: 5px;">员工姓名
            </th>
            <th style="width: 50px; text-align: center">入职日期
            </th>
            <th style="width: 50px; text-align: center;">绩效得分
            </th>
            <th style="width: 50px; text-align: center;">绩效等级
            </th>
            <th style="width: 50px; text-align: center;">是否已评分
            </th>
            <th style="width: 80px; text-align: center">操作
            </th>

        </tr>
        <asp:Repeater ID="Rep1" runat="server" OnItemDataBound="Rep1_ItemDataBound">
            <ItemTemplate>
                <tr>
                    <td style="text-align: center;">
                        <%# Eval("SysNo") %>
                    </td>
                    <td style="text-align: center;padding-left: 5px;">
                         <%# Eval("JXZQ") %>
                    </td>
                    <td style="text-align: center;padding-left: 5px;">
                        <%# Eval("Name") %>
                    </td>
                    <td style="text-align: center;">
                         <%# Eval("EntryDate") %>
                    </td>
                    <td style="text-align: center;">
                         <%# Eval("JXScore") %>
                    </td>
                    <td style="text-align: center;">
                         <%# FormatLevel(Eval("JXLevel").ToString()) %>
                    </td>
                    <td style="text-align: center;">
                        <%# PerformanceEvaluation.Cmn.AppEnum.GetDescription(typeof(PerformanceEvaluation.Cmn.AppEnum.YNStatus),Eval("IsPF")) %>
                    </td>
                    <td style="text-align: center;">
                        <asp:Literal ID="litLink" runat="server" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <div id="pager" class="pager">
    </div>
</body>
</html>
