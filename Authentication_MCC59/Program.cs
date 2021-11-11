using System;

namespace Authentication_MCC59
{
    class Program
    {
        static void Main(string[] args)
        {
            MainMenu();
            int PilihMenu = InputConvert();
            switch (PilihMenu)
            {
                case 1:
                    InsideCreate();
                    break;
                case 2:
                    InsideShow();
                    break;
                case 3:
                    InsideSearch();
                    break;
                case 4:
                    InsideLogin();
                    break;
                case 5:
                    InsideExit();
                    break;
                default:
                    Console.WriteLine("Anda Memasukkan Angka Yang Salah");
                    break;
            }

            
        }
        static int InputConvert()
        {
            int Input = Convert.ToInt32(Console.ReadLine());
            return Input;
        }
        static void MainMenu()
        {
            Console.WriteLine("++++++ Basic Authentication ++++++");
            Console.WriteLine("1.\t Create User");
            Console.WriteLine("2.\t Show User");
            Console.WriteLine("3.\t Search User");
            Console.WriteLine("4.\t Login User");
            Console.WriteLine("5.\t Exit App");
            Console.WriteLine("Pilih Menu Nomor : ");
        }
        static void InsideCreate()
        {
            Console.WriteLine("++++++ Create User ++++++");
            Console.Write("First Name : ");
            string InsertFirstName = Console.ReadLine();
            Console.Write("Last Name : ");
            string InsertLastName = Console.ReadLine();
            Console.Write("Password : ");
            string InsertPassword = Console.ReadLine();
            Console.Clear();
        }
        static void InsideShow()
        {
            Console.WriteLine("++++++ Show User ++++++");
            Console.WriteLine("+++++++++++++++++++++++++++");
            Console.WriteLine("Fullname : ");
            Console.WriteLine("Username : ");
            Console.WriteLine("Password : ");
            Console.WriteLine("+++++++++++++++++++++++++++");
        }
        static void InsideSearch()
        {
            Console.WriteLine("++++++ Search User ++++++");
            
        }
        static void InsideLogin()
        {
            Console.WriteLine("++++++ Login User ++++++");
            Console.WriteLine("+++++++++++++++++++++++++++");
            Console.Write("Username : ");
            string LoginUsername = Console.ReadLine();
            Console.WriteLine("Password : ");
            string LoginPassword = Console.ReadLine();
            Console.WriteLine("+++++++++++++++++++++++++++");
            
        }
        static void InsideExit()
        {
            Console.WriteLine("Keluar");
        }

    }
}
