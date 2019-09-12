using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PI.Common.Models;

namespace PI.ProjectionToolkit.Models
{
    /// <summary>
    /// A list of all the local users on the machine
    /// </summary>
    [Serializable]
    public class Users
    {
        public User current = new User();
        public List<User> users = new List<User>();

        public void AddOrUpdateUser(User user, string oldEmail = "", bool setCurrent = false)
        {
            if (String.IsNullOrEmpty(oldEmail)) oldEmail = user.email;
            var u = users.FirstOrDefault(f => f.email == oldEmail.ToLower());
            if (u == null)
            {
                users.Add(user);
            }
            else
            {
                u.name = user.name;
                u.email = user.email;
                u.organisation = user.organisation;
            }
            if (setCurrent)
            {
                current = u == null ? user : u;
            }
        }
        
        public void RemoveUser(User user)
        {
            RemoveUser(user.email);
        }

        public void RemoveUser(string email)
        {
            if (current != null && current.email == email)
            {
                throw new Exception("You cannot remove the current user");
            }
            var u = users.FirstOrDefault(f => f.email == email);
            if (u == null)
            {
                throw new Exception("There is no user with an email of " + email + " stored");
            }
            else
            {
                users = users.Where(s => s.email != email).ToList();
            }
        }
    }
}
