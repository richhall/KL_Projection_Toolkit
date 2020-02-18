#if UNITY_STANDALONE_OSX
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
        string selectedSource;
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
            string[] sourceList = GetSpoutSyphonList();
            if (lastList != null)
            {
                bool areEqual = lastList.SequenceEqual(sourceList);
                if (areEqual) return;
            }
            lastList = sourceList;
            syphonItems = new List<UI.SyphonItem>();
            //clear the transform
            foreach (Transform child in objListing.transform) Destroy(child.gameObject);
            foreach (var source in sourceList)
            {
                var projectGameObject = Instantiate(prefabSyphonItem, objListing.transform);
                var item = projectGameObject.GetComponent<PI.ProjectionToolkit.UI.SyphonItem>();
                item.SetData(source, source == selectedSource ? imgBackgroundSelected : imgBackground, this);
                syphonItems.Add(item);
            }
        }

        private string[] GetSpoutSyphonList()
        {
            
#if UNITY_STANDALONE_OSX
            var list = Plugin_CreateServerList();
            var count = Plugin_GetServerListCount(list);
            var namesString = new string[count];
            var names = new Tuple<string, string>[count];
            for (var i = 0; i < count; i++)
            {

                var pName = Plugin_GetNameFromServerList(list, i);
                var pAppName = Plugin_GetAppNameFromServerList(list, i);

                var name = (pName != IntPtr.Zero) ? Marshal.PtrToStringAnsi(pName) : "(no name)";
                var appName = (pAppName != IntPtr.Zero) ? Marshal.PtrToStringAnsi(pAppName) : "(no app name)";
                names[i] = Tuple.Create(appName, name);
            }


            for (var i = 0; i < names.Count(); i++)
            {
                namesString[i] = ($"{names[i].Item1}/{names[i].Item2}");
            }



#endif

#if UNITY_STANDALONE_WIN
            var count = PluginEntry.CountSharedObjects();
            var namesString = new string[count];
            var names = new List<string>();
            for (var i = 0;i < count; i++)
                nameString[i] =PluginEntry.GetSharedObjectNameString(i);
    
#endif

            return namesString;


        }

        public void SetSyphon(string filter)
        {
//            if (selectedSource.Item1 != source.Item1)
            
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
#endif