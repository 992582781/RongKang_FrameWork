using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Common
{
  public  class Number_Conversion
    {
        static public int ObjToInt(object Value, int Default)
        {
            int reVal = Default;
            if (Value != null)
            {
                if (isInt(Value.ToString()))
                {
                    reVal = int.Parse(Value.ToString());
                }
            }
            return reVal;
        }
        static public double StrToDouble(string Value, double Default)
        {
            double reVal = Default;
            Value = Value.Trim();
            if (isDouble(Value))
            {
                reVal = double.Parse(Value);
            }
            return reVal;
        }
        static public double ObjToDouble(object Value, double Default)
        {
            double reVal = Default;
            if (Value != null)
            {
                string Val = (string)Value;
                Val = Val.Trim();
                if (isDouble(Val))
                {
                    reVal = double.Parse(Val);
                }
            }
            return reVal;
        }

        static public decimal ObjToDecimal(object Value)
        {
            decimal res = 0m;
            string Val = (string)Value;
            bool convertible = decimal.TryParse(Val, out res);
            return res;
        }

        static public string ObjToStr(object Value)
        {
            string reVal = ObjToStr(Value, "");
            return reVal;
        }
        static public string ObjToStr(object Value, string Default)
        {
            string reVal = Default;
            if (!Value.ToString().Equals(""))
            {
                reVal = (string)Value;
            }
            return reVal;
        }
        public static bool isDouble(String Value)
        {
            bool reVal = true;
            int intDot = 0;
            int intSign = 0;
            if (!(Value == null || Value.Equals("")))
            {
                for (int i = 0; i < Value.Length; i++)
                {
                    if ((Value[i] >= '0' && Value[i] <= '9') || Value[i] == '.' || Value[i] == '-' || Value[i] == '+' || Value[i] == ',')
                    {
                        if (Value[i] == '.')
                        {
                            intDot++;
                            if (intDot > 1)
                            {
                                reVal = false;
                                break;
                            }
                        }
                        if (Value[i] == '-' || Value[i] == '+')
                        {
                            intSign++;
                            if (intSign > 1)
                            {
                                reVal = false;
                                break;
                            }
                        }
                        if (Value[i] == ',')
                        {
                            if (intDot != 0)
                            {
                                reVal = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        reVal = false;
                        break;
                    }
                }
            }
            else
            {
                reVal = false;
            }
            return reVal;
        }
        public static bool isInt(String Value)
        {
            bool reVal = true;
            int intDot = 0;
            int intSign = 0;
            if (!(Value == null || Value.Equals("")))
            {
                for (int i = 0; i < Value.Length; i++)
                {
                    if ((Value[i] >= '0' && Value[i] <= '9') || Value[i] == '.' || Value[i] == '-' || Value[i] == '+' || Value[i] == ',')
                    {
                        if (Value[i] == '.')
                        {
                            intDot++;
                            if (intDot > 1)
                            {
                                reVal = false;
                                break;
                            }
                        }
                        if (Value[i] == '-' || Value[i] == '+')
                        {
                            intSign++;
                            if (intSign > 1)
                            {
                                reVal = false;
                                break;
                            }
                        }
                        if (Value[i] == ',')
                        {
                            if (intDot != 0)
                            {
                                reVal = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        reVal = false;
                        break;
                    }
                }
            }
            else
            {
                reVal = false;
            }
            return reVal;
        }
 

    }
}
