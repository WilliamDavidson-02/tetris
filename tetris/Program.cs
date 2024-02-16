using System;

namespace tetris
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var game = new Game();
            
            game.Start();

            while (!game.GameOver)
            {
                if (Console.KeyAvailable)
                {
                    var input = Console.ReadKey(true);

                    switch (input.Key)
                    {
                        case ConsoleKey.UpArrow:
                        case ConsoleKey.DownArrow:
                        case ConsoleKey.LeftArrow:
                        case ConsoleKey.RightArrow:
                            if (!game.Paused)
                                game.Input(input.Key);
                            break;

                        case ConsoleKey.P:
                            if (game.Paused)
                                game.Resume();
                            else
                                game.Pause();
                            break;

                        case ConsoleKey.Escape:
                            game.Stop();
                            return;
                    }
                }
            }
        }
    }
}