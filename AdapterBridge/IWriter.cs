namespace AdapterBridge
{
    public interface IWriter
    {
        void Write<T>(string key, T data);

        //void Flush();
    }

    public interface IReader
    {
        T Read<T>(string key);
    }

    public interface IReadWriteCreator
    {
        IReader ReadFrom(string path);

        IWriter WriteTo(string path);
    }
}
