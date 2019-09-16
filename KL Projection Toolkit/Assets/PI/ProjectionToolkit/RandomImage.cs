using UnityEngine;
using UnityEngine.UI;

namespace PI.ProjectionToolkit
{
    public class RandomImage : MonoBehaviour
    {
        
        public Sprite[] images;
        public Image objImage;

        void Start()
        {
            SetImage();
        }

        public void SetImage()
        {
            int index = Random.Range(0, images.Length);
            objImage.sprite = images[index];
        }
    }
}