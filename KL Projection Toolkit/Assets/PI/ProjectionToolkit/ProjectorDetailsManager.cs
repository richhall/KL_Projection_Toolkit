using UnityEngine;
using UnityEngine.EventSystems;
using PI.ProjectionToolkit.Models;
using TMPro;
using PI.ProjectionToolkit.UI;

namespace PI.ProjectionToolkit
{
    public class ProjectorDetailsManager : MonoBehaviour
    {
        private ProjectionSite _projectionSite;
        public GameObject objTitle;
        public GameObject objList;
        public GameObject prefabUiHeader;
        public GameObject prefabUiTextLine;
        public GameObject prefabUiTextLineButton;
        public Sprite imgLocationIcon;

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
            AddTextLine("LOCATION", _projectionSite.location.name);
            AddTextLine("CITY/TOWN", _projectionSite.location.town);
            TextLineButton btnLoc = AddTextLineButton("GEO LOCATION", _projectionSite.location.geoLocation.latLng, imgLocationIcon);
            btnLoc.OnButtonClick += BtnLoc_OnButtonClick;

            AddHeader("PROJECTORS");
            AddTextLine("COUNT", _projectionSite.projectors.Count.ToString());

            AddHeader("CAMERAS");
            AddTextLine("COUNT", _projectionSite.cameras.Count.ToString());

            AddHeader("DETAILS");
            AddTextLine("ID", _projectionSite.id);
            AddTextLine("VERSION ID", _projectionSite.versionId);
            AddTextLine("ASSET BUNDLE", _projectionSite.assetBundleName);

            AddHeader("AUDIT");
            AddTextLine("LAST UPDATED", _projectionSite.updated.ToString("yyyy-MM-dd HH:mm"));
            AddTextLine("UPDATED BY", _projectionSite.updatedBy);
            AddTextLine("CREATED", _projectionSite.created.ToString("yyyy-MM-dd HH:mm"));
            AddTextLine("CREATED BY", _projectionSite.createdBy);
        }

        private void BtnLoc_OnButtonClick()
        {
            string url = string.IsNullOrEmpty(_projectionSite.location.mapUrl) ? "https://www.google.co.uk/maps/@" + _projectionSite.location.geoLocation.latLng + ",14z" : _projectionSite.location.mapUrl;
            Application.OpenURL(url);
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

        private TextLineButton AddTextLineButton(string title, string value, Sprite icon)
        {
            var projectGameObject = Instantiate(prefabUiTextLineButton, objList.transform);
            var u = projectGameObject.GetComponent<TextLineButton>();
            if (u != null) u.SetData(title, value, icon);
            return u;
        }
    }
}