//阿拉伯数字转换成大写汉字
function numberParseChina(money) {

    var IsNegative = false; // 是否是负数

    //汉字的数字
    var cnNums = new Array('零', '壹', '贰', '叁', '肆', '伍', '陆', '柒', '捌', '玖');
    //基本单位
    var cnIntRadice = new Array('', '拾', '佰', '仟');
    //对应整数部分扩展单位
    var cnIntUnits = new Array('', '万', '亿', '兆');
    //对应小数部分单位
    var cnDecUnits = new Array('角', '分', '毫', '厘');
    //整数金额时后面跟的字符
    var cnInteger = '整';
    //整型完以后的单位
    var cnIntLast = '圆';
    //最大处理的数字
    var maxNum = 999999999999999.9999;
    //金额整数部分
    var integerNum;
    //金额小数部分
    var decimalNum;
    //输出的中文金额字符串
    var chineseStr = '';
    //分离金额后用的数组，预定义
    var parts;
    if (money == '') { return ''; }
    money = parseFloat(money);
    if (money >= maxNum) {
        //超出最大处理数字
        return '';
    }
    if (money == 0) {
        chineseStr = cnNums[0] + cnIntLast + cnInteger;
        return chineseStr;
    }

    if (money != Math.abs(money)) {
        // 是负数则先转为正数
        money = Math.abs(money);
        IsNegative = true;
    }

    //转换为字符串
    money = money.toString();
    if (money.indexOf('.') == -1) {
        integerNum = money;
        decimalNum = '';
    } else {
        parts = money.split('.');
        integerNum = parts[0];
        decimalNum = parts[1].substr(0, 4);
    }
    //获取整型部分转换
    if (parseInt(integerNum, 10) > 0) {
        var zeroCount = 0;
        var IntLen = integerNum.length;
        for (var i = 0; i < IntLen; i++) {
            var n = integerNum.substr(i, 1);
            var p = IntLen - i - 1;
            var q = p / 4;
            var m = p % 4;
            if (n == '0') {
                zeroCount++;
            } else {
                if (zeroCount > 0) {
                    chineseStr += cnNums[0];
                }
                //归零
                zeroCount = 0;
                chineseStr += cnNums[parseInt(n)] + cnIntRadice[m];
            }
            if (m == 0 && zeroCount < 4) {
                chineseStr += cnIntUnits[q];
            }
        }
        chineseStr += cnIntLast;
    }
    //小数部分
    if (decimalNum != '') {
        var decLen = decimalNum.length;
        for (var i = 0; i < decLen; i++) {
            var n = decimalNum.substr(i, 1);
            if (n != '0') {
                chineseStr += cnNums[Number(n)] + cnDecUnits[i];
            }
        }
    }
    if (chineseStr == '') {
        chineseStr += cnNums[0] + cnIntLast + cnInteger;
    } else if (decimalNum == '') {
        chineseStr += cnInteger;
    }
    if (IsNegative == true) {
        return "负" + chineseStr;
    }
    else {
        return chineseStr;
    }
    //return chineseStr;
}

//千分位的分隔符
function toThousands(num, $this) {
    //若没有任何数据则直接返回
    if (num == "" || num == null) {
        return num;
    } else {
        var $amountInput = $($this);
        $("#div_" + $amountInput.attr("id")).text(numberParseChina(num));
        //判断是否有小数点
        var s = num.indexOf(".");
        if (s == -1) {//是整数
            return (num || 0).toString().replace(/(\d)(?=(?:\d{3})+$)/g, '$1,');
        } else {
            var arr = num.split(".");
            return (arr[0] || 0).toString().replace(/(\d)(?=(?:\d{3})+$)/g, '$1,') + "." + arr[1];
        }
    }
}

//小数点的校验
function decimalPoint($this) {
    var $amountInput = $($this);
    //响应鼠标事件，允许左右方向键移动 
    event = window.event || event;
    if (event.keyCode == 37 | event.keyCode == 39) {
        return;
    }
    //先把非数字的都替换掉，除了数字和. 
    var temp = $amountInput.val().replace(/[^\d.-]/g, "").
        //只允许一个小数点              
        replace(/^\./g, "").replace(/\.{2,}/g, ".").
        //只能输入小数点后两位
        replace(".", "$#$").replace(/\./g, "").replace("$#$", ".").replace(/^(\-)*(\d+)\.(\d\d).*$/, '$1$2.$3');
    //添加分隔符     
    $amountInput.val(toThousands(temp, $this));
}

//判断是否有小数点
function DecimalPlaces($this) {
    var $amountInput = $($this);
    //最后一位是小数点的话，移除
    $amountInput.val(($amountInput.val().replace(/\.$/g, "")));
    //保留两位小数
    var s = $amountInput.val();
    var arr = s.split(".");
    if (s == "") {
        $amountInput.val("0.00");
        return;
    } else if (arr.length == 1) {//没有小数点
        $amountInput.val(s + ".00");
        return;
    } else if (arr.length > 1 && arr[1].length < 2) {//有小数点
        $amountInput.val(s + "0");
        return;
    }
}