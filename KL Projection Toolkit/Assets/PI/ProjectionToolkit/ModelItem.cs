using UnityEngine;
using UnityEngine.EventSystems;
using PI.ProjectionToolkit.Models;
using PI.ProjectionToolkit.UI;
using System.Collections;
using System.Linq;
using UnityEngine.Video;
using Klak.Spout;

namespace PI.ProjectionToolkit
{
    public class ModelItem : MonoBehaviour
    {
        public VideoPlayer videoPlayer;
        public SpoutReceiver spoutReceiver;
        public GameObject prefabSpoutReceiver;
        public GameObject spoutContainer;
        public GameObject obj;
        public Material material;
        
        public void SetVideo(string url)
        {
            ClearSpoutContainer();
            videoPlayer.enabled = true;
            videoPlayer.url = url;
            videoPlayer.Play();
        }

        public void SetSpout(string name)
        {
            if (videoPlayer.isPlaying) videoPlayer.Stop();
            videoPlayer.enabled = false;
            ClearSpoutContainer();
            var objSpoutReceiver = Instantiate(prefabSpoutReceiver, spoutContainer.transform);
            var newSpoutReceiver = objSpoutReceiver.GetComponent<SpoutReceiver>();
            newSpoutReceiver.targetTexture = spoutReceiver.targetTexture;
            newSpoutReceiver.targetRenderer = spoutReceiver.targetRenderer;
            newSpoutReceiver.nameFilter = name;
        }

        public void SetMaterial()
        {
            if (videoPlayer.isPlaying) videoPlayer.Stop();
            videoPlayer.enabled = false;
            ClearSpoutContainer();
        }

        private void ClearSpoutContainer()
        {
            foreach (Transform child in spoutContainer.transform) Destroy(child.gameObject);            
        }
    }
}