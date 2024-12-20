namespace drawing;

public unsafe class Texture : IDisposable
{
    public uint handle;
    public string name;
    public const TextureTarget TARGET = TextureTarget.Texture2D;

    public int width;
    public int height;

    public Texture(int width, int height)
    {
        this.width = width;
        this.height = height;

        handle = Gl.GenTexture();

        Bind();
        Gl.TexImage2D(TARGET, 0, InternalFormat.Rgba8, (uint)width, (uint)height, 0, PixelFormat.Bgra, PixelType.UnsignedByte, null);
        Gl.TexParameter(TARGET, TextureParameterName.TextureMinFilter, (int)GLEnum.Nearest);
        Gl.TexParameter(TARGET, TextureParameterName.TextureMagFilter, (int)GLEnum.Nearest);
        Gl.TexParameter(TARGET, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
        Gl.TexParameter(TARGET, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
        Unbind();
    }

    public void SetData(Bitmap bitmap)
    {
        Bind();
        Gl.TexSubImage2D(TextureTarget.Texture2D, 0, 0, 0, (uint)width, (uint)height, PixelFormat.Bgra, PixelType.UnsignedByte, (void*)bitmap.GetPixels());
        Unbind();
    }

    public void Bind() => Gl.BindTexture(TARGET, handle);
    public void Unbind() => Gl.BindTexture(TARGET, 0);

    public virtual void Dispose()
    {
        Gl.DeleteBuffer(handle);
        handle = 0;
    }
}