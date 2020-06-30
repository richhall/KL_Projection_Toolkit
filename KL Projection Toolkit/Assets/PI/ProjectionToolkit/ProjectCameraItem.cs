using UnityEngine;
using UnityEngine.EventSystems;
using PI.ProjectionToolkit.Models;
using TMPro;
using PI.ProjectionToolkit.UI;
using RockVR.Video;

namespace PI.ProjectionToolkit
{

    public class ProjectCameraItem : MonoBehaviour
    {
        private UnityStandardAssets.Characters.FirstPerson.FirstPersonController fpsCtrl;
        private ProjectManager _projectManager;
        private int index = 0;
        private int recordIndex = -1;
        private Models.Camera _camera;
        public bool selected = false;
        public bool fps = false;
        public bool fpsPaused = false;
        public bool setToRecord = false;
        public GameObject objCamera;
        public GameObject objRecordingCamera;
        public UnityEngine.Camera camera;
        public UnityEngine.Camera recordingCamera;
        public VideoCapture videoCapture;
        public GameObject objVideoProjector;
        private UnityEngine.Video.VideoPlayer _videoPlayer;
        public bool VideoSet { get; protected set; }

        private void Start()
        {
        }

        private void Active()
        {
            if(fps) fpsCtrl = this.GetComponentInParent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
        }
                
        public void SetData(Models.Camera camera, int index, ProjectManager projectManager)
        {
            _camera = camera;
            _projectManager = projectManager;
            this.index = index;
            //set up the video capture
            SetupVideoCapture();
        }



        public void SetVideo(string url)
        {

            _videoPlayer = objVideoProjector.GetComponent<UnityEngine.Video.VideoPlayer>();       
            _videoPlayer.enabled = true;
            _videoPlayer.url = url;
            VideoSet = true;
            //objVideoProjector.GetComponent<ProjectVideo>().PlayVideoProjector();
            //_videoPlayer.Play();
        }

        public void PlayVideo()
        {
            objVideoProjector.GetComponent<ProjectVideo>().PlayVideoProjector();
            _videoPlayer.Play();
            _projectManager.projectionStatus = ProjectManager.ProjectionStatusType.STARTED;
        }

        public void StopVideo()
        {
            _videoPlayer.Stop();
            _videoPlayer.targetTexture.Release();
            _projectManager.projectionStatus = ProjectManager.ProjectionStatusType.STOPPED;
        }

        private void SetupVideoCapture()
        {            
            if (!System.IO.Directory.Exists(_projectManager.CurrentProject.recordFolder)) System.IO.Directory.CreateDirectory(_projectManager.CurrentProject.recordFolder);
            //videoCapture.customPath = true;
            //videoCapture.customPathFolder = _projectManager.CurrentProject.recordFolder;
        }

        //public void StartVideoCapture()
        //{
        //    if (setToRecord)
        //    {
        //        //camera.targetDisplay = 5;
        //        videoCapture.StartCapture();
        //    }
        //}

        //public void StopVideoCapture()
        //{
        //    //camera.targetDisplay = 1;
        //    if (videoCapture.status == VideoCaptureCtrlBase.StatusType.STARTED) videoCapture.StopCapture();
        //}

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
            objCamera.SetActive(selected);
            //this.camera.targetDisplay = 0;
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