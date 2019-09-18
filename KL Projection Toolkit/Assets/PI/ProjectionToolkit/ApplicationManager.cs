using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PI.ProjectionToolkit.Models;
using System.IO;
using TMPro;
using UnityEngine.Networking;

namespace PI.ProjectionToolkit
{
    /// <summary>
    /// Manager to provide all the logic to manage the application
    /// </summary>
    public class ApplicationManager : MonoBehaviour
    {
        public UnityEngine.UI.Button btnExit;
        public UnityEngine.UI.Button btnMessage;
        public TextMeshProUGUI messageText;
        public UnityEngine.UI.Button btnLoading;
        public UnityEngine.UI.Button btnLoadingClose;
        public UnityEngine.UI.Button btnUpdateProjectionSiteMinor;
        public UnityEngine.UI.Button btnMyProjects;
        public UnityEngine.UI.Button btnInstalledSites;
        public UnityEngine.UI.Button btnCreateProjectModal;
        public TextMeshProUGUI loadingText;
        public GameObject prefabProjectListItem;
        public GameObject objProjectList;
        public GameObject objProjectionSiteList;
        public GameObject objLatestProjectionSiteList;
        public GameObject objUpdateProjectionSiteMinorModal;
        public GameObject objCreateProjectModal;
        public GameObject objMessageModalRemoveButton;
        public Color colorAlert;

        public Michsky.UI.Frost.TopPanelManager homePanelManager;

        public GameObject[] DisplayNumbers;
        public GameObject[] ResNumbers;

        public Sprite[] backgroundImages;
        public GameObject objBackground;
        public Image objBackgroundImage;
        public GameObject objRightPanel;
        public GameObject objRightPanelHolding;

        public GameObject objProject;

        public ProjectManager projectManager;

        public void Awake()
        {
            Load();
            AudioListener.volume = 0.5f;
        }

        private string OnlinePath = "https://www.piandmash.com/resources/projection-toolkit/";

        #region PlayerPrefs
        private bool _noRootPathSet = true;
        private string _rootPath = null;
        public string rootPath
        {
            get
            {
                if (String.IsNullOrEmpty(_rootPath))
                {
                    //set the root path
                    string path = Application.dataPath;
                    if (Application.platform == RuntimePlatform.OSXPlayer)
                    {
                        path += "/../../";
                    }
                    else if (Application.platform == RuntimePlatform.WindowsPlayer)
                    {
                        path += "/../";
                    }
                    else
                    {
                        path = Application.persistentDataPath;
                    }
                    _rootPath = System.IO.Path.GetFullPath(path);
                    //if (PlayerPrefs.HasKey("rootPath"))
                    //{
                    //    _rootPath = PlayerPrefs.GetString("rootPath");
                    //    _noRootPathSet = false;
                    //}
                    //else
                    //{
                    //    _rootPath = Application.persistentDataPath;
                    //}
                }
                return _rootPath;
            }
        }

        #endregion

        /// <summary>
        /// The default data folder name
        /// </summary>
        private string dataFolder = @"\data";

        /// <summary>
        /// stores the users in the application
        /// </summary>
        public Users users = new Users();

        /// <summary>
        /// get the user file path
        /// </summary>
        private string usersFile
        {
            get
            {
                return dataFolder + @"\users.json";
            }
        }

        /// <summary>
        /// stores the projects in the application
        /// </summary>
        public Projects projects = new Projects();
        private ProjectReference currentProject;
        public Project currentFullProject;

        /// <summary>
        /// get the projects file path
        /// </summary>
        private string projectsFile
        {
            get
            {
                return dataFolder + @"\projects.json";
            }
        }


        /// <summary>
        /// The default data folder name
        /// </summary>
        public string projectsFolder = @"\projects";

        /// <summary>
        /// The default projection sites folder name
        /// </summary>
        private string projectionSitesFolder = @"\sites";

        /// <summary>
        /// stores the projectionSites in the application
        /// </summary>
        public ProjectionSites projectionSites = new ProjectionSites();

        /// <summary>
        /// get the projectionSites file path
        /// </summary>
        private string projectionSitesFile
        {
            get
            {
                return dataFolder + @"\sites.json";
            }
        }
        
        /// <summary>
        /// stores the latestProjectionSites in the application
        /// </summary>
        public ProjectionSites latestProjectionSites = new ProjectionSites();


