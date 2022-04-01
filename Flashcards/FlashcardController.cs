using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace Flashcards
{
    internal class FlashcardController
    {
        // Connection string to Database.
        private static string conString = ConfigurationManager.AppSettings.Get("conString");

        // Fetchs all database information back to be displayed elsewhere.
        public static List<Flashcard> GetCards()
        {
            List<Flashcard> tableData = new List<Flashcard>();

            using (var con = new SqlConnection(conString))
            {
                using (var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "SELECT * FROM Flashcards";

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                tableData.Add(new Flashcard
                                {
                                    ID = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                });
                            }
                        }
                        else
                        {
                            Console.WriteLine(" No Rows to Display.");
                        }
                    }
                }
            }
            return tableData;
        }

        public static void InsertRow(Flashcard card)
        {
            using (var con = new SqlConnection(conString))
            {
                using (var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "INSERT INTO Flashcards (CardName, StackID) VALUES (@name, @f_id)";
                    cmd.Parameters.AddWithValue("@name", card.Name);
                    cmd.Parameters.AddWithValue("@f_id", card.F_ID);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Invalid Stack ID. \n\nPress any key to return... ");
                        Console.ReadKey();
                    }
                }
            }
        }
    }
}
