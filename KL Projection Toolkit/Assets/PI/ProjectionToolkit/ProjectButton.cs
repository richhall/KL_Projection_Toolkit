using UnityEngine;
using UnityEngine.EventSystems;
using PI.ProjectionToolkit.Models;
using TMPro;

namespace PI.ProjectionToolkit
{
    public class ProjectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public bool showCheckBox = false;
        public GameObject checkBox;
        public bool isOn;
        private Animator buttonAnimator;
        bool isHovering;

        public ProjectionSite projectionSite;
        public ProjectReference projectReference;
        public TextMeshProUGUI title;
        public TextMeshProUGUI date;
        public ProjectManager projectManager;
        private bool isProject = true;
        private bool isLatestProjection = false;

        void Start()
        {
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            isHovering = true;

            if (isOn == true)
            {
                buttonAnimator.Play("Pressed to Hover");
            }

            else
            {
                buttonAnimator.Play("Hover");
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isHovering = false;

            if (isOn == true)
            {
                buttonAnimator.Play("Pt Hover to Pt");
            }

            else
            {
                buttonAnimator.Play("Hover to Normal");
            }
        }

        public void Animate()
        {
            if (isOn == true)
            {
                buttonAnimator.Play("Normal");
                isOn = false;
            }

            else if (isOn == false && isHovering == true)
            {
                buttonAnimator.Play("Hover to Pressed");
                isOn = true;
            }

            else if (isOn == false && isHovering == false)
            {
                buttonAnimator.Play("Normal to Pressed");
                isOn = true;
            }
        }

        public void CheckStart()
        {
            buttonAnimator = this.GetComponent<Animator>();
            checkBox.SetActive(showCheckBox);
            if (isOn == true)
            {
                buttonAnimator.Play("Normal to Pressed");
            }

            else
            {
                buttonAnimator.Play("Normal");
            }
        }

        public void SetProjectReference(ProjectReference project)
        {
            isProject = true;
            projectReference = project;
            title.text = project.name;
            date.text = project.updated.ToString("yyyy-MM-dd HH:mm");
            CheckStart();
        }

        public void SetProjectionSite(ProjectionSite projectionSite, ProjectionSite match, bool isLatestProjection = false)
        {
            this.isLatestProjection = isLatestProjection;
            isProject = false;
            this.projectionSite = projectionSite;
            this.showCheckBox = match != null;
            this.isOn = match != null && projectionSite.versionId == match.versionId;
            title.text = this.projectionSite.name;
            date.text = this.projectionSite.version;
            CheckStart();
        }

        public void Click()
        {
            if (isProject)
            {
                projectManager.LoadProject(projectReference);
            }
            else
            {
                if (isLatestProjection)
                {
                    if (this.isOn)
                    {
                        projectManager.ShowErrorMessage("Your locally installed site is up to date");
                    }
                    else
                    {
                        projectManager.UpdateLocalProjectionSiteLaunch(projectionSite);
                    }
                } else
                {
                    projectManager.CreateNewProjection(projectionSite);
                }
            }
        }
    }
}