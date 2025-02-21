using System.Collections;
using System.Text;
using Microsoft.Data.Sqlite;

namespace SQLiteAPI.Interface
{
    static public class Interface 
    {
        // In here is where all the magic happens, maybe I'll separate the methods to a different static class later.
        private static string? query;
        public static string? GetValueResult;


        static public string[] DatabaseCommandHelp =
        {
            "Create, DatabaseName",
            "Delete, DatabaseName",
            "Select, DatabaseName"
        };
        static public string[] TableCommandHelp =
        {
            "Create, TableName, Specifications",
            "TableDestroy, TableName",
            "AddValue, TableName, Column, Value",
            "ViewTable, TableName"
        };
        static string? connectionString;
        static string? selectedDatabase; 


        public static void ShowHelp(string[] commandList) 
        {
            System.Console.WriteLine("Comandos: \n");

            foreach (string command in commandList)
            {
                System.Console.WriteLine(command);
            }
        }

        public static void CheckIfDatabaseExists(string filepath)
        {
            if (!File.Exists($"{filepath}.db"))
            {
                throw new Exception("Arquivo não existe");
            }
        }

        public static void DatabaseCommand(params string[] args)
        {
            switch(args[0])
            {
            case commandsDefinitions.databaseCommands.Create:
            File.OpenWrite($"{args[1]}.db");
            break;
            case commandsDefinitions.databaseCommands.Delete:
            CheckIfDatabaseExists($"{args[1]}.db");
            File.Delete($"{args[1]}.db");
            break;
            case commandsDefinitions.databaseCommands.Select:
            System.Console.WriteLine("teste");
            File.WriteAllText("selectedDatabase.txt", args[1]);

            break;
            default:
            ShowHelp(DatabaseCommandHelp);
            break;
            
            }
        }





        public static void TableCommand(params string[] args)
        {
            using (SqliteConnection conn = new SqliteConnection(connectionString))
            {
                conn.Open();
                switch (args[0])
                {
                    case commandsDefinitions.TableCommands.Create:
                    
                    query = $"CREATE TABLE @TableName (@TableSpecs)";
                    using (SqliteCommand comm = new SqliteCommand(query, conn))
                    {
                        comm.Parameters.AddWithValue("@TableName", args[1]);
                        comm.Parameters.AddWithValue("@TableSpecs", args[2]);
                        comm.ExecuteNonQuery();
                    }
                    break;
                    case commandsDefinitions.TableCommands.Destroy:
                    query = $"DROP TABLE @Table";
                    using (SqliteCommand comm = new SqliteCommand(query, conn))
                    {
                        comm.Parameters.AddWithValue("@Table", args[1]);
                        comm.ExecuteNonQuery();
                    }
                    break;
                    case commandsDefinitions.TableCommands.AddValue:
                    query = $"INSERT INTO @Table (@Fields) VALUES ('@Value')";
                    using (SqliteCommand comm = new SqliteCommand(query, conn))
                    {
                            comm.Parameters.AddWithValue("@Table", args[1]);
                            comm.Parameters.AddWithValue("@Fields", args[2]);
                            comm.Parameters.AddWithValue("@Values", args[3]);
                            comm.ExecuteNonQuery();
                    }
                    break;
                    case commandsDefinitions.TableCommands.GetValue:
                    query = $"SELECT @Column FROM @Table WHERE Id=@Id";
                    using (SqliteCommand comm = new SqliteCommand(query, conn))
                    {
                        comm.Parameters.AddWithValue("@Column", args[1]);
                        comm.Parameters.AddWithValue("@Table", args[2]);
                        comm.Parameters.AddWithValue("@Id", args[3]);


                        using (SqliteDataReader datareader = comm.ExecuteReader())
                        {
                            while (datareader.Read())
                            {
                                GetValueResult = datareader["Password"].ToString();
                            }
                        }

                    }
                    break;
                    case commandsDefinitions.TableCommands.View:
                    query = $"SELECT * FROM {args[1]}";
                    using (SqliteCommand comm = new SqliteCommand(query, conn))
                    {
                        comm.Parameters.AddWithValue("@Table", args[1]);
                        using (SqliteDataReader datareader = comm.ExecuteReader()) 
                        {
                            System.Console.WriteLine("Habits");
                            System.Console.WriteLine("------------");

                            while (datareader.Read()) 
                            {
                                foreach (string field in datareader)
                                {
                                    System.Console.WriteLine($"{field}");
                                }
                                
                            }
                            Console.ReadKey();
                        }
                    }
                    break;
                    default:
                    ShowHelp(TableCommandHelp);
                    break;
                    
                }
                
            }
        }

    }
}
