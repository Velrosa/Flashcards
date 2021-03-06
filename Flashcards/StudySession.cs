using Flashcards.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards
{
    internal class StudySession
    {
        private FlashcardController controller = new FlashcardController();
        public void StartSession()
        {            
            // Pick a stack to study.
            Console.Write(" Enter the name of a Stack to study: ");
            string stack = Validation.IsStringValid(Console.ReadLine());
            
            // Fetch that stacks cards to display.
            List<Flashcard> cards = controller.GetStackSet(stack);

            // If theres no cards in the stack, return to the menu.
            if (cards.Count == 0)
            {
                Console.WriteLine(" Press any key to return... ");
                Console.ReadKey();
                return;
            }

            // initilise the score card counter and total number of cards.
            int score = 0;
            int cardCounter = 1;
            int cardTotal = cards.Count();

            Console.Clear();
            
            // Display each card to the screen to be answered. Increment card counter and score accordingly.
            foreach (Flashcard card in cards)
            {
                Console.WriteLine($"\n Viewing card {cardCounter} of {cardTotal} from Stack {card.StackName}");
                Console.Write($"\n {card.Question}: ");
                string answer = Console.ReadLine();
                
                if (answer == card.Answer)
                {
                    Console.WriteLine(" Correct!");
                    score++;
                }
                else
                {
                    Console.WriteLine($" Incorrect. The answer was... {card.Answer}");
                }
                cardCounter++;
            }
                        
            // Display finished score to the User and enter the session information into the database.
            Console.WriteLine($"\n You scored {score} points out of {cardTotal}. \n\n Press any key to return... ");

            FlashcardDTO session = new FlashcardDTO();
            session.Date = DateTime.Now;
            session.StackName = cards[0].StackName;
            session.Score = score;
            session.outOf = cardTotal;
            
            controller.InsertRow(session, "session");
            Console.ReadKey();
        }
    }
}
