namespace DelegateEvents;

/// <summary>
/// Komponente, die eine Arbeit verrichtet, und ihren Fortschritt über Events zurückgibt
/// </summary>
internal class Component
{
	public event Action ProcessStarted;

	public event Action ProcessEnded;

	public event Action<int> Progress;

	public void DoWork()
	{
		ProcessStarted?.Invoke();

		for (int i = 0; i < 10; i++)
		{
			Thread.Sleep(200);
			Progress?.Invoke(i);
		}

		ProcessEnded?.Invoke();
	}
}
