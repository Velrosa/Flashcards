using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace Flashcards
{
    internal class StacksController
    {
        // Connection string to Database.
        private static string conString = ConfigurationManager.AppSettings.Get("conString");
        
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

        public static void InsertRow(Stack stack)
        {
            using (var con = new SqlConnection(conString))
            {
                using (var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "INSERT INTO Stacks (StackName) VALUES (@name)";
                    cmd.Parameters.AddWithValue("@name", stack.Name);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("\nStack Name already exists. \n\nPress any key to return... ");
                        Console.ReadKey();
                    }
                }
            }
        }
        public static void UpdateRow(Stack stack)
        {
            using (var con = new SqlConnection(conString))
            {
                using (var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "UPDATE Stacks SET StackName=(@newName) WHERE StackName=(@stackName) ";
                    cmd.Parameters.AddWithValue("@stackName", stack.Name);
                    cmd.Parameters.AddWithValue("@newName", stack.NewName);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void DeleteRow(Stack stack)
        {
            using (var con = new SqlConnection(conString))
            {
                using (var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "DELETE FROM Stacks WHERE StackName=(@stackName)";
                    cmd.Parameters.AddWithValue("@stackName", stack.Name);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
