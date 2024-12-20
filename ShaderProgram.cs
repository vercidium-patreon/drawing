namespace drawing;

public unsafe class ShaderProgram
{
    public static string VERSION = "#version 330\n";

    public ShaderProgram(string vertexShaderSource, string fragmentShaderSource)
    {
        // Create shaders
        var vertexShader = new Shader(ShaderType.VertexShader, VERSION + vertexShaderSource);
        var fragmentShader = new Shader(ShaderType.FragmentShader, VERSION + fragmentShaderSource);


        // Create a shader program
        programHandle = Gl.CreateProgram();


        // Attach the shaders to the program
        Gl.AttachShader(programHandle, vertexShader.shaderHandle);
        Gl.AttachShader(programHandle, fragmentShader.shaderHandle);

        Gl.LinkProgram(programHandle);

        Gl.DetachShader(programHandle, vertexShader.shaderHandle);
        Gl.DetachShader(programHandle, fragmentShader.shaderHandle);
    }

    public virtual void UseProgram()
    {
        Gl.UseProgram(programHandle);
    }

    public int GetUniformLocation(string name) => Gl.GetUniformLocation(programHandle, name);

    protected uint programHandle;
}
