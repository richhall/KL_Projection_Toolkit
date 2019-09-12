using UnityEngine;
using UnityEngine.EventSystems;
using PI.ProjectionToolkit.Models;
using TMPro;

namespace PI.ProjectionToolkit.UI
{
    public class TextLineButton : MonoBehaviour
    {
        public TextMeshProUGUI title;
        public TextMeshProUGUI value;
        public Sprite defaultIcon;
        public GameObject objIcon;

        void Start()
        {
        }

        public void SetData(string title, string value, Sprite icon = null)
        {
            this.title.text = title;
            this.value.text = value;
            var img = objIcon.GetComponent<UnityEngine.UI.Image>();
            img.sprite = icon != null ? icon : defaultIcon;
        }

        public delegate void buttonClickDelegate();
        public event buttonClickDelegate OnButtonClick;

        public void ButtonClicked()
        {
            if (OnButtonClick != null) OnButtonClick();
        }
    }
}