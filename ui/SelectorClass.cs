   
   public class Selector
   {
    public List<string> options;
    public int position = 0;
    public int selectedPosition;
    
    bool flag = true;
    public Selector(List<string> Options)
    {
        options = Options;
    }


    public void SelectorInitialize()
    {
        while (flag)
        {
            Console.Clear();
            PrintOptions(options, position);
            ConsoleKeyInfo cki = WriteSelector();
            flag = SelectorManager(cki);
        }
    }

    void PrintOptions(List<string> options, int position)
    {
        foreach (string option in options)
        {
            if (options.IndexOf(option) == position)
            {
                Console.BackgroundColor = ConsoleColor.White;
            }

            System.Console.WriteLine("  " + option);

            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
    bool SelectorManager(ConsoleKeyInfo cki)
    {
        switch(cki.KeyChar)
            {
                case 'w':
                position -= 1;
                if (position < 0) 
                {
                position = options.Count - 1;
                }
                break;
                case 's':
                position += 1;
                if (position > options.Count - 1) 
                {
                    position = 0;
                }
                break;
                case 'e':
                flag = false;
                break;
                case ' ':
                selectedPosition = position;
                selectedPosition += 1;
                Console.Clear();
                return false;
                default:
                throw new Exception("Erro");
            }
        return true;
    }
    
    ConsoleKeyInfo WriteSelector()
    {
        Console.SetCursorPosition(0, position);
        System.Console.Write(">");
            
        ConsoleKeyInfo cki = Console.ReadKey(true);

        Console.SetCursorPosition(0, position);
        Console.BackgroundColor = ConsoleColor.Black;
        System.Console.Write(" ");

        

        return cki;

    }
   }
   