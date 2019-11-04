using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI.Common
{
    /// <summary>
    /// A serializable Rect
    /// </summary>
    [Serializable]
    public class Rect : Vector2
    {
        public float w = 0f;
        public float h = 0f;

        public Rect(float x = 0f, float y = 0f, float w = 0f, float h = 0f)
        {
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
        }

        public UnityEngine.Rect GetRect()
        {
            return new UnityEngine.Rect(x, y, w, h);
        }
    }
}
