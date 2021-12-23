
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reverser
{
    public static class ObservableCollectionExtensions
    {
        public static void AddRange<T>(this ObservableCollection<T> self, IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                self.Add(item);
            }
        }
    }
}
