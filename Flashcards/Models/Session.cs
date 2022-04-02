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

        public string Date { get; set; }

        public string Score { get; set; }

        public string StackName { get; set; }
    }
}
