namespace OpenTK_Pong_v2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            

            using (Game game = new Game(800, 600, "Test"))
            {
                game.Run();
            }


        }
    }
}