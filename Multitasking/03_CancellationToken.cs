namespace Multitasking;

internal class _03_CancellationToken
{
	static void Main(string[] args)
	{
		CancellationTokenSource source = new CancellationTokenSource();
		CancellationToken token = source.Token;

		Task t = new Task(Run, token); //Wenn Cancel auf der Source ausgeführt wird, wird der Task abgebrochen
		t.Start();

		Thread.Sleep(500);

		source.Cancel();
	}

	static void Run()
	{
		for (int i = 0;  i < 100; i++)
		{
			Thread.Sleep(25);
            Console.WriteLine($"Side Task: {i}");
        }
	}
}
