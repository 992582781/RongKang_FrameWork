using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Common
{


    /// <summary>
    /// 第一个参数 0表示不显示在添加页面，1表示显示在添加页面和列表页面（大于0说明就是需要在添加页面显示的），2表示不在显示列表显示,3表示不在手机页面显示
    /// 第二个参数 显示的汉字（比如Name为名称）
    /// 第三个参数 显示的PlaceHolder汉字（比如Name为只能填写汉字,可以为空）
    /// 第四个参数 数据验证的模式
    /// 第五个参数 控件类型 
    /// </summary>
    [global::System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public class FieldNameAttribute : Attribute
    {


        private readonly int _View_Flag;
        private readonly string _View_Name;
        private readonly string _View_PlaceHolder;
        private readonly Validate _Validate_Type;
        private readonly Control_Type _Control_Type;


        /// <summary>
        /// <summary>
        /// 0表示不显示在添加页面，1表示显示在添加页面和列表页面（大于0说明就是需要在添加页面显示的），2表示不在显示列表显示,3表示不在手机页面显示
        /// </summary>
        /// </summary>
        public int View_Flag
        {
            get { return _View_Flag; }
        }
        /// <summary>
        /// <summary>
        /// 显示的汉字（比如Name为名称）
        /// </summary>
        /// </summary>
        public string View_Name
        {
            get { return _View_Name; }
        }
        /// <summary>
        /// <summary>
        /// 显示的PlaceHolder汉字（比如Name为只能填写汉字,可以为空）
        /// </summary>
        /// </summary>
        public string View_PlaceHolder
        {
            get { return _View_PlaceHolder; }
        }
        /// <summary>
        /// 数据验证的模式
        /// </summary>
        public Validate Validate_Type
        {
            get { return _Validate_Type; }
        }
        /// <summary>
        /// 控件类型
        /// </summary>
        public Control_Type Control_Type
        {
            get { return _Control_Type; }
        }

        public FieldNameAttribute(int View_Flag, string View_Name, string View_PlaceHolder, Validate Validate_Type, Control_Type Control_Type)
        {
            _View_Flag = View_Flag;
            _View_Name = View_Name;
            _View_PlaceHolder = View_PlaceHolder;
            _Validate_Type = Validate_Type;
            _Control_Type = Control_Type;
        }


    }


    ///// <summary>
    ///// 判断是否显示，显示的名称，显示的PlaceHolder，验证模式，控件类型
    ///// </summary>
    //public class Model_Type
    //{
    //    /// <summary>
    //    /// /// <summary>
    //    /// 判断是否显示，显示的名称，显示的PlaceHolder，验证模式，控件类型
    //    /// </summary>
    //    /// </summary>
    //    /// <param name="View_Flag"></param>
    //    /// <param name="View_Name"></param>
    //    /// <param name="View_PlaceHolder"></param>
    //    /// <param name="Validate_Type"></param>
    //    /// <param name="Control_Type"></param>
    //    public Model_Type(int View_Flag, string View_Name, string View_PlaceHolder, Validate Validate_Type, Control_Type Control_Type) { }

    //    /// <summary>
    //    /// <summary>
    //    /// 0表示不显示在添加页面，1表示显示在添加页面和列表页面（大于0说明就是需要在添加页面显示的），2表示不在显示列表显示,3表示不在手机页面显示
    //    /// </summary>
    //    /// </summary>
    //    public int View_Flag { get; set; }

    //    /// <summary>
    //    /// <summary>
    //    /// 显示的汉字（比如Name为名称）
    //    /// </summary>
    //    /// </summary>
    //    public string View_Name { get; set; }

    //    /// <summary>
    //    /// <summary>
    //    /// 显示的PlaceHolder汉字（比如Name为只能填写汉字）
    //    /// </summary>
    //    /// </summary>
    //    public string View_PlaceHolder { get; set; }

    //    /// <summary>
    //    /// 数据验证的模式
    //    /// </summary>
    //    public Validate Validate_Type { get; set; }

    //    /// <summary>
    //    /// 控件类型
    //    /// </summary>
    //    public Control_Type Control_Type { get; set; }
    //}


}
