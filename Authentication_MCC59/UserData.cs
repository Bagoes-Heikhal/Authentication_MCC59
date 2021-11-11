using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication_MCC59
{
    class UserData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Id { get; set; }
        public string Password { get; set; }

        public UserData(string firstName, string lastName, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            Id = firstName.Substring(0, 2) + lastName.Substring(0, 2);
        }

        //public static string StringConstrain()
        //{
        //    string name = "None";

        //    string temp = Console.ReadLine();
        //    if (temp.Length >= 2)
        //    {
        //        name = temp;   
        //    }
        //    else
        //    {
        //        Console.WriteLine("Pla");
        //    }


        //    return name;
        //}
            

    }
}
