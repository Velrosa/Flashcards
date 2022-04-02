using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards
{
    internal class Flashcard
    {
        public int ID { get; set; }
        public string Question { get; set; }

        public string Answer { get; set; }

        public int F_ID { get; set; }
    }
}
