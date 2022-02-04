function MsgAlert(content) {
        AMUI.dialog.alert({
            title: '信息提示',
            content: content,
            onConfirm: function () {
                console.log('close');
            }
        });
}

function MsgAlert_Refresh(content) {
    AMUI.dialog.alert({
        title: '信息提示',
        content: content,
        onConfirm: function () {
            location = location;
        }
    });
}

function MsgAlert_href(content,href) {
    AMUI.dialog.confirm({
        title: '信息提示',
        content: content,
        onConfirm: function () {
            location.href = href;
        },
        onCancel: function () {
            console.log('onCancel')
        }
    });
}

function MsgAlert_Del(content, IDs) {
    AMUI.dialog.confirm({
        title: '信息提示',
        content: content,
        onConfirm: function () {
            del(IDs);
        },
        onCancel: function () {
            console.log('onCancel')
        }
    });
}
 
///拉黑，审核
function MsgAlert_verify(content, IDs) {
    AMUI.dialog.confirm({
        title: '信息提示',
        content: content,
        onConfirm: function () {
            verify(IDs);
        },
        onCancel: function () {
            console.log('onCancel')
        }
    });
}



function MsgAlert_onCancel(content) {
    AMUI.dialog.confirm({
        title: '错误提示',
        content: content,
        onConfirm: function () {
            console.log('onConfirm');
        },
        onCancel: function () {
            console.log('onCancel')
        }
    });
}

function Msgpopup(content) {
    AMUI.dialog.popup({
        title: '标题',
        content: content,
    });
}

function del(IDs) {
    var jsondata = {
        IDs: JSON.stringify(IDs),
        __RequestVerificationToken: $(".hid-token-page").find("input").val()
    }
    $.ajax({
        url: 'Del',   //对应于/controllerName/ActionName
        data: jsondata,
        type: 'post',
        cache: false,
        dataType: 'json',
        success: function (data) {
            if (data.Status) {
                IDs = "";
                MsgAlert_Refresh(data.Msg);
            }
            else {
                MsgAlert(data.Msg);
            }
        }
    });
}

 