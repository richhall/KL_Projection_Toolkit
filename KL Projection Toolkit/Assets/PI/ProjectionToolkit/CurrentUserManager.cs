using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace PI.ProjectionToolkit
{ 
    /// <summary>
    /// Manages the update/creation of a current user
    /// </summary>
    public class CurrentUserManager : MonoBehaviour
    {
        public ApplicationManager applicationManager;
        public TMP_InputField currentUserName;
        public TMP_InputField currentUserEmail;
        public TMP_InputField currentUserOrganisation;

        public void UpdateCurrentUser()
        {
            if(applicationManager != null)
            {
                applicationManager.users.AddOrUpdateUser(new Common.Models.User()
                {
                    name = currentUserName.text,
                    email = currentUserEmail.text,
                    organisation = currentUserOrganisation.text
                }, applicationManager.users.current.email, true);
                applicationManager.SaveUsers();
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            if (applicationManager != null)
            {
                applicationManager.OnUsersLoaded += ProjectManager_OnUsersLoaded;
                if (applicationManager.users != null) ProjectManager_OnUsersLoaded(applicationManager.users);
            }
        }

        private void ProjectManager_OnUsersLoaded(Models.Users users)
        {
            currentUserName.text = users.current.name;
            currentUserEmail.text = users.current.email;
            currentUserOrganisation.text = users.current.organisation;
        }



        // Update is called once per frame
        void Update()
        {

        }
    }
}
