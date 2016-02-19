$(function () {
    // Create variables (in this scope) to hold the API and image size
    // Create variables (in this scope) to hold the API and image size
    var jcrop_api,
        boundx,
        boundy,

        // Grab some information about the preview pane
        $preview   ,
        $pcnt   ,
        $pimg ,

        xsize   ,
        ysize  ;
 

    function updatePreview(c) {
        if (parseInt(c.w) > 0) {
            var rx = xsize / c.w;
            var ry = ysize / c.h;

            $pimg.css({
                width: Math.round(rx * boundx) + 'px',
                height: Math.round(ry * boundy) + 'px',
                marginLeft: '-' + Math.round(rx * c.x) + 'px',
                marginTop: '-' + Math.round(ry * c.y) + 'px'
            });
        }
    };

    $(".btn_sub").click(function () {
        var btnID = this.id;
        var randomID = btnID.replace("btnSubmit", "");
        if ($("[id$='fulFile" + randomID + "']").val() == "") {
            alert("请选择要上传的文件!");
            return false;
        }

        $("[id$='form1']").ajaxSubmit({
            url: "/Response/UploadFileHandler1.ashx",
            type: "post",
            data: { ID: randomID, ExpectDir: $("#txtExpectDir" + randomID).val(), ExpectWidth: $("#txtExpectWidth" + randomID).val(), ExpectHeight: $("#txtExpectHeight" + randomID).val() },
            resetForm: "true",
            beforeSubmit: function () {
                $("[id$='fulFile" + randomID + "']").hide();
                $("[id$='imgUploading" + randomID + "']").show();
                $("[id$='btnSubmit" + randomID + "']").hide();
            },
            success: function (msg) {
                $("[id$='fulFile" + randomID + "']").show();
                $("[id$='imgUploading" + randomID + "']").hide();
                $("[id$='btnSubmit" + randomID + "']").show();
                var jsonData = jQuery.parseJSON(msg);

                if (jsonData.Code == "0") {
                    alert("上传成功");


                    //创建图片及其图片预览
                    if ($("#target").val()==undefined) {
                        var divImg = ' <img id="target" alt="[img]" /> <div id="preview-pane">';
                        divImg += '<div class="preview-container">';
                        divImg += '  <img id="jcroppreview"  class="jcrop-preview" alt="Preview" /> </div></div>';
                        $(document.body).append(divImg);
                    }
                    $("[id$='target']").attr("src", jsonData.Data.Src);
                    $("[id$='jcroppreview']").attr("src", jsonData.Data.Src);
                    $("[id$='preview-pane']").attr("width", jsonData.Data.width);
                    $("[id$='preview-pane']").attr("height", jsonData.Data.height);


                    // Grab some information about the preview pane
                    $preview = $('#preview-pane');
                    $pcnt = $('#preview-pane .preview-container');
                    $pimg = $('#preview-pane .preview-container img');

                    xsize = $pcnt.width();
                    ysize = $pcnt.height();

                    $('#target').Jcrop({
                        onChange: updatePreview,
                        onSelect: updatePreview,
                        aspectRatio: xsize / ysize
                    }, function () {
                        // Use the API to get the real image size
                        var bounds = this.getBounds();
                        boundx = bounds[0];
                        boundy = bounds[1];
                        // Store the API in the jcrop_api variable
                        jcrop_api = this;

                        // Move the preview into the jcrop container for css positioning
                        $preview.appendTo(jcrop_api.ui.holder);
                    });

                    return;
                }
                else if (jsonData.Code == "-2") {
                    alert("禁止上传 0 KB大小的文件!");
                }
                else if (jsonData.Code == "-3") {
                    alert("请选择要上传的文件!");
                }
                else if (jsonData.Code == "-1") {
                    alert("上传失败!");
                }
                return false;
            },
            error: function (jqXHR, errorMsg, errorThrown) {
                $("[id$='fulFile" + randomID + "']").show();
                $("[id$='imgUploading" + randomID + "']").hide();
                $("[id$='btnSubmit" + randomID + "']").show();
                alert("错误信息:" + errorMsg);
                return false;
            }
        });
    });
});