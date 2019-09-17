using UnityEngine;
using UnityEngine.EventSystems;
using PI.ProjectionToolkit.Models;
using TMPro;
using PI.ProjectionToolkit.UI;

namespace PI.ProjectionToolkit
{
    public class SettingsDetailsManager : UiDetailsBase
    {
        public UnityEngine.UI.Scrollbar scrollbarAbout;
        public Sprite imgWebsiteLink;

        void Start()
        {
            SetAboutData();
        }


        public void SetAboutData()
        {
            scrollbarAbout.value = 1; //set scrollbar to top

            //clear the transform
            foreach (Transform child in objList.transform) Destroy(child.gameObject);
            //build listing
            AddHeader("SOFTWARE");
            AddTextLine("NAME", "PROJECTION TOOLKIT");
            AddTextLine("VERSION", Application.version);
            AddTextLine("CREATED BY", "PETE CLEARY");
            AddTextLine("MODELS BY", "RICH HALL");
            TextLineButton btnSite = AddTextLineButton("SUPORT SITE", "www.piandmash.com", imgWebsiteLink);
            btnSite.OnButtonClick += BtnSite_OnButtonClick;
            AddSeperator();

            AddHeader("FUNDED BY");
            TextLineButton btnSitePi = AddTextLineButton("", "PI & MASH", imgWebsiteLink);
            btnSitePi.OnButtonClick += btnSitePi_OnButtonClick;
            TextLineButton btnCollusion = AddTextLineButton("", "COLLUSION", imgWebsiteLink);
            btnCollusion.OnButtonClick += btnCollusion_OnButtonClick;
            TextLineButton btnSiteArts = AddTextLineButton("", "ARTS COUNCIL ENGLAND", imgWebsiteLink);
            btnSiteArts.OnButtonClick += btnSiteArts_OnButtonClick;
            TextLineButton BtnCouncil = AddTextLineButton("", "KINGS LYNN & WEST NORFOLK BOROUGH COUNCIL", imgWebsiteLink);
            BtnCouncil.OnButtonClick += BtnCouncil_OnButtonClick;
            AddSeperator();
            string licenses = "MIT License";
            licenses += "\nCopyright(c) 2019, Pete Cleary, Rich Hall, Pi & Mash, Collusion";
            licenses += "\n";
            licenses += "\n";
            licenses += @"Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files(the ""Software""), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/ or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:";
            licenses += "\nThe above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.";
            licenses += "\n";
            licenses += "\n";
            licenses += @"THE SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.";
            licenses += "\n\nSome icons were generated from FontAwesome under the license agreement at https://fontawesome.com/license";
            AddTextLine("LICENSES", licenses, true);


        }

        private void BtnSite_OnButtonClick()
        {
            Application.OpenURL("https://www.piandmash.com");
        }

        private void btnSitePi_OnButtonClick()
        {
            Application.OpenURL("https://www.piandmash.com");
        }

        private void btnCollusion_OnButtonClick()
        {
            Application.OpenURL("http://www.collusion.org.uk/");
        }

        private void btnSiteArts_OnButtonClick()
        {
            Application.OpenURL("https://www.artscouncil.org.uk");
        }

        private void BtnCouncil_OnButtonClick()
        {
            Application.OpenURL("https://www.west-norfolk.gov.uk/");
        }
    }
}