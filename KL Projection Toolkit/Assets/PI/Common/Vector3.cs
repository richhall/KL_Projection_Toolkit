using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI.Common
{
    /// <summary>
    /// A serializable Vector3
    /// </summary>
    [Serializable]
    public class Vector3 : Vector2
    {
        public float z = 0f;

        public Vector3(float x = 0f, float y = 0f, float z = 0f)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public UnityEngine.Vector3 GetVector3()
        {
            return new UnityEngine.Vector3(x, y, z);
        }
    }
}
