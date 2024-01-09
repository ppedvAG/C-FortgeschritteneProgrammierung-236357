namespace Multitasking;

internal class _04_TaskException
{
	static void Main(string[] args)
	{
		Task t = new Task(Run);
		t.Start();

		//Exceptions werden vom Task verschluckt

		try
		{
			t.Wait();
			//Wirft AggregateException
			//Wird auch bei WaitAll, Result und im Task selbst geworfen
		}
		catch (AggregateException ex)
		{
			//AggregateException: Sammelexception für mehrere Exceptions
			foreach (Exception e in ex.InnerExceptions)
			{
                Console.WriteLine(e);
            }
		}

		Console.ReadKey();
	}

	static void Run()
	{
		throw new Exception("Ein Fehler");
	}
}
