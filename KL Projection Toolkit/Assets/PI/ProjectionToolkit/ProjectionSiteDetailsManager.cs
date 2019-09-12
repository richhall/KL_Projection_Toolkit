using UnityEngine;
using UnityEngine.EventSystems;
using PI.ProjectionToolkit.Models;
using TMPro;
using PI.ProjectionToolkit.UI;

namespace PI.ProjectionToolkit
{
    public class ProjectionSiteDetailsManager : MonoBehaviour
    {
        private ProjectionSite _projectionSite;
        public GameObject objTitle;
        public GameObject objList;
        public GameObject prefabUiHeader;
        public GameObject prefabUiTextLine;

        void Start()
        {
        }

        public void SetData(ProjectionSite projectionSite)
        {
            _projectionSite = projectionSite;
            Header header = objTitle.GetComponent<Header>();
            header.SetData(_projectionSite.name);

            //clear the transform
            foreach (Transform child in objList.transform) Destroy(child.gameObject);
            //build listing
            AddHeader("OVERVIEW");
            AddTextLine("NAME", _projectionSite.name);
            AddTextLine("VERSION", _projectionSite.version);
            AddTextLine("LAST UPDATED", _projectionSite.updated.ToString("yyyy-MM-dd HH:mm"));
            AddTextLine("UPDATED BY", _projectionSite.updatedBy);
            AddTextLine("CREATED", _projectionSite.created.ToString("yyyy-MM-dd HH:mm"));
            AddTextLine("CREATED BY", _projectionSite.createdBy);
        }

        private void AddHeader(string header)
        {
            var projectGameObject = Instantiate(prefabUiHeader, objList.transform);
            var u = projectGameObject.GetComponent<Header>();
            u.SetData(header);
        }

        private void AddTextLine(string title, string value)
        {
            var projectGameObject = Instantiate(prefabUiTextLine, objList.transform);
            var u = projectGameObject.GetComponent<TextLine>();
            if(u != null) u.SetData(title, value);
        }
    }
}