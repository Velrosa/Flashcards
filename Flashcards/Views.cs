using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTableExt;

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
                ConsoleTableBuilder.From(StacksController.GetStack()).ExportAndWriteLine();
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

            if (type == "card")
            {
                Flashcard card = new Flashcard();

                Console.Write("\n Please Enter the Flashcard question: ");
                card.Question = Validation.Validate(Console.ReadLine(), "text");
                if (card.Question == "MENU") { return; }

                Console.Write("\n Please Enter the Flashcard answer: ");
                card.Answer = Validation.Validate(Console.ReadLine(), "text");
                if (card.Answer == "MENU") { return; }

                ShowTable("stack", false);

                Console.Write("\n Please Enter the Stack Name this card belongs to: ");
                card.StackName = Validation.Validate(Console.ReadLine(), "text");
                if (card.StackName == "MENU") { return; }

                FlashcardController.InsertRow(card);
            }
            else if (type == "stack")
            {
                Stack stack = new Stack();

                Console.Write("\n Please Enter the Stack title: ");
                stack.Name = Validation.Validate(Console.ReadLine(), "text");
                if (stack.Name == "MENU") { return; }

                StacksController.InsertRow(stack);
            }
        }
        public static void UpdateView(string type)
        {
            ShowTable(type, false);

            Console.WriteLine("\n Updating a {0}...  \n Type MENU to return.", type);
            
            if (type == "card")
            {
                Flashcard card = new Flashcard();

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

                FlashcardController.UpdateRow(card);
            }
            else if (type == "stack")
            {
                Stack stack = new Stack();
                Console.Write("\n Please Enter the Stack Name you wish to change: ");
                stack.Name = Validation.Validate(Console.ReadLine(), "text");
                if (stack.Name == "MENU") { return; }

                Console.Write("\n Please Enter the new Stack Name: ");
                stack.NewName = Validation.Validate(Console.ReadLine(), "text");
                if (stack.NewName == "MENU") { return; }

                StacksController.UpdateRow(stack);
            }
        }

        public static void DeleteView(string type)
        {
            ShowTable(type, false);

            Console.WriteLine("\n Deleting a {0}...  \n Type MENU to return.", type);


            if (type == "card")
            {
                Flashcard card = new Flashcard();
                Console.Write("\n Enter ID of the {0} to delete: ", type);
                string entryId = Validation.Validate(Console.ReadLine(), "id");
                if (entryId == "MENU") { return; } else { card.ID = Convert.ToInt32(entryId); }
                FlashcardController.DeleteRow(card);
            }
            else if (type == "stack")
            {
                Stack stack = new Stack();
                Console.Write("\n Enter the name of the {0} to delete: ", type);
                stack.Name = Validation.Validate(Console.ReadLine(), "text");
                if (stack.Name == "MENU") { return; }
                StacksController.DeleteRow(stack);
            }
        }
    }
}
