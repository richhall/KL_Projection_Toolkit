#if UNITY_STANDALONE_WIN
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PI.ProjectionToolkit.UI
{
    public class SpoutItem : MonoBehaviour
    {
        public TextMeshProUGUI txtName;
        public Image imgBackground;
        private PI.ProjectionToolkit.SpoutManager spoutManager;

        void Start()
        {
        }

        public void SetData(string name, Sprite background, SpoutManager spoutManager)
        {
            this.name = name;
            this.txtName.text = name;
            ChangeBackground(background);
            this.spoutManager = spoutManager;
        }

        public void ChangeBackground(Sprite background)
        {
            this.imgBackground.sprite = background;
        }

        public void Click()
        {
            spoutManager.SetSpout(this.txtName.text);
        }
    }
}
#endif