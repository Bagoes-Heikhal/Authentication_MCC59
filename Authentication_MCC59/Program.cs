using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Authentication_MCC59
{
    class Program
    {
      
        //Menyimpan Data Username dan Password di Dictionary untuk keperluan searching dan login
        public static Dictionary<string, DataUsers> AuthData = new();
        static Random Rand = new();

        static void Main(string[] args)
        {
            bool StartProgram = true;
            while (StartProgram)
            {
                MainMenu();
                int PilihMenu = InputConvert();
                switch (PilihMenu)
                {
                    case 1:
                        InsideCreate();
                        Console.Clear();
                        break;
                    case 2:
                        InsideShow();
                        //Console.Clear();
                        break;
                    case 3:
                        InsideSearch();
                        Console.Clear();
                        break;
                    case 4:
                        InsideLogin();
                        Console.Clear();
                        break;
                    case 5:
                        InsideDelete();
                        break;
                    case 6:
                        StartProgram = false;
                        break;
                    default:
                        Console.WriteLine("Error");
                        break;
                }
            }  
        }
        static int InputConvert()
        {
            int Input = 0;
            try
            {
                Input = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException e)
            {
                Console.WriteLine("Please Enter the Correct Format (Number 1-5) ! " + e);
            }
            return Input;
        }
        static void MainMenu()
        {
            Console.WriteLine("++++++ Basic Authentication ++++++");
            Console.WriteLine("\t 1. Create User");
            Console.WriteLine("\t 2. Show User");
            Console.WriteLine("\t 3. Search User");
            Console.WriteLine("\t 4. Login User");
            Console.WriteLine("\t 5. Delete User");
            Console.WriteLine("\t 6. Exit App");
            Console.WriteLine("++++++++++++++++++++++++++++++++++");
            Console.WriteLine(" ");
            Console.WriteLine("Pilih Menu Nomor : ");
        }
        static void InsideCreate()
        {
            Console.Clear();
            Console.WriteLine("++++++ Create User ++++++");
            Console.Write("First Name : ");
            string InsertFirstName = Console.ReadLine();
            Console.Write("Last Name : ");
            string InsertLastName = Console.ReadLine();
            Console.Write("Password : ");
            string InsertPassword = Console.ReadLine();
            string CheckPass = InputPassword(InsertPassword);
            try
            {
                string UsernameTemp = InsertFirstName.Substring(0, 2) + InsertLastName.Substring(0, 2);
                string Username = CreateUsername(UsernameTemp);
                AuthData.Add(Username, new DataUsers(InsertFirstName, InsertLastName, CheckPass, Username));
                Console.Clear();
                Console.WriteLine("Your Username : " + Username);
                Console.WriteLine("Your Password : " + InsertPassword);
                Console.WriteLine("+++++++++++++++++++++++++");
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.Clear();
                Console.WriteLine("Error");
            } 
        }
        static string CreateUsername(string Username)
        {
            string TempUsername = Username;
            bool Start = AuthData.ContainsKey(Username);
            while (Start)
            {
                Console.WriteLine("Username Serupa");
                int RandomNumber = Rand.Next(11, 99);
                TempUsername = Username + RandomNumber;
                Start = AuthData.ContainsKey(TempUsername);
            }
            return TempUsername;
        }
        static bool PasswordCheck(string Input)
        {
            var HashNumber = new Regex(@"[0-9]");
            var UpperCase = new Regex(@"[A-Z]");
            var LowerCase = new Regex(@"[a-z]");
            int LengthPassword = 8;
            bool Start = false;

            if (!HashNumber.IsMatch(Input))
            {
                Console.WriteLine("Password Must Have at Least 1 Numeric Value");
            }
            else if (!UpperCase.IsMatch(Input))
            {
                Console.WriteLine("Password Must Have at Least 1 Upper Case (A-Z)");
            }
            else if (!LowerCase.IsMatch(Input))
            {
                Console.WriteLine("Password Must Have at Least 1 Lower Case (a-z)");
            }
            else if (Input.Length < LengthPassword)
            {
                Console.WriteLine("Password Must Have at Least 8 Character Consist of 1 Upper Case, 1 Lower Case, 1 Numeric Value");
            }
            else
            {
                Start = true;
            }
            return Start;

        }
        static string InputPassword(string PasswordTemporary)
        {
            bool Start = true;
            string Pass = null;
            while (Start)
            {
                if (PasswordCheck(PasswordTemporary))
                {
                    Pass = BCrypt.Net.BCrypt.HashPassword(PasswordTemporary);
                    Start = false;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Password Must Have at Least 8 Character Consist of 1 Upper Case, 1 Lower Case, 1 Numeric Value");
                    Console.WriteLine("Please Try Again");
                    Console.WriteLine("Input Password : ");
                    PasswordTemporary = Console.ReadLine();
                }
            }
            return Pass;
        }
        static void InsideShow()
        {
            Console.Clear();
            foreach (var n in AuthData)
            {
                Console.WriteLine("++++++ Show User ++++++");
                Console.WriteLine("+++++++++++++++++++++++++++");
                Console.WriteLine("Fullname : " + n.Value.FirstName + " " +n.Value.LastName);
                Console.WriteLine($"Username : {n.Value.UserName}");
                Console.WriteLine($"Password : {n.Value.Password}");
                Console.WriteLine("+++++++++++++++++++++++++++");
            }
        }
        static void InsideSearch()
        {
            Console.Clear();
            Console.WriteLine("++++++ Search User ++++++");
            Console.WriteLine("+++++++++++++++++++++++++++");
            Console.Write("Username : ");
            string SearchUsername = Console.ReadLine();
            foreach (var n in AuthData)
            {
                if (n.Value.FirstName == SearchUsername || n.Value.LastName == SearchUsername || n.Value.UserName == SearchUsername)
                {
                    Console.WriteLine("+++++++++++++++++++++++++++");
                    Console.WriteLine("Name : " + n.Value.FirstName + " " + n.Value.LastName);
                    Console.WriteLine($"Username : {n.Value.UserName}");
                    Console.WriteLine($"Password : {n.Value.Password}");
                    Console.WriteLine("+++++++++++++++++++++++++++");
                }
                else
                {
                    Console.WriteLine("Error");
                }
            }
         
        }
        static void InsideLogin()
        {
            Console.Clear();
            Console.WriteLine("++++++ Login User ++++++");
            Console.WriteLine("+++++++++++++++++++++++++++");
            Console.Write("Username : ");
            string LoginUsername = Console.ReadLine();
            Console.WriteLine("Password : ");
            string LoginPassword = Console.ReadLine();
            Console.WriteLine("+++++++++++++++++++++++++++");
            try
            {
                if(BCrypt.Net.BCrypt.Verify(LoginPassword, AuthData[LoginUsername].Password)
                {
                    Console.WriteLine("\t Login Successful!");
                }
                else
                {
                    Console.WriteLine("\t Incorrect Username or Password, Try Again");
                }
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine("\t Username Not Found");
            }
            
        }
        static void InsideDelete()
        {
            InsideShow();
            Console.WriteLine(" ");
            Console.Write("Delete Data Ke (Input Angka) :");
            string Name = Console.ReadLine();
            if (AuthData.ContainsKey(Name))
            {
                AuthData.Remove(Name);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Username Not Found");
            }
            InsideShow();
        }
        
    }
}
