using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public class CustomPrincipal : IPrincipal
    {
        WindowsIdentity identity = null;

        public CustomPrincipal(WindowsIdentity windowsIdentity)
        {
            identity = windowsIdentity;
        }

        public IIdentity Identity
        {
            get { return identity; }
        }

        public bool IsInRole(string permission)
        {

            foreach (IdentityReference group in this.identity.Groups)
            {
                //dobijamo naziv grupe
                SecurityIdentifier sid = (SecurityIdentifier)group.Translate(typeof(SecurityIdentifier));

                var name = sid.Translate(typeof(NTAccount));
                //proveravamo ime grupe preko klase Formatter
                string groupName = Formatter.ParseName(name.ToString());

                //ovo nam vraca funcija iz RoleConfig klase
                string[] permissions;


                //proverava da li odredjena grupa ima permisiju za tu funkciju
                if (RoleConfig.GetPermissions(groupName, out permissions))
                {

                    foreach (string permision in permissions)
                    {
                        if (permision.Equals(permission))
                            return true;
                    }
                }

            }


            return false;
        }
    }
}
