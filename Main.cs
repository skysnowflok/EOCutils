using System.Collections;
using System.Linq.Expressions;
using System.Text;
using Microsoft.Data.Sqlite;


namespace Commands
{
    static public class DatabaseCommands
    {
        // In here is where all the magic happens, maybe I'll separate the methods to a different static class later.
        
        public static string? connectionString;
        static string? selectedDatabase;
        static string DatabaseName; 


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

        public static void CreateDatabase(params string[] args) 
        {
            DatabaseName = args[0];
            using (FileStream fs = File.Create($@"SQLITEAPI\{args[0]}.db"))
            {
                        // Ensure the file is created and closed properly
            }
        }
        public static void DeleteDatabase(params string[] args) 
        {
            DatabaseName = args[0];
            CheckIfDatabaseExists($@"SQLITEAPI\{args[0]}.db");
            File.Delete($@"SQLITEAPI\{args[0]}.db");
        }
        public static void SelectDatabase(params string[] args) 
        {
            DatabaseName = args[0];

            File.WriteAllText(@"SQLITEAPI\selectedDatabase.txt", args[0]);
            selectedDatabase = $@"SQLITEAPI\{args[1]}.db";
            connectionString = $@"Data Source={selectedDatabase};";
            System.Console.WriteLine($"Connection string set to: {connectionString}");
        }


            
    }
    static public class TableCommands 
    {
        public static string? GetValueResult;
        private static string query;
        public static void ValidateCommands(string connectionString, params string[] args)
        {
            if (connectionString == null)
            {
                throw new Exception("Connection string is null. Please select a database first.");
            }

            if (args == null || args.Length == 0)
            {
                return;
            }

        }

        public static void CreateTable(params string[] args)
        {
            using (SqliteConnection conn = new SqliteConnection(DatabaseCommands.connectionString))
            {
                string query = $"CREATE TABLE {args[0]} ({args[1]})";

                using(SqliteCommand comm = new SqliteCommand(query, conn))
                {
                    comm.ExecuteNonQuery();
                }
            }
        }
        public static void DestroyTable(params string[] args)
        {
            using (SqliteConnection conn = new SqliteConnection(DatabaseCommands.connectionString))
            {
                query = $"DROP TABLE {args[0]}";

                using(SqliteCommand comm = new SqliteCommand(query, conn))
                {
                    comm.ExecuteNonQuery();
                }
            }
        }
        public static void AddValues(params string[] args)
        {
            using (SqliteConnection conn = new SqliteConnection(DatabaseCommands.connectionString))
            {
                query = $"INSERT INTO {args[0]} ({args[1]}) VALUES ({args[2]})";

                using(SqliteCommand comm = new SqliteCommand(query, conn))
                {
                    comm.ExecuteNonQuery();
                }
            }
        }
        public static void GetValue(params string[] args)
        {
            using (SqliteConnection conn = new SqliteConnection(DatabaseCommands.connectionString))
            {
                query = $"SELECT {args[1]} FROM {args[2]} WHERE Id = @Id";
                using (SqliteCommand comm = new SqliteCommand(query, conn))
                {
                    comm.ExecuteNonQuery();
                    using (SqliteDataReader datareader = comm.ExecuteReader())
                    {
                        while (datareader.Read())
                        {
                            GetValueResult = datareader[args[1]].ToString();
                        }
                    }
                }
            }
        }

        public static void View(params string[] args)
        {
            string TableName = args[0];

            using (SqliteConnection conn = new SqliteConnection(DatabaseCommands.connectionString))
            {
                string query = $"SELECT * FROM {TableName}";
                using (SqliteCommand comm = new SqliteCommand(query, conn))
                {
                    using (SqliteDataReader datareader = comm.ExecuteReader())
                    {
                        
                        string[][] data = FormatValues(datareader);

                        UI.Table tabela = new UI.Table(data);

                        tabela.InitializeTable();
                        
                        System.Console.WriteLine("Pressione qualquer tecla para continuar...");
                        Console.ReadKey();
                    }
                }
            }
        }

        public static string[][] FormatValues(SqliteDataReader datareader)
        {
            List<string[]> rows = new List<string[]>();
            while (datareader.Read())
            {
                string[] row = new string[datareader.FieldCount];
                for (int i = 0; i < datareader.FieldCount; i++)
                {
                    row[i] = datareader.GetValue(i).ToString();
                }
                rows.Add(row);
            }
            string[][] TableData = rows.ToArray();
            return TableData;
        }
        
    }

}


