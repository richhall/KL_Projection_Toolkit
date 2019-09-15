using UnityEngine;
using UnityEngine.EventSystems;
using PI.ProjectionToolkit.Models;
using TMPro;

namespace PI.ProjectionToolkit.UI
{
    public class TextLine : MonoBehaviour
    {
        public TextMeshProUGUI title;
        public TextMeshProUGUI value;
        public TMP_InputField input;
        public UnityEngine.UI.Image inputBackground;
        public Color initBackgroundColor;
        public Color colorAltert;

        void Start()
        {
        }

        public void SetData(string title, string value)
        {
            this.title.text = title;
            if (value != null) this.value.text = value;
            if (input != null) this.input.text = value;
        }

        public void SetData(string title, string value, Color colorAltert)
        {
            this.title.text = title;
            if (value != null) this.value.text = value;
            if (input != null) this.input.text = value;
            this.value.color = colorAltert;
            this.colorAltert = colorAltert;
            if (inputBackground != null)
            {
                inputBackground.color = colorAltert;
            }
        }

        public bool InputValidationRequired(string val)
        {
            inputBackground.color = string.IsNullOrEmpty(this.input.text) ? colorAltert : initBackgroundColor;
            return !string.IsNullOrEmpty(this.input.text);
        }
    }
}