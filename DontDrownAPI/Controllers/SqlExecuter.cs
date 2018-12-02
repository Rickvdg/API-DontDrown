using DontDrownAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DontDrownAPI.Controllers
{
    /*  
    *    ===================
    *    All SELECT querries
    *    ===================
    */
    public static class SqlExecuter
    {
        public static List<VraagItem> GetQuestionsGame(SqlConnection connection, long amount, long lvl)
        {
            return GetQuestions(
                connection,
                $"SELECT top({amount}) id,type_id,vraag,hint,minlevel,maxlevel FROM Vragen WHERE minlevel < {lvl} AND maxlevel >= {lvl} AND active=true ORDER BY NEWID()"
            );
        }

        public static List<VraagItem> GetQuestionsAdmin(SqlConnection connection)
        {
            return GetQuestions(
                connection,
                "SELECT id,type_id,vraag,hint,minlevel,maxlevel FROM Vragen"
            );
        }

        public static VraagItem GetQuestionAdmin(SqlConnection connection, long id)
        {
            try
            {
                return GetQuestions(
                    connection,
                    $"SELECT id,type_id,vraag,hint,minlevel,maxlevel FROM Vragen WHERE id={id} AND active=true"
                ).First();
            }
            catch
            {
                return null;
            }
        }

        private static List<VraagItem> GetQuestions(SqlConnection connection, string command)
        {
            List<VraagItem> items = new List<VraagItem>();
            SqlCommand cmd = new SqlCommand
            {
                CommandText = command,
                CommandType = System.Data.CommandType.Text,
                Connection = connection
            };

            using (connection)
            {
                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                SqlDataReader reader2;

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var vraagItem = new VraagItem()
                        {
                            Id = reader.GetInt32(0),
                            Type = (VraagItem.VraagType)reader.GetInt32(1),
                            Vraag = reader.GetString(2),
                            Hint = reader.GetString(3),
                            MinLevel = reader.GetInt32(4),
                            MaxLevel = reader.GetInt32(5),
                            Active = true
                        };

                        SqlCommand cmd2 = new SqlCommand($"SELECT id,text,correctness FROM Antwoorden WHERE vraag_id = {vraagItem.Id} ORDER BY correctness")
                        {
                            CommandType = System.Data.CommandType.Text,
                            Connection = connection
                        };
                        reader2 = cmd2.ExecuteReader();

                        if (reader2.HasRows)
                        {
                            List<AntwoordItem> antwoordItems = new List<AntwoordItem>();
                            while (reader2.Read())
                            {
                                antwoordItems.Add(new AntwoordItem()
                                {
                                    Id = reader2.GetInt32(0),
                                    Waarde = reader2.GetString(1),
                                    Correctness = reader2.GetInt32(2)
                                });
                            }
                            vraagItem.Antwoorden = antwoordItems;
                        }

                        items.Add(vraagItem);
                    }
                }
                else
                {
                    Console.WriteLine("No rows were found");
                }
                reader.Close();
            }
            return items;
        }

        public static bool Login(SqlConnection connection, string username, string password)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandText = $"SELECT username FROM Accounts WHERE username = {username} AND password = {password}",
                CommandType = System.Data.CommandType.Text,
                Connection = connection
            };
            bool returnValue = false;

            using (connection)
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    returnValue = true;
                }
                else
                {
                    Console.WriteLine("Failed login attempt");
                }
                reader.Close();
            }
            return returnValue;
        }

        /*  
        *    ===================
        *    All INSERT querries
        *    ===================
        */
        public static bool InsertQuestion(SqlConnection connection, VraagItem vraagItem)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandText = $"INSERT INTO Vragen (type_id, vraag, hint, minlevel, maxlevel) VALUES(@param1,@param2,@param3,@param4,@param5)",
                CommandType = System.Data.CommandType.Text,
                Connection = connection
            };
            cmd.Parameters.Add("@param1", System.Data.SqlDbType.Int).Value = vraagItem.Type;
            cmd.Parameters.Add("@param2", System.Data.SqlDbType.VarChar, 255).Value = vraagItem.Vraag;
            cmd.Parameters.Add("@param3", System.Data.SqlDbType.VarChar, 400).Value = vraagItem.Hint;
            cmd.Parameters.Add("@param4", System.Data.SqlDbType.Int).Value = vraagItem.MinLevel;
            cmd.Parameters.Add("@param5", System.Data.SqlDbType.Int).Value = vraagItem.MaxLevel;

            using (connection)
            {
                connection.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }


        /*  
        *    ===================
        *    All DELETE querries
        *    ===================
        */
        public static bool DeleteQuestion(SqlConnection connection, long id)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandText = $"DELETE FROM Vragen WHERE id={id}",
                CommandType = System.Data.CommandType.Text,
                Connection = connection
            };

            using (connection)
            {
                connection.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
