namespace Sprachfeatures;

internal class Program
{
	static void Main(string[] args)
	{
		object o = null;
		if (o is int)
		{
			int y = (int)o;
                Console.WriteLine(y);
            }

		if (o is int x)
		{
                Console.WriteLine(x);
            }

		if (o.GetType() == typeof(int))
		{
			//Genauer Typvergleich
		}

		List<int> list = new List<int>();
		if (list.GetType() == typeof(IEnumerable<int>))
		{
			//Genauer Typvergleich
			//list.GetType() == typeof(List<int>) -> true
			//list.GetType() == typeof(IEnumerable<int>) -> false
		}

		if (list is List<int>)
		{
			//Vererbungshierarchietypvergleich
			//list is List<int> -> true
			//list is IEnumerable<int> -> true
		}

		int zahl = 5;
		ref int zahl2 = ref zahl;
		zahl = 10;

		//Referenztyp (class)
		//Wird referenziert und bei Vergleichen werden die Speicheradressen verglichen

		//Wertetyp (struct)
		//Wird kopiert und bei Vergleichen werden die Inhalte verglichen

		//Null-Coalescing Operator (??): Nimm den linken Wert, wenn er nicht null ist, sonst den rechten Wert
		string s = null;
            Console.WriteLine(s ?? "Anderes"); //Wenn s nicht null ist, schreibe s, sonst Anderes

            Console.WriteLine(s == null ? s : "Anderes");

		if (s  == null)
                Console.WriteLine("Anderes");
		else
                Console.WriteLine(s);

		//Interpolated String ($): Ermöglicht, Code in einen String einzubauen
		Console.WriteLine($"Die Zahl ist {zahl}");
		Console.WriteLine($"Die Zahl mal Zwei ist {zahl * 2}");
		Console.WriteLine($"Der String ist: {s ?? "Leer"}");
		Console.WriteLine($"Der String ist: {(s == null ? "Leer" : s)}");
		Console.WriteLine($"Die Zahl ist: {zahl switch { 0 => "Null", 1 => "Eins", _ => "Andere Zahl" }}"); 
    }
}

public record Person(int ID, string Vorname, string Nachname)
{
	public void Test() { }
}