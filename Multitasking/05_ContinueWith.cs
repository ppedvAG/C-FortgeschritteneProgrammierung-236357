namespace Multitasking;

internal class _05_ContinueWith
{
	static void Main(string[] args)
	{
		Task<int> t = new Task<int>(Berechne);
		t.ContinueWith(vorherigerTask => Console.WriteLine(t.Result)); //2 Tasks verketten, wenn t fertig ist, wird der Folgetask gestartet
		t.Start();

		//Aufgabe: Ergebnis wenn der Task fertig ist ausgeben
		//Effekt: Task läuft im Hintergrund, und macht seinen Output wenn er fertig ist

		for (int i = 0; i < 100; i++)
		{
			Thread.Sleep(25);
			Console.WriteLine($"Main Thread: {i}");
		}

		//TaskContinuationOptions
		Task<int> t2 = new Task<int>(Berechne);
		t2.ContinueWith(e => Console.WriteLine(e.Result), TaskContinuationOptions.OnlyOnRanToCompletion); //Erfolgstask
		t2.ContinueWith(e => Console.WriteLine("Fehler"), TaskContinuationOptions.OnlyOnFaulted); //Fehlertask
		t2.ContinueWith(e => Console.WriteLine("Abgebrochen"), TaskContinuationOptions.OnlyOnCanceled); //Abbruchstask
		t2.ContinueWith(e => Console.WriteLine("T2 fertig")); //Keine Optionen: Wird immer ausgeführt
		t2.Start();

		Console.ReadKey();
	}

	public static int Berechne()
	{
		Thread.Sleep(500);
		return Random.Shared.Next();
	}
}
