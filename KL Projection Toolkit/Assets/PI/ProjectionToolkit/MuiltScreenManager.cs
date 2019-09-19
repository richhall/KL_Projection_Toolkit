using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

namespace PI.ProjectionToolkit
{
    public class MuiltScreenManager : MonoBehaviour
    {
        public ApplicationManager applicationManager;
        public GameObject objDisplayButton;
        public GameObject objNextButton;
        public GameObject objLaunchButton;
        public GameObject objDisplay2;
        public Toggle objToggle2;
        public GameObject objDisplay3;
        public Toggle objToggle3;
        public GameObject objDisplay4;
        public Toggle objToggle4;
        public GameObject prefabDisplay;

        void Start()
        {
            objDisplayButton.SetActive(Display.displays.Length > 1);
            objNextButton.SetActive(Display.displays.Length > 1);
            objLaunchButton.SetActive(Display.displays.Length == 1);
            objDisplay2.SetActive(Display.displays.Length > 1);
            objDisplay3.SetActive(Display.displays.Length > 2);
            objDisplay4.SetActive(Display.displays.Length > 3);
        }

        void Update()
        {
        }

        public void SetDisplays()
        {
            //clear the gameobject
            foreach (Transform child in transform) Destroy(child.gameObject);

            bool multiDisplay = false;
            // Display.displays[0] is the primary, default display and is always ON.
            // Check if additional displays are available and activate each.
            if (Display.displays.Length > 1 && objToggle2.isOn)
            {
                //add in display prefab
                var display = Instantiate(prefabDisplay, transform);
                var displayItem = display.GetComponent<DisplayItem>();
                displayItem.SetData(1, true);
                applicationManager.displayItems.Add(displayItem);

                multiDisplay = true;
                //Display.displays[1].SetParams(Display.displays[1].systemWidth, Display.displays[1].systemHeight, 0, 0); //windows only
                Display.displays[1].Activate();
            }
            if (Display.displays.Length > 2 && objToggle3.isOn)
            {
                //add in display prefab
                var display = Instantiate(prefabDisplay, transform);
                var displayItem = display.GetComponent<DisplayItem>();
                displayItem.SetData(2, true);
                applicationManager.displayItems.Add(displayItem);

                multiDisplay = true;
                //Display.displays[2].SetParams(Display.displays[2].systemWidth, Display.displays[2].systemHeight, 0, 0); //windows only
                Display.displays[2].Activate();
            }
            if (Display.displays.Length > 3 && objToggle4.isOn)
            {
                //add in display prefab
                var display = Instantiate(prefabDisplay, transform);
                var displayItem = display.GetComponent<DisplayItem>();
                displayItem.SetData(3, true);
                applicationManager.displayItems.Add(displayItem);

                multiDisplay = true;
                //Display.displays[3].SetParams(Display.displays[3].systemWidth, Display.displays[3].systemHeight, 0, 0); //windows only
                Display.displays[3].Activate();
            }
            if (multiDisplay)
            {
                //do the main display
                //Display.main.SetParams(Display.displays[0].systemWidth, Display.displays[0].systemHeight, 0, 0); //windows only
                //Screen.SetResolution(1024, 768, true);
               //Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            }
        }


    }
}