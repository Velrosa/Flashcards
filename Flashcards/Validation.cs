using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards
{
    internal class Validation
    {        
        public static string IsStringValid(string inputString)
        {            
            while (true)
            {
                bool isValid = true;
                
                if (inputString == "MENU")
                {
                    return inputString;
                }
                
                if (String.IsNullOrEmpty(inputString))
                {
                    Console.WriteLine(" Null or Empty string is invalid.");
                    isValid = false;
                }
                foreach (char c in inputString)
                {
                    if (!Char.IsLetter(c) && c != ' ' && c != '/' && c != '?')
                    {
                        Console.WriteLine($" \"{c}\" is not a valid character.");
                        isValid = false;
                    }
                }

                if (!isValid)
                {
                    Console.Write(" Invalid entry, Please enter again: ");
                    inputString = Console.ReadLine();
                }
                else { break; }
            }
            return inputString;
        }
        
        public static string IsNumberValid(string inputString)
        {
            while (true)
            {
                bool isValid = true;
                
                if (inputString == "MENU")
                {
                    return inputString;
                }
                
                if (String.IsNullOrEmpty(inputString))
                {
                    Console.WriteLine(" Null or Empty string is invalid.");
                    isValid = false;
                }
                foreach(char c in inputString)
                {
                    if (!Char.IsDigit(c))
                    {
                        Console.WriteLine($" \"{c}\" is not a valid number.");
                        isValid = false;
                    }
                }

                if (!isValid)
                {
                    Console.Write(" Invalid entry, Please enter again: ");
                    inputString = Console.ReadLine();
                }
                else { break; }
            }
            return inputString;
        }

        public static string IsDateValid(string inputString)
        {
            while (true)
            {                
                if (inputString == "MENU")
                {
                    return inputString;
                }
                else if (inputString == "NOW")
                {
                    DateTime today = DateTime.Now;
                    return today.ToString();
                }
                
                if (DateTime.TryParse(inputString, out DateTime date))
                {
                    return date.ToString();
                }
                else
                {
                    Console.Write(" Invalid date, Please enter again (DD/MM/YY HH:MM:SS): ");
                    inputString = Console.ReadLine();
                }
            }
        }
    }
}
