namespace Multitasking;

internal class _07_Lock
{
	static int Counter = 0;
	static object LockObject = new object();

	static void Main(string[] args)
	{
		for(int i = 0; i < 50; i++)
		{
			Task.Run(CounterIncrement);
		}
		Console.ReadLine();
	}

	static void CounterIncrement()
	{
		for (int i = 0; i < 100; i++)
		{
			lock (LockObject)
			{
				Counter++;
				Console.WriteLine(Counter);
			}

			Monitor.Enter(LockObject);
			Counter++;
            Console.WriteLine(Counter);
			Monitor.Exit(LockObject);
        }
	}
}
