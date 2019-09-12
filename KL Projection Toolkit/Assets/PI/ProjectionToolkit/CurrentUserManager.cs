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
        public ProjectManager projectManager;
        public TMP_InputField currentUserName;
        public TMP_InputField currentUserEmail;
        public TMP_InputField currentUserOrganisation;

        public void UpdateCurrentUser()
        {
            if(projectManager != null)
            {
                projectManager.users.AddOrUpdateUser(new Common.Models.User()
                {
                    name = currentUserName.text,
                    email = currentUserEmail.text,
                    organisation = currentUserOrganisation.text
                }, projectManager.users.current.email, true);
                projectManager.SaveUsers();
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            if (projectManager != null)
            {
                projectManager.OnUsersLoaded += ProjectManager_OnUsersLoaded;
                if (projectManager.users != null) ProjectManager_OnUsersLoaded(projectManager.users);
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
