using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public class RoleConfig
    {
        public static bool GetPermissions(string rolename, out string[] permissions)
        {
            //string za permisiju
            //mozda treba i duzi, kod njih stoji 10
            permissions = new string[10];

            string permissionString = string.Empty;

            //iscitava iz .resx fajla name
            permissionString = (string)RoleConfigFile.ResourceManager.GetObject(rolename);

            if (permissionString != null)
            {
                //u .resx su dozvoljenje funkcije odvojene zarezom 
                //pa se zato splituje po zarezu
                permissions = permissionString.Split(',');
                return true;
            }

            return false;
        }

    }
}
