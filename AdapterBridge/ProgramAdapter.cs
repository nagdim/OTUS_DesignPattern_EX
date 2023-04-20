using System.Collections.Generic;

namespace AdapterBridge
{
    public class ProgramAdapter : IWriter, IReader, IReadWriteCreator
    {
        private readonly Dictionary<string, object> m_storage;
        private readonly IReadWriteCreator m_creator;
        private IWriter m_writer;
        private IReader m_reader;

        public ProgramAdapter(IReadWriteCreator creator)
        {
            m_storage = new Dictionary<string, object>();
            m_creator = creator;
        }

        T IReader.Read<T>(string key)
        {
            if (m_storage.Count == 0)
            {
                if (m_reader == null)
                    throw new System.Exception($"First you need to call {nameof(ReadFrom)}.");

                return (T)m_reader.Read<T>(key);
            }

            return (T)m_storage[key];
        }

        void IWriter.Write<T>(string key, T data)
        {
            if (m_writer == null)
                throw new System.Exception($"First you need to call {nameof(WriteTo)}.");

            m_writer.Write(key, data);
            m_storage[key] = data;
        }

        public IReader ReadFrom(string path)
        {
            if (m_storage.Count == 0)
                m_reader = m_creator.ReadFrom(path);

            return this;
        }

        public IWriter WriteTo(string path)
        {
            m_writer = m_creator.WriteTo(path);
            return this;
        }
    }
}
