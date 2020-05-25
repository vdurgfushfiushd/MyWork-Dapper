using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 用户模型关联类
    /// </summary>
    public class UserModule
    {
        /// <summary>
        /// 用户主键
        /// </summary>
        public string User_Id { get; set; }
        /// <summary>
        /// 模型主键
        /// </summary>
        public string Module_Id { get; set; }
    }
}
