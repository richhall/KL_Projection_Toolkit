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
    /// Projection site is a unique site for a real world projection
    /// Contains all the details to build a virtual site within the tool
    /// </summary>
    [Serializable]
    public class ProjectionSite : AuditBase
    {
        public string id = Guid.NewGuid().ToString();
        public string versionId = Guid.NewGuid().ToString();
        public string name = "Site";
        public int majorVersion = 0;
        public int minorVersion = 0;
        public string version { get { return majorVersion.ToString() + "." + minorVersion.ToString(); } }
        public string notes = "";
        public Location location = new Location();
        public string assetBundleName = "Site";
        public List<ProjectorStack> projectors = new List<ProjectorStack>();
        public List<Camera> cameras = new List<Camera>();
    }
}
