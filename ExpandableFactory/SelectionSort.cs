namespace ExpandableFactory
{
    public class SelectionSort : IVariantToSort
    {
        public void Sort(int[] data)
        {
            for (int current = 0; current < data.Length - 1; ++current)
            {
                int min = current;
                for (int next = current + 1; next < data.Length; ++next)
                {
                    if (data[next].CompareTo(data[min]) < 0)
                        min = next;
                }

                var swap = data[current];
                data[current] = data[min];
                data[min] = swap;
            }
        }
    }
}
