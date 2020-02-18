using UnityEngine;
using UnityEngine.EventSystems;
using PI.ProjectionToolkit.Models;
using PI.ProjectionToolkit.UI;
using System.Collections;
using System.Linq;
using UnityEngine.Video;

#if UNITY_STANALONE_WIN
//using Klak.Spout;
#endif

#if UNITY_STANDALONE_OSX
using Klak.Syphon;
#endif


namespace PI.ProjectionToolkit
{
    public class ModelItem : MonoBehaviour
    {
        public VideoPlayer videoPlayer;
        //public SyphonClient syphonClient;
        public GameObject prefabSyphonReceiver;
        public GameObject prefabSpoutReceiver;
        public GameObject spoutSyphonContainer;
        public GameObject obj;
        public Material material;
        public RenderTexture targetTexture;
        public Renderer targetRenderer;
        public string targetMaterialProperty;

        void Start()
        {
#if UNITY_STANDALONE_OSX
            Debug.Log("Standalone OSX");
#endif
#if UNITY_STANDALONE_WIN
            Debug.Log("Standalone WIN");
#endif
        }


        public void SetVideo(string url)
        {
            ClearSpoutSyphonContainer();
            videoPlayer.enabled = true;
            videoPlayer.url = url;
            videoPlayer.Play();
        }

#if UNITY_STANDALONE_OSX
        public void SetSyphon(string appName, string serverName)
        {
            if (videoPlayer.isPlaying) videoPlayer.Stop();
            videoPlayer.enabled = false;
            ClearSpoutSyphonContainer();
            var objSyphonClient = Instantiate(prefabSyphonReceiver, spoutSyphonContainer.transform);
            var newSyphonClient = objSyphonClient.GetComponent<SyphonClient>();
            newSyphonClient.targetTexture = targetTexture;
            newSyphonClient.targetRenderer = targetRenderer;
            newSyphonClient.appName = appName;
            newSyphonClient.serverName = serverName;
            newSyphonClient.enabled = true;
            newSyphonClient.targetMaterialProperty = targetMaterialProperty;

        }
#endif

#if UNITY_STANDALONE_WIN
        public void SetSpout(string name)
        {
            if (videoPlayer.isPlaying) videoPlayer.Stop();
            videoPlayer.enabled = false;
            ClearSpoutSyphonContainer();
            var objSpoutReceiver = Instantiate(prefabSpoutReceiver, spoutSyphonContainer.transform);
            var newSpoutReceiver = objSyphonClient.GetComponent<SpoutReceiver>();
            newSpoutReceiver.targetTexture = targetTexture;
            newSpoutReceiver.targetRenderer = targetRenderer;
            newSpoutReceiver.nameFilter = name;
            newSpoutReceiver.enabled = true;
            newSpoutReceiver.targetMaterialProperty = targetMaterialProperty;

        }
#endif

#if UNITY_STANDALONE_OSX


#endif



        public void SetMaterial()
        {
            if (videoPlayer.isPlaying) videoPlayer.Stop();
            videoPlayer.enabled = false;
            ClearSpoutSyphonContainer();
        }

        private void ClearSpoutSyphonContainer()
        {
            foreach (Transform child in spoutSyphonContainer.transform) Destroy(child.gameObject);            
        }
    }
}