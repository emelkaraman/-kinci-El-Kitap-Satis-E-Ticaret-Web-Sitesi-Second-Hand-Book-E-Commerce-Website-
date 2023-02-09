using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using İkinciElKitapSatisVol1.Models;

namespace İkinciElKitapSatisVol1.Security
{
    public class Roles : RoleProvider
    {
        public override string ApplicationName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string userFullName)
        {
            string[] roller = null;
            
            İkinciElKitapSatisEntities1 k = new İkinciElKitapSatisEntities1();
            Users user = k.Users.FirstOrDefault(x => x.UserFirstName + x.UserLastName == userFullName);
            List<KisiyeGoreTurListesiView> kisiyeGoreTurListesis = k.KisiyeGoreTurListesiView.Where(x => x.UserID == user.UserID).ToList();

            roller = new string[kisiyeGoreTurListesis.ToArray().Length];
            for (int i = 0; i < kisiyeGoreTurListesis.ToArray().Length; i++)
            {
                roller[i] = kisiyeGoreTurListesis[i].TurID.ToString();
            }
            
            return roller;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}