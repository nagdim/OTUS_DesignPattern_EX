using System;

namespace AdapterBridge
{
    public class Program_2 : IProgramRunner
    {
        private const string c_fileWrite = "f2";
        //private readonly IReadWriteCreator m_creator;

        //public Program_2(IReadWriteCreator creator)
        //{
        //    m_creator = creator;
        //}

        public void Run(IReadWriteCreator creator)
        {
            var data_A = new Random().Next(10);
            var data_B = new Random().Next(10);

            var writer = creator.WriteTo(c_fileWrite);

            writer.Write("A", data_A);
            writer.Write("B", data_B);
            //writer.Flush();
        }
    }
}
