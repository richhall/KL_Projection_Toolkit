using UnityEngine;
using UnityEngine.EventSystems;
using PI.ProjectionToolkit.Models;
using TMPro;

namespace PI.ProjectionToolkit.UI
{
    public class Header : MonoBehaviour
    {
        public TextMeshProUGUI header;

        void Start()
        {
        }

        public void SetData(string header)
        {
            this.header.text = header;
        }
    }
}