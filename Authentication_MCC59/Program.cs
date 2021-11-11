using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Authentication_MCC59
{
    class Program
    {
        public static List<UserData> types = new();
        public static Dictionary<string, string> confidential = new();
        public static Random rnd = new();
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
                        Console.Clear();
                        InputData();

                        break;
                    case 2:
                        Console.Clear();
                        ShowUserData();
                        break;
                    case 3:
                        Console.Clear();
                        Search();
                        break;
                    case 4:
                        Console.Clear();
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

            Console.Write("Input Password   : ");
            string passwordTemp = Console.ReadLine();
            string password = InputPassword(passwordTemp);
   
            try
            {
                UserData user = new(firstName, lastName, password);
                string id = firstName.Substring(0, 2) + lastName.Substring(0, 2);
                user.Id = Makeid(id);
                confidential.Add(user.Id, user.Password);
                types.Add(user);
                //Console.Clear();
                Console.WriteLine("Your account have been made");
                Console.WriteLine($"Your ID : {user.Id}");
                Console.WriteLine($"Your Password {user.Password}");
            }
            catch(ArgumentOutOfRangeException)
            {
                Console.Clear();
                Console.WriteLine("Please input name with more than 1 character");
            }
        }

        public static string Makeid(string id)
        {
            string idTemp = id;
            bool start = confidential.ContainsKey(id);
            while (start)
            {
                Console.WriteLine("ID sama");
                int randomNumber1 = rnd.Next(11, 99);
                idTemp = id + randomNumber1;
                start = confidential.ContainsKey(idTemp);
            } 
            return idTemp;
        }

        public static void ShowUserData()
        {
            foreach (var item in types)
            {
                Console.WriteLine("============================== \n");
                Console.WriteLine($"Full Name  : {item.FirstName} {item.LastName}");
                Console.WriteLine($"Your Id is :{item.Id}");
                Console.WriteLine($"Password   : {item.Password}\n");
                Console.WriteLine("============================== ");
            }
        }

        public static void Menu()
        {
            Console.WriteLine("==============================");
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
            string name = Console.ReadLine();
            foreach (var item in types)
            {
                if (item.FirstName == name || item.LastName == name || item.Id == name)
                {
                    Console.WriteLine("============================");
                    Console.WriteLine($"First Name : {item.FirstName}");
                    Console.WriteLine($"Last Name  : {item.LastName}");
                    Console.WriteLine($"Id         : {item.Id }");
                    Console.WriteLine($"Password   : {item.Password}");
                }
                else
                {
                    Console.WriteLine("User not found");
                }
            }
        }

        public static bool PasswordCheck(string input)
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
            else if (!upperChar.IsMatch(input))
            {
                Console.WriteLine("Pasword must have at least  1 upper case (A-Z)");
            }
            else if(!loweChar.IsMatch(input))
            {
                Console.WriteLine("Pasword must have at least  1 lower case (a-z)");
            }
            else if(input.Length < passwordLength)
            {
                Console.WriteLine("Pasword must be at least 8 character");
            }
            else
            {
                start = true;
            }

            return start;
        }

        public static string InputPassword(string passwordTemp)
        {
            bool start = true;
            string password = null;
            while (start)
            {
                if (PasswordCheck(passwordTemp))
                {
                    password = passwordTemp;
                    start = false;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Pasword at least 8 character alphanumeric, one lower case and one upper case");
                    Console.WriteLine("Please try again");
                    Console.Write("Input Password   :");
                    passwordTemp = Console.ReadLine();
                }
            }

            return password;
        }
    }
}
