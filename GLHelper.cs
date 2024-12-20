namespace drawing;

internal unsafe class GLHelper
{
    public static WindowVertexBuffer windowBuffer;

    public static void DrawWindowBuffer()
    {
        if (windowBuffer == null)
            InitWindowBuffer();

        windowBuffer.BindAndDraw();
    }

    static void InitWindowBuffer()
    {
        windowBuffer = new WindowVertexBuffer();

        var data = (WindowVertex*)Allocator.Alloc(WindowVertexBuffer.VERTEX_COUNT * Marshal.SizeOf<WindowVertex>());

        float pS = -1; // Position Start
        float pE = 1; // Position End
        float tS = 0; // TexCoord Start
        float tE = 1; // TexCoord End

        var write = data;

        *write++ = new WindowVertex(pS, pE, tS, 1.0f - tE); // Top left        ___
        *write++ = new WindowVertex(pS, pS, tS, 1.0f - tS); // Bottom left     | /|
        *write++ = new WindowVertex(pE, pE, tE, 1.0f - tE); // Top right       |/_|
        *write++ = new WindowVertex(pE, pS, tE, 1.0f - tS); // Bottom right

        windowBuffer.BufferData(data);

        Allocator.Free(ref data);
    }
}
