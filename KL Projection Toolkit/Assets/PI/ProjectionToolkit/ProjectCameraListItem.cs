using UnityEngine;
using UnityEngine.EventSystems;
using PI.ProjectionToolkit.Models;
using TMPro;
using PI.ProjectionToolkit.UI;

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
        public Sprite imgProjector;
        public Sprite imgCamera;
        public Sprite imgWalkAround;
        public bool selected;
        private int index = 0;
        private Models.Camera _camera;
        public bool setToRecord = false;

        void Start()
        {
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
                    break;
                case Models.CameraType.Virtual:
                    txtType.text = "Virtual Camera";
                    imgIcon.sprite = imgCamera;
                    break;
                case Models.CameraType.WalkAbout:
                    txtType.text = "Walk About Camera";
                    imgIcon.sprite = imgWalkAround;
                    break;
            }
            if (!string.IsNullOrEmpty(type)) txtType.text = type;
            this.index = index;
            imgBackground.sprite = imgBackgroundNormal;
        }

        public delegate void projectCameraListItemDelegate();
        public event projectCameraListItemDelegate OnRecordClick;
        public event projectCameraListItemDelegate OnStopRecordClick;

        public void CameraNormal(bool setToRecord)
        {
            imgBackground.sprite = setToRecord ? imgBackgroundRecording : imgBackgroundNormal;
        }

        public void CameraSelected(bool setToRecord)
        {
            imgBackground.sprite = setToRecord ? imgBackgroundRecording : imgBackgroundSelected;
        }

        public void CameraRecording()
        {
            imgBackground.sprite = imgBackgroundRecording;
        }

        public void OnButtonClick()
        {
            _projectManager.SetCamera(index);
        }

        public void OnRecordButtonClick()
        {
            if (OnRecordClick != null) OnRecordClick();
        }

        public void OnStopRecordButtonClick()
        {
            if (OnStopRecordClick != null) OnStopRecordClick();
        }
    }
}