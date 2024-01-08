namespace DelegateEvents;

internal class Program
{
	public delegate void Vorstellungen(string name); //Definition von Delegate, mithilfe des Delegate Keywords und einem Methodenkopf

	static void Main(string[] args)
	{
		//Delegate: Eigener Typ, der Methodenzeiger halten kann
		Vorstellungen v = new(VorstellungDE); //Erstellung eines Delegates mit Initialmethode
		v("Max"); //Ausführung eines Delegates

		v += VorstellungDE; //Methoden an Delegate anhängen mit +=
		v("Tim");

		v += VorstellungEN;
		v += VorstellungEN;
		v += VorstellungEN;
		v += VorstellungEN;
		v("Leo");

		v -= VorstellungDE; //Methode herunternehmen (von erster zur letzten)
		v -= VorstellungDE;
		v -= VorstellungDE;
		v -= VorstellungDE; //Methode herunternehmen, die nicht angehängt ist, verursacht keinen Fehler
		v("Max");

		v -= VorstellungEN;
		v -= VorstellungEN;
		v -= VorstellungEN;
		v -= VorstellungEN;
		//v("Tim"); //Wenn alle Methoden abgenommen werden, ist das Delegate null

		if (v is not null)
			v("Tim");

		//Null propagation: Führe den Code aus, wenn die Variable nicht null ist
		v?.Invoke("Tim"); //Generell sollte immer ein Null-Check gemacht werden für Delegates

		foreach (Delegate dg in v.GetInvocationList()) //Delegate iterieren
		{
            Console.WriteLine(dg.Method.Name);
        }
	}

	public static void VorstellungDE(string name)
	{
        Console.WriteLine($"Hallo mein Name ist {name}");
    }

	public static void VorstellungEN(string name)
	{
		Console.WriteLine($"Hello my name is {name}");
	}
}