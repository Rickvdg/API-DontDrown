using DontDrownAPI.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
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
                    $"SELECT * FROM testtable"
                ).First();
                //return GetQuestions(
                //    connection,
                //    $"SELECT id,type_id,vraag,hint,minlevel,maxlevel FROM Vragen WHERE id={id} AND active=true"
                //).First();
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
                CommandText = $"SELECT username FROM Accounts WHERE username = '{username}' AND password = '{password}'",
                CommandType = System.Data.CommandType.Text,
                Connection = connection
            };
            bool returnValue = false;

            using (connection)
            {
                connection.Open();

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        returnValue = true;
                    }
                    else
                    {
                        Debug.WriteLine("Failed login attempt");
                    }
                    reader.Close();
                }
                catch
                {
                    Debug.WriteLine("Login failed with an error");
                }
            }
            return returnValue;
        }

        public static List<Account> GetAccounts(SqlConnection connection, string classname)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = connection
            };

            if (!String.IsNullOrEmpty(classname))
            {
                cmd.CommandText = $"SELECT a.id, a.username, a.rol_id, r.naam, a.save_id, s.data, a.klas FROM Accounts a, Rollen r, Saves s WHERE a.rol_id = r.id AND a.save_id = s.id AND klas = {classname} ORDER BY a.rol_id";
            }
            else
            {
                cmd.CommandText = $"SELECT a.id, a.username, a.rol_id, r.naam, a.save_id, s.data, a.klas FROM Accounts a, Rollen r, Saves s WHERE a.rol_id = r.id AND a.save_id = s.id ORDER BY a.rol_id";
            }

            var returnList = new List<Account>();

            using (connection)
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var account = new Account()
                        {
                            Id = reader.GetInt32(0),
                            Username = reader.GetString(1),
                            RolId = reader.GetInt32(2),
                            Rol = reader.GetString(3),
                            SaveId = reader.GetInt32(4),
                            SaveJson = reader.GetString(5),
                            Classname = reader.SafeGetString(6)
                        };
                        returnList.Add(account);
                    }
                }
                else
                {
                    Console.WriteLine("No accounts");
                }
                reader.Close();
            }
            return returnList;
        }

        public static bool GetLevelUp(SqlConnection connection, long userID)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandText = $"SELECT data FROM Saves WHERE id = {userID}",
                CommandType = System.Data.CommandType.Text,
                Connection = connection
            };
            bool returnValue = false;

            using (connection)
            {
                connection.Open();

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        JObject json = JObject.Parse(reader.GetString(0));

                        returnValue = (bool)json["LevelUp"];
                    }
                    else
                    {
                        Debug.WriteLine("UserID not found");
                    }
                    reader.Close();
                }
                catch
                {
                    Debug.WriteLine("Finding levelup failed with an error");
                }
            }
            return returnValue;
        }

        public static string GetSaveData(SqlConnection connection, int userId)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandText = $"SELECT data FROM Saves WHERE id = {userId}",
                CommandType = System.Data.CommandType.Text,
                Connection = connection
            };
            string returnValue = String.Empty;

            using (connection)
            {
                connection.Open();

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        returnValue = reader.GetString(0);
                    }
                    else
                    {
                        Debug.WriteLine("UserID not found");
                    }
                    reader.Close();
                }
                catch
                {
                    Debug.WriteLine("Data not found");
                }
            }
            return returnValue;
        }

        public static string GetSaveData(SqlConnection connection, string username)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandText = $"SELECT data FROM Saves, Accounts WHERE username = {username} AND accounts.id = saves.id",
                CommandType = System.Data.CommandType.Text,
                Connection = connection
            };
            string returnValue = String.Empty;

            using (connection)
            {
                connection.Open();

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        returnValue = reader.GetString(0);
                    }
                    else
                    {
                        Debug.WriteLine("UserID not found");
                    }
                    reader.Close();
                }
                catch
                {
                    Debug.WriteLine("Data not found");
                }
            }
            return returnValue;
        }

        private static string SafeGetString(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            return string.Empty;
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
                //CommandText = $"INSERT INTO Vragen (type_id, vraag, hint, minlevel, maxlevel) VALUES(@param1,@param2,@param3,@param4,@param5)",
                CommandType = System.Data.CommandType.Text,
                Connection = connection
            };
            cmd.CommandText = $"BEGIN TRANSACTION" +
                $" DECLARE @DataID int;" +
                $" INSERT INTO Vragen(type_id, vraag, hint, minlevel, maxlevel)" +
                $" VALUES(@param1, @param2, @param3, @param4, @param5);" +
                $" SELECT @DataID = scope_identity();";
            foreach (var antwoord in vraagItem.Antwoorden)
            {
                cmd.CommandText += $" INSERT INTO Antwoorden(vraag_id, text, correctness)" +
                $" VALUES(@DataID, '{antwoord.Waarde}', {antwoord.Correctness});";
            }
            cmd.CommandText += $" COMMIT";

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
         *  ===================
         *  All UPDATE querries
         *  ===================
         */
        public static bool UpdateLevelUp(SqlConnection connection, long userid, bool canLevelUp)
        {
            Debug.WriteLine($"userid: {userid}, canLevelUp: {canLevelUp}");
            SqlCommand cmd = new SqlCommand
            {
                CommandText = $"UPDATE Saves SET data = JSON_MODIFY(data, '$.LevelUp', '{canLevelUp.ToString().ToLower()}') WHERE id = {userid};",
                CommandType = System.Data.CommandType.Text,
                Connection = connection
            };

            using (connection)
            {
                connection.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public static bool UpdateLevelUpPlayer(SqlConnection connection, long userid)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandText = $"UPDATE Saves SET data = JSON_MODIFY(JSON_MODIFY(data, '$.LevelUp', 'false'), '$.Level',JSON_VALUE(data, '$.Level') + 1) WHERE id = {userid};",
                CommandType = System.Data.CommandType.Text,
                Connection = connection
            };

            using (connection)
            {
                connection.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public static bool UpdateSaveData(SqlConnection connection, long userid, string jsonData)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandText = $"UPDATE Saves SET data = '{jsonData}' WHERE id = {userid} AND ISJSON('{jsonData}') > 0;",
                CommandType = System.Data.CommandType.Text,
                Connection = connection
            };

            using (connection)
            {
                connection.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }


        /*  
        *   ===================
        *   All DELETE querries
        *   ===================
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
