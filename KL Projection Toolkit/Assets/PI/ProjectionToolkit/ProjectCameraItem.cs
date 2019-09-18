using UnityEngine;
using UnityEngine.EventSystems;
using PI.ProjectionToolkit.Models;
using TMPro;
using PI.ProjectionToolkit.UI;

namespace PI.ProjectionToolkit
{

    public class ProjectCameraItem : MonoBehaviour
    {
        private UnityStandardAssets.Characters.FirstPerson.FirstPersonController fpsCtrl;
        private ProjectManager _projectManager;
        private int index = 0;
        private Models.Camera _camera;
        private bool selected = false;
        public bool fpsPaused = false;

        private void Start()
        {
            fpsCtrl = this.GetComponentInParent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
        }
                
        public void SetData(Models.Camera camera, int index, ProjectManager projectManager)
        {
            _camera = camera;
            _projectManager = projectManager;
            this.index = index;
        }

        private void Update()
        {
            if (fpsCtrl != null && this.gameObject.activeSelf && Input.GetKeyUp(KeyCode.F2))
            {
                fpsPaused = !fpsPaused;
                SetFpsCameraSelected();
            }
        }

        public void CameraSelected(bool selected)
        {
            this.selected = selected;
            if (selected) fpsPaused = false;
            SetFpsCameraSelected();
        }

        private void SetFpsCameraSelected()
        {
            if (fpsCtrl != null)
            {
                fpsCtrl.enabled = !fpsPaused;
                if (fpsPaused)
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
                else
                {
                    Cursor.visible = false;
                    //Cursor.lockState = CursorLockMode.None;
                }
            }
        }
    }
}