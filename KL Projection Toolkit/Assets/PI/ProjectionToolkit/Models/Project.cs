using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PI.Common.Models;

namespace PI.ProjectionToolkit.Models
{

    /// <summary>
    /// A users full project with all the settings, inherits from ProjectReference class
    /// </summary>
    [Serializable]
    public class Project : ProjectReference
    {
        public string notes = "";
        public ProjectionSite projectionSite;
        public List<Camera> projectCameras = new List<Camera>();
    }
}
