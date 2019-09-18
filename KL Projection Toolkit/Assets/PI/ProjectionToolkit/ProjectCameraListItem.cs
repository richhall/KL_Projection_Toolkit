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

        void Start()
        {
        }

        public void SetWalkAbout(int index, ProjectManager projectManager)
        {
            _projectManager = projectManager;
            txtName.text = "Character Camera";
            txtType.text = "Walk Around Site";
            imgIcon.sprite = imgWalkAround;
            this.index = index;
            imgBackground.sprite = imgBackgroundNormal;
        }

        public void SetData(Models.Camera camera, int index, ProjectManager projectManager)
        {
            _camera = camera;
            _projectManager = projectManager;
            txtName.text = camera.name;
            txtType.text = camera.physical ? "Physical Projector" : "Virtual Camera";
            imgIcon.sprite = camera.physical ? imgProjector : imgCamera;
            this.index = index;
            imgBackground.sprite = imgBackgroundNormal;
        }

        public void OnButtonClick()
        {
            _projectManager.SetCamera(index);
        }

    }
}