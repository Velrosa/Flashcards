using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards
{
    internal class Validation
    {
        public static string Validate(string entry, string type)
        {
            int valid_num = 0;


            if (type == "number" || type == "id")
            {
                // TryParse the entry to confirm its a number, else re-enter.
                bool isNumber = int.TryParse(entry, out valid_num);
                while (!isNumber || valid_num < 0)
                {
                    if (entry == "MENU")
                    {
                        return entry;
                    }
                    Console.Write(" Invalid entry, Please enter a number: ");
                    entry = Console.ReadLine();
                    isNumber = int.TryParse(entry, out valid_num);
                }
            }

            if (type == "date")
            {
                while (true)
                {
                    if (entry == "MENU")
                    {
                        return entry;
                    }
                    else if (entry == "NOW")
                    {
                        DateTime today = DateTime.Now;
                        return today.ToString();
                    }
                    else if (DateTime.TryParse(entry, out DateTime date))
                    {
                        return date.ToString();
                    }
                    else
                    {
                        Console.Write(" Invalid date, Please enter again (DD/MM/YY HH:MM:SS): ");
                        entry = Console.ReadLine();
                    }
                }
            }
            return entry;
        }
    }
}
