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
                bool start = true;
                while (start)
                {
                    Menu();
                    Console.WriteLine(" ");
                    Console.Write("Choose Your Action : ");
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
                            Edit();
                            break;
                        case 4:
                            Console.Clear();
                            Search();
                            break;
                        case 5:
                            Console.Clear();
                            Login();
                            break;
                        case 6:
                            Delete();
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
            Console.WriteLine("++++++ Create User ++++++");
            Console.WriteLine("+++++++++++++++++++++++++");
            Console.Write("Input First Name : ");
            string firstName = Console.ReadLine();
            Console.Write("Input Last Name  : ");
            string lastName = Console.ReadLine();

            Console.Write("Input Password   : ");
            string passwordTemp = Console.ReadLine();
            Console.WriteLine("Input Confirmation Password");
            string passwordTemp2 = Console.ReadLine();
            if (passwordTemp == passwordTemp2)
            {
                string password = InputPassword(passwordTemp);   
                try
                {
                    string tempId = firstName.Substring(0, 2) + lastName.Substring(0, 2);
                    string id = Makeid(tempId);
                    confidential.Add(id, new UserData(firstName, lastName, password, id));
                    Console.Clear();
                    Console.WriteLine("++++++ Data User ++++++");
                    Console.WriteLine("+++++++++++++++++++++++++");
                    Console.WriteLine("Your Account Have Been Made");
                    Console.WriteLine($"Your ID : {id}");
                    Console.WriteLine($"Your Password {passwordTemp}");
                    Console.WriteLine("+++++++++++++++++++++++++");
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.Clear();
                    Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++");
                    Console.WriteLine("Please Input Name With More Than 1 Character");
                    Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++");
                }
            }
            else
            {
                Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++");
                Console.WriteLine("Your Password and Confirmation Password Isn't Match");
                Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++");
            }

        }

        static string Makeid(string id)
        {
            string idTemp = id;
            bool start = confidential.ContainsKey(id);
            while (start)
            {
                Console.WriteLine("Same ID");
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
                    Console.WriteLine("\t++++++ Show User ++++++");
                    Console.WriteLine("\t+++++++++++++++++++++++++++++++++++++++++++++++++++++");
                    Console.WriteLine($"\tFull Name  : {item.Value.FirstName} {item.Value.LastName}");
                    Console.WriteLine($"\tID : {item.Value.Id}");
                    Console.WriteLine($"\tPassword   : {item.Value.Password}\n");
                    Console.WriteLine("\t+++++++++++++++++++++++++++++++++++++++++++++++++++++");
                }
            }

        static void Search()
        {
            Console.WriteLine("++++++ Search User ++++++");
            Console.WriteLine("+++++++++++++++++++++++++++");
            Console.Write("Input ID : ");
            string name = Console.ReadLine();
            foreach (var item in confidential)
            {
                if (item.Value.FirstName == name || item.Value.LastName == name || item.Value.Id == name)
                {
                    Console.WriteLine("\t+++++++++++++++++++++++++++");
                    Console.WriteLine($"\tFirst Name : {item.Value.FirstName}");
                    Console.WriteLine($"\tLast Name  : {item.Value.LastName}");
                    Console.WriteLine($"\tID         : {item.Value.Id}");
                    Console.WriteLine($"\tPassword   : {item.Value.Password}\n");
                    Console.WriteLine("\t+++++++++++++++++++++++++++");
                }
                else
                {
                    Console.WriteLine("User Not Found");
                }
            }
        }

        static void Menu()
        {
            Console.WriteLine("\t++++++ Basic Authentication ++++++");
            Console.WriteLine("\t++++++++++++++++++++++++++++++++++");
            Console.WriteLine("\t1. Create User");
            Console.WriteLine("\t2. Show User");
            Console.WriteLine("\t3. Edit User");
            Console.WriteLine("\t4. Search User");
            Console.WriteLine("\t5. Login User");
            Console.WriteLine("\t6. Delete User");
            Console.WriteLine("\t7. Exit App");
            Console.WriteLine("\t++++++++++++++++++++++++++++++++++");
        }

        static void MenuEdit()
        {
            Console.WriteLine("\t++++++ Edit User ++++++");
            Console.WriteLine("\t+++++++++++++++++++++++++++");
            Console.WriteLine("\t1. Edit Username");
            Console.WriteLine("\t2. Edit FirstName");
            Console.WriteLine("\t3. Edit Password");
            Console.WriteLine("\t+++++++++++++++++++++++++++");
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
                Console.WriteLine("");
                Console.WriteLine("+++++++++++++++++++++++++++++++++");
                Console.WriteLine("Please Enter The Correct Number!! " + e);
                Console.WriteLine("+++++++++++++++++++++++++++++++++");
            }
            return a;
        }

        static void Login()
        {
            Console.WriteLine("++++++ Login User ++++++");
            Console.WriteLine("+++++++++++++++++++++++++++");
            Console.Write("Input ID : ");
            string id = Console.ReadLine();
            Console.Write("Input Password : ");
            string pass = Console.ReadLine();
            Console.WriteLine("+++++++++++++++++++++++++++");
            try
            {
                if (BCrypt.Net.BCrypt.Verify(pass, confidential[id].Password))
                {
                    Console.WriteLine("");
                    Console.WriteLine("++++++++++++++++++++++++++++++++++");
                    Console.WriteLine("Login Successful!");
                    Console.WriteLine("++++++++++++++++++++++++++++++++++");
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("++++++++++++++++++++++++++++++++++");
                    Console.WriteLine("Password isn't match, Please Try Again!");
                    Console.WriteLine("++++++++++++++++++++++++++++++++++");
                }
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine("");
                Console.WriteLine("++++++++++++++++++++++++++++++++++");
                Console.WriteLine("ID Not Found, Please Try Again!");
                Console.WriteLine("++++++++++++++++++++++++++++++++++");
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
                Console.WriteLine("");
                Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++");
                Console.WriteLine("Password Must Have at Least 1 Numeric Value");
                Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++");
            }
            else if (!upperChar.IsMatch(input))
            {
                Console.WriteLine("");
                Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++");
                Console.WriteLine("Password Must Have at Least 1 Upper Case (A-Z)");
                Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++");
            }
            else if (!loweChar.IsMatch(input))
            {
                Console.WriteLine("");
                Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++");
                Console.WriteLine("Password Must Have at Least 1 Lower Case (a-z)");
                Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++");
            }
            else if (input.Length < passwordLength)
            {
                Console.WriteLine("");
                Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                Console.WriteLine("Password Must Have at Least 8 Character Consist of 1 Upper Case, 1 Lower Case, 1 Numeric Value");
                Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
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
            string password = null;
            while (start)
            {
                if (PasswordCheck(passwordTemp))
                {
                    password = BCrypt.Net.BCrypt.HashPassword(passwordTemp); ;
                    start = false;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                    Console.WriteLine("Password Must Have at Least 8 Character Consist of 1 Upper Case, 1 Lower Case, 1 Numeric Value");
                    Console.WriteLine("Please Reinsert The Password :)");
                    Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                    Console.Write("Input Password   :");
                    passwordTemp = Console.ReadLine();
                }
            }

            return password;
        }

        static void Delete()
        {
            ShowUserData();
            Console.WriteLine(" ");
            Console.WriteLine("++++++ Delete User ++++++");
            Console.WriteLine("+++++++++++++++++++++++++++");
            Console.Write("Delete Data (Input Number) :");
            string Name = Console.ReadLine();
            if (confidential.ContainsKey(Name))
            {
                confidential.Remove(Name);
                Console.WriteLine("");
                Console.WriteLine("\t+++++++++++++++++++++++++++");
                Console.WriteLine("\tData Deleted!");
                Console.WriteLine("\t+++++++++++++++++++++++++++");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("");
                Console.WriteLine("\t+++++++++++++++++++++++++++");
                Console.WriteLine("\tID Not Found");
                Console.WriteLine("\t+++++++++++++++++++++++++++");
            }
            ShowUserData();
                
        }

        static void Edit()
        {
            string password = PasswordExample.ReadPassword();
            bool start = false;
            while(start)
            {
                MenuEdit();
                Console.Write("Choose Your Action : ");
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
    }

    class PasswordExample
        {
            //test
            public static void ReadPassword2()
            {
                Console.Write("Input ID :");
                var loginid = Console.ReadLine();
                Console.Write("Input Password : ");
                var password = ReadPassword();
                Console.Write("Your Password :" + password);
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
  
