using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI.Common
{
    /// <summary>
    /// A serializable Quarternion
    /// </summary>
    [Serializable]
    public class Quaternion : Vector3
    {
        public float w = 0f;

        public Quaternion(float x = 0f, float y = 0f, float z = 0f, float w = 0f)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public UnityEngine.Quaternion GetQuaternion()
        {
            return new UnityEngine.Quaternion(x, y, z, w);
        }
    }

}
