namespace drawing;

public unsafe class ShaderValue
{
    public int location;

    public ShaderValue(ShaderProgram shader, string name)
    {
        location = shader.GetUniformLocation(name);
    }

    public void Set(int v) => Gl.Uniform1(location, v);
}
