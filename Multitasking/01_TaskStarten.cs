namespace Multitasking;

internal class _01_TaskStarten
{
	static void Main(string[] args)
	{
		Task t = new Task(Run);
		t.Start();

		for (int i = 0; i < 100; i++)
			Console.WriteLine($"Main Thread: {i}");

		//Alle Tasks werden abgebrochen, wenn die Vordergrundthreads fertig sind
		//Der Main Thread ist ein Vordergrundthread

		Task t2 = Task.Run(Run); //Task anlegen und gleich starten

		Console.ReadKey(); //Den Main Thread blockieren, passiert in der UI automatisch
	}

	static void Run()
	{
		for (int i = 0; i < 100; i++)
            Console.WriteLine($"Side Task: {i}");
    }
}