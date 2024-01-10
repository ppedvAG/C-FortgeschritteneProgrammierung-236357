using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace LinqErweiterungsmethoden;

internal class Program
{
	static void Main(string[] args)
	{
		List<Fahrzeug> fahrzeuge = new List<Fahrzeug>
		{
			new Fahrzeug(251, FahrzeugMarke.BMW),
			new Fahrzeug(274, FahrzeugMarke.BMW),
			new Fahrzeug(146, FahrzeugMarke.BMW),
			new Fahrzeug(208, FahrzeugMarke.Audi),
			new Fahrzeug(189, FahrzeugMarke.Audi),
			new Fahrzeug(133, FahrzeugMarke.VW),
			new Fahrzeug(253, FahrzeugMarke.VW),
			new Fahrzeug(304, FahrzeugMarke.BMW),
			new Fahrzeug(151, FahrzeugMarke.VW),
			new Fahrzeug(250, FahrzeugMarke.VW),
			new Fahrzeug(217, FahrzeugMarke.Audi),
			new Fahrzeug(125, FahrzeugMarke.Audi)
		};

		#region Listentheorie
		//IEnumerable
		//Interface, welchen die Basis von Listentypen bildet in C# (Array, List, Dictionary, ...)

		//Effekte
		//Ermöglicht foreach auf ein Objekt anzuwenden
		//IEnumerable ist nur eine Anleitung -> Es speichert selbst keine Daten

		IEnumerable<int> ints = Enumerable.Range(0, 20); //Keine konkreten Werte (Anleitung von 0 bis 19)
		
		IEnumerable<int> vieleZahlen = Enumerable.Range(0, (int) 1E9); //Keine konkreten Werte (Anleitung von 0 bis 1 Mrd.)
		//List<int> vieleList = vieleZahlen.ToList(); //Hier werden konkrete Werte erstellt, und Resourcen werden verwendet (RAM, CPU)

		//Linq basiert auf IEnumerable, Linq Abfragen benötigen keine Leistung
		//Erst bei einer Konvertierungsfunktion wird Leistung beansprucht
		vieleZahlen.Where(e => e % 2 == 0); //Dauert 1ms

		//Enumerator
		//Grundkomponente von IEnumerable, führt die Anleitung tatsächlich aus
		//3 Teile:
		//object Current: Hier wird das derzeitige Element bei Zugriff erzeugt
		//bool MoveNext(): Damit wird das nächste Element angegriffen, dieses kann das aus Current herausgeholt werden
		//void Reset(): Damit kann der Zeiger an den Start geschoben werden

		//ToList() per Hand:
		IEnumerator<int> enumerator = ints.GetEnumerator();
		List<int> list = new List<int>();
		while (enumerator.MoveNext())
		{
			list.Add(enumerator.Current);
		}
		#endregion

		#region Linq
		//Predicate und Selector
		//Predicate: Func, die einen Boolean zurückgibt (Where, Count, All, Any, ...)
		//Selector: Func, die ein Generic zurückgibt (Select, GroupBy, Sum, Average, ...)

		fahrzeuge.Where(e => e.MaxV > 200); //Predicate
		fahrzeuge.Average(e => e.MaxV); //Selector

		//Select
		//2 Anwendungsfälle:
		//Einzelne Felder entnehmen
		fahrzeuge.Select(e => e.MaxV); //Liste von Ints

		//Liste transformieren
		fahrzeuge.Select(e => Task.Run(() => { })); //Für eine Objektliste jeweils einen Task ausführen

		"Hello World".Select(e => (int) e);

		//Webshop
		int page = 0;
		fahrzeuge.Skip(10 * page).Take(10 * (page + 1));

		fahrzeuge.SkipWhile(e => e.MaxV < 200); //Überspringe alle Fahrzeuge bis zum ersten Fahrzeug mit >= 200km/h

		//Index Overloads bei Where und Select
		fahrzeuge.Where((fzg, i) => i % 2 == 0);
		fahrzeuge.Select((fzg, i) => new Fahrzeug(i % 2 == 0 ? fzg.MaxV * 2 : fzg.MaxV, fzg.Marke));

		//SelectMany
		//Ermöglicht eine Liste zu glätten
		fahrzeuge.Chunk(10); //Teile die Liste in X große Teile auf
		fahrzeuge.Chunk(10).SelectMany(e => e); //Liste zurückkonvertieren in eine 1D Liste

		//GroupBy
		//Erzeugt eine Gruppe pro Wert des Kriteriums, und füllt alle Elemente in die jeweilige Gruppe
		fahrzeuge.GroupBy(e => e.Marke);
		fahrzeuge.GroupBy(e => e.Marke).ToDictionary(e => e.Key, e => e.ToList());

		ints.GroupBy(e => e % 10); //Jedem Eintrag in der Liste wird eine Zahl zugewiesen (hier die Einser Stelle), danach wird nach dieser Zahl gruppiert

		fahrzeuge.ToLookup(e => e.Marke); //Erzeugt ein Objekt, welches wie ein Dictionary verwendet kann, aber readonly ist

		//Aggregate
		//Wendet für jedes Element einer Liste eine Funktion an. Das Ergebnis der Funktion kann in den Aggregator gespeichert werden
		string output = fahrzeuge.Aggregate(new StringBuilder(), (sb, fzg) => sb.AppendLine($"Das Fahrzeug mit der Marke {fzg.Marke} kann maximal {fzg.MaxV}km/h fahren.")).ToString();
        Console.WriteLine(output);
		#endregion

		#region Erweiterungsmethoden
		int x = 243982187;
        Console.WriteLine(x.Quersumme());
        Console.WriteLine(358719.Quersumme());

		fahrzeuge.Shuffle();

		string user = "admin";
		if (user.In("admin", "123", "Hallo")) { }

		if (x.In(1, 2, 3, 4)) { }

        Console.WriteLine(fahrzeuge.AsString(e => (e.MaxV, e.Marke)));
        #endregion
    }
}

public class Fahrzeug
{
	public Fahrzeug(int maxV, FahrzeugMarke marke)
	{
		MaxV = maxV;
		Marke = marke;
	}

	public int MaxV { get; set; }

	public FahrzeugMarke Marke { get; set; }
}

public enum FahrzeugMarke { Audi, BMW, VW }