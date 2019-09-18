using UnityEngine;
using UnityEngine.EventSystems;
using PI.ProjectionToolkit.Models;
using TMPro;
using PI.ProjectionToolkit.UI;

namespace PI.ProjectionToolkit
{

    public class ProjectCameraItem : MonoBehaviour
    {
        private ProjectManager _projectManager;
        private TextMeshProUGUI txtName;
        private TextMeshProUGUI txtType;
        public Sprite imgIcon;
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
        
        public void SetData(Models.Camera camera, int index, ProjectManager projectManager)
        {
            _camera = camera;
            _projectManager = projectManager;
            txtName.text = camera.name;
            this.index = index;
            imgBackground.sprite = imgBackgroundNormal;
        }

        public void OnButtonClick()
        {
            _projectManager.SetCamera(index);
        }

    }
}