using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using Flashcards.Models;
using ConsoleTableExt;

namespace Flashcards
{
    internal class Program
    {
        static string conString = ConfigurationManager.AppSettings.Get("conString");
        static string dbString = ConfigurationManager.AppSettings.Get("dbString");
        static void Main(string[] args)
        {
            // Creates the Database if it doesnt exist.
            using (var con = new SqlConnection(dbString))
            {
                using (var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "SELECT db_id('Flashcards')";
                    bool state = cmd.ExecuteScalar() != DBNull.Value;

                    if (!state)
                    {
                        cmd.CommandText = "CREATE DATABASE Flashcards";
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            // Check if a tables exist in the database, if not create them.
            using (var con = new SqlConnection(conString))
            {
                using (var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[Stacks]')" +
                                            "AND OBJECTPROPERTY(id, N'IsUserTable') = 1)" +
                                            "CREATE TABLE[dbo].[Stacks] (StackName VARCHAR(50) UNIQUE);";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[Flashcards]')" +
                                        "AND OBJECTPROPERTY(id, N'IsUserTable') = 1)" +
                                        "CREATE TABLE[dbo].[Flashcards] (" +
                                        "CardID INT IDENTITY(1,1) PRIMARY KEY," +
                                        "CardQuestion TEXT," +
                                        "CardAnswer TEXT," +
                                        "StackName VARCHAR(50) FOREIGN KEY REFERENCES Stacks(StackName) " +
                                        "ON DELETE CASCADE ON UPDATE CASCADE);";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[Sessions]')" +
                                        "AND OBJECTPROPERTY(id, N'IsUserTable') = 1)" +
                                        "CREATE TABLE[dbo].[Sessions] (" +
                                        "ID INT IDENTITY(1,1) PRIMARY KEY," +
                                        "Date TEXT," +
                                        "Score TEXT," +
                                        "StackName VARCHAR(50) FOREIGN KEY REFERENCES Stacks(StackName) " +
                                        "ON DELETE CASCADE ON UPDATE CASCADE);";
                    cmd.ExecuteNonQuery();
                }
            }
            
            // Main runtime loop.
            while (true)
            {
                DisplayMenu();
            }
        }
        // Displays all the menus to the User.
        public static void DisplayMenu()
        {
            Console.Clear();

            Console.WriteLine("\n MAIN MENU\n\n" +
                                " What would you like to do?\n\n" +
                                " Type 0 to Close Application.\n" +
                                " Type 1 for Flashcards MENU.\n" +
                                " Type 2 for Stacks MENU.\n" +
                                " Type 3 to View a Stack of Cards.\n" +
                                " Type 4 to Begin a Study Session.\n" +
                                " Type 5 to Display Previous Sessions.\n" +
                                " Type 6 to Remove a Session.\n");

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
                case "3":
                    UserInput.ShowTable("cardStack", true);
                    break;
                case "4":
                    StudySession.Session();
                    break;
                case "5":
                    UserInput.ShowTable("session", true);
                    break;
                case "6":
                    UserInput.DeleteView("session");
                    break;
                default:
                    Console.Write(" Invalid Entry. press any key to return... ");
                    Console.ReadKey();
                    break;
            }         
        }
        // SubMenus for Cards and Stacks.
        public static void SubMenu(string type)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"\n {type.ToUpper()}S MENU\n");
                Console.WriteLine(" What would you like to do?\n\n" +
                                    " Type 0 to Return to MAIN MENU.\n" +
                                    " Type 1 to View all {0}s.\n" +
                                    " Type 2 to Add a {0}.\n" +
                                    " Type 3 to Update a {0}.\n" +
                                    " Type 4 to Delete a {0}.\n", type);

                string selector = Convert.ToString(Console.ReadKey(true).KeyChar);

                switch (selector)
                {
                    case "0":
                        return;
                    case "1":
                        UserInput.ShowTable(type, true);
                        break;
                    case "2":
                        UserInput.InsertView(type);
                        break;
                    case "3":
                        UserInput.UpdateView(type);
                        break;
                    case "4":
                        UserInput.DeleteView(type);
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