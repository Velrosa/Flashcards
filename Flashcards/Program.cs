using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace Flashcards
{
    internal class Program
    {
        static string conString = ConfigurationManager.AppSettings.Get("conString");
        static void Main(string[] args)
        {
            using (var con = new SqlConnection(conString))
            {
                using (var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[Stacks]')" +
                                            "AND OBJECTPROPERTY(id, N'IsUserTable') = 1)" +
                                            "CREATE TABLE[dbo].[Stacks] (StackID INT IDENTITY(1,1) PRIMARY KEY," +
                                                                        "StackName TEXT);";
                    cmd.ExecuteNonQuery();
                }
            }

            using (var con = new SqlConnection(conString))
            {
                using (var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[Flashcards]')" +
                                            "AND OBJECTPROPERTY(id, N'IsUserTable') = 1)" +
                                            "CREATE TABLE[dbo].[Flashcards] (CardID INT IDENTITY(1,1) PRIMARY KEY," +
                                                                            "CardQuestion TEXT," +
                                                                            "CardAnswer TEXT," +
                                                                            "StackID INT FOREIGN KEY REFERENCES Stacks(StackID) " +
                                                                            "ON DELETE CASCADE ON UPDATE CASCADE);";
                    cmd.ExecuteNonQuery();
                }
            }

            while (true)
            {
                DisplayMenu();
            }
        }

        public static void DisplayMenu()
        {
            Console.Clear();

            Console.WriteLine("\n MAIN MENU\n\n" +
                                " What would you like to do?\n\n" +
                                " Type 0 to Close Application.\n" +
                                " Type 1 to Edit Flashcards.\n" +
                                " Type 2 to Edit Stacks.\n" +
                                " Type 3 to Begin a Study Session.\n");

            // Users selection from the Menu.
            string selector = Convert.ToString(Console.ReadKey(true).KeyChar);

            switch (selector)
            {
                case "0":
                    Environment.Exit(0);
                    break;
                
                case "1":
                    SubMenu("card");
                    break;
                case "2":
                    SubMenu("stack");
                    break;                
                default:
                    Console.Write(" Invalid Entry. press any key to return... ");
                    Console.ReadKey();
                    break;
            }         
        }
        public static void SubMenu(string type)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n {0}S MENU\n", type.ToUpper());
                Console.WriteLine(" What would you like to do?\n\n" +
                                    " Type 0 to Return to MAIN MENU.\n" +
                                    " Type 1 to View all {0}s.\n" +
                                    " Type 2 to Add a {0}.\n" +
                                    " Type 3 to Update a {0}.\n" +
                                    " Type 4 to Delete a {0}.\n", type);

                string selector = Convert.ToString(Console.ReadKey(true).KeyChar);

                Console.Clear();
                switch (selector)
                {
                    case "0":
                        return;
                    case "1":
                        Views.ShowTable(type, true);
                        break;
                    case "2":
                        Views.InsertView(type);
                        break;
                    case "3":
                        Views.UpdateView(type);
                        break;
                    case "4":
                        Views.DeleteView(type);
                        break;
                    default:
                        Console.Write(" Invalid Entry. press any key to return... ");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}