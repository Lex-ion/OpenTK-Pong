namespace OpenTK_Pong_v2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            
              using (Game game = new Game(800, 800, "Test"))
              {
                  game.Run();
              }





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
    }
}