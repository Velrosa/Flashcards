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
        public static void Session()
        {
            // Pick a stack to study.
            UserInput.ShowTable("stack", false);
            Console.Write("Enter the name of a Stack to study: ");
            string stack = Validation.IsStringValid(Console.ReadLine());
            
            // Fetch that stacks cards to display.
            List<Flashcard> cards = FlashcardController.GetStackSet(stack);

            // initilise the score card counter and total number of cards.
            int score = 0;
            int cardNum = 1;
            int cardTotal = cards.Count();

            Console.Clear();
            
            // Display each card to the screen to be answered. Increment card counter and score accordingly.
            foreach (Flashcard card in cards)
            {
                Console.WriteLine($"\n Viewing card {cardNum} of {cardTotal} from Stack {card.StackName}");
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
                cardNum++;
            }
                        
            // Display finished score to the User and enter the session information into the database.
            Console.WriteLine($"\n You scored {score} points out of {cardTotal}. \n\n Press any key to return... ");

            FlashcardDTO session = new FlashcardDTO();
            session.Date = (DateTime.Now).ToString();
            session.StackName = cards[0].StackName;
            session.Score = score.ToString() + " out of " + cardTotal.ToString();
            
            FlashcardController.InsertRow(session, "session");
            Console.ReadKey();
        }
    }
}
