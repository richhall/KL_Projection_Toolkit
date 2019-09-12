using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI.Common
{
    /// <summary>
    /// A serializable Vector2
    /// </summary>
    [Serializable]
    public class Vector2
    {
        public float x = 0f;
        public float y = 0f;

        public Vector2(float x = 0f, float y = 0f)
        {
            this.x = x;
            this.y = y;
        }

        public UnityEngine.Vector2 GetVector2()
        {
            return new UnityEngine.Vector2(x, y);
        }
    }
}
