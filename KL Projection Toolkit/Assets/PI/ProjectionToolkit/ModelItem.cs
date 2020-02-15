using UnityEngine;
using UnityEngine.EventSystems;
using PI.ProjectionToolkit.Models;
using PI.ProjectionToolkit.UI;
using System.Collections;
using System.Linq;
using UnityEngine.Video;
//using Klak.Spout;
using Klak.Syphon;


namespace PI.ProjectionToolkit
{
    public class ModelItem : MonoBehaviour
    {
        public VideoPlayer videoPlayer;
        public SyphonClient syphonClient;
        public GameObject prefabSyphonReceiver;
        public GameObject syphonContainer;
        public GameObject obj;
        public Material material;

        void Start()
        {
#if UNITY_STANDALONE_OSX
            Debug.Log("Standalone OSX");
#endif
        }


        public void SetVideo(string url)
        {
            ClearSyphonContainer();
            videoPlayer.enabled = true;
            videoPlayer.url = url;
            videoPlayer.Play();
        }

        public void SetSyphon(string appName, string serverName)
        {
            if (videoPlayer.isPlaying) videoPlayer.Stop();
            videoPlayer.enabled = false;
            ClearSyphonContainer();
            var objSyphonClient = Instantiate(prefabSyphonReceiver, syphonContainer.transform);
            var newSyphonClient = objSyphonClient.GetComponent<SyphonClient>();
            newSyphonClient.targetTexture = syphonClient.targetTexture;
            newSyphonClient.targetRenderer = syphonClient.targetRenderer;
            newSyphonClient.appName = appName;
            newSyphonClient.serverName = serverName;

        }

        public void SetMaterial()
        {
            if (videoPlayer.isPlaying) videoPlayer.Stop();
            videoPlayer.enabled = false;
            ClearSyphonContainer();
        }

        private void ClearSyphonContainer()
        {
            foreach (Transform child in syphonContainer.transform) Destroy(child.gameObject);            
        }
    }
}