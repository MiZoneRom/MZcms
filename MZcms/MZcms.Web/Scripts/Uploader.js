$.getScript("/Scripts/exif.js");//图片Exif信息

//上传控件
(function ($) {

    var upload = {
        init: function (opts, container) {

            var id = new Date().getTime();
            var str = '';
            var input = $(container).find('.form-control');
            var imgSrc = input.val();

            //初始图片
            if (!$.isArray(opts.displayImgSrc) && opts.displayImgSrc.indexOf(',') >= 0) {
                opts.displayImgSrc = opts.displayImgSrc.split(',');
            } else {
                opts.displayImgSrc = [opts.displayImgSrc];
            }

            //是否可删除
            if (opts.canDel) {

                var str = '<div class="clearfix">';

                for (var i = 0; i < opts.imagesCount; i++) {
                    var display = opts.displayImgSrc[i] || i == 0 ? '' : 'style="display:none"',
                        show = opts.displayImgSrc[i] ? '' : 'style="display:none"',
                        hide = opts.displayImgSrc[i] ? 'style="display:none"' : '';

                    if (opts.displayImgSrc != '' && opts.displayImgSrc.length < opts.imagesCount && opts.displayImgSrc.length == i) {
                        display = '';
                    }

                    str += `
                    <div ${display} class="upload-box">
                        <i class="fa fa-upload"></i>
                        <img ${show} src="${(opts.displayImgSrc[i] ? opts.displayImgSrc[i] : '')}" class="img-upload"/>
                        <span class="remove-img" ${show} >删除</span>
                        <span class="btn btn-primary" ${hide} ><i class="fa fa-upload"></i>上传
                            <input type="hidden" class="hiddenImgSrc" value="${(opts.displayImgSrc[i] ? opts.displayImgSrc[i] : imgSrc)}"  name="${opts.imgFieldName}" />
                            <input class="file uploadFilebtn" type="file" name="_file"   id="imgUploader_${id}_${i}"/>
                            <input type="hidden" class="hidOrientation" name="hidFileMaxSize" />
                        </span>
                    </div>`;

                    //str += ' <div class="upload-img-box imageBox" ' + display + '>' +
                    //    '<img ' + show + ' src="' + (opts.displayImgSrc[i] ? opts.displayImgSrc[i] : '') + '" class="img-upload"/>' +
                    //    '<span class="remove-img" ' + show + '>删除</span>' +
                    //    '<div class="img-upload-btn" ' + hide + '>' + (opts.isMobile ? '<i class="glyphicon glyphicon-camera"></i>' : '+') + '</div>' +
                    //    '<input type="hidden" class="hiddenImgSrc" value="' + (opts.displayImgSrc[i] ? opts.displayImgSrc[i] : '') + '"  name="' + opts.imgFieldName + '" />' +
                    //    '<input class="file uploadFilebtn" type="file" name="_file"   id="imgUploader_' + id + '_' + i + '"/>' +
                    //    '<input type="hidden" class="hidOrientation" name="hidFileMaxSize" />' +
                    //    '</div>';

                }

            } else {//不可删除

                $(input).hide();

                for (var i = 0; i < opts.imagesCount; i++) {
                    var display = opts.displayImgSrc[i] || i == 0 ? '' : ' style="display:none"';
                    str += `
                    <div ${display} class="upload-box">
                        <span class="btn btn-preview"><i class="fa fa-upload"></i></span>
                        <span class="btn btn-primary btn-upload">
                            <i class="fa fa-upload"></i>上传
                            <input type="hidden" class="hiddenImgSrc" value="${(opts.displayImgSrc[i] ? opts.displayImgSrc[i] : imgSrc)}"  name="${opts.imgFieldName}" />
                            <input class="file uploadFilebtn" type="file" name="_file"   id="imgUploader_${id}_${i}" />
                            <input type="hidden" class="hidOrientation" name="hidFileMaxSize" />
                        </span>
                    </div>`;
                }

                //console.info(customeFile);

                //var str = '';
                //str += '<div class="col-sm-' + opts.dataWidth + '" >';
                //for (var i = 0; i < opts.imagesCount; i++) {
                //    var display = opts.displayImgSrc[i] || i == 0 ? '' : ' style="display:none"';
                //    str += ' <div ' + display + ' class="imageBox fl">' +
                //        '<input type="hidden" class="hiddenImgSrc"  value="' + (opts.displayImgSrc[i] ? opts.displayImgSrc[i] : '') + '"   name="' + opts.imgFieldName + '" />' +
                //        '<span class="fa fa-picture-o"></span>' +
                //        '<input class="file uploadFilebtn" type="file" name="_file"   id="imgUploader_' + id + '_' + i + '"/>' +
                //        '<input type="hidden" class="hidOrientation" name="hidFileMaxSize" />' +
                //        '</div>';
                //}

            }

            if (opts.imageDescript) {
                str += '<span class="help-block">' + opts.imageDescript + '</span>';
            }
            //str += '</div>';

            //$(container).html(str);

            console.info(str);

            $(container).children('.upload-box').remove();
            $(container).append(str);

        },
        checkImgType: function (filename) {
            var str = filename.substring(filename.lastIndexOf("."), filename.length)
            var str1 = str.toLowerCase();
            if (!/\.(gif|jpg|jpeg|png|bmp)$/.test(str1)) {
                return false;
            }
            return true;
        },
        uploadfile: function (target, opts) {//上传文件

            var imgupfile = $('.uploadFilebtn', target); //上传控件
            var imgHideFile = $('.hiddenImgSrc', target); //图片隐藏域。
            var formControl = $(target).parent().children('.form-control');//输入框

            if (imgupfile.val() != "") {

                var Orientation = 0;//图片的旋转角度

                try {//旋转角度

                    //保存旋转角度
                    EXIF.getData(file, function () {
                        EXIF.getAllTags(this);
                        Orientation = EXIF.getTag(this, 'Orientation');
                        alert(Orientation);
                    });

                } catch (error) {

                }

                if (!this.checkImgType(imgupfile.val())) {
                    Swal.fire("上传格式为gif、jpeg、jpg、png、bmp");
                    imgupfile.val('');
                    return;
                }

                if (imgupfile[0].files[0].size / 1024 > opts.maxSize * 1024) {
                    Swal.fire("上传的图片不能超过" + opts.maxSize + "M");
                    imgupfile.val('');
                    return;
                }

            } else {
                return;
            }

            var hidOrientation = $('.hidOrientation', target); //照片旋转参数
            var orientation = 0;

            try {

                var file = $(imgupfile).prop('files')[0];

                //保存旋转角度
                EXIF.getData(file, function () {
                    EXIF.getAllTags(this);
                    orientation = EXIF.getTag(this, 'Orientation');
                    hidOrientation.val(orientation);
                });

            } catch (error) {
                orientation = 0;
                hidOrientation.val(orientation);
            }

            var myform = $('<form action="' + opts.url + '" method="post" enctype="multipart/form-data" style="display:none"></form>');
            imgupfile.appendTo(myform);
            hidOrientation.appendTo(myform);
            $('body').append(myform);

            target.css('opacity', '.6');

            //开始模拟提交表当。
            myform.ajaxSubmit({
                success: function (data) {

                    if (data == "NoFile" || data == "Error" || data == "格式不正确！") {

                        Swal.fire(data);

                    } else {

                        imgHideFile.val(data);
                        formControl.val(data);

                        if (opts.canDel) {
                            $('img.img-upload', target).attr('src', data).show();
                            $('.btn-preview', target).hide();
                            $('.remove-img', target).show();
                        } else {
                            $('.btn-preview', target).addClass('active');
                        }

                        target.css('opacity', '1');

                        if (opts.callback) {
                            opts.callback(data);
                        }

                        if (target.index() + 1 < opts.imagesCount) {
                            target.next().show();
                        }

                    }

                    target.children(".btn-upload").append(imgupfile);
                    myform.remove();

                }
            });

        },
        bindEvent: function (target, opts) {//绑定上传事件
            var self = this;

            $('input.uploadFilebtn', target).change(function () {
                self.uploadfile(target, opts);
            });

            if (!opts.canDel) {

                $('.btn-preview', target).each(function () {

                    var imgSrc = $(target).parent().children('.form-control');

                    if (imgSrc.val() != '') {
                        $(this).addClass('active');
                    }

                    $(this).mouseenter(function () {

                        var src = $('.hiddenImgSrc', target).val();

                        if (src != '') {
                            var position = $(this).offset(),
                                scrollTop = $('body').scrollTop(),
                                pos = (position.top - scrollTop >= 200),
                                imgstr = '<div class="lg-view-img" style="' + (pos ? 'bottom' : 'top') + ':20px"><img src="' + src + '?version=' + Math.random() + '"></div>';
                            $(this).append(imgstr);
                        }

                    });

                    $(this).mouseleave(function () {
                        $(this).children('.lg-view-img').remove();
                    });

                });

            } else {

                if (!opts.isMobile) {
                    if ($('input.hiddenImgSrc', target).val() != '') {
                        target.hover(function () {
                            $('span.remove-img', $(this)).toggle();
                        });
                    }
                }

                $('span.remove-img', target).click(function () {
                    $('img.img-upload', target).attr('src', '').hide();
                    $('div.img-upload-btn', target).show();
                    $('input.hiddenImgSrc', target).val('');
                    $('input.file', target).val('');
                    $(this).hide();
                });

            }
        }
    }

    var exportFn = {
        getImgSrc: function (target) {
            var images = $('input.hiddenImgSrc', target);
            if (images.length == 1) {
                return images.val();
            } else {
                var srcArr = [];
                images.each(function () {
                    var src = $(this).val();
                    if (src)
                        srcArr.push(src);
                });
                return srcArr;
            }
        }
    };

    $.fn.Uploader = function (options) {

        if (typeof options == 'string') {
            return exportFn[options]($(this));
        }

        var defaults = {
            url: '/Common/PublicOperation/UploadPic',
            displayImgSrc: '',	    //初始化图片
            title: '图片：',		    //是否有标题
            imageDescript: '',	    //图片描述
            imgFieldName: 'Icon',
            imagesCount: 1,		//可上传数量
            maxSize: 2,			    //图片大小限制，单位M 
            callback: null,		    //上传成功回调，返回服务器图片路径
            canDel: false,		    //是否支持删除
            isMobile: false		    //是否为移动端
        }

        var opts = $.extend({}, defaults, options || {});
        upload.init(opts, this);

        $('.upload-box', this).each(function () {
            upload.bindEvent($(this), opts);
        });

    };

})(jQuery);