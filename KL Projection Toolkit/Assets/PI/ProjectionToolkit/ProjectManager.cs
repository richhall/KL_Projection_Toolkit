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
        private Project _project = null;

        public GameObject mainCamera;
        public GameObject fpsController;

        private List<GameObject> cameras = new List<GameObject>();

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
            foreach (Transform child in objCameraList.transform) Destroy(child.gameObject);
            cameras = new List<GameObject>();
            cameras.Add(fpsController);
            int index = 0;
            //add list item
            var listItem = Instantiate(prefabCameraListItem, objCameraList.transform);
            var ci = listItem.GetComponent<ProjectCameraListItem>();
            ci.SetWalkAbout(index, this);
            foreach (var camera in _project.projectionSite.cameras)
            {
                index += 1;
                cameras.Add(AddCamera(camera, index));
            }
            SetCamera(0);
        }

        private GameObject AddCamera(Models.Camera camera, int index)
        {
            //add list item
            var listItem = Instantiate(prefabCameraListItem, objCameraList.transform);
            var ci = listItem.GetComponent<ProjectCameraListItem>();
            ci.SetData(camera, index, this);
            //add camera to cameras
            var gameObject = Instantiate(prefabCameraItem, objCameraList.transform);
            gameObject.SetActive(false);
            //do all the config for the camera
            return gameObject;
        }

        public void SetCamera(int index)
        {
            if(index >= 0 && index < cameras.Count)
            {
                for (var x = 0; x < cameras.Count; x++)
                {
                    cameras[x].SetActive(x == index);
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
