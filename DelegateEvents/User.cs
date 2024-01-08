namespace DelegateEvents;

internal class User
{
	static void Main(string[] args)
	{
		//Der Benutzer kann über die Events jetzt frei entscheiden, was bei den Events passieren soll
		//z.B. Console, WPF, ASP, MAUI, DB, Benachrichtigung aufs Handy, ...
		Component comp = new();
		comp.ProcessStarted += () => Console.WriteLine("Prozess gestartet");
		comp.Progress += (x) => Console.WriteLine("Fortschritt: " + x);
		comp.ProcessEnded += () => Console.WriteLine("Prozess fertig");
		comp.DoWork();
	}
}
