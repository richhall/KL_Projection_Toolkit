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
        private string serverName;

        void Start()
        {
        }

        public void SetData(string appName, string serverName, Sprite background, SyphonManager syphonManager)
        {
            this.name = appName;
            this.serverName = serverName;
            this.txtName.text = appName;
            ChangeBackground(background);
            this.syphonManager = syphonManager;
        }

        public void ChangeBackground(Sprite background)
        {
            this.imgBackground.sprite = background;
        }

        public void Click()
        {
            syphonManager.SetSyphon(Tuple.Create(this.txtName.text,this.serverName));
        }
    }
}