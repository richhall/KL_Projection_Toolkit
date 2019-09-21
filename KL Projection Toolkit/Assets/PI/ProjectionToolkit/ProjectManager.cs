using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PI.ProjectionToolkit.Models;
using System.IO;
using TMPro;
using UnityEngine.Networking;
using RockVR.Video;
using SimpleFileBrowser;
using Klak.Spout;
using Michsky.UI.ModernUIPack;

namespace PI.ProjectionToolkit
{
    /// <summary>
    /// Manager to provide all the logic to manage projects
    /// </summary>
    public class ProjectManager : MonoBehaviour
    {

        public GameObject objRightPanel;
        public GameObject objRightPanelHolding;
        public GameObject objBackground;
        public GameObject objCameraList;
        public GameObject prefabCameraListItem;
        public GameObject objCamerasContainer;
        public GameObject prefabCameraItem;
        public GameObject prefabCameraWalkAround;
        public GameObject objModelContainer;
        public GameObject objModelList;
        public GameObject prefabModelListItem;
        public GameObject prefabModelListItemIteractive;

        public GameObject btnRecord;
        public GameObject btnStopRecord;

        public VideoCaptureCtrl videoCaptureCtrl;

        public GameObject objRecordingPanel;
        public TextMeshProUGUI txtRecordingTitle;
        public TextMeshProUGUI txtRecordingTimer;
        private Animator animRecordingPanel;
        private VideoCapture activeVideoCapture;
        public UnityEngine.UI.Button btnSpoutModal;
        public HorizontalSelector spoutSelector;
        public GameObject prefabSpoutReceiver;

        private Project _project = null;
        public Project CurrentProject
        {
            get
            {
                return _project;
            }
        }

        public GameObject mainCamera;
        public GameObject fpsController;

        private List<PrjectCameraHolder> cameras = new List<PrjectCameraHolder>();

        private void Awake()
        {
            Application.runInBackground = true;
        }

        private void Start()
        {
            animRecordingPanel = objRecordingPanel.GetComponent<Animator>();
        }

