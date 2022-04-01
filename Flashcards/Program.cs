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
                    //cmd.CommandText = "CREATE TABLE IF NOT EXISTS Stacks (" +
                    //                    "StackID INT PRIMARY KEY," +
                    //                    "StackName TEXT);";
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
                    //cmd.CommandText = "CREATE TABLE Flashcards (" +
                    //                    "CardID INT PRIMARY KEY," +
                    //                    "CardName TEXT," +
                    //                    "StackID INT FOREIGN KEY REFERENCES Stacks(StackID));";
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
                                " Type 1 to View All Flashcards.\n" +
                                " Type 2 to Add a Flashcard.\n" +
                                " Type 3 to Update a Coding Session.\n" +
                                " Type 4 to Delete a Coding Session.\n" +
                                " Type 5 to View All Stacks.\n");

            // Users selection from the Menu.
            string selector = Convert.ToString(Console.ReadKey(true).KeyChar);

            switch (selector)
            {
                case "0":
                    Environment.Exit(0);
                    break;
                case "1":
                    FlashcardsView.ShowTable(selector);
                    break;
                case "2":
                    Console.Clear();
                    FlashcardsView.InsertView(selector);
                    break;
                case "3":
                    Console.Clear();
                    //SessionView.UpdateView(selector);
                    break;
                case "4":
                    Console.Clear();
                    //SessionView.DeleteView(selector);
                    break;
                case "5":
                    Console.Clear();
                    FlashcardsView.ShowTable(selector);
                    //if (SessionController.GetActive() == null)
                    //{
                    //    SessionView.OpenSession(selector);
                    //}
                    //else { SessionView.CloseSession(SessionController.GetActive()); }

                    break;
                default:
                    Console.Write(" Invalid Entry. press any key to return... ");
                    Console.ReadKey();
                    break;

            }
        }
    }
}