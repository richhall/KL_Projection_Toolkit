using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class overlay : MonoBehaviour
{
    public Texture2D tex;

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), tex);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
