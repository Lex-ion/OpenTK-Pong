namespace OpenTK_Pong_v2
{
    internal class Program
    {
        public static int Choice = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            Menu();

            //  using (Game game = new Game(800, 800, "Test"))
            //  {
            //      game.Run();
            //  }

            



            //vypisuje připomenutí zkopírování shader
            #region JsemTrouba

            for (int i = 0; i < Console.WindowWidth; i++)
            {
                if (i % 2 == 0)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(' ');
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.Write(' ');
                }

            }
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("JESTLI SE DIVÍŠ PROČ TI NEFUNGUJOU SHADERY, TAK TO PROTOŽE SI JE NEZKOPÍROVAL DO DEBUG SLOŽKY ODKUD PROGRAM KOMPILUJE");

            for (int i = 0; i < Console.WindowWidth; i++)
            {
                if (i % 2 == 0)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(' ');
                }else
                {
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.Write(' ');
                }

            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ReadKey(true);
            #endregion 
            
        }

        public static void Menu()
        {
            string[] Lines = {
               "  Hra pro dva hráče  ",
               "Hra pro jednoho hráče",
               "      Nastavení      ",
               "      Jak hrát?      ",
               "       Ukončit       "
            };

            Render();

            while(true)
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.W or ConsoleKey.UpArrow:
                    if(Choice<=0)
                    {
                        Choice=Lines.Length-1;
                    }else
                        Choice--;
                        new Thread(() => Settings.Beep(5000, 25)).Start();
                        Render();
                    break;

                case ConsoleKey.S or ConsoleKey.DownArrow:
                        if (Choice >= Lines.Length - 1)
                        {
                            Choice = 0;
                        }
                        else
                            Choice++;
                        new Thread(() => Settings.Beep(5000, 25)).Start();
                        Render();
                        break;

                    case ConsoleKey.Enter:
                        Select();
                        Render();
                        break;
            }


            void Render()
            {
                
                Console.Clear();
                for (int i = 0; i < Lines.Length; i++)
                {
                    if (Choice == i)
                    {
                        Console.Write("[ ");
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.Write(Lines[i]);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine(" ]");
                    }
                    else
                    {
                       
                        Console.WriteLine("  "+Lines[i]);
                    }
                }
            }

            void Select()
            {
                switch (Choice)
                {
                    case 0:
                        Console.Clear();
                        Console.WriteLine("Inicializace...");
                        using (Game game = new Game(800, 800, "Test"))
                        {
                            game.Run();
                        }
                        Console.WriteLine($"Hra ukončena! Skóre: {Settings.Score} \nStiskni klávesu pro návrat...");
                        Thread.Sleep(500);
                        Console.ReadKey();

                        break;

                    case 1:
                        Console.Clear();
                        Console.WriteLine("Inicializace...");
                        using (ArcadeGame game = new ArcadeGame(800, 800, "Game"))
                        {
                            game.Run();
                        }
                        Console.WriteLine($"Hra ukončena! Skóre: {Settings.Score.X} \nStiskni klávesu pro návrat...");
                        Thread.Sleep(1000);
                        Console.ReadKey();
                        break;

                    case 2:
                        Settings.Menu();
                        break;

                    case 3:
                        string[] text = SetString();
                        Console.Clear();
                        for (int i = 0; i < text.Length; i++)
                        {
                            Console.WriteLine(text[i]);
                            Thread.Sleep(25);
                        }
                        Console.ReadKey(true);

                        break;

                    case 4:
                        Environment.Exit(0);
                        break;
                }

                string[] SetString()
                {
                    string data="";

                    data += "Pálky se ovládají pomocí WSAD a šipek.#-#";
                    data += "Klávesy A a D zrychlují či zpomalují pálky, stejné platí i u šipek.#-#";
                    data += "Hru lze pozastavit pomocí klávesy ESC a ukončit pomocí klávesy END.#-#";
                    data += "#-#";
                    data += "Obtížnost hry pro jednoho hráče lze upravit v nastavení.#-#";
                    data += "   0 - arkádový mód#-#";
                    data += "   1 - lehká obtížnost#-#";
                    data += "   2 - střední obtížnost#-#";
                    data += "   3 - těžká obtížnost#-#";
                    data += "#-#";
                    data += "Stiskni klávesu pro návrat do menu...";
                    
                    

                    return data.Split("#-#");
                }
            }


        }
    }
}