using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RongKang_Entity
{
    public class Message
    {
        public Message() { }
        #region Message
        public string Msg { get; set; }
        public bool Status { get; set; }
        public int Code { get; set; }
        public string Location_Url { get; set; }
        #endregion Message
    }
}
