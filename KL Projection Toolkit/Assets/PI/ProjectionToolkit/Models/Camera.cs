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
    /// Serializable Camera model that inherits a transform
    /// Used to set the position and physical properties of a camera in the scene, this should match a real world projector
    /// </summary>
    [Serializable]
    public class Camera : Transform
    {
        public string name { get; set; } = "Main Projector";
        public bool physical { get; set; } = true;
        public float fieldOfView { get; set; } = 0f;
        public float focalLength { get; set; } = 0f;
        public Vector2 sensorSize { get; set; } = new Vector2();
        public Vector2 lensShift { get; set; } = new Vector2();
        public UnityEngine.Camera.GateFitMode gateFit { get; set; } = UnityEngine.Camera.GateFitMode.Horizontal;
        public float nearClipPlane { get; set; } = 0.3f;
        public float farClipPlane { get; set; } = 1000f;

        public void SetCamera(UnityEngine.Camera camera)
        {
            if (camera.orthographic) throw new Exception("Unable to set an Orthographic camera");
            if (!camera.usePhysicalProperties) throw new Exception("Unable to set an Orthographic camera");
            camera.fieldOfView = fieldOfView;
            camera.focalLength = focalLength;
            camera.sensorSize = sensorSize.GetVector2();
            camera.lensShift = lensShift.GetVector2();
            camera.gateFit = gateFit;
            camera.nearClipPlane = nearClipPlane;
            camera.farClipPlane = farClipPlane;
        }
    }
}
