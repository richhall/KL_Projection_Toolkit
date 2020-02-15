using UnityEngine;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using Klak.Syphon;

namespace PI.ProjectionToolkit
{
    public class SyphonManager : MonoBehaviour
    {
        public GameObject objListing;
        public GameObject prefabSyphonItem;
        SyphonClient _receiver;
        Tuple<string,string> selectedSource = Tuple.Create("","");
        public Sprite imgBackground;
        public Sprite imgBackgroundSelected;
        public int updateCount = 50;
        private int currentCount = 0;
        public GameObject objSpoutReceivedContainer;
        public GameObject prefabSyphonReceiver;

        List<string> _sourceNames = new List<string>();
        bool _disableCallback;

        void Start()
        {
           int currentCountgg = 0;
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
        private List<PI.ProjectionToolkit.UI.SyphonItem> syphonItems;
        private void RebuildList()
        {
            Tuple<string,string>[] sourceList = GetSyphonList();
          
            syphonItems = new List<UI.SyphonItem>();
            //clear the transform
            foreach (Transform child in objListing.transform) Destroy(child.gameObject);
            foreach (var source in sourceList)
            {
                var projectGameObject = Instantiate(prefabSyphonItem, objListing.transform);
                var item = projectGameObject.GetComponent<PI.ProjectionToolkit.UI.SyphonItem>();
                item.SetData(source.Item2,source.Item1, source.Item1 == selectedSource.Item1 ? imgBackgroundSelected : imgBackground, this);
                syphonItems.Add(item);
            }
        }

        private Tuple<string, string>[] GetSyphonList()
        {


            var list = Plugin_CreateServerList();
            var count = Plugin_GetServerListCount(list);
            var names = new Tuple<string, string>[count];
            for (var i = 0; i < count; i++)
            {

                var pName = Plugin_GetNameFromServerList(list, i);
                var pAppName = Plugin_GetAppNameFromServerList(list, i);

                var name = (pName != IntPtr.Zero) ? Marshal.PtrToStringAnsi(pName) : "(no name)";
                var appName = (pAppName != IntPtr.Zero) ? Marshal.PtrToStringAnsi(pAppName) : "(no app name)";
                names[i] = Tuple.Create(appName, name);
            }
          
   
            return names;
        }

        public void SetSyphon(Tuple<string,string> source)
        {
            if (selectedSource.Item1 != source.Item1)
            {
                selectedSource = source;
                foreach (Transform child in objSpoutReceivedContainer.transform) Destroy(child.gameObject);
                //create new receiver
                var projectGameObject = Instantiate(prefabSyphonReceiver, objSpoutReceivedContainer.transform);
                _receiver = projectGameObject.GetComponent<SyphonClient>();
                _receiver.appName = selectedSource.Item1;
                _receiver.serverName = selectedSource.Item2;
                foreach (var item in syphonItems)
                {
                    item.ChangeBackground(item.name == selectedSource.Item1 ? imgBackgroundSelected : imgBackground);
                }
            }
        }

        #region Native plugin entry points

        [DllImport("KlakSyphon")]
        static extern IntPtr Plugin_CreateServerList();

        [DllImport("KlakSyphon")]
        static extern void Plugin_DestroyServerList(IntPtr list);

        [DllImport("KlakSyphon")]
        static extern int Plugin_GetServerListCount(IntPtr list);

        [DllImport("KlakSyphon")]
        static extern IntPtr Plugin_GetNameFromServerList(IntPtr list, int index);

        [DllImport("KlakSyphon")]
        static extern IntPtr Plugin_GetAppNameFromServerList(IntPtr list, int index);

        #endregion
    }
}