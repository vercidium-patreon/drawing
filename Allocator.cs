namespace drawing;

public static unsafe class Allocator
{
    public static void* Alloc(int byteCount) => NativeMemory.Alloc((nuint)byteCount);

    public static void Free<T>(ref T* data) where T : unmanaged
    {
        if (data != null)
            NativeMemory.Free(data);

        data = null;
    }
}
