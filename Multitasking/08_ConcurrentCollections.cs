using System.Collections.Concurrent;

namespace Multitasking;

internal class _08_ConcurrentCollections
{
	static void Main(string[] args)
	{
		ConcurrentBag<int> ints = new ConcurrentBag<int>(); //List ohne Index, kann mit Linq verarbeitet werden
		//SynchronizedCollection<int> ints; //Effektiv ConcurrentList, muss installiert werden

		ConcurrentDictionary<string, int> dict = new();
		dict.TryAdd("a", 1); //Statt Add
		dict.AddOrUpdate("a", k => 1, (k, v) => 1);
	}
}
