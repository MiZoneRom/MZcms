
$(function () {

    $('.switch-box').each(function () {
        let obj = $(this);
        let checkbox = obj.children('input:checkbox');
        let checkboxId = checkbox.attr('id')
        let label = checkbox.siblings('.control-label');
        let labelText = label.text();
        label.hide();
        checkbox.wrap(`<div class="custom-control custom-switch"></div>`);
        checkbox.addClass('custom-control-input');
        checkbox.parent().append(`<label class="custom-control-label" for="${checkboxId}">${labelText}</label>`);
    });

})

$.fn.initValidform = function () {

    var checkValidform = function (formObj) {

        var url = formObj.attr('url');//表单提交链接
        var submitButton = $(formObj).find("#Submit");//提交按钮

        //绑定按钮点击事件
        $(submitButton).click(function () {

            var dialog = showLoading();

            $.post(url, $(formObj).serialize(), function (result) {

                dialog.close();

                if (result.success) {
                    Swal.fire('保存成功');
                }
                else {
                    Swal.fire('保存失败！' + result.msg);
                }

            });

        });

    };

    return $(this).each(function () {
        checkValidform($(this));
    });
}

function showLoading(msg, delay) {

    if (!delay)
        delay = 100;

    var loading = $(`<div class="loading-container">
                                    <div class="loading-popup">
                                        <div class="loading-actions">
                                            <span class="loading-icon"></span>
                                        </div>
                                    </div>
                                </div>`);

    loading.appendTo('body');

    var s = setTimeout(function () {
        if ($(".ajax-loading").length > 0) {
            loading.show();
            $('.container,.login-box').addClass('blur');
        }
    }, delay);

    return {
        close: function () {
            clearTimeout(s);
            loading.remove();
            $('.container,.login-box').removeClass('blur');
        }
    }

}

function QueryString(name) {
    /// 获取QueryString
    if (name == null || name == "" || name == undefined) {
        var AllVars = window.location.search.substring(1);
        return AllVars.split("&");
    }
    else {
        var AllVars = window.location.search.substring(1);
        var Vars = AllVars.split("&");
        for (i = 0; i < Vars.length; i++) {
            var Var = Vars[i].split("=");
            if (Var[0].toLowerCase() == name.toLowerCase()) return Var[1];
        }
        return "";
    }
};