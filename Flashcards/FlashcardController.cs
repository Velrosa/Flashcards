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
    internal class FlashcardController
    {
        // Connection string to Database.
        private static readonly string conString = ConfigurationManager.AppSettings.Get("conString");
        private static readonly string dbString = ConfigurationManager.AppSettings.Get("dbString");

        public static void CreateDatabaseTables()
        {
            // Creates the Database if it doesnt exist.
            using (var con = new SqlConnection(dbString))
            {
                using (var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "SELECT db_id('Flashcards')";
                    bool state = cmd.ExecuteScalar() != DBNull.Value;

                    if (!state)
                    {
                        cmd.CommandText = "CREATE DATABASE Flashcards";
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            // Check if a tables exist in the database, if not create them.
            using (var con = new SqlConnection(conString))
            {
                using (var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[Stacks]')" +
                                            "AND OBJECTPROPERTY(id, N'IsUserTable') = 1)" +
                                            "CREATE TABLE[dbo].[Stacks] (StackName VARCHAR(50) UNIQUE);";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[Flashcards]')" +
                                        "AND OBJECTPROPERTY(id, N'IsUserTable') = 1)" +
                                        "CREATE TABLE[dbo].[Flashcards] (" +
                                        "CardID INT IDENTITY(1,1) PRIMARY KEY," +
                                        "CardQuestion TEXT," +
                                        "CardAnswer TEXT," +
                                        "StackName VARCHAR(50) FOREIGN KEY REFERENCES Stacks(StackName) " +
                                        "ON DELETE CASCADE ON UPDATE CASCADE);";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[Sessions]')" +
                                        "AND OBJECTPROPERTY(id, N'IsUserTable') = 1)" +
                                        "CREATE TABLE[dbo].[Sessions] (" +
                                        "ID INT IDENTITY(1,1) PRIMARY KEY," +
                                        "Date DATETIME," +
                                        "Score INT," +
                                        "outOf INT," +
                                        "StackName VARCHAR(50) FOREIGN KEY REFERENCES Stacks(StackName) " +
                                        "ON DELETE CASCADE ON UPDATE CASCADE);";
                    cmd.ExecuteNonQuery();
                }
            }
        }
        
        
        // Provided a Model and table name, returns a list of records to be displayed.
        internal List<T> Get<T>(string table)
        {
            List<T> list = new List<T>();

            using (var con = new SqlConnection(conString))
            {
                using (var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = $"SELECT * FROM {table}";

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                int j = 0;
                                // instanciate object of the type provided
                                T obj = (T)Activator.CreateInstance(typeof(T));
                                // for each property on the model.
                                foreach (var prop in typeof(T).GetProperties())
                                {
                                    // get the value from SQL and the model propertys TYPE, add it to the Model object
                                    var value = reader.GetValue(j);
                                    var propType = prop.PropertyType;
                                    prop.SetValue(obj, Convert.ChangeType(value, propType));
                                    j++;
                                }

                                list.Add(obj);
                            }
                        }
                        else
                        {
                            Console.WriteLine(" No Rows to display.");
                        }
                    }
                }
            }
            return list;
        }
        
        // Fetchs all of a stack set to be displayed in study session.
        internal List<Flashcard> GetStackSet(string stackName)
        {
            List<Flashcard> tableData = new List<Flashcard>();

            using (var con = new SqlConnection(conString))
            {
                using (var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "SELECT * FROM Flashcards WHERE StackName=(@stackName)";
                    cmd.Parameters.AddWithValue("@stackName", stackName);

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
        
        internal List<YearlySession> GetMonthySessionData()
        {
            List<YearlySession> tableData = new List<YearlySession>();

            using (var con = new SqlConnection(conString))
            {
                using (var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "SELECT [StackName]," +
                        "ISNULL([1], 0) AS Jan," +
                        "ISNULL([2], 0) AS Feb," +
                        "ISNULL([3], 0) AS Mar," +
                        "ISNULL([4], 0) AS Apr," +
                        "ISNULL([5], 0) AS May," +
                        "ISNULL([6], 0) AS Jun," +
                        "ISNULL([7], 0) AS Jul," +
                        "ISNULL([8], 0) AS Aug," +
                        "ISNULL([9], 0) AS Sep," +
                        "ISNULL([10], 0) AS Oct," +
                        "ISNULL([11], 0) AS Nov," +
                        "ISNULL([12], 0) AS Dec " +
                        "FROM (SELECT [StackName], MONTH([Date]) AS [Month], Score FROM dbo.Sessions) AS D " +
                        "PIVOT (SUM(Score) FOR [Month] IN (" +
                        "[1], [2], [3], [4], [5], [6], [7], [8], [9], [10], [11], [12]" +
                        ")) AS P";

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                tableData.Add(new YearlySession
                                {
                                    StackName = reader.GetString(0),
                                    Jan = reader.GetInt32(1),
                                    Feb = reader.GetInt32(2),
                                    Mar = reader.GetInt32(3),
                                    Apr = reader.GetInt32(4),
                                    May = reader.GetInt32(5),
                                    Jun = reader.GetInt32(6),
                                    Jul = reader.GetInt32(7),
                                    Aug = reader.GetInt32(8),
                                    Sep = reader.GetInt32(9),
                                    Oct = reader.GetInt32(10),
                                    Nov = reader.GetInt32(11),
                                    Dec = reader.GetInt32(12)
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

        // Inserts a row into the database using the DTO and the type of data.
        internal void InsertRow(FlashcardDTO card, string type)
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
                        cmd.CommandText = "INSERT INTO Sessions (Date, Score, outOf, StackName) VALUES (@date, @score, @outOf, @stackname)";
                        cmd.Parameters.AddWithValue("@date", card.Date);
                        cmd.Parameters.AddWithValue("@score", card.Score);
                        cmd.Parameters.AddWithValue("@outOf", card.outOf);
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
        // Updates a row in the database using the DTO and the type of data.
        internal void UpdateRow(FlashcardDTO card, string type)
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
        // Deletes a row in the database using the DTO and the type of data.
        internal void DeleteRow(FlashcardDTO card, string type)
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
