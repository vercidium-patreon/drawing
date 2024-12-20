using SkiaSharp;

namespace drawing;

public unsafe partial class Client
{
    public void Draw()
    {
        // Colours
        var red = new SKColor(255, 0, 0);
        var green = new SKColor(0, 255, 0);
        var blue = new SKColor(0, 0, 255);


        // Dark blue background
        bitmap.FillRectangle(new SKRect(0, 0, WindowWidth, WindowHeight), new SKColor(5, 10, 35));


        // Blue rectangle
        bitmap.FillRectangle(new SKRect(100, 100, 400, 400), blue);


        // Red sin wave
        for (int x = 0; x < WindowWidth; x++)
        {
            var sin = MathF.Sin(x / (float)WindowWidth * MathF.PI * 2);
            var y = (sin + 1) / 2 * WindowHeight;
            bitmap.DrawPixel(x, (int)y, red);
        }


        // Raw canvas drawing
        bitmap.canvas.DrawCircle(new SKPoint(400, 400), 80, new SKPaint()
        {
            Color = green,
            IsStroke = true,
            IsAntialias = true,
            StrokeWidth = 8
        });
    }

    public int WindowWidth => window.Size.X;
    public int WindowHeight => window.Size.Y;
}
