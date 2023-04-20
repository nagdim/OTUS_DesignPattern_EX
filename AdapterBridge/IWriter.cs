namespace AdapterBridge
{
    public interface IWriter
    {
        void Write<T>(string key, T data);

        //void Flush();
    }
}
