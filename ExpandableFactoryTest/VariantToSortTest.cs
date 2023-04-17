using ExpandableFactory;
using ExpandableFactory.IoC;
using ExpandableFactory.IoC.Exceptions;
using Moq;
using System.Linq;
using Xunit;

namespace ExpandableFactoryTest
{
    public class VariantToSortTest
    {
        private readonly Mock<IValueReader> m_reader;
        private readonly Mock<IValueWriter> m_writer;

        public VariantToSortTest()
        {
            m_reader = new Mock<IValueReader>();
            m_writer = new Mock<IValueWriter>();

            IOC.Register("ReadFromFile", s => m_reader.Object);
            IOC.Register("WriteToFile", s => m_writer.Object);

            IOC.Register("SelectionSort", s => new SelectionSort());
            IOC.Register("InsertionSort", s => new InsertionSort());
            IOC.Register("MergeSort", s => new MergeSort());

            IOC.Register("Read_Sort_Write",
                (args) =>
                {
                    var instance = IOC.Resolve<IVariantToSort>((string)args[0]);

                    var sort_reaser = IOC.Resolve<IValueReader>("ReadFromFile");
                    var data = sort_reaser.Read();

                    instance.Sort(data);

                    var writer = IOC.Resolve<IValueWriter>("WriteToFile");

                    writer.Write((string)args[0]);
                    writer.Write(data);
                    return data;
                });
        }

        [Fact]
        public void TrySort_ReadAndWriteData()
        {
            m_reader.Reset();
            m_writer.Reset();

            IOC.Resolve<int[]>("Read_Sort_Write", "SelectionSort");

            m_reader.Verify(s => s.Read(), Times.Once);
            m_writer.Verify(s => s.Write(It.IsAny<string>()), Times.Once);
            m_writer.Verify(s => s.Write(It.IsAny<int[]>()), Times.Once);
        }

        [Fact]
        public void TrySort_ResolveUndefinedImplementation()
        {
            Assert.Throws<ResolveContainerException>(() => IOC.Resolve<int[]>("Read_Sort_Write", "_"));
        }

        [Fact]
        public void TrySort_SelectionInsertMerge_OrderedSequence()
        {
            TrySort_OrderedSequence("SelectionSort");
            TrySort_OrderedSequence("InsertionSort");
            TrySort_OrderedSequence("MergeSort");
        }

        private void TrySort_OrderedSequence(string variantSort)
        {
            m_reader.Reset();
            m_writer.Reset();

            int[] inputArray = new[] { 5, 1, 3, 0, 6, 4, 7, 8, 9 };

            string text = null;
            int[] results = null;

            m_reader.Setup(x => x.Read())
                .Returns(inputArray);

            m_writer.Setup((x) => x.Write(It.IsAny<string>()))
                .Callback<string>(s => text = s);
            m_writer.Setup((x) => x.Write(It.IsAny<int[]>()))
                .Callback<int[]>(s => results = s);

            IOC.Resolve<int[]>("Read_Sort_Write", variantSort);

            Assert.True(results != null);
            Assert.True(text == variantSort);
            Assert.Equal(results, inputArray.OrderBy(s => s));
        }
    }
}
