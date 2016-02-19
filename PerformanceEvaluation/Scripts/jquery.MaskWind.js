jQuery(document).ready(function () {
    (function (jqy) {
        jqy.fn.extend({ showMask: function (options) {
            var defalutOption = {
                zIndex: 9999, //遮罩层显示顺序,遮罩窗口显示顺序默认在该基础上加1
                background: "#000000", //遮罩层背景
                contentSelector: undefined, //遮罩层显示窗口DIV.该选择器最好是唯一的,若是多个会叠加在一起
                closeSelector: undefined, //关闭当前遮罩层的选择器
                maskOpacity: 0.3,//遮罩层透明度
                left: 0,//距离左边,是以contentSelector的坐上脚为原点0计算
                top: 0//距离右边,是以contentSelector的坐上脚为原点0计算
            };
            var opt = jqy.extend(defalutOption, options);

            //创建一个div遮罩层
            var mask = jqy("<div style='position: fixed;z-index: " + opt.zIndex + "; top: 0px; left: 0px;height: 100%;width: 100%;background: " + opt.background + ";display: none;' id='maskdiv'></div>");
            jqy("body").append(mask); //把遮罩层添加到body中
            if (opt.contentSelector && opt.contentSelector != null) {
                var content = jqy(opt.contentSelector);
                if (!content) { j }
                if (content.length > 1) { content = jqy(content[0]); }
                var _this = jqy(this);

                var winHeight = content.outerHeight();
                var winWidth = content.outerWidth(); //将要显示对象的宽

                _this.live("click", function (e) {
                    mask.css({ "display": "block", opacity: 0 }); //显示遮罩层
                    mask.fadeTo(225, opt.maskOpacity); //遮罩层显示后 使用动画效果透明
                    content.css({ "display": "block", "position": "fixed", "opacity": 0, "z-index": (opt.zIndex + 1), "left": 50 + "%", "margin-left": opt.left!=0?opt.left:-(winWidth / 2) + "px", "top": 50 + "%", "margin-top": opt.top!=0?opt.top: - (winHeight / 2) });
                    content.fadeTo(225, 1);
                    //e.preventDefault();
                    
                });
                if (opt.closeSelector && opt.closeSelector != null) {
                    $(opt.closeSelector).live("click", function (e) {//点击关闭按遮罩层使用live对象,防止对象更改
                        mask.fadeOut(225);
                        content.css({ "display": "none" })
                        //e.preventDefault();
                    });
                }

            }
            return this;
        }
        })

    })(jQuery)
})