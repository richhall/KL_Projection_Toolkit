
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
#if UNITY_STANDALONE_WIN
using Klak.Spout;

namespace PI.ProjectionToolkit
{
    public class SpoutManager : MonoBehaviour
    {
        public GameObject objListing;
        public GameObject prefabSpoutItem;
        SpoutReceiver _receiver;
        string selectedSource = "";
        public Sprite imgBackground;
        public Sprite imgBackgroundSelected;
        public int updateCount = 50;
        private int currentCount = 0;
        public GameObject objSpoutReceivedContainer;
        public GameObject prefabSpoutReceiver;

        List<string> _sourceNames = new List<string>();
        bool _disableCallback;

        void Start()
        {
        }

        void Update()
        {
            if (currentCount == 0)
            {
                RebuildList();
            } 
            currentCount += 1;
            if (currentCount > updateCount) currentCount = 0;
        }

        private string[] lastList = null;
        private List<PI.ProjectionToolkit.UI.SpoutItem> spoutItems;
        private void RebuildList()
        {
            string[] sourceList = GetSpoutList();
            if(lastList != null)
            {
                bool areEqual = lastList.SequenceEqual(sourceList);
                if (areEqual) return;
            }
            lastList = sourceList;
            spoutItems = new List<UI.SpoutItem>();
            //clear the transform
            foreach (Transform child in objListing.transform) Destroy(child.gameObject);
            foreach (var source in sourceList)
            {
                var projectGameObject = Instantiate(prefabSpoutItem, objListing.transform);
                var item = projectGameObject.GetComponent<PI.ProjectionToolkit.UI.SpoutItem>();
                item.SetData(source, source == selectedSource ? imgBackgroundSelected : imgBackground, this);
                spoutItems.Add(item);
            }
        }

        private string[] GetSpoutList()
        {
            var count = PluginEntry.CountSharedObjects();
            var names = new string[count];
            for (var i = 0; i < count; i++)
                names[i] = PluginEntry.GetSharedObjectNameString(i);
            return names;
        }

        public void SetSpout(string source)
        {
            if (selectedSource != source)
            {
                selectedSource = source;
                foreach (Transform child in objSpoutReceivedContainer.transform) Destroy(child.gameObject);
                //create new receiver
                var projectGameObject = Instantiate(prefabSpoutReceiver, objSpoutReceivedContainer.transform);
                _receiver = projectGameObject.GetComponent<SpoutReceiver>();
                _receiver.nameFilter = selectedSource;
                foreach (var item in spoutItems)
                {
                    item.ChangeBackground(item.name == selectedSource ? imgBackgroundSelected : imgBackground);
                }
            }
        }
    }
}
#endif
#if UNITY_STANDALONE_OSX
namespace PI.ProjectionToolkit
{
    public class SpoutManager : MonoBehaviour
    {
    }
}
#endif