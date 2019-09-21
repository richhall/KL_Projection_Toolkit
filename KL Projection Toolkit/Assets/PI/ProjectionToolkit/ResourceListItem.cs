using UnityEngine;
using UnityEngine.EventSystems;
using PI.ProjectionToolkit.Models;
using TMPro;
using PI.ProjectionToolkit.UI;
using System.Collections;
using System.Linq;

namespace PI.ProjectionToolkit
{
    public class ResourceListItem : MonoBehaviour
    {
        public TextMeshProUGUI txtName;
        public TextMeshProUGUI txtType;
        public UnityEngine.UI.Image imgIcon;
        public UnityEngine.UI.Image imgBackground;
        
        public Sprite[] sprites;

        public delegate void resourceListItemDelegate(ResourceListItem displayListItem);
        //public event displayListItemDelegate OnProjectorClick;
        //public event displayListItemDelegate OnVideoClick;
        //public event displayListItemDelegate OnImageClick;

        void Start()
        {
        }

        public void SetData(string name, string extensionType)
        {
            txtName.text = name;
            switch (extensionType.ToLower())
            {
                case ".jpg":
                    imgIcon.sprite = sprites[0];
                    txtType.text = "Image (jpg)";
                    break;
                case ".png":
                    imgIcon.sprite = sprites[0];
                    txtType.text = "Image (png)";
                    break;
                case ".mp4":
                    imgIcon.sprite = sprites[1];
                    txtType.text = "Video (mp4)";
                    break;
                case ".mov":
                    imgIcon.sprite = sprites[1];
                    txtType.text = "Video (mov)";
                    break;
                case ".spout":
                    imgIcon.sprite = sprites[2];
                    txtType.text = "Spout Feed";
                    break;
                case ".syphon":
                    imgIcon.sprite = sprites[2];
                    txtType.text = "Syphon Feed";
                    break;
            }
        }


        //public void ProjectorClick()
        //{
        //    if (OnProjectorClick != null) OnProjectorClick(this);
        //}

        //public void BackgroundClick()
        //{
        //    if (OnImageClick != null) OnImageClick(this);
        //    //StartCoroutine(ShowLoadProjectPathDialogCoroutine());
        //}


        //public void VideoClick()
        //{
        //    if (OnVideoClick != null) OnVideoClick(this);
        //}
        
    }
}