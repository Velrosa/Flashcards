using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Models
{
    internal class Session
    {
        public int ID { get; set; }

        public DateTime Date { get; set; }

        public int Score { get; set; }

        public int outOf { get; set; }

        public string StackName { get; set; }
    }
}
