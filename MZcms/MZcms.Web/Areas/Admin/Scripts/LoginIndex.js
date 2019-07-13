$(function () {

    $('#checkcodeimg').click(function () {
        $('#checkcode').focus();
        changeCheckCode();
    });

    //点击提交按钮
    $('#submit').click(function () {

        var dialog = showLoading();

        var username = $('#username').val();
        var password = $('#password').val();
        var checkcode = $('#checkcode').val();

        if (username == '' || password == '') {
            dialog.close();
            $.dialog.errorTips("请输入用户名及密码");
            return;
        }

        $.ajax({
            type: 'post',
            url: path + 'Login',
            data: {
                username: username,
                password: password,
                checkCode: checkcode
            },
            dataType: "json",
            async: true,
            success: function (result) {

                dialog.close();

                //登录成功
                if (result.success) {

                    var options = { path: "/" };

                    if ($('#keeplogin').attr('checked'))
                        options.expires = 365;

                    var url = QueryString('return');
                    if (!url)
                        url = '/Admin';

                    location.replace(url);

                } else {

                    $('#checkcode').val('');

                    if (result.errorTimes >= result.minTimesWithoutCheckCode) {
                        var checkCode = $('#checkcodebox');
                        var display = checkCode.css('display') != 'none';
                        if (!display) {
                            checkCode.show();
                            $('#checkcode').focus();
                        }
                        else
                            changeCheckCode();
                    }

                    $.dialog.errorTips(result.msg);

                }

            }, error: function () {
                $.dialog.errorTips(result.msg);
            }
        });

    });

});

function changeCheckCode() {
    $('#checkcodeimg').attr('src', path + 'GetCheckCode?time=' + new Date().getTime());
}