using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTableExt;

namespace Flashcards
{
    internal class FlashcardsView
    {

        // Displays a table with all the current records in.
        public static void ShowTable(string selector)
        {
            Console.Clear();

            Console.WriteLine("\n Displaying all records:\n");

            if (selector == "1")
            {
                ConsoleTableBuilder.From(FlashcardController.GetCards()).ExportAndWriteLine();
            }
            else if (selector == "2")
            {
                ConsoleTableBuilder.From(StacksController.GetStack()).ExportAndWriteLine();
            }

            if (selector == "1" || selector == "2")
            {
                Console.Write("\n Press any key to return to menu... ");
                Console.ReadKey();
            }
        }
        public static void InsertView(string selector)
        {
            if (selector == "1")
            {
                Flashcard card = new Flashcard();

                Console.WriteLine("\n Adding a new Flashcard...   \n Type MENU to return.");

                Console.Write("\n Please Enter the Flashcard text: ");
                card.Name = Validation.Validate(Console.ReadLine(), "text");
                if (card.Name == "MENU") { return; }

                Console.Write("\n Please Enter the Stack ID this card belongs to: ");
                string entryId = Validation.Validate(Console.ReadLine(), "id");
                if (entryId == "MENU") { return; } else { card.F_ID = Convert.ToInt32(entryId); }

                FlashcardController.InsertRow(card);
            }
            else if (selector == "2")
            {
                Stack stack = new Stack();

                Console.WriteLine("\n Adding a new Stack...   \n Type MENU to return.");

                Console.Write("\n Please Enter the Stack title: ");
                stack.Name = Validation.Validate(Console.ReadLine(), "text");
                if (stack.Name == "MENU") { return; }

                StacksController.InsertRow(stack);
            }
        }
    }
}