        private VideoCaptureCtrlBase.StatusType lastRecordingStatus = VideoCaptureCtrlBase.StatusType.NOT_START;
        private void Update()
        {
            if(lastRecordingStatus != VideoCaptureCtrl.instance.status)
            {
                switch (VideoCaptureCtrl.instance.status)
                {
                    case VideoCaptureCtrlBase.StatusType.STARTED:
                        txtRecordingTitle.text = "RECORDING";
                        animRecordingPanel.Play("Recording In");
                        break;
                    case VideoCaptureCtrl.StatusType.STOPPED:
                        txtRecordingTitle.text = "PROCESSING";
                        //processing
                        animRecordingPanel.Play("Recording In");
                        break;
                    case VideoCaptureCtrl.StatusType.PAUSED:
                        txtRecordingTitle.text = "PAUSED";
                        //processing
                        animRecordingPanel.Play("Recording In");
                        break;
                    case VideoCaptureCtrl.StatusType.NOT_START:
                        break;
                    case VideoCaptureCtrl.StatusType.FINISH:
                        txtRecordingTitle.text = "COMPLETE";
                        //processing
                        animRecordingPanel.Play("Recording Out");
                        break;
                }
                lastRecordingStatus = VideoCaptureCtrl.instance.status;
            }
            if(VideoCaptureCtrl.instance.status == VideoCaptureCtrlBase.StatusType.STARTED &&  activeVideoCapture != null)
            {
                txtRecordingTimer.text = "FRAME COUNT: " + activeVideoCapture.getEncodedFrameCount.ToString();
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SetCamera(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SetCamera(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SetCamera(2);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                SetCamera(3);
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                SetCamera(4);
            }
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                SetCamera(5);
            }
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                SetCamera(6);
            }
            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                SetCamera(7);
            }
            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                SetCamera(8);
            }
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                SetCamera(9);
            }
        }

        public Project LoadProject(Project project)
        {
            RockVR.Video.PathConfig.SaveFolder = project.recordFolder + "/";
            btnRecord.SetActive(true);
            _project = project;
            SetProjectHud();
            BuildCameras();
            BuildModels();
            return _project;
        }

        public void SetProjectHud()
        {
            this.gameObject.SetActive(_project != null);
            objRightPanel.SetActive(_project != null);
            objRightPanelHolding.SetActive(_project == null);
            objRightPanel.SetActive(_project != null);
            objBackground.SetActive(_project == null);
        }

        private void BuildCameras()
        {
            //clear the transform
            foreach (Transform child in objCamerasContainer.transform) Destroy(child.gameObject);
            foreach (Transform child in objCameraList.transform) Destroy(child.gameObject);
            cameras = new List<PrjectCameraHolder>();
            //cameras.Add(fpsController);
            ////add list item
            //var listItem = Instantiate(prefabCameraListItem, objCameraList.transform);
            //var ci = listItem.GetComponent<ProjectCameraListItem>();
            //ci.SetWalkAbout(index, this);
            int index = 0;
            int defaultIndex = 0;
            foreach (var projectorStack in _project.projectionSite.projectors)
            {
                foreach (var camera in projectorStack.projectors)
                {
                    if (camera.defaultCamera) defaultIndex = index;
                    cameras.Add(AddCamera(camera, index, projectorStack.name));
                    index += 1;
                }
                //add stack
            }
            foreach (var camera in _project.projectionSite.cameras)
            {
                if (camera.defaultCamera) defaultIndex = index;
                cameras.Add(AddCamera(camera, index, null));
                index += 1;
            }
            SetCamera(defaultIndex);
        }

        private PrjectCameraHolder AddCamera(Models.Camera camera, int index, string type)
        {
            //add list item
            var listItem = Instantiate(prefabCameraListItem, objCameraList.transform);
            var cameraListItem = listItem.GetComponent<ProjectCameraListItem>();
            cameraListItem.SetData(camera, index, this, type);
            //add camera to cameras
            GameObject prefab = prefabCameraItem;
            switch (camera.cameraType)
            {
                case Models.CameraType.WalkAbout:
                    prefab = prefabCameraWalkAround;
                    break;
            }
            var gameObject = Instantiate(prefab, objCamerasContainer.transform);
            gameObject.name = camera.name;
            //gameObject.SetActive(false);
            var cameraItem = gameObject.GetComponent<ProjectCameraItem>();
            cameraItem.SetData(camera, index, this);
            camera.SetTransform(gameObject);
            PrjectCameraHolder holder = new PrjectCameraHolder()
            {
                camera = camera,
                cameraContainer = gameObject,
                cameraItem = cameraItem,
                cameraListItem = cameraListItem,
                projectManager = this
            };
            holder.Setup();
            //do all the config for the camera
            return holder;
        }

        public bool SetRecordController()
        {
            if (VideoCaptureCtrl.instance.status == VideoCaptureCtrlBase.StatusType.NOT_START
                || VideoCaptureCtrl.instance.status == VideoCaptureCtrlBase.StatusType.FINISH)
            {
                activeVideoCapture = null;
                List<VideoCaptureBase> captures = new List<VideoCaptureBase>();
                int targetDisplay = 4;
                foreach (var camera in cameras)
                {
                    camera.cameraListItem.setToRecord = camera.cameraItem.setToRecord;
                    if (camera.cameraItem.setToRecord)
                    {
                        camera.cameraItem.objRecordingCamera.SetActive(true);
                        camera.cameraItem.recordingCamera.targetDisplay = targetDisplay;
                        captures.Add(camera.cameraItem.videoCapture);
                        if (activeVideoCapture == null) activeVideoCapture = camera.cameraItem.videoCapture;
                        if (targetDisplay < 8) targetDisplay += 1;
                    } else
                    {
                        //camera.cameraItem.camera.targetDisplay = 0;
                        camera.cameraItem.objRecordingCamera.SetActive(false);
                    }
                }
                VideoCaptureCtrl.instance.videoCaptures = captures.ToArray();
                return true;
            }
            return false;
        }
        
        public void Record()
        { 
            switch (VideoCaptureCtrl.instance.status)
            {
                case VideoCaptureCtrlBase.StatusType.NOT_START:
                case VideoCaptureCtrlBase.StatusType.FINISH:
                    VideoCaptureCtrl.instance.StartCapture();
                    btnRecord.SetActive(false);
                    btnStopRecord.SetActive(true);
                    break;
                case VideoCaptureCtrlBase.StatusType.STARTED:
                    VideoCaptureCtrl.instance.StopCapture();
                    btnRecord.SetActive(true);
                    btnStopRecord.SetActive(false);
                    break;
            }
        }
        

        public void SetMainCamera()
        {
            for (var x = 0; x < cameras.Count; x++)
            {
                cameras[x].cameraItem.objCamera.SetActive(false);
            }
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            mainCamera.SetActive(true);
        }

        public void SetCamera(int index)
        {
            if(index >= 0 && index < cameras.Count)
            {
                Models.CameraType selectedCameraType = Models.CameraType.Virtual;
                for (var x = 0; x < cameras.Count; x++)
                {
                    cameras[x].cameraItem.CameraSelected(x == index);
                    cameras[x].cameraListItem.selected = x == index;
                    cameras[x].cameraItem.objCamera.SetActive(x == index);
                    if (x == index)
                    {
                        selectedCameraType = cameras[x].camera.cameraType;
                        //cameras[x].cameraListItem.CameraSelected();
                    }
                    else
                    {
                        //cameras[x].cameraListItem.CameraNormal();
                    }
                }
                if (selectedCameraType != Models.CameraType.WalkAbout)
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
                mainCamera.SetActive(false);
            }
        }

        //public void SetCamera_Main()
        //{
        //    SetCamera(0);
        //}

        //public void SetCamera_WalkAround()
        //{
        //    SetCamera(1);
        //}


        private void BuildModels()
        {
            //clear the transform
            foreach (Transform child in objModelContainer.transform) Destroy(child.gameObject);
            foreach (Transform child in objModelList.transform) Destroy(child.gameObject);
            int index = 0;
            int defaultIndex = 0;
            foreach (var model in _project.projectionSite.models)
            {
                //add in the model
                GameObject objModel = Instantiate(Resources.Load<GameObject>(_project.projectionSite.folder + "/" + model.prefabName), objModelContainer.transform);
                model.SetTransform(objModel);

                var prefab = model.projectionSurface ? prefabModelListItemIteractive : prefabModelListItem;
                //add list item
                var listItem = Instantiate(prefab, objModelList.transform);
                var modelListItem = listItem.GetComponent<ModelListItem>();
                modelListItem.SetData(model.name, model.name, objModel, model.targetMaterialProperty, prefabSpoutReceiver);
                modelListItem.OnSpoutClick += ModelListItem_OnSpoutClick;
                modelListItem.OnMaterialClick += ModelListItem_OnMaterialClick;
                modelListItem.OnVideoClick += ModelListItem_OnVideoClick;
            }
            SetCamera(defaultIndex);
        }

        private void ModelListItem_OnVideoClick(ModelListItem listItem)
        {
            if (listItem.modelItem != null)
            {
                FileBrowser.SetFilters(true, new FileBrowser.Filter("Videos", ".mp4", ".mov"));
                FileBrowser.SetDefaultFilter(".mp4");
                StartCoroutine(ShowVideoPathDialogCoroutine(listItem));
            }
        }

        IEnumerator ShowVideoPathDialogCoroutine(ModelListItem listItem)
        {
            yield return FileBrowser.WaitForLoadDialog(false, CurrentProject.path, "Select Video");

            if (FileBrowser.Success)
            {
                listItem.modelItem.SetVideo(FileBrowser.Result);
            }
        }

        private void ModelListItem_OnMaterialClick(ModelListItem listItem)
        {
            var modelItem = listItem.objModel.GetComponent<ModelItem>();
            if (modelItem != null)
            {
                modelItem.SetMaterial();
            }
        }

        private ModelListItem tmpModelListItem;
        private void ModelListItem_OnSpoutClick(ModelListItem listItem)
        {
            var modelItem = listItem.objModel.GetComponent<ModelItem>();
            if (modelItem != null)
            {
                tmpModelListItem = listItem;
                spoutSelector.SetElements(GetSpoutList());
                btnSpoutModal.onClick.Invoke();
            }
        }

        public void SpoutSelected()
        {
            tmpModelListItem.modelItem.SetSpout(spoutSelector.selectedElement);
        }

        private List<string> GetSpoutList()
        {
            var count = PluginEntry.CountSharedObjects();
            var names = new List<string>();
            for (var i = 0; i < count; i++)
                names.Add(PluginEntry.GetSharedObjectNameString(i));
            return names;
        }
    }

}
