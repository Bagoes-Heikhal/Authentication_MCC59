using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Authentication_MCC59
{
    class Program
    {
        public static List<UserData> types = new();
        public static Dictionary<string, string> confidential = new();

        static void Main(string[] args)
        {            
            bool start = true;
            confidential.Add("bagoes", "123");
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
                        Search();
                        break;
                    case 4:
                        Login();
                        break;
                    case 5:
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
            bool start = true;

            Console.Write("Input Password   : ");
            string passwordTemp = Console.ReadLine();
            string password = "";
            while (start)
            {
                if (PasswordInput(passwordTemp))
                {
                    password = passwordTemp;
                    start = false;
                }
                else
                {
                    Console.Write("Please try again");
                    Console.Write("Input Password   :");
                    passwordTemp = Console.ReadLine();
                }
            }

            try{
                UserData user = new UserData(firstName, lastName, password);
                Console.WriteLine($"Your Id is {user.Id}");
                confidential.Add(user.Id, user.Password);
                types.Add(user);
            }catch(ArgumentOutOfRangeException)
            {
                Console.WriteLine("Please insert each name more than 1 character");
            }
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
            Console.WriteLine("3. Search Password");
            Console.WriteLine("4. Login");
            Console.WriteLine("5. Off");
            Console.WriteLine("==============================");
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
            Console.Write("Input Id : ");
            string id = Console.ReadLine();
            Console.Write("Input Password : ");
            string pass = Console.ReadLine();

            try
            {
                if (confidential[id] == pass)
                {
                    Console.WriteLine("login berhasil");
                }
            }
            catch(KeyNotFoundException)
            {
                Console.WriteLine("Id or Password Wrong, Plase try again !");
            }


        }

        public static void Search()
        {
            Console.Write("Input Id : ");
            string id = Console.ReadLine();
            try
            {
                Console.WriteLine($"Your password is {confidential[id]}");
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine("We cannot find your ID");
            }

            
        }

        public static bool PasswordInput(string input)
        {
            var hashNumber = new Regex(@"[0-9]");
            var upperChar = new Regex(@"[A-Z]");
            var loweChar = new Regex(@"[a-z]");
            int passwordLength = 8;

            bool start = false;

            if (!hashNumber.IsMatch(input))
            {
                Console.WriteLine("Pasword must have at least  1 numeric value");
            }
            if (!upperChar.IsMatch(input))
            {
                Console.WriteLine("Pasword must have at least  1 upper case (A-Z)");
            }
            if (!loweChar.IsMatch(input))
            {
                Console.WriteLine("Pasword must have at least  1 lower case (a-z)");
            }
            if (input.Length < passwordLength)
            {
                Console.WriteLine("Pasword must be at least 8 character");
            }
            else
            {
                start = true;
            }

            return start;
        }
    }
}
