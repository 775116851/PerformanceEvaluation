<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PerformanceRatingOpt.aspx.cs" Inherits="PerformanceEvaluation.PerformanceEvaluation.Basic.PerformanceRatingOpt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>员工绩效打分</title>
    <link href="../Style/pager.css" rel="stylesheet" />
    <link href="../Style/index.css" rel="stylesheet" />
    <link href="../Style/reset.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.7.1.min.js"></script>
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
            <h3>员工绩效打分</h3>
            <table id="Table1" cellspacing="1" cellpadding="2" width="80%" style="text-align:center;">
                <tbody>
                    <tr>
                        <td></td>
                        <td>上级领导：<asp:Label ID="lblSJ" runat="server"></asp:Label> &nbsp;</td>
                        <td>下级员工：<asp:Label ID="lblXJ" runat="server"></asp:Label></td>
                        <td colspan="10"></td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="Information">
            <table id="Table2" cellspacing="1" cellpadding="2" width="80%" style="text-align:center;">
                <tr>
                    <td>
                        <table id="datatable">
                            <tr>
                                <td style="width: 20px; text-align: center">编号</td>
                                <td style="width: 20px; text-align: center">绩效ID</td>
                                <td style="width: 290px; text-align: left">绩效说明</td>
                                <td style="width: 30px; text-align: center">绩效权重(%)</td>
                                <td style="width: 30px; text-align: center">绩效满分</td>
                                <td style="width: 80px; text-align: center">评分</td>
                            </tr>
                        <asp:Repeater ID="Rep1" runat="server" OnItemDataBound="Rep1_ItemDataBound">
                            <%--<HeaderTemplate>
                                <table >
                                    <tr>
                                        <td style="width: 20px; text-align: center">编号</td>
                                        <td style="width: 20px; text-align: center">绩效ID</td>
                                        <td style="width: 290px; text-align: left">绩效说明</td>
                                        <td style="width: 30px; text-align: center">绩效权重(%)</td>
                                        <td style="width: 30px; text-align: center">绩效满分</td>
                                        <td style="width: 80px; text-align: center">评分</td>
                                    </tr>
                            </HeaderTemplate>--%>
                            <ItemTemplate>
                                <tr>
                                    <td style="width: 20px; text-align: center">
                                        <asp:Label ID="lblSysNo" runat="server" Text='<%# Eval("SysNo") %>'></asp:Label>
                                        <input id="ipt_SysNo" runat="server" style="width: 32px" type="hidden" value='<%# Eval("SysNo") %>' />
                                        <input id="ipt_JXId" runat="server" style="width: 32px" type="hidden" value='<%# Eval("JXId") %>' />
                                        <input id="ipt_JXMXScore" runat="server" style="width: 32px" type="hidden" value='<%# Eval("JXMXScore") %>' />
                                        <input id="ipt_JXCategory" runat="server" style="width: 32px" type="hidden" value='<%# Eval("JXCategory") %>' />
                                        <input id="ipt_JXMXSysNo" runat="server" style="width: 32px" type="hidden" value='<%# Eval("JXMXSysNo") %>' />
                                    </td>
                                    <td style="width: 20px; text-align: center">
                                        <%# Eval("JXId") %>
                                    </td>
                                    <td style="width: 290px; text-align: left">
                                        
                                        <%# Eval("JXInfo") %>
                                    </td>
                                    <td style="width: 30px; text-align: center">
                                        <%# Eval("JXGrad") %>
                                    </td>
                                    <td style="width: 30px; text-align: center">
                                        <%# Eval("JXScore") %>
                                    </td>
                                    <td style="width: 80px; text-align: center">
                                        <input id="ipt_JXGrad" runat="server" style="width: 32px" type="hidden" value='<%# Eval("JXGrad") %>'  sname="NGrad"/>
                                        <input id="ipt_JXScore" runat="server" style="width: 32px" type="hidden" value='<%# Eval("JXScore") %>' sname="NScore" />
                                        <asp:TextBox ID="txtMScore" sname="Nsort" runat="server" Width="96px" Text='<%# GetIntScore(Eval("JXMXScore").ToString()) %>' MaxLength="3"></asp:TextBox>
                                        <asp:TextBox ID="txtTotalScore" sname="Msort" runat="server" Text='<%# GetTotalScore(Eval("JXMXScore").ToString(),Eval("JXGrad").ToString(),Eval("JXScore").ToString()) %>' ReadOnly="true" MaxLength="3" Width="40px" Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <%--<FooterTemplate>
                                </table>
                            </FooterTemplate>--%>
                        </asp:Repeater>
                        </table>
                    </td>
                </tr>
            </table>
            <div>
                <asp:Label ID="lblMsg" runat="server" />
            </div>
            <ul class="ConditionsTwo ConditionsTwo-1">
                <li>
                    <asp:Button ID="btnSave" runat="server" Text="提交" class="InputOne3" OnClick="btnSave_Click"/>
                </li>
                <li>
                </li>
            </ul>
        </div>

    </div>
    </form>
</body>
</html>
<script type="text/javascript">
    $(function () {
        $("#<%=btnSave.ClientID %>").click(function () {
            var tCount = $("#datatable tbody tr").length;
            if (!tCount || tCount <= 1) {
                $("#<%=lblMsg.ClientID %>").attr("style", "color:red").show().text("无评分项目，不可保存！");
                return false
            }
            var mCount = 0;
            var isNPass = false;
            var r = /^[0-9]*[1-9][0-9]*$/;
            $("#datatable tbody tr:not(:first)").each(function (a, b) {
                var nSort = $(b).find("input:[sname=Nsort]").val();
                var nScore = $(b).find("input:[sname=NScore]").val();
                if ($.trim(nSort).length <= 0) {
                    isNPass = true;
                    $("#<%=lblMsg.ClientID %>").attr("style", "color:red").show().text("请先将评分项填写完整后，再提交！");
                    return false;
                }
                if (nSort && (isNaN(nSort) || parseInt(nSort) <= 0 || r.test(nSort) == false))
                {
                    isNPass = true;
                    $("#<%=lblMsg.ClientID %>").attr("style", "color:red").show().text("评分项应填入正整数值，请重新修改后再保存！");
                    return false;
                }
                if (parseInt(nSort) > parseInt(nScore)) {
                    isNPass = true;
                    $("#<%=lblMsg.ClientID %>").attr("style", "color:red").show().text("评分项应填入正整数值，且评分值不大于 " + nScore + " 分，请重新修改后再保存！");
                    return false;
                }
                mCount++;
            });
            if (isNPass == true) {
                return false;
            }
            if (tCount-1 != mCount) {
                $("#<%=lblMsg.ClientID %>").attr("style", "color:red").show().text("请先将评分项填写完整后，再提交！");
                return false;
            }
            return true;
        });

        $("input:[sname=Nsort]").change(function () {
            var mScore = $(this).val();
            var mJXScore = $(this).parent().find("input:[sname=NScore]").val();
            var mJXGrad = $(this).parent().find("input:[sname=NGrad]").val();
            if ($.trim(mScore).length <= 0) {
                $(this).parent().find("input:[sname=Msort]").val();
            }
            else {
                $(this).parent().find("input:[sname=Msort]").val((parseFloat(mScore) * parseFloat(mJXGrad) / parseFloat(mJXScore)).toFixed(2));
            }
        })
    })
</script>
