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
                                    Question = reader.GetString(1),
                                    Answer = reader.GetString(2),
                                    StackName = reader.GetString(3)
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
                    cmd.CommandText = "INSERT INTO Flashcards (CardQuestion, CardAnswer, StackName) VALUES (@question, @answer, @stackname)";
                    cmd.Parameters.AddWithValue("@question", card.Question);
                    cmd.Parameters.AddWithValue("@answer", card.Answer);
                    cmd.Parameters.AddWithValue("@stackname", card.StackName);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("\nInvalid Stack ID. \n\nPress any key to return... ");
                        Console.ReadKey();
                    }
                }
            }
        }
        public static void UpdateRow(Flashcard card)
        {
            using (var con = new SqlConnection(conString))
            {
                using (var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "UPDATE Flashcards SET CardQuestion=(@question), CardAnswer=(@answer), StackName=(@stackName) WHERE CardID=(@id) ";
                    cmd.Parameters.AddWithValue("@id", card.ID);
                    cmd.Parameters.AddWithValue("@question", card.Question);
                    cmd.Parameters.AddWithValue("@answer", card.Answer);
                    cmd.Parameters.AddWithValue("@stackName", card.StackName);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("\nInvalid Stack ID. \n\nPress any key to return... ");
                        Console.ReadKey();
                    }
                }
            }
        }
        public static void DeleteRow(Flashcard card)
        {
            using (var con = new SqlConnection(conString))
            {
                using (var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "DELETE FROM Flashcards WHERE CardID=(@Id)";
                    cmd.Parameters.AddWithValue("@Id", card.ID);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
