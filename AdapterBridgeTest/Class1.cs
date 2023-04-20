using AdapterBridge;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdapterBridgeTest
{
    public class IWriterTest
    {
        private readonly Program_1 m_program_1;
        private readonly Program_2 m_program_2;

        public IWriterTest()
        {
            m_program_1 = new Program_1();
            m_program_2 = new Program_2();
        }


        [Fact]
        public void TryRunProgram1_ReadDataFromReader_CompleteSuccess()
        {
            var mockCreator = new Mock<IReadWriteCreator>();
            var mockReader = new Mock<IReader>();
            var mockWriter = new Mock<IWriter>();

            var storageReader = new Dictionary<string, int> { { "A", 10 }, { "B", 20 } };
            var storageWriter = new Dictionary<string, int>();

            mockReader.Setup(s => s.Read<int>(It.IsIn<string>("A", "B")))
                .Returns<string>(s =>
                {
                    var data = storageReader[s];

                    storageReader.Remove(s);
                    return data;
                });

            mockCreator.Setup(s => s.ReadFrom(It.IsAny<string>()))
                .Returns(mockReader.Object);
            mockCreator.Setup(s => s.WriteTo(It.IsAny<string>()))
                .Returns(mockWriter.Object);
            //
            m_program_1.Run(mockCreator.Object);

            Assert.True(storageReader.Count == 0);
            mockReader.Verify(s => s.Read<int>(It.IsAny<string>()), Times.Exactly(2));
        }

        [Fact]
        public void TryRunProgram1_CalculateValuesAndWriteToWriter_CompleteSuccess()
        {
            var mockCreator = new Mock<IReadWriteCreator>();
            var mockReader = new Mock<IReader>();
            var mockWriter = new Mock<IWriter>();

            var storageReader = new Dictionary<string, int> { { "A", 10 }, { "B", 20 } };
            var storageWriter = new Dictionary<string, int>();

            mockReader.Setup(s => s.Read<int>(It.IsIn<string>("A", "B")))
                .Returns<string>(s =>
                {
                    return storageReader[s];
                });

            mockWriter.Setup(s => s.Write(It.IsIn<string>("R"), It.IsAny<int>()))
                .Callback<string, int>((key, data) =>
                {
                    storageWriter.Add(key, data);
                });

            mockCreator.Setup(s => s.ReadFrom(It.IsAny<string>()))
                .Returns(mockReader.Object);
            mockCreator.Setup(s => s.WriteTo(It.IsAny<string>()))
                .Returns(mockWriter.Object);

            //
            m_program_1.Run(mockCreator.Object);

            //
            mockWriter.Verify(s => s.Write(It.IsAny<string>(), It.IsAny<int>()), Times.Exactly(1));

            Assert.True(storageWriter.Count == 1);
            Assert.Contains(30, storageWriter.Values);
        }

        [Fact]
        public void TryRunProgram2_GenerateDataAndFromReader_CompleteSuccess()
        {
            var mockCreator = new Mock<IReadWriteCreator>();
            var mockReader = new Mock<IReader>();
            var mockWriter = new Mock<IWriter>();

            var storageWriter = new Dictionary<string, int>();

            mockWriter.Setup(s => s.Write(It.IsIn<string>("A", "B"), It.IsAny<int>()))
                .Callback<string, int>((key, data) =>
                {
                    storageWriter.Add(key, data);
                });

            mockCreator.Setup(s => s.ReadFrom(It.IsAny<string>()))
                .Returns(mockReader.Object);
            mockCreator.Setup(s => s.WriteTo(It.IsAny<string>()))
                .Returns(mockWriter.Object);

            //
            m_program_2.Run(mockCreator.Object);

            //
            mockReader.Verify(s => s.Read<int>(It.IsAny<string>()), Times.Never);
            mockWriter.Verify(s => s.Write(It.IsAny<string>(), It.IsAny<int>()), Times.Exactly(2));

            Assert.True(storageWriter.Count == 2);
        }

        [Fact]
        public void TryRunProgram3_Program1AndProgram2RunTimeOnes()
        {
            var mockCreator = new Mock<IReadWriteCreator>();

            var prog_1 = new Mock<IProgramRunner>();
            var prog_2 = new Mock<IProgramRunner>();

            var program_3 = new Program_3(prog_1.Object, prog_2.Object);

            program_3.Run(mockCreator.Object);

            prog_1.Verify(s => s.Run(It.IsAny<IReadWriteCreator>()), Times.Once);
            prog_2.Verify(s => s.Run(It.IsAny<IReadWriteCreator>()), Times.Once);
        }

        [Fact]
        public void TryRunProgram3_CallProgram2ThanProgram()
        {
            var mockCreator = new Mock<IReadWriteCreator>();

            var prog_1 = new Mock<IProgramRunner>();
            var prog_2 = new Mock<IProgramRunner>();

            prog_1.Setup(s => s.Run(It.IsAny<IReadWriteCreator>())).Throws(new Exception("Call prog #1"));
            prog_2.Setup(s => s.Run(It.IsAny<IReadWriteCreator>())).Throws(new Exception("Call prog #2"));

            var program_3 = new Program_3(prog_1.Object, prog_2.Object);

            try
            {
                program_3.Run(mockCreator.Object);
            }
            catch (Exception ex)
            {
                Assert.Contains("Call prog #2", ex.Message);
            }
        }

        [Fact]
        public void TryRunProgram3_CallProgram3WithAdapter()
        {
            var mockCreator = new Mock<IReadWriteCreator>();
            var mockReader = new Mock<IReader>();
            var mockWriter = new Mock<IWriter>();

            mockCreator.Setup(s => s.WriteTo(It.IsAny<string>()))
                .Returns(mockWriter.Object);

            var storageReader = new Dictionary<string, int>();
            var storageWriter = new Dictionary<string, int>();

            mockWriter.Setup(s => s.Write(It.IsAny<string>(), It.IsAny<int>()))
                .Callback<string, int>((key, data) =>
                {
                    storageWriter.Add(key, data);
                });

            new Program_3(m_program_1, m_program_2).Run(mockCreator.Object);

            Assert.True(storageWriter.Count == 3);

            Assert.Contains("R", storageWriter.Keys);
            Assert.Contains("A", storageWriter.Keys);
            Assert.Contains("B", storageWriter.Keys);

            Assert.True(storageWriter.Sum(s => s.Key == "R" ? 0 : s.Value) == storageWriter.Sum(s => s.Key == "R" ? s.Value : 0));
        }

        [Fact]
        public void TryCallAdapter_FirstCalledWriteToThanCalledWrite()
        {
            var mockCreator = new Mock<IReadWriteCreator>();
            var mockWriter = new Mock<IWriter>();

            mockCreator.Setup(s => s.WriteTo(It.IsAny<string>()))
                .Returns(mockWriter.Object);

            var adapter = new ProgramAdapter(mockCreator.Object);
            /// Assert 1
            Assert.Throws<Exception>(
                () => ((IWriter)adapter).Write("A", It.IsAny<int>()));

            /// Assert 2
            Assert.Equal(adapter.WriteTo(It.IsAny<string>()), (IWriter)adapter);

            /// Assert 3
            mockCreator.Verify(
                s => s.WriteTo(It.IsAny<string>()), Times.Once);

            /// Assert 4
            ((IWriter)adapter).Write("A", It.IsAny<int>());
            mockWriter.Verify(
                s => s.Write(It.IsAny<string>(), It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void TryCallAdapter_WriteAndReadTheSameData()
        {
            var mockCreator = new Mock<IReadWriteCreator>();
            var mockReader = new Mock<IReader>();
            var mockWriter = new Mock<IWriter>();

            mockCreator.Setup(s => s.ReadFrom(It.IsAny<string>()))
                .Returns(mockReader.Object);
            mockCreator.Setup(s => s.WriteTo(It.IsAny<string>()))
                .Returns(mockWriter.Object);

            var adapter = new ProgramAdapter(mockCreator.Object);

            var writer = adapter.WriteTo(It.IsAny<string>());

            Assert.Equal(writer, (IWriter)adapter);

            writer.Write("A", 10);
            writer.Write("B", 20);

            var reader = adapter.ReadFrom(It.IsAny<string>());

            Assert.Equal(reader, (IReader)adapter);

            Assert.Equal(reader.Read<int>("A"), 10);
            Assert.Equal(reader.Read<int>("B"), 20);
        }
    }
}
