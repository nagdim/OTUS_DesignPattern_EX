namespace ExpandableFactory
{
    public class InsertionSort : IVariantToSort
    {
        public void Sort(int[] data)
        {
            for (int current = 1; current < data.Length; current++)
            {
                for (int next = current; next > 0 && data[next - 1] > data[next]; next--)
                {
                    var tmp = data[next - 1];
                    data[next - 1] = data[next];
                    data[next] = tmp;
                }
            }
        }
    }
}
