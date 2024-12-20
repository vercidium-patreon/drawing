using SkiaSharp;

namespace drawing;

public class Bitmap
{
    SKBitmap bitmap;
    SKSurface surface;
    public SKCanvas canvas;

    public int width;
    public int height;

    public Bitmap(int width, int height)
    {
        this.width = width;
        this.height = height;

        bitmap = new(width, height);
    }

    public void Prepare()
    {
        surface = SKSurface.Create(bitmap.Info, bitmap.GetPixels(out _), bitmap.BytesPerPixel * bitmap.Width);
        canvas = surface.Canvas;
        canvas.Clear(SKColor.Empty);
    }

    public void Flush()
    {
        // Flush canvas
        canvas.Flush();

        canvas = null;
        surface = null;
    }

    public nint GetPixels() => bitmap.GetPixels();

    public void Dispose()
    {
        bitmap.Dispose();
        bitmap = null;
    }

    // Drawing commands
    public void DrawPixel(int x, int y, SKColor colour)
    {
        canvas.DrawRect(x, y, 1, 1, new SKPaint()
        {
            Color = colour,
            IsStroke = false,
            IsAntialias = false,
        });
    }

    public void FillRectangle(SKRect rect, SKColor colour)
    {
        canvas.DrawRect(rect, new SKPaint()
        {
            Color = colour,
            IsStroke = false,
            IsAntialias = false,
        });
    }

}
