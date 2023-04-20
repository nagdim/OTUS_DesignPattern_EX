namespace AdapterBridge
{
    public interface IReadWriteCreator
    {
        IReader ReadFrom(string path);

        IWriter WriteTo(string path);
    }
}
