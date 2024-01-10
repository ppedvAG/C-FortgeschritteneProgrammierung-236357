using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AsyncAwaitWPF;

public partial class MainWindow : Window
{
	public MainWindow() => InitializeComponent();

	private void Start(object sender, RoutedEventArgs e)
	{
		for (int i = 0; i < 100; i++)
		{
			Thread.Sleep(25); //Beliebige Arbeit simulieren
			TB.Text += i + "\n"; //Längere Arbeiten blockieren den Main Thread -> Tasks
		}
	}

	private void StartTaskRun(object sender, RoutedEventArgs e)
	{
		Task.Run(() =>
		{
			for (int i = 0; i < 100; i++)
			{
				Thread.Sleep(25);
				Dispatcher.Invoke(() => TB.Text += i + "\n"); //UI Updates dürfen nicht von Side Threads/Tasks ausgeführt werden -> Dispatcher um das Update auf den Main Thread zu legen
			}
		});
	}

	private async void StartAwait(object sender, RoutedEventArgs e)
	{
		for (int i = 0; i < 100; i++)
		{
			await Task.Delay(25); //Beliebige Arbeit simulieren
			TB.Text += i + "\n";
			Scroll.ScrollToEnd();
		}
	}

	private async void StartHttpClient(object sender, RoutedEventArgs e)
	{
		//Aufbau
		//Task starten: Task Variable anlegen und Async Methode starten
		//(optional) Zwischenschritte
		//Auf das Ergebnis warten mit await auf den vorher gestarteten Task

		using HttpClient client = new HttpClient();
		Task<HttpResponseMessage> response = client.GetAsync("http://www.gutenberg.org/files/54700/54700-0.txt"); //Aufgabe starten

		//Zwischenschritte
		TB.Text = "Request gestartet";
		Button1.IsEnabled = false;

		HttpResponseMessage message = await response; //Auf das Ergebnis warten

		if (message.IsSuccessStatusCode)
		{
			Task<string> text = message.Content.ReadAsStringAsync(); //Aufgabe starten

			//Zwischenschritt
			TB.Text = "Text wird ausgelesen";

			await Task.Delay(500);

			//Auf das Ergebnis warten
			string result = await text;
			TB.Text = result;
		}
		Button1.IsEnabled = true;
	}

	private async void StartIAsyncEnumerable(object sender, RoutedEventArgs e)
	{
		//Wenn es eine Datenquelle gibt, die in einem unbestimmten Intervall Daten hergibt oder auch nicht kann ein IAsyncEnumerable verwendet werden

		AsyncDataSource source = new();
		await foreach (int x in source.GetNumbers())
		{
			TB.Text += x + "\n";
			Scroll.ScrollToBottom();
		}
		TB.Text += "Fertig";
	}
}
