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
        public long created = DateTime.Now.ToFileTimeUtc();
        public DateTime createdAsDate
        {
            get { return DateTime.FromFileTimeUtc(created); }
            set { created = value.ToFileTimeUtc(); }
        }
        public string createdAsString
        {
            get { return createdAsDate.ToString("yyyy-MM-dd HH:mm"); }
        }

        public string createdBy = "";
        public string updatedBy = "";
        public long updated = DateTime.Now.ToFileTimeUtc();

        public DateTime updatedAsDate
        {
            get { return DateTime.FromFileTimeUtc(updated); }
            set { updated = value.ToFileTimeUtc(); }
        }
        public string updatedAsString
        {
            get { return updatedAsDate.ToString("yyyy-MM-dd HH:mm"); }
        }
    }
}
