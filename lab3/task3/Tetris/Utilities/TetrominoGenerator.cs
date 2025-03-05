using System.Drawing;
using Tetris.Models;

namespace Tetris.Utilities
{
    public class TetrominoGenerator
    {
        public static Tetromino Get()
        {
            Random random = new Random();
            int index = random.Next(Enum.GetValues(typeof(TetrominoType)).Length);

            TetrominoType type = (TetrominoType)index;

            Point position = new Point(GameBoard.Width / 2 - 1, 0);

            return new Tetromino(type, position);
        }
    }
}
