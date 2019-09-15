using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using PI.ProjectionToolkit.Models;
using TMPro;
using PI.ProjectionToolkit.UI;
using SimpleFileBrowser;

namespace PI.ProjectionToolkit
{
    public class CreateProjectManager : UiDetailsBase
    {
        private Project _project;
        private ProjectionSite _projectionSite;
        public UnityEngine.UI.Scrollbar scrollbar;
        private ProjectManager _projectManager;
        public Sprite imgFolderIcon;

        private TextLineButton _folderLine;

        void Start()
        {
            // Set filters (optional)
            // It is sufficient to set the filters just once (instead of each time before showing the file browser dialog), 
            // if all the dialogs will be using the same filters
            FileBrowser.SetFilters(true, new FileBrowser.Filter("Videos", ".mp4", ".mov"));

            // Set default filter that is selected when the dialog is shown (optional)
            // Returns true if the default filter is set successfully
            // In this case, set Images filter as the default filter
            FileBrowser.SetDefaultFilter(".mp4");

            // Set excluded file extensions (optional) (by default, .lnk and .tmp extensions are excluded)
            // Note that when you use this function, .lnk and .tmp extensions will no longer be
            // excluded unless you explicitly add them as parameters to the function
            FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");

            // Add a new quick link to the browser (optional) (returns true if quick link is added successfully)
            // It is sufficient to add a quick link just once
            // Name: Users
            // Path: C:\Users
            // Icon: default (folder icon)
            FileBrowser.AddQuickLink("Users", "C:\\Users", null);
        }

        public void SetData(ProjectionSite projectionSite, ProjectManager projectManager)
        {
            _projectionSite = projectionSite;
            _projectManager = projectManager;
            _project = new Project()
            {
                createdBy = projectManager.users.current.email,
                updatedBy = projectManager.users.current.email,
                projectionSite = _projectionSite,
                siteId = _projectionSite.id,
                siteVersionId = _projectionSite.versionId
            };
            scrollbar.value = 1; //set scrollbar to top
            //clear the transform
            foreach (Transform child in objList.transform) Destroy(child.gameObject);
            //build listing
            AddHeader("PROJECTION SITE");
            AddTextLine("NAME", _projectionSite.name);
            AddTextLine("VERSION", _projectionSite.version);
            switch (_projectionSite.status)
            {
                case ProjectionSiteStatus.Unknown:
                    AddTextLine("VERSION STATUS", "UNKNOWN");
                    break;
                case ProjectionSiteStatus.OutOfDate:
                    AddTextLine("VERSION STATUS", "OUT OF DATE", false, true);
                    break;
                case ProjectionSiteStatus.NotOnServer:
                    AddTextLine("VERSION STATUS", "NOT ON SERVER", false, true);
                    break;
                case ProjectionSiteStatus.UpToDate:
                    AddTextLine("VERSION STATUS", "UP TO DATE");
                    break;
                case ProjectionSiteStatus.NewOnServer:
                    AddTextLine("VERSION STATUS", "NEW");
                    break;
            }
            AddHeader("PROJECT SETTINGS");
            AddTextLine("NAME", _project.name);
            _folderLine = AddTextLineButton("FOLDER", _project.path, imgFolderIcon);
            _folderLine.OnButtonClick += _folderLine_OnButtonClick;
        }

        private void _folderLine_OnButtonClick()
        {
            StartCoroutine(ShowLoadProjectPathDialogCoroutine());
        }

        IEnumerator ShowLoadProjectPathDialogCoroutine()
        {
            // Show a load file dialog and wait for a response from user
            // Load file/folder: file, Initial path: default (Documents), Title: "Load File", submit button text: "Load"
            yield return FileBrowser.WaitForLoadDialog(true, _projectManager.rootPath, "Select Folder");

            if (FileBrowser.Success)
            {
                _project.path = FileBrowser.Result;
                _folderLine.value.text = FileBrowser.Result;
            }
        }
    }
}