namespace Multitasking;

internal class _02_TaskMitReturn
{
	static void Main(string[] args)
	{
		Task<int> t = new Task<int>(Berechne);
		t.Start();

		for (int i = 0; i < 100; i++)
			Console.WriteLine($"Main Thread: {i}");

		Console.WriteLine(t.Result);
		//Problem: Blockiert den Main Thread
		//Lösungen: await oder ContinueWith

		t.Wait(); //Blockiert auch den Main Thread
		Task.WaitAll(t); //Wartet, bis alle gegebenen Tasks fertig sind
		Task.WaitAny(t); //Wartet, bis einer der gegebenen Tasks fertig ist

		Console.ReadKey();
	}

	static int Berechne()
	{
		Thread.Sleep(500);
		return Random.Shared.Next();
	}
}
