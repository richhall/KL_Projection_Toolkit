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
        private Project _project = null;

        public GameObject mainCamera;
        public GameObject fpsController;

        private List<PrjectCameraHolder> cameras = new List<PrjectCameraHolder>();

        private void Start()
        {
        }

        private void Update()
        {
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

        public void LoadProject(Project project)
        {
            _project = project;
            SetProjectHud();
            BuildCameras();
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
            var ci = listItem.GetComponent<ProjectCameraListItem>();
            ci.SetData(camera, index, this, type);
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
            gameObject.SetActive(false);
            PrjectCameraHolder holder = new PrjectCameraHolder()
            {
                cameraContainer = gameObject,
                cameraItem = ci
            };
            //do all the config for the camera
            return holder;
        }

        public void SetMainCamera()
        {
            for (var x = 0; x < cameras.Count; x++)
            {
                cameras[x].cameraContainer.SetActive(false);
            }
            mainCamera.SetActive(true);
        }

        public void SetCamera(int index)
        {
            if(index >= 0 && index < cameras.Count)
            {
                for (var x = 0; x < cameras.Count; x++)
                {
                    cameras[x].cameraContainer.SetActive(x == index);
                }
                mainCamera.SetActive(false);
            }
        }

        public void SetCamera_Main()
        {
            SetCamera(0);
        }

        public void SetCamera_WalkAround()
        {
            SetCamera(1);
        }
    }

}
