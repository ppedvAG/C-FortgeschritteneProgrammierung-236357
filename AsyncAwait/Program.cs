using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace AsyncAwait;

internal class Program
{
	static async Task Main(string[] args)
	{
		Stopwatch sw = Stopwatch.StartNew();
		//Toast();
		//Tasse();
		//Kaffee();
		//Console.WriteLine(sw.ElapsedMilliseconds); //7s

		//sw.Restart();
		//Task t1 = Task.Run(Toast);
		//Task t2 = Task.Run(Tasse).ContinueWith(x => Kaffee());
		//Task.WaitAll(t1, t2); //WaitAll blockiert -> Schlecht
		//Console.WriteLine(sw.ElapsedMilliseconds); //4s

		/////////////////////////////////////////////////////////////////////////////////////////

		//async und await
		//await blockiert nicht den Main Thread, da im Hintergrund eine State Machine aufgebaut wird mit Task.Run, ContinueWith, ...

		//Wenn eine Async Methode gestartet wird, die einen Task zurück gibt, wird diese als Task gestartet

		//Aufbauten von Async Methoden
		//async void: Kann await benutzen und ist synchron
		//async Task: Kann await benutzen und ist asynchron -> Diese Methode kann awaited werden
		//async Task<T>: Kann await benutzen und ist asynchron -> Diese Methode kann awaited werden und hat ein Ergebnis

		//sw.Restart();
		//ToastAsync(); //Diese drei Methoden werden als Tasks gestartet, und nicht awaited -> Main Thread früher fertig
		//TasseAsync();
		//KaffeeAsync();
		//Console.WriteLine(sw.ElapsedMilliseconds); //24ms

		//sw.Restart();
		//await ToastAsync(); //await: Warte darauf, das diese Methode fertig wird
		//await TasseAsync();
		//await KaffeeAsync();
		//Console.WriteLine(sw.ElapsedMilliseconds); //7s

		//sw.Restart();
		//Task toast = ToastAsync(); //Toast wird gestartet
		//Task tasse = TasseAsync(); //Tasse wird gestartet
		//await tasse; //Warte auf die Tasse, bevor der Kaffee gestartet wird
		//Task kaffee = KaffeeAsync(); //Kaffee wird gestartet
		//await kaffee;
		//await toast; //Warte auf Kaffee + Toast
		//Console.WriteLine(sw.ElapsedMilliseconds); //4s

		//sw.Restart();
		//Task<Toast> toast = ToastObjectAsync();
		//Task<Tasse> tasse = TasseObjectAsync();
		//Tasse t = await tasse; //Wenn ein Task awaited wird, der ein Ergebnis hat (Rückgabewert), kommt dieser bei await heraus
		//Task<Kaffee> kaffee = KaffeeObjectAsync(t);
		//Kaffee k = await kaffee;
		//Toast b = await toast;
		//Fruehstueck f = new Fruehstueck(b, k);
		//Console.WriteLine(sw.ElapsedMilliseconds); //4s

		//Vereinfachen
		Fruehstueck f = new Fruehstueck(await ToastObjectAsync(), await KaffeeObjectAsync(await TasseObjectAsync()));

		/////////////////////////////////////////////////////////////////////////////////////////

		//Task.Run
		//Task.Run ist selbst awaitable, dadurch können alle Methoden asynchron gemacht werden mit await
		for (int i = 0; i < 1E9; i++)
		{
            Console.WriteLine(i);
        }

		Task task = Task.Run(() =>
		{
			for (int i = 0; i < 1E9; i++)
			{
				Console.WriteLine(i);
			}
		});

		await task;

		//Task.WhenAll, Task.WhenAny
		//Selbiges wie WaitAll und WaitAny, aber awaitable

		//Aufbau
		//Task starten: Task Variable anlegen und Async Methode starten
		//(optional) Zwischenschritte
		//Auf das Ergebnis warten mit await auf den vorher gestarteten Task
	}

	#region Synchron
	static void Toast()
	{
		Thread.Sleep(4000);
        Console.WriteLine("Toast fertig");
    }

	static void Tasse()
	{
		Thread.Sleep(1500);
		Console.WriteLine("Tasse fertig");
	}

	static void Kaffee()
	{
		Thread.Sleep(1500);
		Console.WriteLine("Kaffee fertig");
	}
	#endregion

	#region Asynchron
	static async Task ToastAsync()
	{
		await Task.Delay(4000);
		Console.WriteLine("Toast fertig");
		//Hier wird kein return benötigt, weil die Rückgabe der Task selbst ist
	}

	static async Task TasseAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Tasse fertig");
	}

	static async Task KaffeeAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Kaffee fertig");
	}
	#endregion

	#region Asynchron mit Objekten
	static async Task<Toast> ToastObjectAsync()
	{
		await Task.Delay(4000);
		Console.WriteLine("Toast fertig");
		return new Toast();
	}

	static async Task<Tasse> TasseObjectAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Tasse fertig");
		return new Tasse();
	}

	static async Task<Kaffee> KaffeeObjectAsync(Tasse t)
	{
		await Task.Delay(1500);
		Console.WriteLine("Kaffee fertig");
		return new Kaffee();
	}
	#endregion
}

public class Toast { }

public class Tasse { }

public class Kaffee { }

public record Fruehstueck(Toast t, Kaffee k);