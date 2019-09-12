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
    public class Projects
    {
        public List<ProjectReference> projects = new List<ProjectReference>();

        public void AddProject(Project project)
        {
            //look for a project first
            var p = projects.FirstOrDefault(f => f.path.ToLower() == project.path.ToLower());
            if(p == null)
            {
                projects.Add(project.GetProjectAsProjectReference());
            }
        }
    }
}
