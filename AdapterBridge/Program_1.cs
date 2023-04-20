using System.Collections.Generic;
using System.Xml;

namespace AdapterBridge
{
    public class Program_1 : IProgramRunner
    {
        private const string c_fileRead = "f0";
        private const string c_fileWrite = "f1";

        //private readonly IReadWriteCreator m_creator;

        //public Program_1(IReadWriteCreator creator)
        //{
        //    m_creator = creator;
        //}

        public void Run(IReadWriteCreator creator)
        {
            var reader = creator.ReadFrom(c_fileRead);
            var data_A = reader.Read<int>("A");
            var data_B = reader.Read<int>("B");

            var data_R = data_A + data_B;

            var writer = creator.WriteTo(c_fileWrite);

            writer.Write("R", data_R);
        }
    }

    public class Program_3 : IProgramRunner
    {
        private readonly IProgramRunner m_program_1;
        private readonly IProgramRunner m_program_2;
        public Program_3(IProgramRunner program_1, IProgramRunner program_2)
        {
            m_program_1 = program_1;
            m_program_2 = program_2;
        }

        public void Run(IReadWriteCreator creator)
        {
            creator = new ProgramAdapter(creator);

            m_program_2.Run(creator);
            m_program_1.Run(creator); 
        }
    }


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
