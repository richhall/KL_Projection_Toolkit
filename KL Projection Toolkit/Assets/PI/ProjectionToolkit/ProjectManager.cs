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
using Klak.Syphon;
using Michsky.UI.ModernUIPack;
using PI.ProjectionToolkit.UI;
using System.Runtime.InteropServices;

namespace PI.ProjectionToolkit
{
    /// <summary>
    /// Manager to provide all the logic to manage projects
    /// </summary>
    public class ProjectManager : MonoBehaviour
    {
        public ApplicationManager applicationManager;
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

        public GameObject btnSave;
        public GameObject btnRefresh;
        public GameObject btnRecord;
        public GameObject btnStopRecord;

        public VideoCaptureCtrl videoCaptureCtrl;

        public GameObject objRecordingPanel;
        public TextMeshProUGUI txtRecordingTitle;
        public TextMeshProUGUI txtRecordingTimer;
        private Animator animRecordingPanel;
        private VideoCapture activeVideoCapture;
        public UnityEngine.UI.Button btnSyphonModal;
        public HorizontalSelector syphonSelector;
        public GameObject prefabSyphonReceiver;

        public ProjectCameraHolder currentCameraHolder;
        public CameraEditor cameraEditor;

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

        public List<ProjectCameraHolder> cameras = new List<ProjectCameraHolder>();

        private void Awake()
        {
            Application.runInBackground = true;
        }

        private void Start()
        {
            animRecordingPanel = objRecordingPanel.GetComponent<Animator>();
            cameraEditor.OnCameraChanged += CameraEditor_OnCameraChanged;
        }

        private void CameraEditor_OnCameraChanged(Models.Camera camera, bool changeFieldOfViewOverFocalLength, bool updateDisplay2, bool updateDisplay3, bool updateDisplay4)
        {
            currentCameraHolder.UpdateCamera(camera, changeFieldOfViewOverFocalLength, updateDisplay2, updateDisplay3, updateDisplay4);
            cameraEditor.UpdateFovAndFocalLength(camera);
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
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                if (Input.GetKeyUp(KeyCode.Alpha1))
                {
                    SetCamera(0);
                }
                if (Input.GetKeyUp(KeyCode.Alpha2))
                {
                    SetCamera(1);
                }
                if (Input.GetKeyUp(KeyCode.Alpha3))
                {
                    SetCamera(2);
                }
                if (Input.GetKeyUp(KeyCode.Alpha4))
                {
                    SetCamera(3);
                }
                if (Input.GetKeyUp(KeyCode.Alpha5))
                {
                    SetCamera(4);
                }
                if (Input.GetKeyUp(KeyCode.Alpha6))
                {
                    SetCamera(5);
                }
                if (Input.GetKeyUp(KeyCode.Alpha7))
                {
                    SetCamera(6);
                }
                if (Input.GetKeyUp(KeyCode.Alpha8))
                {
                    SetCamera(7);
                }
                if (Input.GetKeyUp(KeyCode.Alpha9))
                {
                    SetCamera(8);
                }
                if (Input.GetKeyUp(KeyCode.Alpha0))
                {
                    SetCamera(9);
                }
            }
        }


        public Project LoadProject(Project project)
        {
            RockVR.Video.PathConfig.SaveFolder = project.recordFolder + "/";
            btnRecord.SetActive(true);
            btnSave.SetActive(true);
            btnRefresh.SetActive(true);
            if (_project != null && _project.id == project.id)
            {
                SetCamera(0);
            }
            else
            {
                _project = project;
                BuildCameras();
                BuildModels();
            }
            SetProjectHud();
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
            cameras = new List<ProjectCameraHolder>();
            int index = 0;
            int defaultIndex = 0;
            foreach (var projectorStack in _project.projectionSite.projectors)
            {
                foreach (var camera in projectorStack.projectors)
                {
                    //add stack transform details
                    camera.position.x += projectorStack.position.x;
                    camera.position.y += projectorStack.position.y;
                    camera.position.z += projectorStack.position.z;
                    camera.rotation.x += projectorStack.rotation.x;
                    camera.rotation.y += projectorStack.rotation.y;
                    camera.rotation.z += projectorStack.rotation.z;
                    camera.rotation.w += projectorStack.rotation.w;
                    if (camera.defaultCamera) defaultIndex = index;
                    cameras.Add(AddCamera(camera, index, projectorStack.name));
                    index += 1;
                }
            }
            foreach (var camera in _project.projectionSite.cameras)
            {
                if (camera.defaultCamera) defaultIndex = index;
                cameras.Add(AddCamera(camera, index, null));
                index += 1;
            }
            SetCamera(defaultIndex);
        }

