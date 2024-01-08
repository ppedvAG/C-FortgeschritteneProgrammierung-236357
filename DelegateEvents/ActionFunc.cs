using System.Runtime.CompilerServices;

namespace DelegateEvents;

internal class ActionFunc
{
	static void Main(string[] args)
	{
		//Action, Func: Vorgegebene Delegates die an vielen Stellen in C# vorkommen
		//z.B.: TPL, Linq, Reflection, ...
		//-> Parameter, um das Objekt/die Methode selbstständig konfigurieren zu können
		//Essentiell für die fortgeschrittene Programmierung
		//Können alles, was im vorherigen Teil vorgekommen ist


		//Action: Delegate mit void als Rückgabetyp und bis zu 16 Parametern
		//Die Action empfängt die Parameter als Generics
		Action<string> vorstellung = Program.VorstellungEN;
		vorstellung += Program.VorstellungDE;
		vorstellung("Max");
		vorstellung?.Invoke("Tim");

		Action<int, int> action = Addiere;
		action += Subtrahiere;
		action?.Invoke(4, 5);

		//Verwendung: Parameter bei Methoden um Methoden konfigurierbar zu machen
		DoAction10Times(4, 8, Addiere);
		DoAction10Times(4, 8, Subtrahiere);
		DoAction10Times(4, 8, action);

		//Praktische Beispiele
		List<int> intList = Enumerable.Range(0, 100).ToList();
		intList.ForEach(o => Console.WriteLine(o * 2)); //Jedes Element der Liste mal 2 ausgeben
		intList.ForEach(Console.WriteLine); //Die ForEach Methode erwartet eine Funktion mit void als Rückgabetyp und int als Parameter
											//intList.ForEach(intList.Add);


		//Func: Delegate mit einem Generic als Rückgabetyp und bis zu 16 Parametern
		//WICHTIG: Das letzte Generic ist immer der Rückgabetyp
		Func<int, int, double> func = Multipliziere;
		double d = func(3, 5);
		double? d2 = func?.Invoke(3, 5); //?.Invoke könnte null sein, wenn f null ist
		double d3 = func?.Invoke(3, 5) ?? double.NaN;

        Console.WriteLine(DoFuncSum10Times(3, 6, Multipliziere));
		Console.WriteLine(DoFuncSum10Times(3, 6, Dividiere));
		Console.WriteLine(DoFuncSum10Times(3, 6, func));

		//Praktische Beispiele
		intList.Where(TeilbarDurch2);

		bool TeilbarDurch2(int x) => x % 2 == 0;

		//Anonyme Methoden: Methoden, für die keine separate Methode angelegt wird
		func += delegate (int x, int y) { return x + y; }; //Anonyme Methode
		 
		func += (int x, int y) => { return x + y; }; //Kürzere Form
		 
		func += (x, y) => { return x - y; };
		 
		func += (x, y) => (double) x / y; //Kürzeste, häufigste Form

		//Anonyme Methoden bei DoAction/DoFunc
		DoAction10Times(8, 4, (o, u) => Console.WriteLine(o + u));
	}

	//static IEnumerable<int> Where(List<int> list, Func<int, bool> func)
	//{
	//	foreach (int i in list)
	//		if (func(i))
	//			yield return i;
	//}

	#region Action
	private static void Addiere(int x, int y) => Console.WriteLine(x + y);

	private static void Subtrahiere(int x, int y) => Console.WriteLine(x - y);

	static void DoAction10Times(int x, int y, Action<int, int> action)
	{
		for (int i = 0; i < 10; i++)
			action?.Invoke(x, y); //Hier wird nur die Action ausgeführt
	}
	#endregion

	#region Func
	public static double Multipliziere(int x, int y) => x * y;

	public static double Dividiere(int x, int y) => (double) x / y;

	public static double DoFuncSum10Times(int x, int y, Func<int, int, double> func)
	{
		double sum = 0;
		for (int i = 0; i < 10; i++)
			sum += func?.Invoke(x, y) ?? double.NaN;
		return sum;
	}
	#endregion
}