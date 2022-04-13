using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using Flashcards.Models;
using ConsoleTableExt;

namespace Flashcards
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FlashcardController.CreateDatabaseTables();
            
            // Main runtime loop.
            while (true)
            {
                UserInput userInput = new UserInput();
                userInput.MainMenu();
            }
        }
    }
}