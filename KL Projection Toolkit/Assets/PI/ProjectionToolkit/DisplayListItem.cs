using UnityEngine;
using UnityEngine.EventSystems;
using PI.ProjectionToolkit.Models;
using TMPro;
using PI.ProjectionToolkit.UI;
using System.Collections;
using System.Linq;

namespace PI.ProjectionToolkit
{
    public class DisplayListItem : MonoBehaviour
    {
        public int index = 0;
        public int displayIndex = 0;
        public TextMeshProUGUI txtName;
        public TextMeshProUGUI txtType;
        public UnityEngine.UI.Image imgIcon;
        public UnityEngine.UI.Image imgBackground;

        public delegate void displayListItemDelegate(DisplayListItem displayListItem);
        public event displayListItemDelegate OnProjectorClick;
        public event displayListItemDelegate OnVideoClick;
        public event displayListItemDelegate OnImageClick;

        void Start()
        {
        }

        public void SetData(string name, int index)
        {
            txtName.text = name;
            this.index = index;
            this.displayIndex = index - 1;
        }


        public void ProjectorClick()
        {
            if (OnProjectorClick != null) OnProjectorClick(this);
        }

        public void BackgroundClick()
        {
            if (OnImageClick != null) OnImageClick(this);
            //StartCoroutine(ShowLoadProjectPathDialogCoroutine());
        }


        public void VideoClick()
        {
            if (OnVideoClick != null) OnVideoClick(this);
        }
        
    }
}