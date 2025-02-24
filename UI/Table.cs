
namespace UI
{
    class Table 
    {
        private string[] header = Array.Empty<string>();
        private string[][] fields;
        public int[] spaces;

        public Table(params string[][] args)
        {
            fields = new string[args.Length][];
            for (int i = 0; i < args.Length; i++)
            {
                if (i == 0)
                {
                    header = args[i];
                }
                fields[i] = args[i];
            }

            spaces = new int[header.Length];
        }

        public void InitializeTable()
        {
            List<string> columnValues = new List<string>();
            int[] spaces = GetSpaces();

            // Inicia o header
            WriteHeader(spaces, header);

            // Inicia os campos de dados
            RemoveFirstField();
            WriteDataRows(spaces, fields);
        }

        private int[] GetSpaces()
        {
            List<string> columnValues = new List<string>();
            for (int j = 0; j < header.Length; j++)
            {
                for (int i = 0; i < fields.Length; i++)
                {
                    columnValues.Add(fields[i][j]);
                }
                int BiggestCharacterCount = GetBiggestCharacterCount(columnValues);
                System.Console.WriteLine(BiggestCharacterCount);
                spaces[j] = BiggestCharacterCount;
            }

            return spaces;
        }

        private void RemoveFirstField()
        {
            string[][] data = new string[fields.Length - 1][];
            Array.Copy(fields, 1, data, 0, fields.Length - 1);
            fields = data;

        }
        private int GetBiggestCharacterCount(List<string> field)
        {
            int biggestCharacterCount = 0;
            for (int i = 0; i < field.Count; i++)
            {
                int numberOne = field[i].Length;
                biggestCharacterCount = Math.Max(biggestCharacterCount, numberOne);
            }
            return biggestCharacterCount;
        }

        private void WriteHeader(int[] spaces, params string[] values)
        {
            WriteRow(true, spaces, values);
        }

        private void WriteDataRows(int[] spaces, params string[][] rowsOfData)
        {
            foreach (string[] rowOfData in rowsOfData)
            WriteRow(false, spaces, rowOfData);
        }

        private void WriteRow(bool header, int[] amountsOfSpace, params string[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                if (header)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                }
                Console.Write(values[i].PadRight(amountsOfSpace[i] + 1));
                Console.ResetColor();
            }
            Console.WriteLine();
        }
    } 

}