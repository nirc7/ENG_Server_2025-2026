using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WindowsFormsApp1
{
    internal static class BLLServices
    {
        public static int InsertUser(string name, string family)
        {
            //if role is admin? 
            User user = DALServices.InsertUser(name, family);
            if (user != null)
                return user.Id;
            else
                return -1;
        }

        public static int UpdateUser(int id, string name, string family)
        {
            User user;
            //is permitted to do update?
            try
            {
                user = new User(id, name, family);
            }
            catch (Exception e)
            {
                return 0;
            }
            int res = DALServices.UpdateUser(user);
            return res;
        }

        internal static int DeleteUser(int id)
        {
            //is permitted to do update?
            int res = DALServices.DeleteUser(id);
            return res;
        }
    }
}
