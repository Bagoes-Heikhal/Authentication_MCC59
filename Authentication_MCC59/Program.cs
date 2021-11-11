using System;
using System.Collections.Generic;

namespace Authentication_MCC59
{
    class Program
    {
        //Menyimpan Data dalam Bentuk List, Memanggil class DataUsers utk akses ke atribut
        public static List<DataUsers> NewData = new();
        //Menyimpan Data Username dan Password di Dictionary untuk keperluan searching dan login
        public static Dictionary<string, string> AuthData = new();
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
                        Console.Clear();
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
                        StartProgram = false;
                        break;
                    default:
                        Console.WriteLine("Anda Memasukkan Angka Yang Salah");
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
            Console.WriteLine("\t 5. Exit App");
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
            DataUsers Users = new DataUsers(InsertFirstName, InsertLastName, InsertPassword);
            NewData.Add(Users);
            AuthData.Add(Users.UserName, Users.Password);
            Console.WriteLine("+++++++++++++++++++++++++");
        }
        static void InsideShow()
        {
            Console.Clear();
            Console.WriteLine("++++++ Show User ++++++");
            foreach (var n in NewData)
            {
                Console.WriteLine("+++++++++++++++++++++++++++");
                Console.WriteLine("Fullname : " + n.FirstName + " " +n.LastName);
                Console.WriteLine("Username : " + n.UserName);
                Console.WriteLine("Password : " + n.Password);
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
            Console.WriteLine("Your Password is " + AuthData[SearchUsername]);
            Console.WriteLine("+++++++++++++++++++++++++++");


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
                if(AuthData[LoginUsername] == LoginPassword)
                {
                    Console.WriteLine("\t Login Successful!");
                }
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine("\t Incorrect Username or Password, Try Again");
            }
            
        }
        
    }
}
