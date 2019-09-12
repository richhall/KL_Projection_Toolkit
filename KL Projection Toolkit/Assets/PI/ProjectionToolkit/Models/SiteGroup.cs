using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PI.Common;
using PI.Common.Models;

namespace PI.ProjectionToolkit.Models
{
    /// <summary>
    /// Site Group is a group of projection sites
    /// </summary>
    [Serializable]
    public class SiteGroup : AuditBase
    {
        public string name { get; set; } = "Site Group";
        public int majorVersion { get; set; } = 0;
        public int minorVersion { get; set; } = 0;
        public string version { get { return majorVersion.ToString() + "." + minorVersion.ToString(); } }
        public string notes { get; set; } = "";
        public List<ProjectionSite> sites { get; set; } = new List<ProjectionSite>();
    }
}
