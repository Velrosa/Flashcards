using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Models
{
    internal class FlashcardDTO
    {
        public int ID { get; set; }
        public string Question { get; set; }

        public string Answer { get; set; }

        public string StackName { get; set; }

        public string NewName { get; set; }

        public string Score { get; set; }

        public string Date { get; set; }
    }
}
