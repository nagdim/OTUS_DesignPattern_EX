using System.Xml;

namespace AdapterBridge
{
    public class Program_1 : IProgramRunner
    {
        private const string c_fileRead = "f0";
        private const string c_fileWrite = "f1";

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
}
