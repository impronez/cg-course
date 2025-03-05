using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace Tetris.Utilities
{
    public class TextRenderer : IDisposable
    {
        private Bitmap _bmp;
        private Graphics _gfx;
        private int _texture;
        private Rectangle _rectGFX;
        private bool _disposed;
        private PointF _position;

        public Font font = new Font(FontFamily.GenericMonospace, 24);

        // Конструктор нового экземпляра класса
        public TextRenderer(int width, int height)
        {
            _bmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            _gfx = Graphics.FromImage(_bmp);
            // Используем сглаживание
            _gfx.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            // Генерация текстуры
            _texture = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, _texture);

            // Свойства текстуры
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            // Создание пустой текстуры
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba,
                width, height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, IntPtr.Zero);

            _position = PointF.Empty;
        }

        // Заливка образа цветом color
        public void Clear(Color color)
        {
            _position = PointF.Empty;
            _gfx.Clear(color);
            _rectGFX = new Rectangle(0, 0, _bmp.Width, _bmp.Height);
        }

        // Выводит строку текста в точке point на растровом образе, используя шрифт font и кисть brush
        public void DrawString(string text, Brush brush)
        {
            _gfx.DrawString(text, font, brush, PointF.Empty);
        }

        public void DrawNewString(string text, Brush brush)
        {
            _position.Y += font.Height;
            _gfx.DrawString(text, font, brush, _position);
        }

        // Получает обработчик текстуры (System.Int32), который связывается с TextureTarget.Texture2D
        public int Texture
        {
            get
            {
                UploadBitmap(); // Загружаем растровые данные в текстуру
                return _texture;
            }
        }

        public void Use(TextureUnit unit)
        {
            GL.ActiveTexture(unit);  // Активируем нужный текстурный юнит
            GL.BindTexture(TextureTarget.Texture2D, Texture); // Привязываем текстуру
        }

        // Выгружает растровые данные в текстуру OpenGL
        private void UploadBitmap()
        {
            if (_rectGFX != RectangleF.Empty)
            {
                System.Drawing.Imaging.BitmapData data = _bmp.LockBits(_rectGFX,
                    System.Drawing.Imaging.ImageLockMode.ReadOnly,
                    System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                GL.BindTexture(TextureTarget.Texture2D, _texture);

                // Текстура формируется на основе растровых данных
                GL.TexSubImage2D(TextureTarget.Texture2D, 0,
                    _rectGFX.X, _rectGFX.Y, _rectGFX.Width, _rectGFX.Height,
                    PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

                // Освобождаем память, занимаемую data
                _bmp.UnlockBits(data);
                _rectGFX = Rectangle.Empty;
            }
        }

        // Освобождение ресурсов
        private void Dispose(bool manual)
        {
            if (!_disposed)
            {
                if (manual)
                {
                    _bmp.Dispose();
                    _gfx.Dispose();

                    // Освобождение текстуры
                    GL.DeleteTexture(_texture);
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~TextRenderer()
        {
            // Выводим предупреждение, если объект не был корректно освобожден
            Console.WriteLine("[Предупреждение] Есть проблемы с освобождением ресурсов: {0}.", typeof(TextRenderer));
        }
    }
}
