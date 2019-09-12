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
    /// Projector stack contains any number of cameras in the projector listing that match real world projector positions
    /// Inherits the transform as the whole stack can be moved within the projeciton site
    /// </summary>
    [Serializable]
    public class ProjectorStack : Transform
    {
        public string siteId;
        public string name  = "Main Stack";
        public int majorVersion = 0;
        public int minorVersion = 0;
        public string version { get { return majorVersion.ToString() + "." + minorVersion.ToString(); } }
        public List<Camera> projectors = new List<Camera>();
        public string notes = "";
    }
}
