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

        void Start()
        {
        }

        public void SetData(string title, string value)
        {
            this.title.text = title;
            this.value.text = value;
        }
    }
}