using UnityEngine;
using UnityEngine.EventSystems;
using PI.ProjectionToolkit.Models;
using TMPro;
using PI.ProjectionToolkit.UI;
using RockVR.Video;

namespace PI.ProjectionToolkit
{
    public class ProjectCameraHolder
    {
        public Models.Camera camera;
        public GameObject cameraContainer;
        public ProjectCameraItem cameraItem;
        public ProjectCameraListItem cameraListItem;
        public ProjectManager projectManager;
        public UnityEngine.Camera displayCamera;
        public bool AttachedToDisplay2 = false;
        public bool AttachedToDisplay3 = false;
        public bool AttachedToDisplay4 = false;

        public void Setup()
        {
            cameraListItem.OnRecordClick += CameraListItem_OnRecordClick;
            cameraListItem.OnStopRecordClick += CameraListItem_OnStopRecordClick;
            cameraListItem.OnProjectClick += CameraListItem_OnProjectClick;
            camera.SetCamera(cameraItem.camera);
        }

        private void CameraListItem_OnStopRecordClick()
        {
            cameraItem.setToRecord = false;
            cameraItem.setToRecord = !projectManager.SetRecordController();
            //if (!cameraItem.setToRecord) cameraListItem.CameraNormal();
        }

        private void CameraListItem_OnRecordClick()
        {
            cameraItem.setToRecord = true;
            cameraItem.setToRecord = projectManager.SetRecordController();
            //if(cameraItem.setToRecord) cameraListItem.CameraRecording();
        }

        private void CameraListItem_OnProjectClick()
        {
            projectManager.ProjectCameraListItem_OnVideoClick(this);
        }

        public void SetDisplayCamera()
        {
            if(displayCamera != null) SetCamera(displayCamera);
        }

        public void SetCamera(UnityEngine.Camera camera)
        {
            //set the transform position to match
            camera.transform.localPosition = this.camera.position.GetVector3();
            camera.transform.localEulerAngles = this.camera.rotation.GetVector3();
            camera.transform.localScale = this.camera.scale.GetVector3();
            //set camera values
            this.camera.SetCamera(camera);
        }


        public void UpdateCamera(Models.Camera camera, bool changeFieldOfViewOverFocalLength, bool updateDisplay2, bool updateDisplay3, bool updateDisplay4)
        {
            if (!cameraItem.setToRecord)
            {
                this.camera = camera;
                this.camera.SetCamera(cameraItem.camera, changeFieldOfViewOverFocalLength);
                camera.fieldOfView = cameraItem.camera.fieldOfView;
                camera.focalLength = cameraItem.camera.focalLength;
                this.camera.SetTransform(this.cameraContainer);
            }
        }
    }
    
}