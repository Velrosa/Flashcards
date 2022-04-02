using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTableExt;
using Flashcards.Models;

namespace Flashcards
{
    internal class Views
    {
        // Displays a table with all the current records in.
        public static void ShowTable(string type, bool pause)
        {
            Console.Clear();

            Console.WriteLine("\n Displaying all records:\n");

            if (type == "card")
            {
                ConsoleTableBuilder.From(FlashcardController.GetCards()).ExportAndWriteLine();
            }
            else if (type == "stack")
            {
                ConsoleTableBuilder.From(FlashcardController.GetStack()).ExportAndWriteLine();
            }

            if (pause == true)
            {
                Console.Write("\n Press any key to return to menu... ");
                Console.ReadKey();
            }
        }
        public static void InsertView(string type)
        {
            Console.WriteLine("\n Adding a new {0}...   \n Type MENU to return.", type);
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
        public static void UpdateView(string type)
        {
            ShowTable(type, false);

            Console.WriteLine("\n Updating a {0}...  \n Type MENU to return.", type);
            FlashcardDTO card = new FlashcardDTO();

            if (type == "card")
            {
                Console.Write("\n Please Enter the ID of the {0} to change: ", type);
                string entryId = Validation.Validate(Console.ReadLine(), "id");
                if (entryId == "MENU") { return; } else { card.ID = Convert.ToInt32(entryId); }

                ShowTable("stack", false);

                Console.Write("\n Please Enter the StackName this card belongs to: ");
                card.StackName = Validation.Validate(Console.ReadLine(), "text");
                if (card.StackName == "MENU") { return; }

                ShowTable(type, false);
                
                Console.WriteLine(" CardID being edited {0}, StackName it belongs to {1}", entryId, card.StackName);

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

        public static void DeleteView(string type)
        {
            ShowTable(type, false);

            Console.WriteLine("\n Deleting a {0}...  \n Type MENU to return.", type);
            FlashcardDTO card = new FlashcardDTO();

            if (type == "card")
            {
                Console.Write("\n Enter ID of the {0} to delete: ", type);
                string entryId = Validation.Validate(Console.ReadLine(), "id");
                if (entryId == "MENU") { return; } else { card.ID = Convert.ToInt32(entryId); }
            }
            else if (type == "stack")
            {
                Console.Write("\n Enter the name of the {0} to delete: ", type);
                card.StackName = Validation.Validate(Console.ReadLine(), "text");
                if (card.StackName == "MENU") { return; }
            }

            FlashcardController.UpdateRow(card, type);
        }
    }
}
