using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace astar
{
    //NOTE: needs optimalisation
    public class PriorityQueue<TItem, TPriority>
        where TPriority : IComparable<TPriority>
    {
        private Dictionary<TItem, TPriority> _items;

        public PriorityQueue()
        {
            _items = new Dictionary<TItem, TPriority>();
        }

        public int Count => _items.Count;

        public void Enqueue(TItem item, TPriority priority)
        {
            _items[item] = priority;
        }

        public TItem Dequeue()
        {
            var retPriority = _items.Min(x => x.Value);
            var retItem = _items.First(x => x.Value.Equals(retPriority)).Key;
            _items.Remove(retItem);
            return retItem;
        }
    }
}
