﻿

function generateRoleInfo() {
    //角色对象
    var role = {
        RoleName: $('#RoleName').val(),
        ManagerPrivileges: []
    };
    var all = $("input[name='privilege']").length;
    var chkPrivileges = $("input[name='privilege']:checked");
    if (chkPrivileges.length == 0) {
        $.dialog.tips("请至少选择一个权限！");
        return;
    }
    else {
        if (chkPrivileges.length == all) {
            var PrivilegeInfo = {
                Privilege: null
            };
            PrivilegeInfo.Privilege = 0;
            role.ManagerPrivileges.push(PrivilegeInfo);
        }
        else {
            $(chkPrivileges).each(function (index, item) {
                var PrivilegeInfo = {
                    Privilege: null
                };
                PrivilegeInfo.Privilege = ($(item).val());
                role.ManagerPrivileges.push(PrivilegeInfo);
            });
        }
    }
    return role;
}

function submitRole() {
    var object;

    if ($('form').valid()) {
        try {

            object = generateRoleInfo();
            if (object == null) return;

            var objectString = JSON.stringify(object);
            var loading = showLoading();
            $.post('./add', { roleJson: objectString }, function (result) {
                if (result.success) {
                    $.dialog.succeedTips('保存成功', function () { location.href = './Management'; });
                }
                else
                    $.dialog.errorTips('保存失败！' + result.msg);
                loading.close();
            }, "json");
        }
        catch (e) {
            $.dialog.errorTips(e.message);
        }
    }
}

$(function () {
    $('#RoleName').focus();

    //全选点击事件
    $('#checkall').click(function () {
        var checked = $(this).prop('checked');
        $("input[name='privilege']").prop('checked', checked);
        $("input[name='privilege_group']").prop('checked', checked);
    });

    //子项全选点击事件
    $("input[name='privilege_group']").click(function () {
        var checked = $(this).prop('checked');
        $(this).parents('.card-header').siblings('.card-body').find("input[name='privilege']").prop('checked', checked);
    });

});

