namespace drawing;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct WindowVertex(float posX, float posY, float uvX, float uvY)
{
    float PositionX = posX;
    float PositionY = posY;
    float UVX = uvX;
    float UVY = uvY;
}

public unsafe class WindowVertexBuffer
{
    public WindowVertexBuffer()
    {
        vertexSize = (uint)Marshal.SizeOf<WindowVertex>();

        // Create a VAO
        vaoHandle = Gl.GenVertexArray();
        Gl.BindVertexArray(vaoHandle);


        // Create a VBO
        vboHandle = Gl.GenBuffer();
        Gl.BindBuffer(BufferTargetARB.ArrayBuffer, vboHandle);


        // Set up attribs
        Gl.EnableVertexAttribArray(0);
        Gl.EnableVertexAttribArray(1);

        Gl.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, vertexSize, (void*)0);
        Gl.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, vertexSize, (void*)8);


        // Clean up
        UnbindVAO();
        UnbindVBO();
    }

    public void BufferData(WindowVertex* data)
    {
        BindVBO();
        Gl.BufferData(BufferTargetARB.ArrayBuffer, VERTEX_COUNT * vertexSize, data, BufferUsageARB.StaticDraw);
        UnbindVBO();
    }

    public void BindAndDraw()
    {
        BindVAO();
        Gl.DrawArrays(PrimitiveType.TriangleStrip, 0, VERTEX_COUNT);
        UnbindVAO();
    }

    // GPU data
    public const int VERTEX_COUNT = 4;
    protected uint vertexSize;


    // Buffer handles
    uint vaoHandle;
    uint vboHandle;


    // Shortcut functions
    public void BindVAO() => Gl.BindVertexArray(vaoHandle);
    public void BindVBO() => Gl.BindBuffer(BufferTargetARB.ArrayBuffer, vboHandle);

    public static void UnbindVAO() => Gl.BindVertexArray(0);
    public static void UnbindVBO() => Gl.BindBuffer(BufferTargetARB.ArrayBuffer, 0);
}
