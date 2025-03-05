using System.Drawing;

namespace Tetris.Models
{
    public enum TetrominoType
    {
        I,
        J,
        L,
        O,
        S,
        Z,
        T
    }

    public enum Direction
    {
        Left,
        Right,
        Down
    }

    public class Tetromino
    {
        public TetrominoType Type { get; }
        public Color?[,] Blocks { get; private set; }
        public Color Color { get; }
        public Point Position { get; set; }

        public Tetromino(TetrominoType type, Point position)
        {
            Type = type;

            Position = position;

            Blocks = GetBlocks(type);

            Color = TetrominoColors[type];
        }

        public Tetromino(Tetromino other)
        {
            Type = other.Type;
            Position = other.Position;
            Blocks = other.Blocks;
            Color = other.Color;
        }

        public void Rotate()
        {
            int rows = Blocks.GetLength(0);
            int cols = Blocks.GetLength(1);
            Color?[,] rotatedBlocks = new Color?[cols, rows];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    rotatedBlocks[j, rows - 1 - i] = Blocks[i, j];
                }
            }

            Blocks = rotatedBlocks;
        }

        public void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    Position = new Point(Position.X - 1, Position.Y);
                    break;
                case Direction.Right:
                    Position = new Point(Position.X + 1, Position.Y);
                    break;
                case Direction.Down:
                    Position = new Point(Position.X, Position.Y + 1);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Invalid tetromino move direction");
            }
        }

        public int GetWidth()
        {
            return Blocks.GetLength(1);
        }

        public int GetHeight()
        {
            return Blocks.GetLength(0);
        }

        private static Color?[,] GetBlocks(TetrominoType type)
        {
            var c = TetrominoColors[type];

            Color?[,] blocks;

            switch (type)
            {
                case TetrominoType.I:
                    blocks = new Color?[,] { { c }, { c }, { c }, { c } };
                    break;
                case TetrominoType.J:
                    blocks = new Color?[,] { { null, c }, { null, c }, { c, c } };
                    break;
                case TetrominoType.L:
                    blocks = new Color?[,] { { c, null }, { c, null }, { c, c } };
                    break;
                case TetrominoType.O:
                    blocks = new Color?[,] { { c, c }, { c, c } };
                    break;
                case TetrominoType.S:
                    blocks = new Color?[,] { { null, c, c }, { c, c, null } };
                    break;
                case TetrominoType.Z:
                    blocks = new Color?[,] { { c, c, null }, { null, c, c } };
                    break;
                case TetrominoType.T:
                    blocks = new Color?[,] { { c, null }, { c, c }, { c, null } };
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Invalid tetromino type");
            }

            return blocks;
        }

        private static readonly Dictionary<TetrominoType, Color> TetrominoColors = new()
        {
            { TetrominoType.L, Color.Red },
            { TetrominoType.J, Color.Green },
            { TetrominoType.Z, Color.Chocolate },
            { TetrominoType.S, Color.Blue },
            { TetrominoType.I, Color.Yellow },
            { TetrominoType.O, Color.Purple },
            { TetrominoType.T, Color.Magenta }
        };
    }
}
