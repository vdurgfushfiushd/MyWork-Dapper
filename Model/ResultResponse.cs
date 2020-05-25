using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ResultResponse
    {
        /// <summary>
        /// 返回的状态
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// 返回的信息
        /// </summary>
        public string Message { get; set; }
    }
}
