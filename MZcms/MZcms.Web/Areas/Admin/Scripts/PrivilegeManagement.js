$(function () {
    query();
})

function Delete(id) {
    $.dialog.confirm('确定删除该条记录吗？', function () {
        var loading = showLoading();
        $.post("./Delete", { id: id }, function (data) { $.dialog.tips(data.msg); query(); loading.close(); });
    });
}

function query() {
    $("#list").MZcmsDatagrid({
        url: './List',
        nowrap: false,
        rownumbers: true,
        NoDataMsg: '没有找到符合条件的数据',
        border: false,
        fit: true,
        fitColumns: true,
        pagination: false,
        idField: "Id",
        queryParams: {},
        toolbar: /*"#goods-datagrid-toolbar",*/'',
        columns:
            [[
                { field: "Id", hidden: true },
                { field: "Name", title: '权限组名', align: "left" },

                {
                    field: "operation", operation: true, title: "操作", align: "right",
                    formatter: function (value, row, index) {
                        var id = row.Id.toString();
                        var html = ["<span class=\"btn-group\">"];
                        html.push("<a class=\"btn btn-default btn-sm\" href='./Edit/" + id + "'>编辑</a>");
                        html.push("<span class=\"btn btn-danger btn-sm\" onclick=\"Delete('" + id + "');\">删除</span>");
                        html.push("</span>");
                        return html.join("");
                    }
                }
            ]]
    });
}