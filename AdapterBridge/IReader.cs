﻿namespace AdapterBridge
{
    public interface IReader
    {
        T Read<T>(string key);
    }
}
