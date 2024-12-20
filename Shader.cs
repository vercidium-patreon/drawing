namespace drawing;

public class Shader
{
    public Shader(ShaderType type, string source)
    {
        // Create and compile a shader
        shaderHandle = Gl.CreateShader(type);

        Gl.ShaderSource(shaderHandle, source);
        Gl.CompileShader(shaderHandle);
    }

    public uint shaderHandle;
}
