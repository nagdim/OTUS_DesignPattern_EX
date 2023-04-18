using System;
using System.Collections;

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

    public class MergeSort : IVariantToSort
    {
        public void Sort(int[] data)
        {
            SortCore(data, 0, data.Length - 1);
        }

        private void SortCore(int[] data, int lo, int hi)
        {
            if (hi <= lo)
                return;

            int mid = lo + (hi - lo) / 2;
            SortCore(data, lo, mid);
            SortCore(data, mid + 1, hi);

            var buf = new int[data.Length];

            Array.Copy(data, buf, data.Length);

            for (int k = lo; k <= hi; k++)
                buf[k] = data[k];

            int i = lo, j = mid + 1;
            for (int k = lo; k <= hi; k++)
            {
                if (i > mid)
                {
                    data[k] = buf[j];
                    j++;
                }
                else if (j > hi)
                {
                    data[k] = buf[i];
                    i++;
                }
                else if (buf[j] < buf[i])
                {
                    data[k] = buf[j];
                    j++;
                }
                else
                {
                    data[k] = buf[i];
                    i++;
                }
            }
        }
    }

    public interface IVariantToSort
    {
        void Sort(int[] data);
    }
}