        private ProjectCameraHolder AddCamera(Models.Camera camera, int index, string type)
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
            ProjectCameraHolder holder = new ProjectCameraHolder()
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
                    if (VideoCaptureCtrl.instance.videoCaptures.Length > 0)
                    {
                        VideoCaptureCtrl.instance.StartCapture();
                        btnRecord.SetActive(false);
                        btnStopRecord.SetActive(true);
                    }
                    else
                    {
                        //show error
                        applicationManager.ShowErrorMessage("To start recording please select a camera to record.");
                        btnRecord.SetActive(true);
                        btnStopRecord.SetActive(false);
                    }
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
            currentCameraHolder = null;
            if (index >= 0 && index < cameras.Count)
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
                        currentCameraHolder = cameras[x];
                        cameraEditor.SetData(cameras[x].camera, cameras[x].AttachedToDisplay2, cameras[x].AttachedToDisplay3, cameras[x].AttachedToDisplay4);
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
                modelListItem.SetData(model.name, model.name, objModel, model.targetMaterialProperty, prefabSyphonReceiver);
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
                Tuple<string, string>[] t = GetSyphonList();

                var names = new List<string>();
                for (var i = 0; i < t.Count(); i++)
                {
                    names[i] = String.Format("{0}/{1}", t[i].Item1, t[i].Item2);
                }

                    syphonSelector.SetElements(names);
                btnSyphonModal.onClick.Invoke();
            }
        }

        public void SyphonSelected()
        {
            var split = syphonSelector.selectedElement.Split('/');
            if (split.Count() != 2)
            {
                return;
            }
            else {
                tmpModelListItem.modelItem.SetSyphon(split[0], split[1]);
            }
        }

        private Tuple<string, string>[] GetSyphonList()
        {


            var list = Plugin_CreateServerList();
            var count = Plugin_GetServerListCount(list);
            var names = new Tuple<string, string>[count];
            for (var i = 0; i < count; i++)
            {

                var pName = Plugin_GetNameFromServerList(list, i);
                var pAppName = Plugin_GetAppNameFromServerList(list, i);

                var name = (pName != IntPtr.Zero) ? Marshal.PtrToStringAnsi(pName) : "(no name)";
                var appName = (pAppName != IntPtr.Zero) ? Marshal.PtrToStringAnsi(pAppName) : "(no app name)";
                names[i] = Tuple.Create(appName, name);
            }


            return names;
        }

        public void SaveProject()
        {
            this.applicationManager.SaveProject(this.CurrentProject);
        }

        public void RefreshProjectFromSite()
        {
            this.applicationManager.LoadProject(this.CurrentProject.GetProjectAsProjectReference(), true);
            this.applicationManager.ShowErrorMessage("Your site details, such as cameras and models, have been reloaded", null);
        }

        #region Native plugin entry points

        [DllImport("KlakSyphon")]
        static extern IntPtr Plugin_CreateServerList();

        [DllImport("KlakSyphon")]
        static extern void Plugin_DestroyServerList(IntPtr list);

        [DllImport("KlakSyphon")]
        static extern int Plugin_GetServerListCount(IntPtr list);

        [DllImport("KlakSyphon")]
        static extern IntPtr Plugin_GetNameFromServerList(IntPtr list, int index);

        [DllImport("KlakSyphon")]
        static extern IntPtr Plugin_GetAppNameFromServerList(IntPtr list, int index);

        #endregion
    }

}
