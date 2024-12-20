namespace drawing;

public static class BitmapShader
{
    public static ShaderProgram shader;

    public static void Initialise()
    {
        shader = new(VertexShader, FragmentShader);

        tex = new(shader, "tex");
    }

    public static void UseProgram()
    {
        if (shader == null)
            Initialise();

        shader.UseProgram();
    }

    public static ShaderValue tex;

    public static string FragmentShader = @"
out vec4 oColor;

in vec2 vUV;

uniform sampler2D tex;

void main()
{
    oColor = texture(tex, vUV);
}
";

    public static string VertexShader = @"
layout (location = 0) in vec2 aPosition;
layout (location = 1) in vec2 aUV;

out vec2 vUV;

void main()
{
    gl_Position = vec4(aPosition, 0, 1.0);
    vUV = aUV;
}
";
}
