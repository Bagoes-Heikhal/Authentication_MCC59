using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Authentication_MCC59
{
    class Program
    {
        static Dictionary<string, UserData> confidential = new();

        static Random rnd = new();

        static void Main(string[] args)
        {
            string password = PasswordExample.ReadPassword();
            bool start = false;
            while(start)
            {
                Menu();
                Console.Write("Choose your action : ");
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
                        Delete();
                        break;
                    case 6:
                        Edit();
                        break;
                    case 7:
                        start = false;
                        break;
                    default:
                        break;
                }
            }
        }

        static void InputData()
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
                string tempId = firstName.Substring(0, 2) + lastName.Substring(0, 2);
                string id = Makeid(tempId);
                confidential.Add(id, new UserData(firstName, lastName, password, id));
                Console.Clear();
                Console.WriteLine("Your account have been made");
                Console.WriteLine($"Your ID : {id}");
                Console.WriteLine($"Your Password {passwordTemp}");
            }
            catch(ArgumentOutOfRangeException)
            {
                Console.Clear();
                Console.WriteLine("Please input name with more than 1 character");
            }
        }

        static string Makeid(string id)
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

        static void ShowUserData()
        {
            foreach (var item in confidential)
            {
                Console.WriteLine("============================== \n");
                Console.WriteLine($"Full Name  : {item.Value.FirstName} {item.Value.LastName}");
                Console.WriteLine($"Your Id is : {item.Value.Id}");
                Console.WriteLine($"Password   : {item.Value.Password}\n");
                Console.WriteLine("============================== ");
            }
        }

        static void Search()
        {
            Console.Write("Input Id : ");
            string name = Console.ReadLine();
            foreach (var item in confidential)
            {
                if (item.Value.FirstName == name || item.Value.LastName == name || item.Value.Id == name)
                {
                    Console.WriteLine("============================\n");
                    Console.WriteLine($"First Name : {item.Value.FirstName}");
                    Console.WriteLine($"Last Name  : {item.Value.LastName}");
                    Console.WriteLine($"Id         : {item.Value.Id}");
                    Console.WriteLine($"Password   : {item.Value.Password}\n");
                    Console.WriteLine("============================\n");
                }
                else
                {
                    Console.WriteLine("User not found");
                }
            }
        }

        static void Menu()
        {
            Console.WriteLine("==============================");
            Console.WriteLine("1. Input");
            Console.WriteLine("2. Show Data");
            Console.WriteLine("3. Search");
            Console.WriteLine("4. Login");
            Console.WriteLine("5. Delete");
            Console.WriteLine("6. Edit");
            Console.WriteLine("7. Off");
            Console.WriteLine("==============================\n");
        }

        static int NumInput()
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

        static void Login()
        {
            Console.Write("Input Id : ");
            string id = Console.ReadLine();
            Console.Write("Input Password : ");
            string pass = Console.ReadLine();
            try
            {
                if (BCrypt.Net.BCrypt.Verify(pass, confidential[id].Password))
                {
                    Console.WriteLine("Login Sucsessfull");
                }
                else
                {
                    Console.WriteLine("Password Wrong, Plase try again !");
                }
            }
            catch(KeyNotFoundException)
            {
                Console.WriteLine("Cant fint your ID, Plase try again !");
            }


        }

        static bool PasswordCheck(string input)
        {
            // Jelasin alasan method ini dibuat
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

        static string InputPassword(string passwordTemp)
        {
            bool start = true;
            bool start2 = true;
            string password = null;
            while (start)
            {
                if (PasswordCheck(passwordTemp))
                {
                    Console.Write("Enter your password once again : ");
                    string secondPassword = Console.ReadLine();
                    while (start2)
                    {
                        if (secondPassword == passwordTemp)
                        {
                            password = BCrypt.Net.BCrypt.HashPassword(passwordTemp); ;
                            start = false;
                            start2 = false;
                        }
                        else
                        {
                            Console.WriteLine("Password wrong! please try again");
                            secondPassword = Console.ReadLine();
                        }
                    }
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

        static void Delete()
        {

        }

        static void Edit()
        {

        }

    }

    class PasswordExample
        {
            public static void ReadPassword2()
            {
                Console.WriteLine("Pls key in your Login ID");
                var loginid = Console.ReadLine();
                Console.WriteLine("Pls key in your Password");
                var password = ReadPassword();
                Console.Write("Your Password is:" + password);
                Console.ReadLine();
            }


            public static string ReadPassword()
            {
                string password = "";
                ConsoleKeyInfo info = Console.ReadKey(true);
                while (info.Key != ConsoleKey.Enter)
                {
                    if (info.Key != ConsoleKey.Backspace)
                    {
                        Console.Write("*");
                        password += info.KeyChar;
                    }
                    else if (info.Key == ConsoleKey.Backspace)
                    {
                        if (!string.IsNullOrEmpty(password))
                        {
                            // remove one character from the list of password characters
                            password = password.Substring(0, password.Length - 1);
                            // get the location of the cursor
                            int pos = Console.CursorLeft;
                            // move the cursor to the left by one character
                            Console.SetCursorPosition(pos - 1, Console.CursorTop);
                            // replace it with space
                            Console.Write(" ");
                            // move the cursor to the left by one character again
                            Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        }
                    }
                    info = Console.ReadKey(true);
                }
                // add a new line because user pressed enter at the end of their password
                Console.WriteLine();
                return password;
            }
        }
}
