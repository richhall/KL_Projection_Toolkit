using UnityEngine;
using UnityEngine.EventSystems;
using PI.ProjectionToolkit.Models;
using TMPro;
using PI.ProjectionToolkit.UI;
using RockVR.Video;

namespace PI.ProjectionToolkit
{
    public class PrjectCameraHolder
    {
        public Models.Camera camera;
        public GameObject cameraContainer;
        public ProjectCameraItem cameraItem;
        public ProjectCameraListItem cameraListItem;
        public ProjectManager projectManager;

        public void Setup()
        {
            cameraListItem.OnRecordClick += CameraListItem_OnRecordClick;
            cameraListItem.OnStopRecordClick += CameraListItem_OnStopRecordClick;
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
    }
    
}