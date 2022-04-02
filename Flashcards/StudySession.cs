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
            List<Flashcard> cards = FlashcardController.GetCards();

            int score = 0;
            int cardNum = 1;
            int cardTotal = cards.Count();

            Console.Clear();
            Console.WriteLine("Studying the Stack {0}", "Beginners");
            
            foreach (Flashcard card in cards)
            {
                Console.WriteLine("\n Viewing card {0} of {1}", cardNum, cardTotal);
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
            Console.ReadKey();
        }
    }
}
