using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI.Common
{

    ///// <summary>
    ///// ITransform interface to implement a serialized transform
    ///// </summary>
    //public interface ITransform
    //{
    //    Vector3 position;
    //    Quaternion rotation;
    //    Vector3 scale;
    //    void SetTransform(UnityEngine.GameObject gameObject);
    //    void SetTransform(UnityEngine.Transform transform);
    //}

    /// <summary>
    /// A serializable Transform, implements the ITransform interface
    /// </summary>
    [Serializable]
    public class Transform
    {
        public Vector3 position = new Vector3(0,0,0);
        public Quaternion rotation = new Quaternion(0, 0, 0, 0);
        public Vector3 scale = new Vector3(0, 0, 0);

        public void SetTransform(UnityEngine.GameObject gameObject)
        {
            SetTransform(gameObject.transform);
        }

        public void SetTransform(UnityEngine.Transform transform)
        {
            transform.localPosition = position.GetVector3();
            transform.localEulerAngles = rotation.GetVector3();
            transform.localScale = scale.GetVector3();
        }
        
    }
}
