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
        // Displays a table with all the current records in.
        public static void ShowTable(string type, bool pause)
        {
            Console.Clear();

            Console.WriteLine("\n Displaying all records... \n");

            if (type == "card")
            {
                var returnList = new FlashcardController().Get<Flashcard>("Flashcards");
                ConsoleTableBuilder.From(returnList).ExportAndWriteLine();
            }
            else if (type == "stack")
            {
                var returnList = new FlashcardController().Get<Stack>("Stacks");
                ConsoleTableBuilder.From(returnList).ExportAndWriteLine();
            }
            else if (type == "session")
            {
                var returnList = new FlashcardController().Get<Session>("Sessions");
                ConsoleTableBuilder.From(returnList).ExportAndWriteLine();
            }
            else if (type == "cardStack")
            {
                Console.Clear();
                ShowTable("stack", false);

                Console.WriteLine("\n Type MENU to return.");
                Console.Write("\n Enter the name of the card stack to display: ");
                string entry = Validation.Validate(Console.ReadLine(), "text");
                if (entry == "MENU") { return; }

                Console.Clear();
                Console.WriteLine("\n Displaying all records... \n");
                ConsoleTableBuilder.From(FlashcardController.GetStackSet(entry)).ExportAndWriteLine();
            }

            // Waits on the displayed table, for viewing purposes
            if (pause == true)
            {
                Console.Write("\n Press any key to return to menu... ");
                Console.ReadKey();
            }
        }
        // Used for Inserting records to the controller.
        public static void InsertView(string type)
        {
            Console.Clear();
            Console.WriteLine($"\n Adding a new {type}...   \n Type MENU to return.");
            FlashcardDTO card = new FlashcardDTO();

            if (type == "card")
            {
                Console.Write("\n Please Enter the Flashcard question: ");
                
                card.Question = Validation.Validate(Console.ReadLine(), "text");
                if (card.Question == "MENU") { return; }

                Console.Write("\n Please Enter the Flashcard answer: ");
                card.Answer = Validation.Validate(Console.ReadLine(), "text");
                if (card.Answer == "MENU") { return; }

                ShowTable("stack", false);
            }

            Console.Write("\n Please Enter a Stack Name: ");
            card.StackName = Validation.Validate(Console.ReadLine(), "text");
            if (card.StackName == "MENU") { return; }

            FlashcardController.InsertRow(card, type);

        }
        // Used for Updating records to the controller.
        public static void UpdateView(string type)
        {
            ShowTable(type, false);

            Console.WriteLine($"\n Updating a {type}...  \n Type MENU to return.");
            FlashcardDTO card = new FlashcardDTO();

            if (type == "card")
            {
                Console.Write($"\n Please Enter the ID of the {type} to change: ");
                string entryId = Validation.Validate(Console.ReadLine(), "id");
                if (entryId == "MENU") { return; } else { card.ID = Convert.ToInt32(entryId); }

                // Show the stacks table so you can easier pick which stack it belongs to.
                ShowTable("stack", false);

                Console.Write("\n Please Enter the StackName this card belongs to: ");
                card.StackName = Validation.Validate(Console.ReadLine(), "text");
                if (card.StackName == "MENU") { return; }

                // Show the cards table for reference.
                ShowTable(type, false);
                
                Console.WriteLine($" CardID being edited {entryId}, StackName it belongs to {card.StackName}");

                Console.Write("\n Please Enter the new Card question: ");
                card.Question = Validation.Validate(Console.ReadLine(), "text");
                if (card.Question == "MENU") { return; }

                Console.Write("\n Please Enter the new Card answer: ");
                card.Answer = Validation.Validate(Console.ReadLine(), "text");
                if (card.Answer == "MENU") { return; }

            }
            else if (type == "stack")
            {
                Console.Write("\n Please Enter the Stack Name you wish to change: ");
                card.StackName = Validation.Validate(Console.ReadLine(), "text");
                if (card.StackName == "MENU") { return; }

                Console.Write("\n Please Enter the new Stack Name: ");
                card.NewName = Validation.Validate(Console.ReadLine(), "text");
                if (card.NewName == "MENU") { return; }
            }

            FlashcardController.UpdateRow(card, type);

        }
        // Used to Delete records to the controller.
        public static void DeleteView(string type)
        {
            ShowTable(type, false);

            Console.WriteLine($"\n Deleting a {type}...  \n Type MENU to return.");
            FlashcardDTO card = new FlashcardDTO();

            if (type == "card" || type == "session")
            {
                Console.Write($"\n Enter ID of the {type} to delete: ");
                string entryId = Validation.Validate(Console.ReadLine(), "id");
                if (entryId == "MENU") { return; } else { card.ID = Convert.ToInt32(entryId); }
                Console.Write($"\n Are you sure you wish to delete record ID: {entryId} (y or n)? ");
                string delete = Console.ReadLine();
                if (delete != "y") { return; }

            }
            else if (type == "stack")
            {
                Console.Write($"\n Enter the name of the {type} to delete: ");
                card.StackName = Validation.Validate(Console.ReadLine(), "text");
                if (card.StackName == "MENU") { return; }
                Console.Write($"\n Are you sure you wish to delete record Name: {card.StackName} (y or n)? ");
                string delete = Console.ReadLine();
                if (delete != "y") { return; }
            }

            FlashcardController.DeleteRow(card, type);
        }
    }
}
