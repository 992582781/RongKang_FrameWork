/// <reference path="amazeui.page.js" />
 $(function () {
        var IDs = new Array();
        var ID;

        $(".allcheck").click(function () {
            var flag = $(this).prop("checked");
            $(".checkbox").each(function (index) {
                $(".checkbox").eq(index).prop("checked", flag);
                ID = $(".checkbox").eq(index).attr("data-rowindex");
                IDs.push(ID);
                alert(ID);
            })  
        })

        $('#Add').click(function () {
            MsgAlert_href("新增数据", "Edit")
        })

        $('.edit').click(function () {
            ID = $(this).parent().parent().parent().parent().find(".checkbox").attr("data-rowindex");
            MsgAlert_href("编辑数据", "Edit?ID=" + ID);
        })
 

        $('.del').click(function () {
            ID = $(this).parent().parent().parent().parent().find(".checkbox").attr("data-rowindex");
            IDs.push(ID);
            MsgAlert_Del("确定删除吗？",IDs)
        })





        $('.checkbox').click(function () {
            ID = $(this).attr("data-rowindex");
            if ($(this).prop("checked"))
            {
                IDs.push(ID);
            }
            else
            {
                IDs.pop(ID);
            }
        })

        $('.alldel').click(function () {
            if (IDs.length < 1)
            {
                MsgAlert("没有选择需要删除的数据！")
                return;
            }
            MsgAlert_Del("确定删除吗？", IDs)
        })

 });

function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}