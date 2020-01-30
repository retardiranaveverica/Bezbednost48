using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public class Formatter
    {
        public static string ParseName(string winLogOnName)
        {
            string[] parts = new string[] { };
            if(winLogOnName.Contains("@"))
            {
                parts = winLogOnName.Split('@');
                return parts[0];
            }
            else if(winLogOnName.Contains("\\"))
            {
                parts = winLogOnName.Split('\\');
                return parts[1];
            }
            else
            {
                return winLogOnName;
            }

        }
    }
}
