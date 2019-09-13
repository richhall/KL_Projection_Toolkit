using UnityEngine;
using UnityEngine.EventSystems;
using PI.ProjectionToolkit.Models;
using TMPro;
using PI.ProjectionToolkit.UI;

namespace PI.ProjectionToolkit
{
    public class ProjectionSiteDetailsManager : UiDetailsBase
    {
        private ProjectionSite _projectionSite;
        public GameObject objTitle;
        public Sprite imgLocationIcon;
        public UnityEngine.UI.Scrollbar scrollbar;

        void Start()
        {
        }

        public void SetData(ProjectionSite projectionSite)
        {
            _projectionSite = projectionSite;
            Header header = objTitle.GetComponent<Header>();
            header.SetData(_projectionSite.name);
            scrollbar.value = 1; //set scrollbar to top

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
            AddTextLine("PROJECTOR STACKS", _projectionSite.projectors.Count.ToString());
            AddTextLine("CAMERAS", _projectionSite.cameras.Count.ToString());
            AddTextLine("NOTES", _projectionSite.notes, true);
            AddSeperator();

            AddProjectorStack(_projectionSite.projectors);

            AddCamera(_projectionSite.cameras, true);

            AddHeader("DETAILS");
            AddTextLine("ID", _projectionSite.id);
            AddTextLine("VERSION ID", _projectionSite.versionId);
            AddTextLine("ASSET BUNDLE", _projectionSite.assetBundleName);
            AddSeperator();

            AddHeader("AUDIT");
            AddTextLine("LAST UPDATED", _projectionSite.updated.ToString("yyyy-MM-dd HH:mm"));
            AddTextLine("UPDATED BY", _projectionSite.updatedBy);
            AddTextLine("CREATED", _projectionSite.created.ToString("yyyy-MM-dd HH:mm"));
            AddTextLine("CREATED BY", _projectionSite.createdBy);
            AddSeperator();
        }

        private void BtnLoc_OnButtonClick()
        {
            string url = string.IsNullOrEmpty(_projectionSite.location.mapUrl) ? "https://www.google.co.uk/maps/@" + _projectionSite.location.geoLocation.latLng + ",14z" : _projectionSite.location.mapUrl;
            Application.OpenURL(url);
        }

    }
}