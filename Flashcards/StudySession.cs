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
            Views.ShowTable("stack", false);
            
            Console.WriteLine("Enter the name of a Stack to study: ");
            string stack = Console.ReadLine();
            
            List<Flashcard> cards = FlashcardController.GetStackSet(stack);

            int score = 0;
            int cardNum = 1;
            int cardTotal = cards.Count();

            Console.Clear();
            
            foreach (Flashcard card in cards)
            {
                Console.WriteLine("\n Viewing card {0} of {1} from Stack {2}", cardNum, cardTotal, card.StackName);
                Console.Write("\n {0}: ", card.Question);
                string answer = Console.ReadLine();
                if (answer == card.Answer)
                {
                    Console.WriteLine(" Correct!");
                    score++;
                }
                else
                {
                    Console.WriteLine(" Incorrect. The answer was... {0}", card.Answer);
                }
                cardNum++;
            }
                        
            Console.WriteLine("\n You scored {0} points out of {1}. \n\n Press any key to return... ", score, cardTotal);

            FlashcardDTO session = new FlashcardDTO();
            session.Date = (DateTime.Now).ToString();
            session.StackName = cards[0].StackName;
            session.Score = score.ToString() + " out of " + cardTotal.ToString();
            
            FlashcardController.InsertRow(session, "session");
            Console.ReadKey();
        }
    }
}
