using System;
using System.Collections.Generic;
using System.Linq;

namespace AuthTokenProject
{
    public class BaseRepository<T> where T : class, IGameItem, new()
    {
        private int _idCounter = 0;
        private readonly Dictionary<int, T> _storages = new Dictionary<int, T>();

        public T New()
        {
            var item = new T() { ID = _idCounter++, };

            _storages[item.ID] = item;
            return item;
        }

        public T Get(int id)
        {
            if (_storages.ContainsKey(id))
                return _storages[id];

            return null;
        }
        public T Get(Predicate<T> predicate)
        {
            return _storages.Values.FirstOrDefault(x => predicate(x));
        }

        public T[] GetList(Predicate<T> predicate)
        {
            return _storages.Values.Where(s => predicate(s)).ToArray();
        }
    }
}
