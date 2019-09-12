using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PI.Common.Models;

namespace PI.ProjectionToolkit.Models
{

    /// <summary>
    /// Refrerence item to a project used in the projects class
    /// </summary>
    [Serializable]
    public class ProjectReference : AuditBase
    {
        public string id = Guid.NewGuid().ToString();
        public string path = "";
        public string name = "";
        public string image = "";
        public string siteId;
        public string siteVersionId;

        public ProjectReference GetProjectAsProjectReference()
        {
            return new ProjectReference()
            {
                id = this.id,
                name = this.name,
                path = this.path,
                image = this.image,
                siteId = this.siteId,
                siteVersionId = this.siteVersionId,
                created = this.created,
                createdBy = this.createdBy,
                updated = this.updated,
                updatedBy = this.updatedBy
            };
        }
    }
    
}
