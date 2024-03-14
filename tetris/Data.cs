using System.Collections.Generic;

namespace tetris
{
    public class Data
    {
        // Shapes
        private static readonly int[,] I =
        {
            { 0, 0, 0, 0 },
            { 11, 11, 11, 11 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        private static readonly int[,] J =
        {
            { 1, 0, 0 },
            { 1, 1, 1 }, 
            { 0, 0, 0 },
        };
        private static readonly int[,] L =
        {
            { 0, 0, 6 },
            { 6, 6, 6 }, 
            { 0, 0, 0 }
        };
        private static readonly int[,] O =
        {
            { 14, 14 }, 
            { 14, 14 }
        };
        private static readonly int[,] S =
        {
            { 0, 2, 2 }, 
            { 2, 2, 0 },

        };
        private static readonly int[,] T =
        {
            { 0, 5, 0 },
            { 5, 5, 5 }, 
            { 0, 0, 0 },
        };
        private static readonly int[,] Z =
        {
            { 10, 10, 0 }, 
            { 0, 10, 10 },
        };
        
        // SRS Wall kicks (x, y)
        public readonly int[,,] WallKicks =
        {
            { { 0, 0 }, { -1, 0 }, { -1, 1 }, { 0, -2 }, { -1, -2 } },
            { { 0, 0 }, { 1, 0 }, { 1, -1 }, { 0, 2 }, { 1, 2 } },
            { { 0, 0 }, { 1, 0 }, { 1, 1 }, { 0, -2 }, { 1, -2 } },
            { { 0, 0 }, { -1, 0 }, { -1, -1 }, { 0, 2 }, { -1, 2 } },
        };
        
        public readonly int[,,] WallKicksI =
        {
            { { 0, 0 }, { -2, 0 }, { 1, 0 }, { -2, -1 }, { 1, 2 } },
            { { 0, 0 }, { -1, 0 }, { 2, 0 }, { -1, 2 }, { 2, -1 } },
            { { 0, 0 }, { 2, 0 }, { -1, 0 }, { 2, 1 }, { -1, -2 } },
            { { 0, 0 }, { 1, 0 }, { -2, 0 }, { 1, -2 }, { -2, 1 } },
        };
        
        public readonly List<int[,]> Shapes = new List<int[,]>() { I, J, L, O, S, T, Z }; 
        
        public char GetLetterRepresentation(int code)
        {
            var letter = ' ';
            
            switch (code)
            {   
                case 11:
                    letter = 'I';
                    break;
                case 1:
                    letter = 'J';
                    break;
                case 6:
                    letter = 'L';
                    break;
                case 14:
                    letter = 'O';
                    break;
                case 2:
                    letter = 'S';
                    break;
                case 5:
                    letter = 'T';
                    break;
                case 10:
                    letter = 'Z';
                    break;
            }

            return letter;
        }
    }
}