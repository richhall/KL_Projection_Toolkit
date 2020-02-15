using UnityEngine;
using UnityEngine.EventSystems;
using PI.ProjectionToolkit.Models;
using PI.ProjectionToolkit.UI;
using System.Collections;
using System.Linq;
using UnityEngine.Video;

#if UNITY_STANDALONE_WIN
using Klak.Spout;
#endif

#if UNITY_STANDALONE_OSX
using Klak.Syphon;
#endif

namespace PI.ProjectionToolkit
{
    public class ModelItem : MonoBehaviour
    {
        public VideoPlayer videoPlayer;

#if UNITY_STANDALONE_WIN
        public SpoutReceiver spoutReceiver;
#endif

#if UNITY_STANDALONE_OSX
        public SyphonClient syphonClient;
#endif
        public GameObject prefabSpoutReceiver;
        public GameObject spoutSyphonContainer;
        public GameObject obj;
        public Material material;

        void Start()
        {

#if UNITY_EDITOR
            Debug.Log("Unity Editor");
#endif

#if UNITY_IOS
            Debug.Log("Iphone");
#endif

#if UNITY_STANDALONE_OSX
            Debug.Log("Stand Alone OSX");
#endif

#if UNITY_STANDALONE_WIN
            Debug.Log("Stand Alone Windows");
#endif

        }


        public void SetVideo(string url)
        {
            ClearSpoutSyphonContainer();
            videoPlayer.enabled = true;
            videoPlayer.url = url;
            videoPlayer.Play();
        }

#if UNITY_STANDALONE_WIN
        public void SetSpout(string name)
        {
            if (videoPlayer.isPlaying) videoPlayer.Stop();
            videoPlayer.enabled = false;
            ClearSpoutSyphonContainer();
            var objSpoutReceiver = Instantiate(prefabSpoutReceiver, spoutSyphonContainer.transform);
            var newSpoutReceiver = objSpoutReceiver.GetComponent<SpoutReceiver>();
            newSpoutReceiver.targetTexture = spoutReceiver.targetTexture;
            newSpoutReceiver.targetRenderer = spoutReceiver.targetRenderer;
            newSpoutReceiver.nameFilter = name;
        }
#endif

#if UNITY_STANDALONE_OSX
        public void SetSyphon(string appName, string serverName)
        {
            if (videoPlayer.isPlaying) videoPlayer.Stop();
            videoPlayer.enabled = false;
            ClearSpoutSyphonContainer();
            var objSyphonReceiver = Instantiate(prefabSpoutReceiver, spoutSyphonContainer.transform);
            var newSyphonClient = objSyphonReceiver.GetComponent<SyphonClient>();
            newSyphonClient.targetTexture = spoutReceiver.targetTexture;
            newSyphonClient.targetRenderer = spoutReceiver.targetRenderer;
            newSyphonClient.appName = appName;
            newSyphonClient.serverName = serverName;
        }
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