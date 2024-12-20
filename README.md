This repository demonstrates the process of:
- Using SkiaSharp to render to a bitmap
- Copying the bitmap data to an OpenGL texture
- Using a shader to render that texture to the screen

This project uses Silk.NET so it *should* be cross-platform, but I've only tested it on Windows.

Key files:
- `ClientDrawing.cs` is where you'll write your bitmap drawing code
- `Program.cs` contains the main entry point
- `Client.cs` contains the render loop