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
    /// Serializable SiteModel model that inherits a transform
    /// Contains a reference to a 3D model in the site
    /// </summary>
    [Serializable]
    public class SiteModel : Transform
    {
        public string name = "Site Model";
        public string prefabName = "";
        public bool projectionSurface = true;
        public string targetMaterialProperty = "_MainText";
    }
}
