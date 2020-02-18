﻿using UnityEngine;
using UnityEngine.EventSystems;
using PI.ProjectionToolkit.Models;
using TMPro;
using PI.ProjectionToolkit.UI;
using System.Collections;
using System.Linq;

namespace PI.ProjectionToolkit
{
    public class ModelListItem : MonoBehaviour
    {
        public int index = 0;
        public int displayIndex = 0;
        public TextMeshProUGUI txtName;
        public TextMeshProUGUI spoutSyphonButton;
        public TextMeshProUGUI txtType;
        public UnityEngine.UI.Image imgIcon;
        public UnityEngine.UI.Image imgBackground;
        public GameObject objModel;
        public ModelItem modelItem;
        public string targetMaterialProperty;

        public Sprite[] sprites;

        public delegate void modelListItemDelegate(ModelListItem listItem);
        public event modelListItemDelegate OnMaterialClick;
        public event modelListItemDelegate OnVideoClick;
        public event modelListItemDelegate OnSpoutClick;
        public event modelListItemDelegate OnSyphonClick;

        void Active()
        {
            SetBackground();
        }

        public void SetData(string name, bool interactive, string type, GameObject model, string targetMaterial, GameObject prefabSpoutReceiver)
        {
            txtName.text = name;
            txtType.text = name;
            objModel = model;
            targetMaterialProperty = targetMaterial;
            SetBackground();
            if (objModel != null) modelItem = objModel.GetComponent<ModelItem>();
            if (modelItem != null) modelItem.prefabSyphonReceiver = prefabSpoutReceiver;
            if (interactive)
            {
#if UNITY_STANDALONE_OSX
                spoutSyphonButton.text = "APPLY SYPHON";
#endif
#if UNITY_STANDALONE_WIN
                spoutSyphonButton.text = "APPLY SPOUT";
#endif
            }

        }

        public void MaterialClick()
        {
            if (OnMaterialClick != null) OnMaterialClick(this);
        }

        public void VideoClick()
        {
            if (OnVideoClick != null) OnVideoClick(this);
        }


        public void SpoutClick()
        {
            if (OnSpoutClick != null) OnSpoutClick(this);
        }

        public void SyphonClick()
        {
            if (OnSyphonClick != null) OnSyphonClick(this);
        }


        public void ShowHideModel()
        {
            if (objModel != null)
            {
                objModel.SetActive(!objModel.activeSelf);
                SetBackground();
            }
        }

        private void SetBackground()
        {
            if (objModel != null)
            {
                imgBackground.sprite = objModel.activeSelf ? sprites[0] : sprites[1];
            }
        }
    }
}