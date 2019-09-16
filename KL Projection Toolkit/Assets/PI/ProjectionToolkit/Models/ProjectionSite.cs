using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PI.Common;
using PI.Common.Models;

namespace PI.ProjectionToolkit.Models
{
    public enum ProjectionSiteStatus
    {
        Unknown,
        UpToDate,
        OutOfDate,
        NotOnServer,
        NewOnServer
    }

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
        public string folder = "Site";
        public int majorVersion = 0;
        public int minorVersion = 0;
        public string version { get { return majorVersion.ToString() + "." + minorVersion.ToString(); } }
        public string notes = "";
        public Location location = new Location();
        public string assetBundleName = "Site";
        public List<ProjectorStack> projectors = new List<ProjectorStack>();
        public List<Camera> cameras = new List<Camera>();
        public List<string> siteResources = new List<string>(); //stay in the site
        public List<string> projectResources = new List<string>(); //get copied over to the project on create
        public List<string> resources
        {
            get
            {
                List<string> allResources = new List<string>();
                allResources.AddRange(siteResources);
                allResources.AddRange(projectResources);
                return allResources;
            }
        }

        public ProjectionSiteStatus status { get; set; } = ProjectionSiteStatus.Unknown;

    }
}
