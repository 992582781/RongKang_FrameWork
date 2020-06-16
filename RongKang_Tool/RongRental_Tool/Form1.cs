using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace RongKang_Tool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Four_Click(object sender, EventArgs e)
        {
            string path = @"D:\RongKang_Tool";

            string classname = this.ClassName.Text.ToString().Trim();

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            #region 生成ibll
            string Filepath = @"D:\RongKang_Tool\" + "I" + classname + "Bll.cs";

            File.Delete(Filepath);

            if (!File.Exists(Filepath))
            {
                FileStream fs1 = new FileStream(Filepath, FileMode.Create, FileAccess.Write);//创建写入文件 
                StreamWriter sw = new StreamWriter(fs1);
                sw.WriteLine("using System;");//开始写入值
                sw.WriteLine("using System.Collections.Generic;");//开始写入值
                sw.WriteLine("using System.Linq;");//开始写入值
                sw.WriteLine("using System.Text;");//开始写入值
                sw.WriteLine("using System.Threading.Tasks;");//开始写入值

                sw.WriteLine("namespace RongKang_IBll");
                sw.WriteLine("{");
                sw.WriteLine(" public interface I" + classname + "Bll<T> : IBaseBll<T>");
                sw.WriteLine("{");
                sw.WriteLine("}");
                sw.WriteLine("}");
                sw.Close();
                fs1.Close();
            }
            else
            {
                FileStream fs = new FileStream(Filepath, FileMode.Open, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("using System;");//开始写入值
                sw.WriteLine("using System.Collections.Generic;");//开始写入值
                sw.WriteLine("using System.Linq;");//开始写入值
                sw.WriteLine("using System.Text;");//开始写入值
                sw.WriteLine("using System.Threading.Tasks;");//开始写入值

                sw.WriteLine("namespace RongKang_IBll");
                sw.WriteLine("{");
                sw.WriteLine(" public interface I" + classname + "Bll<T> : IBaseBll<T>");
                sw.WriteLine("{");
                sw.WriteLine("}");
                sw.WriteLine("}");
                sw.Close();
                fs.Close();
            }
            #endregion



            #region 生成bll
            Filepath = @"D:\RongKang_Tool\" + "" + classname + "Bll.cs";
            File.Delete(Filepath);
            if (!File.Exists(Filepath))
            {
                FileStream fs1 = new FileStream(Filepath, FileMode.Create, FileAccess.Write);//创建写入文件 
                StreamWriter sw = new StreamWriter(fs1);
                sw.WriteLine("using System;");//开始写入值
                sw.WriteLine("using System.Collections.Generic;");//开始写入值
                sw.WriteLine("using System.Linq;");//开始写入值
                sw.WriteLine("using System.Text;");//开始写入值
                sw.WriteLine("using System.Threading.Tasks;");//开始写入值
                sw.WriteLine("using RongKang_Entity;");
                sw.WriteLine("using RongKang_ViewModel;");
                sw.WriteLine("using RongKang_IBll;");
                sw.WriteLine("using RongKang_IDal;");

                sw.WriteLine("namespace RongKang_Bll");
                sw.WriteLine("{");
                sw.WriteLine(" public class " + classname + "Bll : BaseBll<" + classname + ">,I" + classname + "Bll<" + classname + ">");
                sw.WriteLine("{");

                sw.WriteLine("private I" + classname + "Dal<" + classname + "> dal;");
                sw.WriteLine("public " + classname + "Bll(I" + classname + "Dal<" + classname + "> dal)");
                sw.WriteLine(": base(dal)");
                sw.WriteLine("{");
                sw.WriteLine("this.dal = dal;//在构造函数中初始化类的dal属性");
                sw.WriteLine("}");
                sw.WriteLine("}");
                sw.WriteLine("}");
                sw.Close();
                fs1.Close();
            }
            else
            {
                FileStream fs = new FileStream(Filepath, FileMode.Open, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("using System;");//开始写入值
                sw.WriteLine("using System.Collections.Generic;");//开始写入值
                sw.WriteLine("using System.Linq;");//开始写入值
                sw.WriteLine("using System.Text;");//开始写入值
                sw.WriteLine("using System.Threading.Tasks;");//开始写入值
                sw.WriteLine("using RongKang_Entity;");
                sw.WriteLine("using RongKang_IBll;");
                sw.WriteLine("using RongKang_IDal;");

                sw.WriteLine("namespace RongKang_IBll");
                sw.WriteLine("{");
                sw.WriteLine(" public class " + classname + "Bll : BaseBll<" + classname + ">,I" + classname + "Bll<" + classname + ">");
                sw.WriteLine("{");

                sw.WriteLine("private IBaseDal<" + classname + "> dal;");
                sw.WriteLine("public " + classname + "Bll(I" + classname + "Dal<" + classname + "> dal)");
                sw.WriteLine(": base(dal)");
                sw.WriteLine("{");
                sw.WriteLine("this.dal = dal;//在构造函数中初始化类的dal属性");
                sw.WriteLine("}");
                sw.WriteLine("}");
                sw.WriteLine("}");
                fs.Close();
            }
            #endregion



            #region 生成iDal
            Filepath = @"D:\RongKang_Tool\" + "I" + classname + "Dal.cs";
            File.Delete(Filepath);
            if (!File.Exists(Filepath))
            {
                FileStream fs1 = new FileStream(Filepath, FileMode.Create, FileAccess.Write);//创建写入文件 
                StreamWriter sw = new StreamWriter(fs1);
                sw.WriteLine("using System;");//开始写入值
                sw.WriteLine("using System.Collections.Generic;");//开始写入值
                sw.WriteLine("using System.Linq;");//开始写入值
                sw.WriteLine("using System.Text;");//开始写入值
                sw.WriteLine("using System.Threading.Tasks;");//开始写入值

                sw.WriteLine("namespace RongKang_IDal");
                sw.WriteLine("{");
                sw.WriteLine(" public interface I" + classname + "Dal<T> : IBaseDal<T>");
                sw.WriteLine("{");
                sw.WriteLine("}");
                sw.WriteLine("}");
                sw.Close();
                fs1.Close();
            }
            else
            {
                FileStream fs = new FileStream(Filepath, FileMode.Open, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("using System;");//开始写入值
                sw.WriteLine("using System.Collections.Generic;");//开始写入值
                sw.WriteLine("using System.Linq;");//开始写入值
                sw.WriteLine("using System.Text;");//开始写入值
                sw.WriteLine("using System.Threading.Tasks;");//开始写入值

                sw.WriteLine("namespace RongKang_IDal");
                sw.WriteLine("{");
                sw.WriteLine(" public interface I" + classname + "Dal<T> : IBaseDal<T>");
                sw.WriteLine("{");
                sw.WriteLine("}");
                sw.WriteLine("}");
                sw.Close();
                fs.Close();
            }
            #endregion



            #region 生成Dal
            Filepath = @"D:\RongKang_Tool\" + "" + classname + "Dal.cs";
            File.Delete(Filepath);
            if (!File.Exists(Filepath))
            {
                FileStream fs1 = new FileStream(Filepath, FileMode.Create, FileAccess.Write);//创建写入文件 
                StreamWriter sw = new StreamWriter(fs1);
                sw.WriteLine("using System;");//开始写入值
                sw.WriteLine("using System.Collections.Generic;");//开始写入值
                sw.WriteLine("using System.Linq;");//开始写入值
                sw.WriteLine("using System.Text;");//开始写入值
                sw.WriteLine("using System.Threading.Tasks;");//开始写入值
                sw.WriteLine("using RongKang_Entity;");
                sw.WriteLine("using RongKang_ViewModel;");
                sw.WriteLine("using RongKang_IDal;");
                sw.WriteLine("using Repository;");
                sw.WriteLine("namespace RongKang_Dal");
                sw.WriteLine("{");
                sw.WriteLine(" public class " + classname + "Dal : BaseDal<" + classname + ">,I" + classname + "Dal<" + classname + ">");
                sw.WriteLine("{");
                sw.WriteLine("}");
                sw.WriteLine("}");
                sw.Close();
                fs1.Close();
            }
            else
            {
                FileStream fs = new FileStream(Filepath, FileMode.Open, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("using System;");//开始写入值
                sw.WriteLine("using System.Collections.Generic;");//开始写入值
                sw.WriteLine("using System.Linq;");//开始写入值
                sw.WriteLine("using System.Text;");//开始写入值
                sw.WriteLine("using System.Threading.Tasks;");//开始写入值
                sw.WriteLine("using RongKang_Entity;");
                sw.WriteLine("using RongKang_IDal;");
                sw.WriteLine("using Repository;");
                sw.WriteLine("namespace RongKang_Dal");
                sw.WriteLine("{");
                sw.WriteLine(" public class " + classname + "Dal : BaseDal<" + classname + ">,I" + classname + "Dal<" + classname + ">");
                sw.WriteLine("{");
                sw.WriteLine("}");
                sw.WriteLine("}");
                sw.Close();
                fs.Close();
            }
            #endregion

            MessageBox.Show("完成，请到目录下查看！");
        }
    }
}

           