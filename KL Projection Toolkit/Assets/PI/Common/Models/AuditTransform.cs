using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PI.Common;

namespace PI.Common.Models
{
    /// <summary>
    /// Audit transform inherits the standard transform for serialization and also contains the IAudit fields
    /// </summary>
    [Serializable]
    public class AuditTransform : Transform
    {
        #region Audit

        public DateTime created = DateTime.Now;
        public string createdBy = "";
        public DateTime updated = DateTime.Now;
        public string updatedBy = "";

        #endregion
    }
}
