using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;
using System.IO;

namespace PI.ProjectionToolkit
{
    public class DisplayItem : MonoBehaviour
    {
        public int index = 1;
        public bool isOn = false;
        public Canvas canvas;
        public Camera camera;
        public GameObject objNumber;
        public GameObject objResolution;
        public GameObject objResolutionScreen;
        private TextMeshProUGUI txtNumber;
        private TextMeshProUGUI txtResolution;
        private TextMeshProUGUI txtResolutionScreen;
        public GameObject objBackground;
        private RawImage imgBackground;
        public GameObject objVideo;
        private RawImage imgVideo;
        private VideoPlayer videoPlayer;
        public RenderTexture[] videoTextures;
        private DisplayListItem _displayListItem;

        void Start()
        {
        }

        public void SetData(int index, bool isOn)
        {
            canvas.targetDisplay = index;
            txtNumber = objNumber.GetComponent<TextMeshProUGUI>();
            txtResolution = objResolution.GetComponent<TextMeshProUGUI>();
            txtResolutionScreen = objResolutionScreen.GetComponent<TextMeshProUGUI>();
            imgBackground = objBackground.GetComponent<RawImage>();
            imgVideo = objVideo.GetComponent<RawImage>();
            videoPlayer = objVideo.GetComponent<VideoPlayer>();
            this.index = index;
            this.isOn = isOn;
            this.gameObject.SetActive(isOn);
            if (isOn)
            {
                txtNumber.text = (index + 1).ToString();
                try
                {
                    txtResolution.text = Display.displays[index].renderingWidth.ToString() + " x " + Display.displays[index].renderingHeight.ToString();
                    txtResolutionScreen.text = Display.displays[index].systemWidth.ToString() + " x " + Display.displays[index].systemHeight.ToString();
                }
                catch
                {
                    //ignore as in editor and isOn set to true when display doesn't exist
                }
                camera.targetDisplay = index;
                imgVideo.texture = videoTextures[index - 1];
                videoPlayer.targetTexture = videoTextures[index - 1];
            }
        }

        void Update()
        {
            if (!isOn) return;

            if (Input.GetKeyDown(KeyCode.F12))
            {
                ShowHideInfo();
            }
        }

        public void ShowHideInfo()
        {
            if (!isOn) return;
            objNumber.SetActive(!objNumber.activeSelf);
            objResolution.SetActive(!objResolution.activeSelf);
            objResolutionScreen.SetActive(!objResolutionScreen.activeSelf);
        }

        public void DisplayVideo(string url)
        {
            if (!isOn) return;
            objBackground.SetActive(false);
            objVideo.SetActive(true);
            videoPlayer.url = url;
            videoPlayer.Play();
        }

        public void DisplayBackground(string url)
        {
            if (!isOn) return;
            if (videoPlayer.isPlaying) videoPlayer.Stop();
            // read image and store in a byte array
            byte[] byteArray = File.ReadAllBytes(url);
            //create a texture and load byte array to it
            // Texture size does not matter 
            Texture2D sampleTexture = new Texture2D(2, 2);
            // the size of the texture will be replaced by image size
            bool isLoaded = sampleTexture.LoadImage(byteArray);
            if (isLoaded)
            {
                imgBackground.texture = sampleTexture;
            }

            objBackground.SetActive(true);
            objVideo.SetActive(false);
        }

        public void DisplayCamera(ProjectCameraItem cameraItem)
        {
            if (!isOn) return;
            //setup camera position based on the camera sent
            objBackground.SetActive(false);
            objVideo.SetActive(false);
        }

    }
}