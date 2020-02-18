using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class spoutSyphonText : MonoBehaviour
{

    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {

    }


    void SetText()
    {
#if UNITY_STANDALONE_OSX
        text.text = "APPLY SYPHON";
#endif
#if UNITY_STANDALONE_WIN
        text.text = "APPLY SPOUT";
#endif

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
