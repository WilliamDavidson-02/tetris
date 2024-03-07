using System;

namespace tetris
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.CursorVisible = false;
            
            var game = new Game();
            game.Start();
            

            while (!game.GameOver)
            {
                game.Input();
              // Game loop  
            }
        }
    }
}