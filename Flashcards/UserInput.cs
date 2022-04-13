using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTableExt;
using Flashcards.Models;

namespace Flashcards
{
    internal class UserInput
    {
        private FlashcardController controller = new FlashcardController();
        internal void MainMenu()
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
                                " Type 6 to Display Yearly total for Sessions.\n" +
                                " Type 7 to Remove a Session.\n");

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
                    ShowTable("cardStack", true);
                    break;
                case "4":
                    StudySession stdySession = new StudySession();
                    ShowTable("stack", false);
                    stdySession.StartSession();
                    break;
                case "5":
                    ShowTable("session", true);
                    break;
                case "6":
                    ShowTable("yearlySession", true);
                    break;
                case "7":
                    DeleteView("session");
                    break;
                case "8":
                    Console.WriteLine(DateTime.Now.ToString());
                    Console.ReadKey();
                    break;
                default:
                    Console.Write(" Invalid Entry. press any key to return... ");
                    Console.ReadKey();
                    break;
            }
        }

        // SubMenus for Cards and Stacks.
        internal void SubMenu(string type)
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
                        ShowTable(type, true);
                        break;
                    case "2":
                        InsertView(type);
                        break;
                    case "3":
                        UpdateView(type);
                        break;
                    case "4":
                        DeleteView(type);
                        break;
                    default:
                        Console.Write(" Invalid Entry. press any key to return... ");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // Displays a table with all the current records in.
        internal void ShowTable(string type, bool pause)
        {
            Console.Clear();

            Console.WriteLine("\n Displaying all records... \n");

            // Prints tables to the screen depending on type provided.
            switch (type)
            {
                case "card":
                    var cardList = new FlashcardController().Get<Flashcard>("Flashcards");
                    ConsoleTableBuilder.From(cardList).ExportAndWriteLine();
                    break;
                case "stack":
                    var stackList = new FlashcardController().Get<Stack>("Stacks");
                    ConsoleTableBuilder.From(stackList).ExportAndWriteLine();
                    break;
                case "session":
                    var sessionList = new FlashcardController().Get<Session>("Sessions");
                    ConsoleTableBuilder.From(sessionList).ExportAndWriteLine();
                    break;
                case "yearlySession":
                    ConsoleTableBuilder.From(controller.GetMonthySessionData()).ExportAndWriteLine();
                    break;
                case "cardStack":
                    Console.Clear();
                    ShowTable("stack", false);

                    Console.WriteLine("\n Type MENU to return.");
                    Console.Write("\n Enter the name of the card stack to display: ");
                    string entry = Validation.IsStringValid(Console.ReadLine());
                    if (entry == "MENU") { return; }

                    Console.Clear();
                    Console.WriteLine("\n Displaying all records... \n");
                    ConsoleTableBuilder.From(controller.GetStackSet(entry)).ExportAndWriteLine();
                    break;
            }

            // Waits on the displayed table, for viewing purposes
            if (pause == true)
            {
                Console.Write("\n Press any key to return to menu... ");
                Console.ReadKey();
            }
        }
        // Used for Inserting records to the controller.
        internal void InsertView(string type)
        {
            Console.Clear();
            Console.WriteLine($"\n Adding a new {type}...   \n Type MENU to return.");
            FlashcardDTO card = new FlashcardDTO();

            if (type == "card")
            {
                Console.Write("\n Please Enter the Flashcard question: ");
                
                card.Question = Validation.IsStringValid(Console.ReadLine());
                if (card.Question == "MENU") { return; }

                Console.Write("\n Please Enter the Flashcard answer: ");
                card.Answer = Validation.IsStringValid(Console.ReadLine());
                if (card.Answer == "MENU") { return; }

                ShowTable("stack", false);
            }

            Console.Write("\n Please Enter a Stack Name: ");
            card.StackName = Validation.IsStringValid(Console.ReadLine());
            if (card.StackName == "MENU") { return; }

            controller.InsertRow(card, type);

        }
        // Used for Updating records to the controller.
        internal void UpdateView(string type)
        {
            ShowTable(type, false);

            Console.WriteLine($"\n Updating a {type}...  \n Type MENU to return.");
            FlashcardDTO card = new FlashcardDTO();

            if (type == "card")
            {
                Console.Write($"\n Please Enter the ID of the {type} to change: ");
                string entryId = Validation.IsNumberValid(Console.ReadLine());
                if (entryId == "MENU") { return; } else { card.ID = Convert.ToInt32(entryId); }

                // Show the stacks table so you can easier pick which stack it belongs to.
                ShowTable("stack", false);

                Console.Write("\n Please Enter the StackName this card belongs to: ");
                card.StackName = Validation.IsStringValid(Console.ReadLine());
                if (card.StackName == "MENU") { return; }

                // Show the cards table for reference.
                ShowTable(type, false);
                
                Console.WriteLine($" CardID being edited {entryId}, StackName it belongs to {card.StackName}");

                Console.Write("\n Please Enter the new Card question: ");
                card.Question = Validation.IsStringValid(Console.ReadLine());
                if (card.Question == "MENU") { return; }

                Console.Write("\n Please Enter the new Card answer: ");
                card.Answer = Validation.IsStringValid(Console.ReadLine());
                if (card.Answer == "MENU") { return; }

            }
            else if (type == "stack")
            {
                Console.Write("\n Please Enter the Stack Name you wish to change: ");
                card.StackName = Validation.IsStringValid(Console.ReadLine());
                if (card.StackName == "MENU") { return; }

                Console.Write("\n Please Enter the new Stack Name: ");
                card.NewName = Validation.IsStringValid(Console.ReadLine());
                if (card.NewName == "MENU") { return; }
            }

            controller.UpdateRow(card, type);

        }
        // Used to Delete records to the controller.
        internal void DeleteView(string type)
        {
            ShowTable(type, false);

            Console.WriteLine($"\n Deleting a {type}...  \n Type MENU to return.");
            FlashcardDTO card = new FlashcardDTO();

            if (type == "card" || type == "session")
            {
                Console.Write($"\n Enter ID of the {type} to delete: ");
                string entryId = Validation.IsNumberValid(Console.ReadLine());
                if (entryId == "MENU") { return; } else { card.ID = Convert.ToInt32(entryId); }
                
                Console.Write($"\n Are you sure you wish to delete record ID: {entryId} (y or n)? ");
                string delete = Console.ReadLine();
                if (delete != "y") { return; }

            }
            else if (type == "stack")
            {
                Console.Write($"\n Enter the name of the {type} to delete: ");
                card.StackName = Validation.IsStringValid(Console.ReadLine());
                if (card.StackName == "MENU") { return; }
                
                Console.Write($"\n Are you sure you wish to delete record Name: {card.StackName} (y or n)? ");
                string delete = Console.ReadLine();
                if (delete != "y") { return; }
            }

            controller.DeleteRow(card, type);
        }
    }
}