        public TextMeshProUGUI dpath;
        public void Load()
        {
            ////get the root path
            //if (String.IsNullOrEmpty(_rootPath))
            //{
            //    if (PlayerPrefs.HasKey("rootPath"))
            //    {
            //        _rootPath = PlayerPrefs.GetString("rootPath").Replace("/", @"\");
            //        _noRootPathSet = false;
            //    }
            //}
            //if(_noRootPathSet)
            //{

            //    //show the root path modal


            //    //the result will run the change rootpath
            //    ChangeRootPath(Application.persistentDataPath); //for testing
            //}

            //create or check initial paths
            ChangeRootPath(null);
            //load users & projects
            LoadUsers();
            LoadProjects();
            LoadProjectionSites();
        }

        public void ChangeRootPath(string newPath)
        {
            try
            {
                string oldPath = rootPath;
                if (!String.IsNullOrEmpty(newPath)) _rootPath = newPath.Replace("/", @"\");
                if ((!String.IsNullOrEmpty(newPath)) && !String.IsNullOrEmpty(oldPath) && Directory.Exists(oldPath + dataFolder))
                {
                    //move over the data folder
                    Directory.Move(oldPath + dataFolder, _rootPath + dataFolder);
                }
                else
                {
                    //create a new data directory at the root path
                    if (!Directory.Exists(_rootPath + dataFolder))
                    {
                        Directory.CreateDirectory(_rootPath + dataFolder);
                    }
                    //create a new projection sites directory at the root path
                    if (!Directory.Exists(_rootPath + projectionSitesFolder))
                    {
                        Directory.CreateDirectory(_rootPath + projectionSitesFolder);
                    }
                    //create a new project directory at the root path
                    if (!Directory.Exists(_rootPath + projectsFolder))
                    {
                        Directory.CreateDirectory(_rootPath + projectsFolder);
                    }
                    //create the users and project files in the default root path
                    if (!File.Exists(_rootPath + usersFile)) SaveUsers(new Users());
                    if (!File.Exists(_rootPath + projectsFile)) SaveProjects(new Projects());
                    if (!File.Exists(_rootPath + projectionSitesFile)) SaveProjectionSites(new ProjectionSites());
                    //if (!File.Exists(_rootPath + projectsFile))
                    //{
                    //    var p = new Projects();
                    //    p.AddProject(new Project()
                    //    {
                    //        name = "Project 1",
                    //        createdBy = "pete",
                    //        path = @"D:\Art\KL_Projection_Toolkit\Projects\Project 1"
                    //    });
                    //    p.AddProject(new Project()
                    //    {
                    //        name = "Project 2",
                    //        createdBy = "pete",
                    //        path = @"D:\Art\KL_Projection_Toolkit\Projects\Project 2"
                    //    });
                    //    SaveProjects(p);
                    //}
                    //if (!File.Exists(_rootPath + projectionSitesFile))
                    //{
                    //    projectionSites = new ProjectionSites();
                    //    projectionSites.AddSite(new ProjectionSite()
                    //    {
                    //        name = "Custom House",
                    //        createdBy = "pete",
                    //        location = new Common.Models.Location()
                    //        {
                    //            name = "Custom House",
                    //            town = "Kings Lynn",
                    //            geoLocation = new Common.Models.GeoLocation()
                    //            {
                    //                lat = 52.753848,
                    //                lng = 0.3913075
                    //            }
                    //        }
                    //    });
                    //    var site = new ProjectionSite()
                    //    {
                    //        name = "St Nicks",
                    //        createdBy = "pete",
                    //        location = new Common.Models.Location()
                    //        {
                    //            name = "St. Nicholas' Chapel",
                    //            town = "Kings Lynn",
                    //            geoLocation = new Common.Models.GeoLocation()
                    //            {
                    //                lat = 52.7575175,
                    //                lng = 0.394143
                    //            }
                    //        }
                    //    };
                    //    var stack = new ProjectorStack()
                    //    {
                    //        name = "Main Stack",
                    //        majorVersion = 1,
                    //        minorVersion = 0,
                    //        siteId = site.id
                    //    };
                    //    stack.projectors.Add(new Models.Camera()
                    //    {
                    //        name = "Base Projector",
                    //        physical = true
                    //    });
                    //    stack.projectors.Add(new Models.Camera()
                    //    {
                    //        name = "Top Projector",
                    //        position = new Common.Vector3()
                    //        {
                    //            y = 20
                    //        },
                    //        physical = true
                    //    });
                    //    site.projectors.Add(stack);
                    //    site.cameras.Add(new Models.Camera()
                    //    {
                    //        name = "View 1",
                    //        position = new Common.Vector3()
                    //        {
                    //            x = 100,
                    //            z = 20
                    //        }
                    //    });
                    //    projectionSites.AddSite(site);
                    //    SaveProjectionSites(projectionSites);
                    //}
                }
            }
            catch(Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        public delegate void userLoadedDelegate(Users users);
        public event userLoadedDelegate OnUsersLoaded;  

        private Users LoadUsers()
        {
            try
            {
                if (File.Exists(_rootPath + usersFile))
                {
                    string json = File.ReadAllText(_rootPath + usersFile);
                    users = JsonUtility.FromJson<Users>(json);
                }
                else
                {
                    //create new users 
                    users = new Users();
                    SaveUsers(users);
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
            if (OnUsersLoaded != null) OnUsersLoaded(users);
            return users;
        }

        public void SaveUsers(Users users = null)
        {
            try
            {
                if(users != null) this.users = users;
                var json = JsonUtility.ToJson(this.users);
                File.WriteAllText(_rootPath + usersFile, json);
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        public delegate void projectLoadedDelegate(Projects projects);
        public event projectLoadedDelegate OnProjectsLoaded;

        private Projects LoadProjects()
        {
            try
            {
                if (File.Exists(_rootPath + projectsFile))
                {
                    string json = File.ReadAllText(_rootPath + projectsFile);
                    projects = JsonUtility.FromJson<Projects>(json);
                }
                else
                {
                    SaveProjects(projects);
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
            this.RebuildProjects();
            return projects;
        }

        public void RebuildProjects()
        {
            if (objProjectList != null)
            {
                //clear the transform
                foreach (Transform child in objProjectList.transform) Destroy(child.gameObject);
                foreach (var project in projects.projects.OrderByDescending(o => o.updatedAsDate))
                {
                    var projectGameObject = Instantiate(prefabProjectListItem, objProjectList.transform);
                    var btn = projectGameObject.GetComponent<ProjectButton>();
                    btn.applicationManager = this;
                    btn.SetProjectReference(project, currentProject != null && project.id == currentProject.id);
                }
            }
            if (OnProjectsLoaded != null) OnProjectsLoaded(projects);
        }

        public void SaveProjects(Projects projects)
        {
            try
            {
                var json = JsonUtility.ToJson(projects);
                File.WriteAllText(_rootPath + projectsFile, json);
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        public void CreateNewProject(Project project)
        {
            try
            {
                if (File.Exists(project.projectFile)) throw new Exception("A project file already exists at the selected location.\n" + project.projectFile);
                //create directory if needed
                if (!Directory.Exists(project.path)) Directory.CreateDirectory(project.path);
                if (!Directory.Exists(project.resourcesFolder)) Directory.CreateDirectory(project.resourcesFolder);
                if (!Directory.Exists(project.siteResourcesFolder)) Directory.CreateDirectory(project.siteResourcesFolder);
                //copy site resources over
                foreach(string siteResource in project.projectionSite.projectResources)
                {
                    string siteFile = _rootPath + projectionSitesFolder + @"\" + project.projectionSite.folder + @"\" + siteResource;
                    var projectFile = project.siteResourcesFolder + @"\" + siteResource;
                    var directory = Path.GetDirectoryName(projectFile);
                    if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
                    if (File.Exists(siteFile)) File.Copy(siteFile, projectFile);
                }
                //add full project file to project folder
                var json = JsonUtility.ToJson(project);
                File.WriteAllText(project.projectFile, json);
                //add project reference
                this.projects.AddProject(project);
                SaveProjects(this.projects);
                currentProject = project;
                this.RebuildProjects();
                btnMyProjects.onClick.Invoke();

                ShowErrorMessage("Create Project: " + project.name);
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        public void RemoveProjectClick()
        {
            RemoveProject(currentProject, false);
        }

        public void RemoveProject(ProjectReference project, bool delete = false)
        {
            try
            {
                if (delete)
                {
                    //create directory if needed
                    if (!Directory.Exists(project.path)) Directory.Delete(project.path);
                }
                //add project reference
                this.projects.RemoveProject(project.id);
                SaveProjects(this.projects);
                this.RebuildProjects();
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
            ShowErrorMessage("Create Project: " + project.name);
        }

        public void LoadProject(ProjectReference project)
        {
            currentProject = project;
            //check if main project exists
            if (File.Exists(project.projectFile))
            {
                try
                {
                    string json = File.ReadAllText(currentProject.projectFile);
                    RebuildProjects();
                    btnMyProjects.onClick.Invoke();
                    homePanelManager.PanelAnim(1);
                    var p = JsonUtility.FromJson<Project>(json);
                    //load projection site
                    var site = this.projectionSites.sites.FirstOrDefault(f => f.id == p.projectionSite.id);
                    if (site != null) p.projectionSite = site;
                    projectManager.LoadProject(p);
                }
                catch (Exception ex)
                {
                    ShowErrorMessage("Error Loading Project: " + project.name + "\nPlease check the project.json file is correct", ex);
                }
            }
            else
            {
                ShowErrorMessage("Project File Doesn't Exist\n" + project.projectFile);
                objMessageModalRemoveButton.SetActive(true);
            }
        }
        

        public delegate void projectionSiteLoadedDelegate(ProjectionSites projectionSites);
        public event projectionSiteLoadedDelegate OnProjectionSitesLoaded;

        private ProjectionSites LoadProjectionSites()
        {
            try
            {
                if (File.Exists(_rootPath + projectionSitesFile))
                {
                    string json = File.ReadAllText(_rootPath + projectionSitesFile);
                    projectionSites = JsonUtility.FromJson<ProjectionSites>(json);
                }
                else
                {
                    SaveProjectionSites(projectionSites);
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
            RebuildProjectionSites();
            if (OnProjectionSitesLoaded != null) OnProjectionSitesLoaded(projectionSites);
            return projectionSites;
        }

        public void SaveProjectionSites(ProjectionSites projectionSites)
        {
            try
            {
                var json = JsonUtility.ToJson(projectionSites);
                File.WriteAllText(_rootPath + projectionSitesFile, json);
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void RebuildProjectionSites()
        {
            //clear the transform
            foreach (Transform child in objProjectionSiteList.transform) Destroy(child.gameObject);
            //add new ones in
            foreach (var projectionSite in projectionSites.sites.OrderBy(o => o.name).ThenByDescending(o => o.majorVersion))
            {
                if(latestProjectionSites != null && latestProjectionSites.sites != null && latestProjectionSites.sites.Count > 0)
                {
                    //look for match in projectionSites on version
                    ProjectionSite match = latestProjectionSites.sites.FirstOrDefault(c => c.versionId == projectionSite.versionId);
                    if (match != null)
                    {
                        projectionSite.status = ProjectionSiteStatus.UpToDate;
                    } else
                    {
                        match = latestProjectionSites.sites.OrderBy(o => o.minorVersion).FirstOrDefault(c => c.id == projectionSite.id);
                        projectionSite.status = match == null ? ProjectionSiteStatus.NotOnServer : match.minorVersion > projectionSite.minorVersion ? ProjectionSiteStatus.OutOfDate : ProjectionSiteStatus.Unknown;
                    }
                }
                var projectionSiteGameObject = Instantiate(prefabProjectListItem, objProjectionSiteList.transform);
                var btn = projectionSiteGameObject.GetComponent<ProjectButton>();
                btn.applicationManager = this;
                btn.SetProjectionSite(projectionSite, true, false);
            }
        }

        private ProjectionSite holdingProjectionSiteMinor;
        public void ShowProjectionSiteModal(ProjectionSite projectionSite, bool isLatestProjection)
        {
            holdingProjectionSiteMinor = projectionSite;
            ProjectionSiteDetailsManager man = objUpdateProjectionSiteMinorModal.GetComponent<ProjectionSiteDetailsManager>();
            if(isLatestProjection)
            {
                man.ShowFooterUpdateLocal(projectionSite.status == ProjectionSiteStatus.NewOnServer ? "Would you like to install the new projection site?" : "Would you like to update your local version settings with the latest configuration?");
            } else
            {
                man.ShowFooterInfoAndCreate();
            }
            man.SetData(holdingProjectionSiteMinor, this);
            btnUpdateProjectionSiteMinor.onClick.Invoke();
        }

        public void ProjectInfoModalRefreshClick()
        {
            DownloadLatestSites(true);
        }

        public void UpdateLocalProjectionSiteCancel()
        {
            holdingProjectionSiteMinor = null;
        }

        public void UpdateLocalProjectionSiteComplete()
        {
            try
            {
                string directoryPath = _rootPath + projectionSitesFolder + @"\" + holdingProjectionSiteMinor.folder;
                if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
                //download files
                if(holdingProjectionSiteMinor.resources.Count > 0)
                {
                    ShowLoading("Downloading projection site files");
                    StartCoroutine(DownloadSiteFile(this, 0));
                } else
                {
                    UpdateLocalProjectionSiteComplete_Finalise();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("There was an error in installing the projection site.", ex);
            }
        }

        public void UpdateLocalProjectionSiteComplete_Finalise()
        {
            try
            {
                if (holdingProjectionSiteMinor.status == ProjectionSiteStatus.NewOnServer)
                {
                    //for now just add the new site
                    projectionSites.AddSite(holdingProjectionSiteMinor);
                }
                else
                {
                    projectionSites.MinorVersionUpdate(holdingProjectionSiteMinor);
                }
                SaveProjectionSites(projectionSites);
                RebuildProjectionSites();
                RebuildLatestProjectionSites();
                btnInstalledSites.onClick.Invoke();
                RebuildProjectionSites();
            }
            catch (Exception ex)
            {
                ShowErrorMessage("There was an error in installing the projection site.", ex);
            }
        }

        IEnumerator DownloadSiteFile(ApplicationManager applicationManager, int index)
        {
            string file = null;
            string fileFullOnline = null;
            string fileFullLocal = _rootPath + projectionSitesFolder + @"\" + holdingProjectionSiteMinor.folder + @"\";
            try
            {
                if (index > applicationManager.holdingProjectionSiteMinor.resources.Count)
                {
                    //close the loading modal
                    CloseLoading();
                    applicationManager.UpdateLocalProjectionSiteComplete_Finalise();
                }
                else
                {
                    file = applicationManager.holdingProjectionSiteMinor.resources[index];
                    fileFullOnline = applicationManager.holdingProjectionSiteMinor.folder + @"\" + applicationManager.holdingProjectionSiteMinor.resources[index];
                    fileFullLocal += file;
                    UpdateLoading("Downloading projection site files: " + (index + 1).ToString() + " of " + applicationManager.holdingProjectionSiteMinor.resources.Count + "\n" + file);
                }
            }
            catch (Exception ex)
            {
                //close the loading modal
                CloseLoading();
                ShowErrorMessage("Unable to download site files", ex);
            }

            if (!String.IsNullOrEmpty(file))
            {
                UnityWebRequest www = UnityWebRequest.Get(OnlinePath + fileFullOnline);
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    CloseLoading();
                    ShowErrorMessage("Unable to download file, please check your internet connection.", new Exception(www.error));
                }
                else
                {
                    //check the directory exists
                    var directory = Path.GetDirectoryName(fileFullLocal);
                    if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
                    // binary data
                    byte[] results = www.downloadHandler.data;
                    File.WriteAllBytes(fileFullLocal, results);
                    //call the next one
                    index += 1;
                    if (index < applicationManager.holdingProjectionSiteMinor.resources.Count)
                    {
                        StartCoroutine(DownloadSiteFile(applicationManager, index + 1));
                    } else
                    {
                        //close the loading modal
                        CloseLoading();
                        applicationManager.UpdateLocalProjectionSiteComplete_Finalise();
                    }

                }
            }
        }

        public void CreateNewProjectionClick()
        {
            CreateProjectManager man = objCreateProjectModal.GetComponent<CreateProjectManager>();
            man.SetData(holdingProjectionSiteMinor, this);
            btnCreateProjectModal.onClick.Invoke();
        }

        public void CreateNewProjection(ProjectionSite projectionSite)
        {
            ShowErrorMessage("Create Project from Site: " + projectionSite.name);
        }

        private void RebuildLatestProjectionSites()
        {
            //clear the transform
            foreach (Transform child in objLatestProjectionSiteList.transform) Destroy(child.gameObject);
            //add new ones in
            foreach (var projectionSite in latestProjectionSites.sites)
            {
                //look for match in projectionSites on version
                ProjectionSite match = projectionSites.sites.FirstOrDefault(c => c.versionId == projectionSite.versionId);
                ProjectionSite siteMatch = projectionSites.sites.FirstOrDefault(c => c.id == projectionSite.id);
                projectionSite.status = match != null ? ProjectionSiteStatus.UpToDate : siteMatch != null ? ProjectionSiteStatus.OutOfDate : ProjectionSiteStatus.NewOnServer;
                //on id
                //if(match == null) match = projectionSites.sites.FirstOrDefault(c => c.id == projectionSite.id);
                var projectionSiteGameObject = Instantiate(prefabProjectListItem, objLatestProjectionSiteList.transform);
                var btn = projectionSiteGameObject.GetComponent<ProjectButton>();
                btn.applicationManager = this;
                btn.SetProjectionSite(projectionSite, true, true);
            }
        }

        public void LatestSitesClick()
        {
            DownloadLatestSites();
        } 


        public void DownloadLatestSites(bool rebuildInfoModal = false)
        {
            ShowLoading("Downloading latest site configurations");
            try
            {
                StartCoroutine(GetSiteConfigurations(rebuildInfoModal));
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Unable to download site configurations", ex);
            }
        }

        IEnumerator GetSiteConfigurations(bool rebuildInfoModal = false)
        {
            UnityWebRequest www = UnityWebRequest.Get(OnlinePath + "sites.json");
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                CloseLoading();
                ShowErrorMessage("Unable to download site configurations, please check your internet connection.", new Exception(www.error));
            }
            else
            {
                // Show results as text
                Debug.Log(www.downloadHandler.text);
                try
                {
                    latestProjectionSites = JsonUtility.FromJson<ProjectionSites>(www.downloadHandler.text);
                    RebuildLatestProjectionSites();
                    RebuildProjectionSites();
                    if (rebuildInfoModal) ShowProjectionSiteModal(holdingProjectionSiteMinor, false);
                    //close the loading modal
                    CloseLoading();
                }
                catch (Exception ex)
                {
                    //close the loading modal
                    CloseLoading();
                    ShowErrorMessage("Unable to download site configurations", ex);
                }
                
                // Or retrieve results as binary data
                byte[] results = www.downloadHandler.data;
            }
        }


        public void ShowErrorMessage(string message, Exception ex = null)
        {
            objMessageModalRemoveButton.SetActive(false);
            Debug.Log(message);
            if (ex != null) Debug.Log(ex);
            messageText.text = message;
            btnMessage.onClick.Invoke();
        }

        public void ShowLoading(string message)
        {
            loadingText.text = message;
            btnLoading.onClick.Invoke();
        }

        public void UpdateLoading(string message)
        {
            loadingText.text = message;
        }

        public void CloseLoading()
        {
            btnLoadingClose.onClick.Invoke();
        }

        private void Start()
        {
            projectManager.SetProjectHud();
            SetBackgroundImage();
        }

        // Update is called once per frame
        void Update()
        {
            if ( ((Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Q)) 
                || (Input.GetKey(KeyCode.RightShift) && Input.GetKey(KeyCode.Q)))
                && btnExit!= null)
            {
                btnExit.onClick.Invoke();
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                AudioListener.volume = (AudioListener.volume == 0) ? 0.5f : 0;
            }

            if (Input.GetKeyDown(KeyCode.F12))
            {
                foreach(var d in DisplayNumbers)
                {
                    d.SetActive(!d.activeSelf);
                }
                var index = 0;
                foreach (var r in ResNumbers)
                {
                    r.SetActive(!r.activeSelf);
                    try
                    {
                        var t = r.GetComponent<TextMeshProUGUI>();
                        t.text = Display.displays[index].renderingWidth.ToString() + "," + Display.displays[index].renderingHeight.ToString();
                    }
                    catch
                    {

                    }
                    index += 1;
                }
                
            }
            //if (Input.GetKeyUp(KeyCode.F1))
            //{
            //    showCanvas = !showCanvas;
            //    objCanvas.SetActive(showCanvas);
            //}
        }

        //public void SetLoadedProjectHud()
        //{
        //    objRightPanel.SetActive(currentProject != null);
        //    objRightPanelHolding.SetActive(currentProject == null);
        //    objRightPanel.SetActive(currentProject != null);
        //    objProject.SetActive(currentProject != null);
        //    if (currentProject != null)
        //    {
        //        HideBackground();
        //    }
        //}

        //public void HideBackground()
        //{
        //    objBackground.SetActive(false);
        //}

        public void SetBackgroundImage()
        {
            objBackground.SetActive(true);
            int index = UnityEngine.Random.Range(0, backgroundImages.Length);
            objBackgroundImage.sprite = backgroundImages[index];
        }
    }

}
