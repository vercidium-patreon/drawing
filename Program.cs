global using static drawing.Globals;

global using System;
global using System.Runtime.InteropServices;
global using Silk.NET.OpenGL;

namespace drawing;

// This class allows every file to call Gl.DoStuff()
public static class Globals
{
    public static GL Gl;
}

public class Program
{
    static void Main(string[] args)
    {
        new Client().Run();
    }
}
