using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MulitScreenTest : MonoBehaviour
{
    public Toggle display2;
    public Toggle display3;
    public Toggle display4;

    // Start is called before the first frame update
    void Start()
    {
        display2.isOn = Display.displays.Length > 1;
        display3.isOn = Display.displays.Length > 2;
        display4.isOn = Display.displays.Length > 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDisplays()
    {
        // Display.displays[0] is the primary, default display and is always ON.
        // Check if additional displays are available and activate each.
        if (Display.displays.Length > 1 && display2.isOn) {
            Display.displays[1].Activate();
        }
        if (Display.displays.Length > 2 && display3.isOn)
        {
            Display.displays[2].Activate();
        }
        if (Display.displays.Length > 3 && display4.isOn)
        {
            Display.displays[3].Activate();
        }
        SceneManager.LoadScene(1);
    }
}
