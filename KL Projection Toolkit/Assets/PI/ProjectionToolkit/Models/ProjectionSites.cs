using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PI.Common.Models;

namespace PI.ProjectionToolkit.Models
{
    /// <summary>
    /// A list of all the local projects on the machine
    /// </summary>
    [Serializable]
    public class ProjectionSites
    {
        public List<ProjectionSite> sites = new List<ProjectionSite>();

        public void AddSite(ProjectionSite site)
        {
            //look for a site first
            var s = sites.FirstOrDefault(f => f.versionId == site.versionId);
            if(s == null)
            {
                sites.Add(site);
            }
        }

        public void MinorVersionUpdate(ProjectionSite site)
        {
            //look for a site first
            var s = sites.FirstOrDefault(f => f.id == site.id);
            if (s != null)
            {
                sites.Remove(s);
                sites.Add(site);
            }
        }
    }
}
