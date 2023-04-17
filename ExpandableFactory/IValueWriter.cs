namespace ExpandableFactory
{
    public interface IValueWriter
    {
        void Write(string text);
        void Write(int[] data);
    }
}
