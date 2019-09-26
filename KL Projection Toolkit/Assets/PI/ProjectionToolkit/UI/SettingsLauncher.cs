using UnityEngine;
using UnityEngine.EventSystems;
using PI.ProjectionToolkit.Models;
using TMPro;

namespace PI.ProjectionToolkit.UI
{
    public class SettingsLauncher : MonoBehaviour
    {
        public Michsky.UI.Frost.TopPanelManager topPanelManager;
        public PI.ProjectionToolkit.ProjectManager projectManager;

        private bool isOn = false;

        void Start()
        {
        }

        public void Toggle()
        {
            //do for in project view
            if(projectManager.CurrentProject == null)
            {
                if (isOn)
                {
                    topPanelManager.PanelAnim(0);
                } else
                {
                    topPanelManager.PanelAnim(2);
                }
                projectManager.SetMainCamera();
            } else
            {
                if (isOn)
                {
                    topPanelManager.PanelAnim(1);
                }
                else
                {
                    topPanelManager.PanelAnim(2);
                }
            }

            isOn = !isOn;
        }
    }
}