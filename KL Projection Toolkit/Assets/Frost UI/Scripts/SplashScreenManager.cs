using UnityEngine;

namespace Michsky.UI.Frost
{
    public class SplashScreenManager : MonoBehaviour
    {
        [Header("RESOURCES")]
        public GameObject splashScreen;
        public GameObject mainPanels;
        public Animator backgroundAnimator;
        public Animator startFadeIn;
        private Animator mainPanelsAnimator;
        private Animator splashScreenAnimator;

        [Header("SETTINGS")]
        public bool disableSplashScreen;
        public bool enableLoginScreen = true;

        private bool show = true;

        void Start()
        {
            mainPanelsAnimator = mainPanels.GetComponent<Animator>();
            splashScreenAnimator = splashScreen.GetComponent<Animator>();

            if (disableSplashScreen == true)
            {
                splashScreen.SetActive(false);
                mainPanels.SetActive(true);
                mainPanelsAnimator.Play("Panel Start");
                backgroundAnimator.Play("Switch");
            }

            else
            {
                splashScreen.SetActive(true);
                mainPanelsAnimator.Play("Wait");
                startFadeIn.Play("Start with Splash");
            }

            if (enableLoginScreen == false && disableSplashScreen == false)
            {
                splashScreenAnimator.Play("Loading");
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyUp(KeyCode.F1))
            {
                show = !show;
                if (show)
                {
                    mainPanelsAnimator.Play("Panel Start");
                }
                else
                {
                    mainPanelsAnimator.Play("Panel Out");
                }
            }
        }
    }
}