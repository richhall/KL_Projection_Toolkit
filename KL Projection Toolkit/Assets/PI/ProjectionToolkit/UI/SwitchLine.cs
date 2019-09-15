using UnityEngine;
using UnityEngine.EventSystems;
using PI.ProjectionToolkit.Models;
using TMPro;
using Michsky.UI.Frost;

namespace PI.ProjectionToolkit.UI
{
    public class SwitchLine : MonoBehaviour
    {
        public TextMeshProUGUI title;
        public bool value = false;
        public SwitchManager switchManager;

        void Start()
        {
        }

        public void SetData(string title, bool value)
        {
            this.title.text = title;
            switchManager.isOn = value;
        }

        public bool isOn
        {
            get { return switchManager.isOn;  }
            set { switchManager.isOn = value; }
        }
    }
}