using Silk.NET.Input;
using Silk.NET.Windowing;

namespace drawing;

public unsafe partial class Client
{
    public Client()
    {
        // Create a Silk.NET window
        var options = WindowOptions.Default;
        options.API = new GraphicsAPI(ContextAPI.OpenGL, new APIVersion(3, 3));
        options.Position = new(200, 200);
        options.PreferredDepthBufferBits = 32;

        window = Window.Create(options);

        // Callback when the window is created
        window.Load += () =>
        {
            // Create an OpenGL Context
            Gl = window.CreateOpenGL();
            OnDidCreateOpenGLContext();


            // Precalculate input stuff
            inputContext = window.CreateInput();
            keyboard = inputContext.Keyboards[0];
            keyboard.KeyDown += OnKeyDown;
            keyboard.KeyUp += OnKeyUp;
        };

        window.Render += (_) => Render();

        window.Size = new(1920, 1080);
        window.FramesPerSecond = 144;
        window.UpdatesPerSecond = 144;
        window.VSync = false;

        // Initialise OpenGL and the input context
        window.Initialize();
    }

    // Silk
    IWindow window;
    IKeyboard keyboard;
    IInputContext inputContext;

    Bitmap bitmap;
    Texture texture;

    public void Run()
    {
        // Run forever
        window.Run();
    }

    void OnDidCreateOpenGLContext()
    {
        var major = Gl.GetInteger(GetPName.MajorVersion);
        var minor = Gl.GetInteger(GetPName.MinorVersion);

        var version = major * 10 + minor;
        Console.WriteLine($"OpenGL Version: {version}");
    }

    public void OnKeyDown(IKeyboard keyboard, Key key, int something)
    {
        OnKeyEvent(new KeyEvent()
        {
            IsPress = true,
            KeyCode = key,
        });
    }

    public void OnKeyUp(IKeyboard keyboard, Key key, int something)
    {
        OnKeyEvent(new KeyEvent()
        {
            IsPress = false,
            KeyCode = key,
        });
    }

    void Render()
    {
        // Prepare OpenGL
        PreRenderSetup();


        // Ensure bitmaps and textures are the same size as the window
        if (bitmap == null || bitmap.width != window.Size.X || bitmap.height != window.Size.Y)
        {
            bitmap?.Dispose();
            texture?.Dispose();

            bitmap = new(window.Size.X, window.Size.Y);
            texture = new(window.Size.X, window.Size.Y);
        }


        // Render to the bitmap
        bitmap.Prepare();

        Draw();

        bitmap.Flush();


        // Copy bitmap pixels to a texture
        texture.SetData(bitmap);


        // Render the texture
        BitmapShader.UseProgram();
        BitmapShader.tex.Set(1);

        Gl.ActiveTexture(TextureUnit.Texture1);
        texture.Bind();

        GLHelper.DrawWindowBuffer();
    }

    public void PreRenderSetup()
    {
        // Prepare rendering
        Gl.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        Gl.Disable(EnableCap.DepthTest);
        Gl.Disable(EnableCap.Blend);
        Gl.Disable(EnableCap.StencilTest);
        Gl.Disable(EnableCap.CullFace);

        // No need to clear colour here as the shader renders to the whole window every frame

        // Set the viewport to the window size
        Gl.Viewport(0, 0, (uint)window.Size.X, (uint)window.Size.Y);
    }
}