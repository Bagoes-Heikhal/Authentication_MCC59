using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication_MCC59
{
    class DataUsers
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }

        public DataUsers(string FName, string LName, string Pass, string UName)
        {
            FirstName = FName;
            LastName = LName;
            Password = Pass;
            UserName = UName;
        }
    }
}
