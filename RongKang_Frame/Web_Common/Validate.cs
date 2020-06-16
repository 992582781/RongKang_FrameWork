using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Common
{
    /// <summary>
    /// 数值验证的枚举
    /// </summary>
    public enum Validate
    {
        /// <summary>
        /// 无需验证
        /// </summary>
        [Description("无需验证")]
        Empty = 0,
        /// <summary>
        /// 必填
        /// </summary>
        [Description("必填")]
        Required = 1,


        /// <summary>
        /// 邮箱格式验证(必填)
        /// </summary>
        [Description("格式不正确")]
        Email = 2,
        /// <summary>
        /// 邮箱格式验证(可以不填写)
        /// </summary>
        [Description("格式不正确")]
        EmptyOrEmail = 3,



        /// <summary>
        /// 日期格式验证(必填)
        /// </summary>
        [Description("格式不正确")]
        Time = 4,

        /// <summary>
        /// 日期格式验证(可以不填写)
        /// </summary>
        [Description("格式不正确")]
        EmptyOrTime = 5,

        /// <summary>
        /// 手机号码格式验证(必填)
        /// </summary>
        [Description("格式不正确")]
        TelPhone = 6,
        /// <summary>
        /// 手机号码格式验证(可以不填写)
        /// </summary>
        [Description("格式不正确")]
        EmptyOrTelPhone = 7,




        /// <summary>
        /// 浮点数格式验证(必填)
        /// </summary>
        [Description("格式不正确")]
        Decimal = 8,
        /// <summary>
        /// 浮点数格式验证(可以不填写)
        /// </summary>
        [Description("格式不正确")]
        EmptyOrDecimal = 9,


        /// <summary>
        /// 数字格式验证(必填)
        /// </summary>
        [Description("格式不正确")]
        Number = 10,
        /// <summary>
        /// 数字格式验证(可以不填写)
        /// </summary>
        [Description("格式不正确")]
        EmptyOrNumber = 11,


        /// <summary>
        /// 密码格式验证(必填)
        /// </summary>
        [Description("以字母开头，长度在6~18之间，只能包含字母、数字和下划线")]
        Password = 12,
        /// <summary>
        /// 密码格式验证(可以不填写)
        /// </summary>
        [Description("以字母开头，长度在6~18之间，只能包含字母、数字和下划线")]
        EmptyOrPassword = 13,



        /// <summary>
        /// 身份证格式验证(必填)
        /// </summary>
        [Description("格式不正确")]
        IdentityCard = 14,
        /// <summary>
        /// 身份证格式验证(可以不填写)
        /// </summary>
        [Description("格式不正确")]
        EmptyOrIdentityCard = 15,


        /// <summary>
        /// 车牌验证(必填)
        /// </summary>
        [Description("格式不正确")]
        License = 16,
        /// <summary>
        /// 车牌验证(可以不填写)
        /// </summary>
        [Description("格式不正确")]
        EmptyOrLicense = 17,



        /// <summary>
        /// 中文验证(必填)
        /// </summary>
        [Description("请输入中文")]
        HasCHZN = 18,
        /// <summary>
        /// 中文验证(可以不填写)
        /// </summary>
        [Description("请输入中文")]
        EmptyOrHasCHZN = 19
    }

    public class EnumDescription
    {
        public static string GetEnumDescription(Validate enumValue)
        {

            string str = enumValue.ToString();

            System.Reflection.FieldInfo field = enumValue.GetType().GetField(str);

            object[] objs = field.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);

            if (objs == null || objs.Length == 0) return str;

            System.ComponentModel.DescriptionAttribute da = (System.ComponentModel.DescriptionAttribute)objs[0];

            return da.Description;

        }
    }


    /// <summary>
    /// 显示的控件枚举
    /// </summary>
    public enum Control_Type
    {
        /// <summary>
        /// 下拉框
        /// </summary>
        [Description("下拉框")]
        SelectText = 0,
        /// <summary>
        /// 文本框
        /// </summary>
        [Description("文本框")]
        Text = 1,
        /// <summary>
        /// 时间框
        /// </summary>
        [Description("时间框")]
        TimeText = 2,
        /// <summary>
        /// 数值框
        /// </summary>
        [Description("数值框")]
        NumberText = 3,
        /// <summary>
        /// 邮件框
        /// </summary>
        [Description("邮件框")]
        EmailText = 4,
        /// <summary>
        /// 网址框
        /// </summary>
        [Description("网址框")]
        UrlText = 4
    }

    public class Control_TypeDescription
    {
        public static string GetEnumDescription(Control_Type enumValue)
        {

            string str = enumValue.ToString();

            System.Reflection.FieldInfo field = enumValue.GetType().GetField(str);

            object[] objs = field.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);

            if (objs == null || objs.Length == 0) return str;

            System.ComponentModel.DescriptionAttribute da = (System.ComponentModel.DescriptionAttribute)objs[0];

            return da.Description;

        }
    }
}
