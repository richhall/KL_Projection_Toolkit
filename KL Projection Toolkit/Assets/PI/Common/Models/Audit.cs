using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PI.Common;

namespace PI.Common.Models
{
    ///// <summary>
    ///// IAudit interface to store the created and updated details of an object
    ///// </summary>
    //public interface IAudit
    //{
    //    DateTime created
    //    string createdBy
    //    DateTime updated
    //    string updatedBy
    //}

    /// <summary>
    /// Base audit class that implements the IAudit interface to store the created and updated details of an object
    /// </summary>
    [Serializable]
    public class AuditBase
    {
        public DateTime created = DateTime.Now;
        public string createdBy = "";
        public DateTime updated = DateTime.Now;
        public string updatedBy = "";
    }
}
