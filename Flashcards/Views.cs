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

                ShowTable("stack", false);
   
                Console.Write("\n Please Enter the Stack ID this card belongs to: ");
                string entryId = Validation.Validate(Console.ReadLine(), "id");
                if (entryId == "MENU") { return; } else { card.F_ID = Convert.ToInt32(entryId); }

                Console.Write("\n Please Enter the Flashcard question: ");
                card.Question = Validation.Validate(Console.ReadLine(), "text");
                if (card.Question == "MENU") { return; }

                Console.Write("\n Please Enter the Flashcard answer: ");
                card.Answer = Validation.Validate(Console.ReadLine(), "text");
                if (card.Answer == "MENU") { return; }

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
            
            Console.Write("\n Please Enter the ID of the {0} to change: ", type);
            string entryId = Validation.Validate(Console.ReadLine(), "id");
            
            if (entryId == "MENU") { return; }

            if (type == "card")
            {
                Flashcard card = new Flashcard();
                card.ID = Convert.ToInt32(entryId);

                ShowTable("stack", false);

                Console.Write("\n Please Enter the Stack ID this card belongs to: ");
                string foreignID = Validation.Validate(Console.ReadLine(), "id");
                if (foreignID == "MENU") { return; } else { card.F_ID = Convert.ToInt32(foreignID); }

                ShowTable("card", false);
                Console.WriteLine(" CardID being edited {0}, StackID it belongs to {1}", entryId, foreignID);

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
                stack.ID = Convert.ToInt32(entryId);

                Console.Write("\n Please Enter the new Stack title: ");
                stack.Name = Validation.Validate(Console.ReadLine(), "text");
                if (stack.Name == "MENU") { return; }

                StacksController.UpdateRow(stack);
            }
        }

        public static void DeleteView(string type)
        {
            ShowTable(type, false);

            Console.WriteLine("\n Deleting a {0}...  \n Type MENU to return.", type);
            Console.Write("\n Enter ID of the {0} to delete: ", type);
            string entryId = Validation.Validate(Console.ReadLine(), "id");

            if (entryId == "MENU") { return; }
            else
            {
                if (type == "card")
                {
                    Flashcard card = new Flashcard();
                    card.ID = Convert.ToInt32(entryId);
                    FlashcardController.DeleteRow(card);
                }
                else if (type == "stack")
                {
                    Stack stack = new Stack();
                    stack.ID = Convert.ToInt32(entryId);
                    StacksController.DeleteRow(stack);
                }
            }
        }
    }
}
