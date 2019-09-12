﻿using System;
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
        public string name = "Main Projector";
        public bool physical  = true;
        public float fieldOfView = 0f;
        public float focalLength = 0f;
        public Vector2 sensorSize = new Vector2();
        public Vector2 lensShift = new Vector2();
        public UnityEngine.Camera.GateFitMode gateFit  = UnityEngine.Camera.GateFitMode.Horizontal;
        public float nearClipPlane = 0.3f;
        public float farClipPlane = 1000f;

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
