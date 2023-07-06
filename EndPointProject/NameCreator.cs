namespace EndPointProject
{
    public static class NameCreator
    {
        public static string Create<T>(object tag)
        {
            return $"{typeof(T).Name.ToLower()}{(tag != null ? $"_{tag}" : string.Empty)}";
        }
    }
}
