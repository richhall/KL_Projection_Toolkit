using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace PI.ProjectionToolkit.UI
{
    public class SyphonItem : MonoBehaviour
    {
        public TextMeshProUGUI txtName;
        public Image imgBackground;
        private PI.ProjectionToolkit.SyphonManager syphonManager;


        void Start()
        {
        }

        public void SetData(string name, Sprite background, SyphonManager syphonManager)
        {
            this.name = name;
            this.txtName.text = name;
            ChangeBackground(background);
            this.syphonManager = syphonManager;
            this.txtName.enabled = true;
        }

        public void ChangeBackground(Sprite background)
        {
            this.imgBackground.sprite = background;
        }

        public void Click()
        {
            syphonManager.SetSyphon(this.txtName.text);
        }
    }
}