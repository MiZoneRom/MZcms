﻿
(function ($) {
    $.extend(Array.prototype, {
        indexOf: function (o) {
            for (var i = 0, len = this.length; i < len; i++) {
                if (this[i] == o) {
                    return i;
                }
            }
            return -1;
        }, remove: function (o) {
            var index = this.indexOf(o);
            if (index != -1) {
                this.splice(index, 1);
            }
            return this;
        }, removeById: function (filed, id) {
            for (var i = 0, len = this.length; i < len; i++) {
                if (this[i][filed] == id) {
                    this.splice(i, 1);
                    return this;
                }
            }
            return this;
        }
    });
    //内部方法
    //请求Ajax方法
    function request(target, param) {
        var opts = $.data(target, "MZcmsDatagrid").options;
        if (param) {
            opts.queryParams = $.extend({}, opts.queryParams, param);
        }
        if (!opts.url) {
            return;
        }
        var queryObj = $.extend({}, opts.queryParams, { page: opts.pageNumber, rows: opts.pageSize });

        //显示加载信息
        $(target).MZcmsDatagrid("loading");

        //Ajax获取数据
        setTimeout(function () {
            ajaxRequest();
        }, 0);



        (function initialOperationBtn() {
            //初始化操作按钮
            var operationButtons = $(opts.operationButtons);
            if (operationButtons.length) {
                opts.operationButtonsInnerHTML = operationButtons.html();
                if (operationButtons.hasClass('keep') == false)
                    operationButtons.remove();
            }

            //初始化Toolbar
            if ($(opts.toolbar).length) {
                var inner = $(opts.toolbar).html();
                $(opts.toolbar).remove();
                opts.toolbarInnerHTML = inner;
                $(target).prev("form").remove();
                $(opts.toolbarInnerHTML).insertBefore($(target));
            }
        })();
        function ajaxRequest() {
            $.ajax({
                type: opts.method,
                url: opts.url,
                data: queryObj,
                dataType: "json",
                success: function (data) {
                    setTimeout(function () {
                        $(target).MZcmsDatagrid("loaded");
                    }, 0);

                    loadData(target, data);

                },
                error: function () {
                    setTimeout(function () {
                        $(target).MZcmsDatagrid("loaded");
                    }, 0);

                    if (opts.onLoadError) {
                        opts.onLoadError.apply(target, arguments);
                    }
                }
            });
        };
    };

    function getSelectRows(target) {
        var opts = $.data(target, "MZcmsDatagrid").options;
        var data = $.data(target, "MZcmsDatagrid").data;
        var tBody = $(target).find("tbody");
        if (opts.idField) {
            return $.data(target, "MZcmsDatagrid").selectedRows;
        } else {
            var rowAry = [];
            $("tr.MZcmsDatagrid-row-selected", tBody).each(function () {
                var row_index = parseInt($(this).attr("MZcmsDatagrid-row-index"));
                rowAry.push(data.rows[row_index]);
            });
            return rowAry;
        }
    };

    function selectAll(target) {
        var opts = $.data(target, "MZcmsDatagrid").options;
        var data = $.data(target, "MZcmsDatagrid").data;
        var selectRows = $.data(target, "MZcmsDatagrid").selectedRows;
        var tBody = $(target).find("tbody");
        var rows = data.rows;
        selectRows = [];
        $("div.icheck-primary input[type=checkbox]:enabled", tBody)
            .prop("checked", true).parents('tr').addClass("MZcmsDatagrid-row-selected");
        for (var i = 0; i < rows.length; i++) {
            if (opts.idField) {
                var row = rows[i];
                if ($("div.icheck-primary input[type=checkbox]").eq(i).is(':checked')) {
                    selectRows.push(row);
                }
            }
        }
        $.data(target, "MZcmsDatagrid").selectedRows = selectRows;
    };
    function clearSelectRows(target) {
        var opts = $.data(target, "MZcmsDatagrid").options;
        var data = $.data(target, "MZcmsDatagrid").data;
        var selectRows = $.data(target, "MZcmsDatagrid").selectedRows;
        var tBody = $(target).find("tbody");
        $("tr", tBody).removeClass("MZcmsDatagrid-row-selected");
        $("div.icheck-primary input[type=checkbox]", tBody).prop("checked", false);
        if (opts.idField && data.rows != null) {
            for (var i = 0; i < data.rows.length; i++) {
                selectRows.removeById(opts.idField, data.rows[i][opts.idField]);
            }
        }
    };

    function clearSelections(target) {
        clearSelectRows(target);
        var selectRows = $.data(target, "MZcmsDatagrid").selectedRows;
        selectRows.splice(0, selectRows.length);
    };

    function getRowByIndex(target, index) {
        var data = $.data(target, "MZcmsDatagrid").data;
        if (index < 0 || index >= data.rows.length || isNaN(index)) return;
        return data.rows[index];
    }

    function selectRow(target, index) {
        var opts = $.data(target, "MZcmsDatagrid").options;
        var data = $.data(target, "MZcmsDatagrid").data;
        var tBody = $(target).find("tbody");
        var selectRows = $.data(target, "MZcmsDatagrid").selectedRows;
        if (index < 0 || index >= data.rows.length) {
            return;
        }
        if (opts.singleSelect == true) {
            clearSelections(target);
        }
        var tr = $("tr[MZcmsDatagrid-row-index=" + index + "]", tBody);
        if (!tr.hasClass("MZcmsDatagrid-row-selected")) {
            tr.addClass("MZcmsDatagrid-row-selected");
            var ck = $("div.icheck-primary input[type=checkbox]", tr);
            ck.attr("checked", true);
            if (opts.idField) {
                var row = data.rows[index];
                (function () {
                    for (var i = 0; i < selectRows.length; i++) {
                        if (selectRows[i][opts.idField] == row[opts.idField]) {
                            return;
                        }
                    }
                    selectRows.push(row);
                    //console.log(selectRows);
                })();
            }
        }
    };


    function unSelectRow(target, index) {
        var opts = $.data(target, "MZcmsDatagrid").options;
        var data = $.data(target, "MZcmsDatagrid").data;
        var selectRows = $.data(target, "MZcmsDatagrid").selectedRows;
        if (index < 0 || index >= data.rows.length) {
            return;
        }
        var tBody = $(target).find("tbody");
        var tr = $("tr[MZcmsDatagrid-row-index=" + index + "]", tBody);
        var ck = $("tr[MZcmsDatagrid-row-index=" + index + "] div.icheck-primary input[type=checkbox]", tBody);
        tr.removeClass("MZcmsDatagrid-row-selected");
        ck.prop("checked", false);
        var row = data.rows[index];
        if (opts.idField) {
            selectRows.removeById(opts.idField, row[opts.idField]);
        }
    };

    //绑定事件预留方法
    function bindCellsEvents(target) {
        var opts = $.data(target, "MZcmsDatagrid").options;
        var total = $.data(target, "MZcmsDatagrid").data.total;
        var pageTarget = $(target).parent().parent('.card');

        var before = $("a.beforePageBtn", pageTarget);
        var after = $("a.afterPageBtn", pageTarget);
        var pageCount = parseInt(total / opts.pageSize) + (total % opts.pageSize == 0 ? 0 : 1);

        //排序事件-xielingxiao
        $(target).off('click', 'th[data-sort]');
        $(target).on('click', 'th[data-sort]', function () {
            var IsAsc = false;
            $(this).siblings().removeClass('state_switch select');
            if (!$(this).hasClass('select')) {
                $(this).addClass('select');
            } else {
                if ($(this).hasClass('state_switch')) {
                    $(this).removeClass('state_switch');
                    IsAsc = false;
                } else {
                    $(this).addClass('state_switch');
                    IsAsc = true;
                }
            }
            request(target, { Sort: $(this).data('sort'), IsAsc: IsAsc });
        });


        //翻页跳转弹框
        $('.jump-to,.tipic-dialog', pageTarget).click(function (e) {
            e ? e.stopPropagation() : event.cancelBubble = true;
        });

        $('.jump-to', pageTarget).click(function () {
            $('.tipic-dialog', pageTarget).fadeToggle(200).find('input').first().focus();
        });

        $('.pageSizeSelect', pageTarget).change(function () {
            opts.pageNumber = 1;
            opts.pageSize = this.value;
            request(target);
        });

        $(document).click(function () {
            $('.tipic-dialog', pageTarget).hide();
        });

        if (opts.pageNumber > 1) {
            $(before).removeAttr('disabled');
            $(before).unbind("click").bind("click", function () {
                if (opts.pageNumber > 1) {
                    opts.pageNumber = parseInt(opts.pageNumber) - 1;
                    request(target);
                } else {
                    $.dialog.errorTips('已经是第一页了');
                }

            });
        } else {
            $(before).attr('disabled', 'disabled');
        }
        if (opts.pageNumber < pageCount) {
            $(after).removeAttr('disabled');
            $(after).unbind("click").bind("click", function () {
                if (opts.pageNumber < pageCount) {
                    opts.pageNumber = parseInt(opts.pageNumber) + 1;
                    request(target);
                } else {
                    $.dialog.errorTips('已经是最后一页了');
                }

            });
        } else {
            $(after).attr('disabled', 'disabled');
        }

        $("input.pageJumpBtn", pageTarget).unbind("click").bind("click", function () {
            var jump = $("input.pageNumberTxt", pageTarget).val();
            jump = jump.replace(/\b(0+)/gi, "");//去掉前置0
            if (isNaN(jump) || jump > pageCount || jump <= 0) {
                $.dialog.errorTips('请您输入有效的页码');
                $("input.pageNumberTxt", pageTarget).val("");
            }
            else {
                opts.pageNumber = jump;
                request(target);
            }
        });

        $("div.checkbox-toggle", pageTarget).unbind("click").bind("click", function () {
            if (opts.singleSelect) {
                return false;
            }
            if (!$(this).hasClass('checked')) {
                $(this).addClass('checked').html('<i class="far fa-check-square"></i>');
                selectAll(target);
            } else {
                $(this).removeClass('checked').html('<i class="far fa-square"></i>');
                clearSelectRows(target);
            }
        });

        $("div.icheck-primary input[type=checkbox]", $(target)).unbind("click").bind("click", function (e) {
            var findIndex = $(this).parent().parent().parent().attr("MZcmsDatagrid-row-index");
            if (opts.singleSelect) {
                clearSelections(target);
                selectRow(target, findIndex);
            } else {
                if ($(this).is(":checked")) {
                    selectRow(target, findIndex);
                } else {
                    unSelectRow(target, findIndex);
                }
            }
            e.stopPropagation();
        });
    }

    //加载数据到列表
    function loadData(target, _data) {


        var opts = $.data(target, "MZcmsDatagrid").options;

        var selectRows = $.data(target, "MZcmsDatagrid").selectedRows;

        $.data(target, "MZcmsDatagrid").data = _data;

        //开始渲染列表
        opts.view.render.call(opts.view, target, $(target));

        initialPager(target);
        bindCellsEvents(target);

        clearSelections(target);
        opts.onLoadSuccess();

    };

    //获取所有列表的列集合
    function getColumnFiles(target) {
        var opts = $.data(target, "MZcmsDatagrid").options;
        var cols = opts.columns;
        if (cols.length == 0) {
            return [];
        }
        var columnFileds = [];
        function findLocation(index) {
            var c = 0;
            var i = 0;
            while (true) {
                if (columnFileds[i] == undefined) {
                    if (c == index) {
                        return i;
                    }
                    c++;
                }
                i++;
            }
        };
        function getColumnFileds(r) {
            var ff = [];
            var c = 0;
            for (var i = 0; i < cols[r].length; i++) {
                var col = cols[r][i];
                if (col == null || col == undefined) continue;
                if (col.field) {
                    ff.push([c, col.field]);
                }
                c += parseInt(col.colspan || "1");
            }
            for (var i = 0; i < ff.length; i++) {
                ff[i][0] = findLocation(ff[i][0]);
            }
            for (var i = 0; i < ff.length; i++) {
                var f = ff[i];
                columnFileds[f[0]] = f[1];
            }
        };
        for (var i = 0; i < cols.length; i++) {
            getColumnFileds(i);
        }
        return columnFileds;
    };

    function getColOptions(target, filed) {
        var opts = $.data(target, "MZcmsDatagrid").options;
        if (opts.columns) {
            for (var i = 0; i < opts.columns.length; i++) {
                var colOpts = opts.columns[i];
                for (var j = 0; j < colOpts.length; j++) {
                    var col = colOpts[j];
                    if (col.field == filed) {
                        return col;
                    }
                }
            }
        }
        return null;
    };

    function initialPager(target) {

        var total = $.data(target, "MZcmsDatagrid").data.total;
        var opts = $.data(target, "MZcmsDatagrid").options;
        $(target).parent().parent('.card').find(".mailbox-controls").remove();
        if (total > 0) {
            var pageCount = parseInt(total / opts.pageSize) + (total % opts.pageSize == 0 ? 0 : 1);
            if (isNaN(pageCount)) pageCount = 1;
            if (pageCount < 1) pageCount = 1;
            if (total == 0) pageCount = 0;
            var currentPage = opts.pageNumber;
            if (opts.pagination) {
                var htmlArray = ["<div class=\"mailbox-controls\">"];
                htmlArray.push("<div class=\"btn-group\">");
                if (opts.hasCheckbox && !opts.singleSelect) {
                    htmlArray.push("<div class='btn btn-default btn-sm checkbox-toggle'><i class=\"far fa-square\"></i></div>");
                }
                htmlArray.push(opts.operationButtonsInnerHTML);
                htmlArray.push("</div>");
                htmlArray.push("<div class=\"float-right\">");
                htmlArray.push("<span class=\"active-page\">" + currentPage.toString() + "-" + pageCount.toString() + "/" + total.toString() + " </span>");
                htmlArray.push("<div class=\"btn-group\">");
                htmlArray.push("    <a class=\"btn btn-default btn-sm beforePageBtn\" href=\"javascript:void(0);\"><i class=\"fas fa-chevron-left\"></i></a>");
                htmlArray.push("    <a class=\"btn btn-default btn-sm afterPageBtn\" href=\"javascript:void(0);\"><i class=\"fas fa-chevron-right\"></i></a>");
                htmlArray.push("</div>");
                htmlArray.push("<div class=\"btn-group\">");
                htmlArray.push("<a class=\"btn btn-default btn-sm jump-to\"><i class=\"fas fa-share\"></i></a>");
                htmlArray.push("</div>");

                var str = '<div class="btn-group"><a class="btn btn-default btn-sm">每页</a><select class="btn btn-default btn-sm pageSizeSelect">';
                if (opts.pageArr.indexOf(opts.pageSize) == -1) {
                    opts.pageArr.unshift(10);
                    if (opts.pageSize < 10) {
                        opts.pageArr[0] = opts.pageSize;
                    } else {
                        opts.pageArr[1] = opts.pageSize;
                    }
                }
                for (var i = 0; i < opts.pageArr.length; i++) {
                    var item = opts.pageArr[i];
                    str += '<option value=' + item + ' ' + (opts.pageSize == item ? 'selected' : '') + '>' + item + '</option>';
                }

                htmlArray.push(str + '</select><a class="btn btn-default btn-sm">条</a></div>');
                htmlArray.push("<div class=\"btn-group tipic-dialog\" style=\"display:none;\">");
                htmlArray.push("<span class=\"btn btn-default btn-sm\">跳转到第</span><input class=\"btn btn-default btn-sm pageNumberTxt input-int-num\" type=\"text\" name=\"\"><span class=\"btn btn-default btn-sm\">页</span>");
                htmlArray.push("<input type=\"button\" class=\"btn btn-default btn-sm pageJumpBtn\" value=\"确定\"></input>");
                htmlArray.push("</div></div></div>");


                $(target).parent().siblings('.card-footer').append($(htmlArray.join("")));

                $('.input-int-num').keyup(function () {
                    (this.v = function () { this.value = this.value.replace(/[^0-9-]+/, ''); if (this.value == '') { this.value = '1' } }).call(this);
                }).blur(function () {
                    this.v();
                });

            }
        }
    }



    //插件构造函数
    $.fn.MZcmsDatagrid = function (options, param) {
        if (typeof options == "string") {
            return $.fn.MZcmsDatagrid.methods[options](this, param);
        }

        options = options || {};
        return this.each(function () {
            var state = $.data(this, "MZcmsDatagrid");
            var opts;
            if (state) {
                opts = $.extend(state.options, options);
                state.options = opts;
            } else {
                opts = $.extend({}, $.fn.MZcmsDatagrid.defaults, options);
                //console.log(opts);
                $.data(this, "MZcmsDatagrid", {
                    options: opts,
                    selectedRows: [],
                    data: {
                        total: 0,
                        rows: []
                    }
                });
            }
            opts.view.renderHead.call(opts.view, this, $(this));

            //Ajax获取数据
            if (opts.url) {
                request(this);
            }
        });
    };

    //插件对外公开的所有方法集合
    $.fn.MZcmsDatagrid.methods = {
        options: function (jq) {
            var opts = $.data(jq[0], "MZcmsDatagrid").options;
            return opts;
        },
        data: function (jq) {
            var data = $.data(jq[0], "MZcmsDatagrid").data;
            return data;
        },
        reload: function (jq, param) {
            return jq.each(function () {
                if (!param.pageNumber) param.pageNumber = 1;
                $.data(this, "MZcmsDatagrid").options.pageNumber = param.pageNumber;
                request(this, param);
            });
        },
        clearReload: function (jq, param) {
            return jq.each(function () {
                var opts = $.data(jq[0], "MZcmsDatagrid").options;
                opts.pageNumber = 1;
                opts.queryParams = param || {};
                request(this);
            });
        },
        //显示正在加载提示信息
        loading: function (jq) {
            return jq.each(function () {
                var opts = $.data(this, "MZcmsDatagrid").options;

                if (opts.loadMsg) {
                    /*  显示正在加载提示信息
                    */
                    var html = ["<tbody><tr style='display:none;'><td colspan=" + opts.columns[0].length + "></td></tr></tbody>"];
                    if ($("#loadAjax").length === 0) {
                        html.push("<tr id='loadAjax'><td style='height:100px;text-align:center;' colspan='" + opts.columns[0].length.toString() + "'><div class=\"overlay\"><i class=\"fas fa-sync-alt\"></i></div></td></tr>");
                    }
                    $(jq).find("tbody").remove();
                    $(jq).append(html.join(""));
                }
            });
        },
        //清除加载提示信息
        loaded: function (jq) {
            return jq.each(function () {
                /* 清除加载提示信息
                */
                $("#loadDiv", $(jq)).remove();
            });
        },

        loadData: function (jq, data) {
            return jq.each(function () {
                loadData(this, data);
                //cacheRows(this);
            });
        },
        getSelected: function (jq) {
            var rows = getSelectRows(jq[0]);
            return rows.length > 0 ? rows[0] : null;
        },
        getRows: function (jq) {
            return $.data(jq[0], "MZcmsDatagrid").data.rows;
        },
        getSelections: function (jq) {
            return getSelectRows(jq[0]);
        },

        clearSelections: function (jq) {
            return jq.each(function () {
                clearSelections(this);
            });
        },
        getRowByIndex: function (jq, index) {
            return getRowByIndex(jq[0], index)
        },
        selectRow: function (jq, index) {
            return jq.each(function () {
                selectRow(this, index);
            });
        }, getColumnFields: function (jq) {
            return getColumnFiles(jq[0]);
        }, getColumnOption: function (jq, filed) {
            return getColOptions(jq[0], filed);
        }
    };

    var renderView = {
        render: function (target, container) {
            var opts = $.data(target, "MZcmsDatagrid").options;
            var rows = $.data(target, "MZcmsDatagrid").data.rows;
            var closFileds = $(target).MZcmsDatagrid("getColumnFields");

            if (rows == null || rows.length <= 0) {
                $("#loadAjax").remove();
                var html = ["<tbody><tr><td style='text-align:center;' colspan=" + opts.columns[0].length + "><h2 style='font-size: 18px;color:#8e8f92;'>" + opts.NoDataMsg + "</h2></td></tr></tbody>"];
                $(container).find("tbody").remove();
                $(container).append(html.join(""));
            }
            else {
                $("#loadAjax").remove();
                $(container).find("tbody tr").show();
                var htmlAry = ["<tbody>"];
                var rowData = {};
                for (var i = 0; i < rows.length; i++) {
                    var cls = (i % 2 && opts.striped) ? "class=\"MZcmsDatagrid-row-alt\"" : "";
                    var rowStyle = opts.rowStyler ? opts.rowStyler.call(target, i, rows[i]) : "";
                    var strStyle = rowStyle ? "style=\"" + rowStyle + "\"" : "";
                    var rowHead = opts.rowHeadFormatter ? opts.rowHeadFormatter.call(this, target, i, rows[i]) : "";
                    var rowFoot = opts.rowFootFormatter ? opts.rowFootFormatter.call(this, target, i, rows[i]) : "";
                    htmlAry.push(rowHead);
                    if (opts.childField && rows[i][opts.childField].length > 0) {
                        for (var j = 0; j < rows[i][opts.childField].length; j++) {
                            var childData = $.extend({}, rows[i], rows[i][opts.childField][j]);
                            htmlAry.push("<tr MZcmsDatagrid-row-index=\"" + i + "\" " + cls + " " + strStyle + " ");
                            if (opts.rowFormatter)
                                htmlAry.push(opts.rowFormatter(childData, i));
                            htmlAry.push(" >");
                            htmlAry.push(this.renderRow.call(this, target, closFileds, i, childData));
                            htmlAry.push("</tr>");
                        }
                    }
                    else {
                        htmlAry.push("<tr MZcmsDatagrid-row-index=\"" + i + "\" " + cls + " " + strStyle + " ");
                        if (opts.rowFormatter)
                            htmlAry.push(opts.rowFormatter(rows[i], i));
                        htmlAry.push(" >");
                        htmlAry.push(this.renderRow.call(this, target, closFileds, i, rows[i]));
                        htmlAry.push("</tr>");
                    }
                    htmlAry.push(rowFoot);
                }
                htmlAry.push("</tbody>");
                $(container).find("tbody").remove();
                $(container).append(htmlAry.join(""));
            }
        },
        renderHead: function (target, container) {
            var opts = $.data(target, "MZcmsDatagrid").options;
            var closFileds = $(target).MZcmsDatagrid("getColumnFields");
            var htmlAry = ["<thead><tr>"];

            for (var i = 0; i < closFileds.length; i++) {
                if (closFileds[i] == "门店未授权") {
                    continue;
                }
                var content = "",
                    className = "",
                    findFiled = closFileds[i],
                    col = $(target).MZcmsDatagrid("getColumnOption", findFiled),
                    sort = '',
                    sortHtml = '',
                    widthStyle = col.width === undefined ? "width:auto;" : "width:" + (col.width) + "px;";
                if (col.checkbox) {
                    content = "选择";
                    className = " class=\"td-choose\"";
                    opts.hasCheckbox = true;
                } else {
                    content = col.title;
                }
                if (col.operation) {
                    content = "操作";
                    className = " class=\"td-operate\"";
                }

                //添加排序功能-xielingxiao
                if (col.sort) {
                    sort = ' data-sort="' + col.field + '"';
                    content += '<i class="icon_sort"></i>';
                }

                var style = col.hidden ? "style=\"display:none;\"" : "style=\"text-align:center;" + widthStyle + "\"";
                htmlAry.push('<th ' + className + style + sort + '>');
                htmlAry.push(content);
                htmlAry.push("</th>");
            }

            htmlAry.push("<tr id='loadAjax'><th style='height:100px;text-align:center;' colspan='" + closFileds.length.toString() + "'><div class=\"overlay\"><i class=\"fas fa-sync-alt\"></i></div></th></tr>");
            htmlAry.push("</tr></thead>");
            if (!$(container).find("thead").length)
                $(container).html(htmlAry.join(""));
        },
        renderRow: function (target, fileds, rowIndex, rowData) {
            var opts = $.data(target, "MZcmsDatagrid").options;
            var cc = [];
            for (var i = 0; i < fileds.length; i++) {
                var findFiled = fileds[i];
                if (findFiled == "门店未授权") {
                    continue;
                }
                var col = $(target).MZcmsDatagrid("getColumnOption", findFiled);
                if (col) {
                    var className = "";
                    var findStyler = col.styler ? (col.styler(rowData[findFiled], rowData, rowIndex) || "") : "";
                    var width = col.width === undefined ? "width:auto;" : "width:" + (col.width) + "px;";
                    var strFindStyler = col.hidden ? "style=\"display:none;" + findStyler + "\"" : (findStyler ? "style=\"" + findStyler + width + "\"" : "style=\"" + width + "\"");

                    if (col.checkbox) {
                        className = " class=\"td-choose\"";
                    } else if (col.operation) {
                        className = " class=\"td-operate td-lg\"";
                    }
                    cc.push("<td " + className + "  field=\"" + findFiled + "\" " + strFindStyler + ">");

                    //var strFindStyler = col.width === undefined ? "width:auto;" : "width:" + (col.width) + "px;";
                    strFindStyler = "text-align:" + (col.align || "center") + ";";
                    cc.push("<div style=\"" + strFindStyler + "\" ");
                    if (col.checkbox) {
                        cc.push(" class=\"icheck-primary ");
                    } else {
                        cc.push(" class=\"MZcmsDatagrid-cell ");
                    }
                    cc.push("\">");
                    if (col.checkbox) {
                        if (col.formatter) {
                            cc.push(col.formatter(rowData[findFiled], rowData, rowIndex));
                        } else {
                            cc.push("<input type=\"checkbox\" id=\"check1\" />");
                            cc.push("<label for=\"check1\"></label>");
                        }
                    } else {
                        if (col.formatter) {
                            cc.push(col.formatter(rowData[findFiled], rowData, rowIndex));
                        } else {
                            cc.push(rowData[findFiled]);
                        }
                    }
                    cc.push("</div>");
                    cc.push("</td>");
                }
            }
            return cc.join("");
        }
    };
    $.fn.MZcmsDatagrid.defaults = {
        rowFormatter: null,
        rowHeadFormatter: null,
        rowFootFormatter: null,
        columns: null,
        toolbar: null,
        toolbarInnerHTML: '',
        operationButtons: null,
        operationButtonsInnerHTML: null,
        method: "post",
        hasCheckbox: false,
        idField: null,
        url: null,
        loadMsg: "正在加载,请稍等 ...",
        NoDataMsg: '没有找到符合条件的数据',
        singleSelect: false,
        pagination: false,
        pageNumber: 1,
        pageSize: 20,
        pageArr: [10, 20, 50, 100, 200],
        queryParams: {},
        sortOrder: "asc",
        view: renderView,
        onLoadSuccess: function () { },
        onLoadError: function () { },
        childField: ''
    };

})(jQuery);