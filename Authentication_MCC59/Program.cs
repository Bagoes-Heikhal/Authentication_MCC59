using System;
using System.Collections.Generic;

namespace Authentication_MCC59
{
    class Program
    {
        public static List<UserData> types = new();
        public static Dictionary<string, string> confidential = new();

        static void Main(string[] args)
        {            
            bool start = true;
            while(start)
            {
                Menu();
                int choose = NumInput();
                switch (choose)
                {
                    case 1:
                        InputData();
                        break;
                    case 2:
                        ShowUserData();
                        break;
                    case 3:
                        Login();
                        break;
                    case 4:
                        start = false;
                        break;
                    default:
                        break;
                }
            }
        }

        public static void InputData()
        {
            Console.Write("Input first name : ");
            string firstName = Console.ReadLine();
            Console.Write("Input last name  : ");
            string lastName = Console.ReadLine();
            Console.Write("Input Paswword   : ");
            string password = Console.ReadLine();

            UserData user = new UserData(firstName, lastName, password);
            confidential.Add(user.Id, user.Password);
            types.Add(user);
        }

        public static void ShowUserData()
        {
            foreach (var item in types)
            {
                Console.WriteLine($"Full Name : {item.FirstName} {item.LastName}");
                Console.WriteLine($"Password : {item.Password}");
            }
        }
        public static void Menu()
        {
            Console.WriteLine("1. Input");
            Console.WriteLine("2. Show Data");
            Console.WriteLine("3. Login");
            Console.WriteLine("4. =========");
        }
        public static int NumInput()
        {
            int a = 0;
            try
            {
                a = int.Parse(Console.ReadLine());
            }
            catch (FormatException e)
            {
                Console.WriteLine("Please Enter a number !! " + e);
            }
            return a;
        }
        public static void Login()
        {
            Console.WriteLine("Input Id : ");
            string id = Console.ReadLine();
            Console.WriteLine("Input Password : ");
            string pass = Console.ReadLine();

            Console.WriteLine(confidential[id]);
            Console.WriteLine(confidential[pass]);

            //if (confidential[id] == id && confidential.Contains == pass)
            //{

            //}

        }
    }
}
