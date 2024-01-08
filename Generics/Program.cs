using System.Net;

namespace Generics;

internal class Program
{
	static void Main(string[] args)
	{
		List<string> list = new List<string>(); //T wird durch string ersetzt
		list.Add("Max"); //T item -> string item
	}
}

public class DataStore<T>
{
	//Überall innerhalb der Klasse wo ein Typ stehen würde, kann T stehen
	public T[] data { get; set; } //T bei Variable

	public List<T> Data => data.ToList(); //T weitergeben

	public void Add(int index, T item) //T als Parameter
	{
		data[index] = item;
	}

	public T GetValue(int index) //T bei Rückgabetyp
	{
		if (index < 0 || index >= data.Length)
			return default; //default: Standardwert des Generics
		return data[index];
	}

	public T1 MethodeMitGeneric<T1>(T1 list)
		where T1 : List<T>
		//Constraint: Generic einschränken zu einem Typen oder einem Untertypen
		//Ermöglicht, Funktionen aus dem Typen zu benutzen bei Objekten des Generics
	{
        Console.WriteLine(default(T1));
        Console.WriteLine(typeof(T1));
        Console.WriteLine(nameof(T1)); //Name als String hinter dem Generic ("int", "string", "bool", ...)

		//list.Add(new T());

		T1 t = (T1) Convert.ChangeType(list, typeof(T1));
		return t;
    }
}