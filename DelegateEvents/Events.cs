namespace DelegateEvents;

internal class Events
{
	//Event: Statischer Punkt, an den eine Methode angehängt werden kann
	//Events bestehen immer aus einem Delegate
	//Events können nicht instanziert werden

	//Events bestehen generell (vom Aufbau her) aus zwei Teilen:
	//- Entwicklerseite, definiert das Event selbst und führt es aus
	//- Anwenderseite, hängt an das Event die entsprechende Methode an

	//Beispiel: Click-Event
	//Entwicklerseite: Definiert das Event und führt das Event aus wenn der User den Button anklickt (Mauszeiger im Button, Linksklick, keine UI-Elemente darüber, ...)
	//Anwenderseite: Definiert, was beim Click passiert

	/// <summary>
	/// Definition des Events auf der Entwicklerseite
	/// </summary>
	event EventHandler TestEvent;

	event EventHandler<TestEventArgs> ArgsEvent;

	event EventHandler<int> IntEvent;

	public void Start()
	{
		TestEvent += Events_TestEvent; //Anwenderseite
		TestEvent?.Invoke(this, EventArgs.Empty); //Entwicklerseite

		ArgsEvent += Events_ArgsEvent;
		ArgsEvent?.Invoke(this, new TestEventArgs() { Data = "123" });

		IntEvent += Events_IntEvent;
		IntEvent?.Invoke(this, 10);
	}

	private void Events_IntEvent(object sender, int e)
	{
        Console.WriteLine(e);
    }

	private void Events_ArgsEvent(object sender, TestEventArgs e) //Hier wird das Generic eingesetzt
	{
        Console.WriteLine(e.Data);
    }

	private void Events_TestEvent(object sender, EventArgs e)
	{
        Console.WriteLine("TestEvent ausgeführt");
    }

	static void Main(string[] args) => new Events().Start();
}

public class TestEventArgs : EventArgs
{
	public string Data;
}