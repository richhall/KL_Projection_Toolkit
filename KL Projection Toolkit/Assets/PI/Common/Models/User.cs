using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PI.Common;

namespace PI.Common.Models
{
    /// <summary>
    /// Basic user interface
    /// </summary>
    public interface IUser
    {
        string name { get; set; }
        string email { get; set; }
        string organisation { get; set; }
    }

    /// <summary>
    /// A basic user
    /// </summary>
    [Serializable]
    public class User : AuditBase
    {
        public string name  = "";
        public string email = "";
        public string organisation = "";

    }
}
