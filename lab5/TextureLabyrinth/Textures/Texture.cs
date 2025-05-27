using OpenTK.Graphics.OpenGL;
using StbImageSharp;

namespace TextureLabyrinth.Textures;

public class Texture
{
    private readonly int _handle;

    public static Texture LoadFromFile(string path)
    {
        var handle = GL.GenTexture();

        GL.ActiveTexture(TextureUnit.Texture0);
        GL.BindTexture(TextureTarget.Texture2D, handle);

        using (Stream stream = File.OpenRead(path))
        {
            var image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0,
                PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);
        }

        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapNearest);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMaxAnisotropy, 0);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMinFilter.Linear);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
        // анизотропная фильтрация
        GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

        return new Texture(handle);
    }

    private Texture(int handle)
    {
        _handle = handle;
    }

    public void Use()
    {
        GL.ActiveTexture(TextureUnit.Texture0);
        GL.BindTexture(TextureTarget.Texture2D, _handle);
    }
}