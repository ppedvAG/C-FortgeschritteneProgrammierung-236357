using System.Text;
using System.Text.Json;

namespace LinqErweiterungsmethoden;

public static class ExtensionMethods
{
	public static int Quersumme(this int x)
	{
		return x.ToString().Sum(e => (int) char.GetNumericValue(e));
	}

	public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> x)
	{
		return x.OrderBy(e => Random.Shared.Next());
	}

	//SQL IN
	//string s = "Hallo"; if (s.In("1", "2", "3"))
	//int x = 4; if (x.In(1, 2, 3, 4))
	public static bool In<T>(this T obj, params T[] values)
	{
		return values.Contains(obj);
	}

	//Python Liste printen in C#
	//Mit Selector, um die Form vorgeben zu können, in welcher die Objekte der Liste ausgegeben werden
	public static string AsString<T, TSelector>(this IEnumerable<T> x, Func<T, TSelector> selector)
	{
		StringBuilder sb = new();
		sb.Append("[");
		sb.Append(string.Join(", ", x.Select(e => selector(e)))); //Hier wird auf jedes Element der Liste der Selektor angewandt
		sb.Append("]");
		return sb.ToString();
	}

	public static JsonElement GetProperty(this JsonElement element, params string[] propertyName)
	{
		JsonElement current = element;
		foreach (string str in propertyName)
			current = current.GetProperty(str);
		return current;
	}
}
