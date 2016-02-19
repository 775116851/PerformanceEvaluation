<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PerformanceEvaluation.PerformanceEvaluation.Home.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>绩效管理平台</title>
    <link href="../Style/reset.css" rel="stylesheet" />
    <link href="../Style/default.css" rel="stylesheet" />
    <link href="../Style/index.css" rel="stylesheet" />
</head>
<body style="overflow: hidden; margin: 0px; padding: 0px;">
    <form id="form1" runat="server">
        <div id="header">
                <div class="log1o">
                    <a href="###">
                        <div id="divLogo" alt="logo" onclick=""></div>
                    </a>
                </div>
                <p class="info">绩效管理平台</p>
                 <dl style="display:none">
                        <dt></dt>
                        <dd></dd>
                 </dl>
            </div>
        <div id="container">
            <div class="side">
                <dl>
                 <%--   <dt>
                        <img src="../images/SideHeadImg1.gif" width="50" height="50" alt="" />
                    </dt>--%>
                    <dd>欢迎您，<span id="spLoginName"><b>[未知用户]</b></span></dd>
                    <dd><span><a href="###" onclick="Logout();">[退出]</a></span><span> <a href="###" onclick="onChangePwdClicked();">[修改密码]</a></span></dd>
                    <dd></dd>
                </dl>
                <div id="divLeftMenuPanel">
                </div>
            </div>
            <div class="middle">
                <div class="floater"></div>
                <div class="middleOff" title="收起左侧菜单" onclick="toggleLeftMenu(this)"></div>
            </div>
            <div class="content">
                <div id="divTopPanel">
                    <div class="TopNav">
                        <ul class="Btn">
                        </ul>
                    </div>
                    <div class="BreadCrumb">
                        <span>当前位置：</span>
                        <span id="spanMenuPath"></span>
                    </div>
                </div>
                <iframe name="ifrmContent" id="ifrmContent" scrolling="auto" frameborder="0"></iframe>
            </div>
        </div>
        <input runat="server" type="hidden" id="hidAction" />
    </form>
</body>
</html>
<script src="../Scripts/jquery-1.7.1.min.js"></script>
<script src="../Scripts/BuildMenu.js"></script>
<script src="../Scripts/json2.js"></script>
<script src="../Scripts/Common.js"></script>
<script type="text/javascript">
    var SourceMenuList = $.parseJSON('<%=MenuListStr%>');//所有菜单数据
    var LoginName = '<%=UserName%>';

    $(function () {
        var m1list = getMenuListByLevel(SourceMenuList, 1);
        buildTopMenuList(m1list);
        $("ul.Btn li").first().addClass("On");

        LoginName && $("#spLoginName b").text(LoginName);
        var m1list = getMenuListByLevel(SourceMenuList, 1);
        var m2list = getMenuListByLevel(SourceMenuList, 2);
        var m3list = getMenuListByLevel(SourceMenuList, 3);
        buildLeftMenuBar(m1list, m2list, m3list);
        $("#divLeftMenuPanel .Level2MenuPanel:first").show().find("ul:first").show().children("li:first").addClass("On");
        adjustLayout();
        $("#divLeftMenuPanel ul li a[link!='']:first").click();

    })

    function Logout() {
        if (confirm("确定要退出登录吗？")) {
            $("#hidAction").val("Logout");
            $("#form1").submit();
            showAjaxMask();
        }
    }

    function changePlatHandler(obj) {
        var dd = $(obj).parent("dd");
        if (!dd.hasClass("On")) {
            dd.addClass("On").siblings("dd").removeClass("On");
        }
    }

    function buildLeftMenuBar(M1List, M2List, M3List) {
        var panel = $("#divLeftMenuPanel");
        for (var i = 0; i < M1List.length; i++) {
            var m2Panel = $("<div class='Level2MenuPanel' id='divLevel2MenuPanel_" + M1List[i].SysNo + "'>");//每个一级菜单的所有二级菜单容器
            m2Panel.hide();
            panel.append(m2Panel);
            for (var j = 0; j < M2List.length; j++) {
                if (M2List[j].M1SysNo == M1List[i].SysNo)//是否是该一级菜单下的二级菜单
                {
                    var m3Panel = $("<div>").addClass("same").append("<h3><a href='###' onclick='toggleLevel2Menu(this)'>" + M2List[j].MenuName + "</a></h3>");
                    var ul = $("<ul>").appendTo(m3Panel).hide();
                    for (var k = 0; k < M3List.length; k++) {
                        if (M3List[k].M1SysNo == M1List[i].SysNo && M3List[k].M2SysNo == M2List[j].SysNo)//是否是该二级菜单下的所属三级菜单
                        {
                            var a = $("<a href=### link=" + M3List[k].MenuLink + ">" + M3List[k].MenuName + "</a>").click(function () { onLevel3MenuclickedHandler(this); });
                            ul.append($("<li style='margin:0 auto;'>").append(a));
                        }
                    }
                    m2Panel.append(m3Panel);
                }
            }
            if (m2Panel.find(".same").length == 0) {
                m2Panel.append("<div style='text-align:center; padding-top:10px;'>无菜单数据!</div>");
            }
        }
    }

    function toggleLevel2Menu(obj) {
        $(obj).parents("div.same").children("ul").slideToggle(300);
        $(obj).parents("div.same").siblings(".same").children("ul").slideUp(300);
    }

    function onLevel3MenuclickedHandler(obj) {
        var li = $(obj).parent();
        if (li) {
            $("div.Level2MenuPanel ul li").removeClass("On");
            li.addClass("On");
            $(obj).attr("link") && (ifrmContent.location = $(obj).attr("link"));
        }

        var m1Name = $("#divTopPanel li.On a").text();
        var m2Name = $(obj).parents("ul").first().prev().find("a").text();
        var m3Name = $(obj).text();
        $("#spanMenuPath").text(m1Name + " > " + m2Name + " > " + m3Name);
    }

    function onChangePwdClicked() {
        openWindow("ChangePassword.aspx", 600, 300);
    }

    function buildTopMenuList(list) {
        for (var i = 0; i < list.length; i++) {
            buildSingleTopMenu(list[i]);
        }
    }

    function buildSingleTopMenu(menu) {
        var li = $("<li>").appendTo($("ul.Btn"));
        var a = $("<a id='aMenu_" + menu.SysNo + "' href='###'>" + menu.MenuName + "</a>").appendTo(li);
        a.click(function () { onTopMenuClickHandler(this); });
    }


    function onTopMenuClickHandler(obj) {
        if (!$(obj).parent().hasClass("On")) {
            $(obj).parent().addClass("On").siblings("li").removeClass("On");
            $("#divLeftMenuPanel .Level2MenuPanel").hide();
            var index = (obj.id.split("_"))[1];
            $("#divLevel2MenuPanel_" + index).show();
        }
    }

    function toggleLeftMenu(obj) {
        $(".side").toggle();

        if ($(obj).hasClass("middleOff")) {
            $(obj).removeClass("middleOff").addClass("middleOn").attr("title", "打开左侧菜单");
            $(".content").css("margin-left", "12px");
        }
        else if ($(obj).hasClass("middleOn")) {
            $(obj).removeClass("middleOn").addClass("middleOff").attr("title", "收起左侧菜单");
            $(".content").css("margin-left", "213px");
        }
    }

    function adjustLayout() {
        $("#container").height($("body").outerHeight() - 52);
        $("#ifrmContent").height($("#container").outerHeight() - 64);
    }
</script>
