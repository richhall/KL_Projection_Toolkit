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
        private Project _project = null;

        public void LoadProject(Project project)
        {
            _project = project;
            SetProjectHud();
        }

        public void SetProjectHud()
        {
            this.gameObject.SetActive(_project != null);
            objRightPanel.SetActive(_project != null);
            objRightPanelHolding.SetActive(_project == null);
            objRightPanel.SetActive(_project != null);
            objBackground.SetActive(_project == null);
        }
    }

}
