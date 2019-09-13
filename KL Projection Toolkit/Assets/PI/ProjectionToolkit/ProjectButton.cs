using UnityEngine;
using UnityEngine.EventSystems;
using PI.ProjectionToolkit.Models;
using TMPro;

namespace PI.ProjectionToolkit
{
    public class ProjectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public bool showCheckBox = false;
        public Sprite imgCheckBoxIconUpToDate;
        public Sprite imgCheckBoxIconOutOfDate;
        public Sprite imgCheckBoxIconNotOnServer;
        public Sprite imgCheckBoxIconUnknown;
        public Sprite imgCheckBoxIconNewOnServer;
        public GameObject objCheckBox;
        public GameObject objCheckBoxFill;
        public GameObject objCheckBoxIcon;
        private Color colorWhite = new Color(1, 1, 1);
        private Color colorBlack = new Color(0, 0, 0);
        public Color colorAlert;
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
            objCheckBox.SetActive(showCheckBox);
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

        public void SetProjectionSite(ProjectionSite projectionSite, bool showCheckBox = false, bool isLatestProjection = false)
        {
            this.isLatestProjection = isLatestProjection;
            isProject = false;
            this.projectionSite = projectionSite;
            this.showCheckBox = showCheckBox;
            this.isOn = projectionSite.status == ProjectionSiteStatus.UpToDate || projectionSite.status == ProjectionSiteStatus.OutOfDate;
            var imgIcon = objCheckBoxIcon.GetComponent<UnityEngine.UI.Image>();
            var img = objCheckBox.GetComponent<UnityEngine.UI.Image>();
            var imgFill = objCheckBoxFill.GetComponent<UnityEngine.UI.Image>();
            switch (projectionSite.status)
            {
                case ProjectionSiteStatus.UpToDate:
                    imgIcon.sprite = imgCheckBoxIconUpToDate;
                    break;
                case ProjectionSiteStatus.OutOfDate:
                    imgIcon.sprite = imgCheckBoxIconOutOfDate;
                    break;
                case ProjectionSiteStatus.NotOnServer:
                    imgIcon.sprite = imgCheckBoxIconNotOnServer;
                    break;
                case ProjectionSiteStatus.Unknown:
                    imgIcon.sprite = imgCheckBoxIconUnknown;
                    break;
                case ProjectionSiteStatus.NewOnServer:
                    imgIcon.sprite = imgCheckBoxIconNewOnServer;
                    break;
            }
            //imgIcon.color = isOn ? colorBlack : colorWhite;
            //set colours
            img.color = projectionSite.status == ProjectionSiteStatus.OutOfDate || projectionSite.status == ProjectionSiteStatus.NotOnServer ? colorAlert : colorWhite;
            imgFill.color = new Color(img.color.r, img.color.g, img.color.b, 0.75f);
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
                    projectManager.UpdateLocalProjectionSiteLaunch(projectionSite);
                    //if (this.isOn)
                    //{
                    //    projectManager.ShowErrorMessage("Your locally installed site is up to date");
                    //}
                    //else
                    //{
                    //}
                } else
                {
                    projectManager.CreateNewProjection(projectionSite);
                }
            }
        }
    }
}