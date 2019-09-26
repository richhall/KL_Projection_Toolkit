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

        public void AlignValue(TextAlignmentOptions textAlignmentOptions = TextAlignmentOptions.TopRight)
        {
            if (value != null)
            {
                this.value.alignment = textAlignmentOptions;
            }
        }

        public void SetData(string title, string value, int height = 0)
        {
            this.title.text = title;
            if (value != null) this.value.text = value;
            if (input != null) this.input.text = value;
            if(height > 0)
            {
                var rect = this.GetComponent<RectTransform>();
                if (rect != null) rect.sizeDelta = new Vector2(rect.sizeDelta.x, height);
            }
        }

        public void SetData(string title, string value, Color colorAltert, int height = 0)
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
            if (height > 0)
            {
                var rect = this.GetComponent<RectTransform>();
                if (rect != null) rect.sizeDelta.Set(rect.sizeDelta.x, height);
            }
        }

        public bool InputValidationRequired(string val)
        {
            inputBackground.color = string.IsNullOrEmpty(this.input.text) ? colorAltert : initBackgroundColor;
            return !string.IsNullOrEmpty(this.input.text);
        }
    }
}