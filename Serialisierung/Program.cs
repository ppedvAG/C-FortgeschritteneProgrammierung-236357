using System.Text.Json;
using System.Text.Json.Serialization;

namespace Serialisierung;

internal class Program
{
	static List<Fahrzeug> fahrzeuge = new()
	{
		new PKW(0, 251, FahrzeugMarke.BMW),
		new Fahrzeug(1, 274, FahrzeugMarke.BMW),
		new Fahrzeug(2, 146, FahrzeugMarke.BMW),
		new Fahrzeug(3, 208, FahrzeugMarke.Audi),
		new Fahrzeug(4, 189, FahrzeugMarke.Audi),
		new Fahrzeug(5, 133, FahrzeugMarke.VW),
		new Fahrzeug(6, 253, FahrzeugMarke.VW),
		new Fahrzeug(7, 304, FahrzeugMarke.BMW),
		new Fahrzeug(8, 151, FahrzeugMarke.VW),
		new Fahrzeug(9, 250, FahrzeugMarke.VW),
		new Fahrzeug(10, 217, FahrzeugMarke.Audi),
		new Fahrzeug(11, 125, FahrzeugMarke.Audi)
	};

	static void Main(string[] args)
	{
		//NewtonsoftJson();
		//SystemTextJson();
	}

	static void NewtonsoftJson()
	{
		////Newtonsoft.Json

		////2. JsonSerializerSettings
		//JsonSerializerSettings settings = new JsonSerializerSettings();
		//settings.Formatting = Formatting.Indented;
		//settings.TypeNameHandling = TypeNameHandling.Objects; //Vererbung Serialisieren
		//settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; //Zirkelbezüge ignoriere

		////1. De-/Serialisierung
		//string json = JsonConvert.SerializeObject(fahrzeuge, settings);

		//List<Fahrzeug> readFzg = JsonConvert.DeserializeObject<List<Fahrzeug>>(json, settings);

		////3. Attribute
		////JsonIgnore: Ignoriert das Feld
		////JsonProperty: Eigenschaften über das Feld ändern
		////JsonRequired: Feld darf nicht null sein
		////JsonExtensionData: Schreibt alle Felder die kein unterliegendes Property haben in ein Dictionary

		////4. Json per Hand durchgehen
		////Beispiel: Einzelne Felder aus einem großen JSON auslesen
		////Edit -> Paste Special -> Paste JSON as Classes
		//JToken doc = JToken.Parse(json); //Gesamtes Dokument zu einem Token parsen
		//foreach (JToken jt in doc)
		//{
		//	int id = jt["ID"].Value<int>();
		//	int maxV = jt["MaxV"].Value<int>();
		//	FahrzeugMarke marke = (FahrzeugMarke) jt["Marke"].Value<int>();
		//	Console.WriteLine($"Fahrzeug: {id}, {maxV}, {marke}");
		//}
	}

	static void SystemTextJson()
	{
		//System.Text.Json

		//2. JsonSerializerOptions
		JsonSerializerOptions settings = new JsonSerializerOptions();
		settings.WriteIndented = true;
		settings.ReferenceHandler = ReferenceHandler.IgnoreCycles; //Zirkelbezüge ignoriere

		//1. De-/Serialisierung
		string json = JsonSerializer.Serialize(fahrzeuge, settings);

		List<Fahrzeug> readFzg = JsonSerializer.Deserialize<List<Fahrzeug>>(json, settings);

		//3. Attribute
		//JsonIgnore: Ignoriert das Feld
		//JsonPropertyName: Eigenschaften über das Feld ändern
		//JsonRequired: Feld darf nicht null sein
		//JsonDerivedType: Vererbung serialisieren
		//JsonExtensionData: Schreibt alle Felder die kein unterliegendes Property haben in ein Dictionary

		//4. Json per Hand durchgehen
		//Beispiel: Einzelne Felder aus einem großen JSON auslesen
		//Edit -> Paste Special -> Paste JSON as Classes
		JsonDocument doc = JsonDocument.Parse(json); //Gesamtes Dokument zu einem Token parsen
		foreach (JsonElement jt in doc.RootElement.EnumerateArray())
		{
			int id = jt.GetProperty("ID").GetInt32();
			int maxV = jt.GetProperty("MaxV").GetInt32();
			FahrzeugMarke marke = (FahrzeugMarke) jt.GetProperty("Marke").GetInt32();
			Console.WriteLine($"Fahrzeug: {id}, {maxV}, {marke}");
		}
	}
}

[JsonDerivedType(typeof(Fahrzeug), "F")]
[JsonDerivedType(typeof(PKW), "P")]
public class Fahrzeug
{
	//[JsonIgnore]
	//[JsonProperty("Identifier")]
	//[JsonRequired]
	public int ID { get; set; }

	[JsonIgnore]
	[JsonPropertyName("Maximalgeschwindigkeit")]
	[JsonRequired]
	public int MaxV { get; set; }

	public FahrzeugMarke Marke { get; set; }

	[JsonExtensionData]
	public Dictionary<string, object> OtherField = new();

	public Fahrzeug(int iD, int maxV, FahrzeugMarke marke)
	{
		ID = iD;
		MaxV = maxV;
		Marke = marke;
	}
}

public class PKW : Fahrzeug
{
	public PKW(int iD, int maxV, FahrzeugMarke marke) : base(iD, maxV, marke) { }
}

public enum FahrzeugMarke { Audi, BMW, VW }