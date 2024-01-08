using System.Collections;

Zug z = new();
z += new Wagon();
z += new Wagon();
z += new Wagon();
z += new Wagon();
z++;

z[1] = new Wagon();
//z["Rot"] = new();

foreach (Wagon w in z)
{

}

z.Select(e => new { e.Farbe, HC = e.GetHashCode() }); //'a: Anonymer Typ
z.Select(e => (e.Farbe, e.GetHashCode())); //Select mit Tuple


class Zug : IEnumerable<Wagon>
{
	List<Wagon> wagons = new();

	public Wagon this[int index]
	{
		get => wagons[index];
		set => wagons[index] = value;
	}

	public Wagon this[string farbe]
	{
		get => wagons.First(e => e.Farbe == farbe);
	}

	public Wagon this[int anzSitze, string farbe]
	{
		get => wagons.First(e => e.AnzSitze == anzSitze && e.Farbe == farbe);
	}

	public IEnumerator<Wagon> GetEnumerator()
	{
		return wagons.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return wagons.GetEnumerator();
	}

	public static Zug operator +(Zug self, Wagon wagon)
	{
		self.wagons.Add(wagon);
		return self;
	}

	public static Zug operator +(Zug self, Zug other)
	{
		self.wagons.AddRange(other.wagons);
		return self;
	}

	public static Zug operator ++(Zug self)
	{
		self.wagons.Add(new Wagon());
		return self;
	}
}

class Wagon
{
	public int AnzSitze { get; set; }

	public string Farbe { get; set; }

	public static bool operator ==(Wagon a, Wagon b)
	{
		return a.AnzSitze == b.AnzSitze && a.Farbe == b.Farbe;
	}

	public static bool operator !=(Wagon a, Wagon b)
	{
		return !(a == b);
	}
}