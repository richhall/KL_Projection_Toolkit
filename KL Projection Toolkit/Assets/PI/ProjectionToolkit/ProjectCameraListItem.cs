using UnityEngine;
using UnityEngine.EventSystems;
using PI.ProjectionToolkit.Models;
using TMPro;
using PI.ProjectionToolkit.UI;
using UnityEngine.Video;

namespace PI.ProjectionToolkit
{
    public class ProjectCameraListItem : MonoBehaviour
    {
        private ProjectManager _projectManager;
        public TextMeshProUGUI txtName;
        public TextMeshProUGUI txtType;
        public UnityEngine.UI.Image imgIcon;
        public UnityEngine.UI.Image imgBackground;
        public Sprite imgBackgroundRecording;
        public Sprite imgBackgroundNormal;
        public Sprite imgBackgroundSelected;
        public Sprite imgBackgroundSelectedRecording;
        public Sprite imgProjector;
        public Sprite imgCamera;
        public Sprite imgWalkAround;
        public bool selected;
        private int index = 0;
        private Models.Camera _camera;
        public bool setToRecord = false;
        public GameObject objSetToRecord;
        public GameObject objStopRecord;
        public GameObject objSetToProject;



        public delegate void projectCameraListItemDelegate();
        public event projectCameraListItemDelegate OnRecordClick;
        public event projectCameraListItemDelegate OnStopRecordClick;
        public event projectCameraListItemDelegate OnProjectClick;

        void Start()
        {
            Setup();
        }

        private bool lastSelected = false;
        private bool lastSetToRecord = false;
        private void Update()
        {
           // if(lastSelected != selected || lastSetToRecord != setToRecord)
         //   {
                Setup();
          //  }
        }

        public void Setup()
        {
            //reset active buttons
            //objSetToRecord.SetActive(!setToRecord);
            //objStopRecord.SetActive(setToRecord);
            //set the image
            imgBackground.sprite = setToRecord ? selected ? imgBackgroundSelectedRecording : imgBackgroundRecording : selected ? imgBackgroundSelected : imgBackgroundNormal;
            lastSelected = selected;
            lastSetToRecord = setToRecord;
        }

        //public void SetWalkAbout(int index, ProjectManager projectManager)
        //{
        //    _projectManager = projectManager;
        //    txtName.text = "Character Camera";
        //    txtType.text = "Walk Around Site";
        //    imgIcon.sprite = imgWalkAround;
        //    this.index = index;
        //    imgBackground.sprite = imgBackgroundNormal;
        //}

        public void SetData(Models.Camera camera, int index, ProjectManager projectManager, string type = null)
        {
            _camera = camera;
            _projectManager = projectManager;
            txtName.text = camera.name;
            switch (camera.cameraType)
            {
                case Models.CameraType.Projector:
                    txtType.text = "Physical Projector";
                    imgIcon.sprite = imgProjector;
                    objSetToProject.SetActive(true);
                    break;
                case Models.CameraType.Virtual:
                    txtType.text = "Virtual Camera";
                    imgIcon.sprite = imgCamera;
                    objSetToProject.SetActive(false);
                    break;
                case Models.CameraType.WalkAbout:
                    txtType.text = "Walk About Camera";
                    imgIcon.sprite = imgWalkAround;
                    objSetToProject.SetActive(false);
                    break;
            }
            if (!string.IsNullOrEmpty(type)) txtType.text = type;
            this.index = index;
            imgBackground.sprite = imgBackgroundNormal;
        }



        //public void CameraNormal()
        //{
        //    imgBackground.sprite = setToRecord ? imgBackgroundRecording : imgBackgroundNormal;
        //}

        //public void CameraSelected()
        //{
        //    imgBackground.sprite = selected ? imgBackgroundRecording : imgBackgroundSelected;
        //}

        //public void CameraRecording()
        //{
        //    imgBackground.sprite = imgBackgroundRecording;
        //}

        public void OnButtonClick()
        {
            _projectManager.SetCamera(index);
        }

        public void OnRecordButtonClick()
        {
            if (OnRecordClick != null) OnRecordClick() ;
        }

        public void OnStopRecordButtonClick()
        {
            if (OnStopRecordClick != null) OnStopRecordClick();
        }

        public void OnProjectButtonClick()
        {

                if (OnProjectClick != null) OnProjectClick();

        }
    }
}