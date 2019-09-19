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
        private int displayCount = 1;
        public Canvas mainCanvas;

        void Start()
        {
            displayCount = UnityEditor.EditorApplication.isPlaying ? 4 : Display.displays.Length;
            InitDisplays();
        }

        private void InitDisplays()
        {

            objDisplayButton.SetActive(displayCount > 1);
            objNextButton.SetActive(displayCount > 1);
            objLaunchButton.SetActive(displayCount == 1);
            objDisplay2.SetActive(displayCount > 1);
            objDisplay3.SetActive(displayCount > 2);
            objDisplay4.SetActive(displayCount > 3);
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
            if (displayCount > 1 && objToggle2.isOn)
            {
                //add in display prefab
                var display1 = Instantiate(prefabDisplay, transform);
                var displayItem1 = display1.GetComponent<DisplayItem>();
                displayItem1.SetData(1, true);
                applicationManager.displayItems.Add(displayItem1);

                multiDisplay = true;
                //Display.displays[1].SetParams(Display.displays[1].systemWidth, Display.displays[1].systemHeight, 0, 0); //windows only
                if(!UnityEditor.EditorApplication.isPlaying) Display.displays[1].Activate();
            }
            if (displayCount > 2 && objToggle3.isOn)
            {
                //add in display prefab
                var display2 = Instantiate(prefabDisplay, transform);
                var displayItem2 = display2.GetComponent<DisplayItem>();
                displayItem2.SetData(2, true);
                applicationManager.displayItems.Add(displayItem2);

                multiDisplay = true;
                //Display.displays[2].SetParams(Display.displays[2].systemWidth, Display.displays[2].systemHeight, 0, 0); //windows only
                if (!UnityEditor.EditorApplication.isPlaying) Display.displays[2].Activate();
            }
            if (displayCount > 3 && objToggle4.isOn)
            {
                //add in display prefab
                var display3 = Instantiate(prefabDisplay, transform);
                var displayItem3 = display3.GetComponent<DisplayItem>();
                displayItem3.SetData(3, true);
                applicationManager.displayItems.Add(displayItem3);

                multiDisplay = true;
                //Display.displays[3].SetParams(Display.displays[3].systemWidth, Display.displays[3].systemHeight, 0, 0); //windows only
                if (!UnityEditor.EditorApplication.isPlaying) Display.displays[3].Activate();
            }
            if (multiDisplay)
            {
                //do the main display
                //Display.main.SetParams(Display.displays[0].systemWidth, Display.displays[0].systemHeight, 0, 0); //windows only
                //Screen.SetResolution(1024, 768, true);
                //Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                mainCanvas.targetDisplay = 0;
            }
        }


    }
}