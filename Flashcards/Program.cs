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
                                                                            "CardName TEXT," +
                                                                            "StackID INT FOREIGN KEY REFERENCES Stacks(StackID));";
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
                                " Type 1 for Flashcards.\n" +
                                " Type 2 for Stacks.\n");

            // Users selection from the Menu.
            string selector = Convert.ToString(Console.ReadKey(true).KeyChar);

            switch (selector)
            {
                case "0":
                    Environment.Exit(0);
                    break;
                
                case "1":
                    Console.Clear();
                    Console.WriteLine("\n FLASHCARDS MENU\n\n" +
                                        " What would you like to do?\n\n" +
                                        " Type 0 to Return to MAIN MENU.\n" +
                                        " Type 1 to View All Flashcards.\n" +
                                        " Type 2 to Add a FlashCard.\n" +
                                        " Type 3 to Update a Flashcard.\n" +
                                        " Type 4 to Delete a Flashcard.\n");

                    selector = Convert.ToString(Console.ReadKey(true).KeyChar);

                    switch (selector)
                    {
                        case "0":
                            return;
                        case "1":
                            Console.Clear();
                            FlashcardsView.ShowTable("1");
                            break;
                        case "2":
                            Console.Clear();
                            FlashcardsView.InsertView("1");
                            break;
                    }
                    
                    break;
                
                case "2":
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine("\n STACKS MENU\n\n" +
                                            " What would you like to do?\n\n" +
                                            " Type 0 to Return to MAIN MENU.\n" +
                                            " Type 1 to View All Stacks.\n" +
                                            " Type 2 to Add a Stack.\n" +
                                            " Type 3 to Update a Stack.\n" +
                                            " Type 4 to Delete a Stack.\n");

                        selector = Convert.ToString(Console.ReadKey(true).KeyChar);

                        switch (selector)
                        {
                            case "0":
                                return;
                            case "1":
                                Console.Clear();
                                FlashcardsView.ShowTable("2");
                                break;
                            case "2":
                                Console.Clear();
                                FlashcardsView.InsertView("2");
                                break;
                        }
                    }
                    
                    break;
                
                default:
                    Console.Write(" Invalid Entry. press any key to return... ");
                    Console.ReadKey();
                    break;

            }
        }
    }
}