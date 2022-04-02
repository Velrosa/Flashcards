using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using Flashcards.Models;

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
        public static List<Flashcard> GetStackSet(string stack)
        {
            List<Flashcard> tableData = new List<Flashcard>();

            using (var con = new SqlConnection(conString))
            {
                using (var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "SELECT * FROM Flashcards WHERE StackName=(@stackName)";
                    cmd.Parameters.AddWithValue("@stackName", stack);

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
        public static List<Stack> GetStack()
        {
            List<Stack> tableData = new List<Stack>();

            using (var con = new SqlConnection(conString))
            {
                using (var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "SELECT * FROM Stacks";

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                tableData.Add(new Stack
                                {
                                    Name = reader.GetString(0)
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
        public static List<Session> GetSessions()
        {
            List<Session> tableData = new List<Session>();

            using (var con = new SqlConnection(conString))
            {
                using (var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "SELECT * FROM Sessions";

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                tableData.Add(new Session
                                {
                                    ID = reader.GetInt32(0),
                                    Date = reader.GetString(1),
                                    Score = reader.GetString(2),
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
        public static void InsertRow(FlashcardDTO card, string type)
        {
            using (var con = new SqlConnection(conString))
            {
                using (var cmd = con.CreateCommand())
                {
                    con.Open();
                    if (type == "card")
                    {
                        cmd.CommandText = "INSERT INTO Flashcards (CardQuestion, CardAnswer, StackName) VALUES (@question, @answer, @stackname)";
                        cmd.Parameters.AddWithValue("@question", card.Question);
                        cmd.Parameters.AddWithValue("@answer", card.Answer);
                    }
                    else if (type == "stack")
                    {
                        cmd.CommandText = "INSERT INTO Stacks (StackName) VALUES (@stackname)";
                    }
                    else if (type == "session")
                    {
                        cmd.CommandText = "INSERT INTO Sessions (Date, Score, StackName) VALUES (@date, @score, @stackname)";
                        cmd.Parameters.AddWithValue("@date", card.Date);
                        cmd.Parameters.AddWithValue("@score", card.Score);
                    }

                    cmd.Parameters.AddWithValue("@stackname", card.StackName);

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("\nInvalid Stack Name \n\nPress any key to return... ");
                        Console.ReadKey();
                    }
                }
            }
        }
        public static void UpdateRow(FlashcardDTO card, string type)
        {
            using (var con = new SqlConnection(conString))
            {
                using (var cmd = con.CreateCommand())
                {
                    con.Open();
                    if (type == "card")
                    {
                        cmd.CommandText = "UPDATE Flashcards SET CardQuestion=(@question), CardAnswer=(@answer), StackName=(@stackName) WHERE CardID=(@id) ";
                        cmd.Parameters.AddWithValue("@id", card.ID);
                        cmd.Parameters.AddWithValue("@question", card.Question);
                        cmd.Parameters.AddWithValue("@answer", card.Answer);
                    }
                    else if (type == "stack")
                    {
                        cmd.CommandText = "UPDATE Stacks SET StackName=(@newName) WHERE StackName=(@stackName) ";
                        cmd.Parameters.AddWithValue("@newName", card.NewName);
                    }

                    cmd.Parameters.AddWithValue("@stackName", card.StackName);
                    
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("\nInvalid Stack Name. \n\nPress any key to return... ");
                        Console.ReadKey();
                    }
                }
            }
        }
        public static void DeleteRow(FlashcardDTO card, string type)
        {
            using (var con = new SqlConnection(conString))
            {
                using (var cmd = con.CreateCommand())
                {
                    con.Open();
                    if (type == "card")
                    {
                        cmd.CommandText = "DELETE FROM Flashcards WHERE CardID=(@Id)";
                        cmd.Parameters.AddWithValue("@Id", card.ID);
                    }
                    else if (type == "stack")
                    {
                        cmd.CommandText = "DELETE FROM Stacks WHERE StackName=(@stackName)";
                        cmd.Parameters.AddWithValue("@stackName", card.StackName);
                    }
                    else if (type == "session")
                    {
                        cmd.CommandText = "DELETE FROM Sessions WHERE ID=(@Id)";
                        cmd.Parameters.AddWithValue("@Id", card.ID);
                    }
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
