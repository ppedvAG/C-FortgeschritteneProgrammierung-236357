using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncAwaitWPF;

internal class AsyncDataSource
{
	public async IAsyncEnumerable<int> GetNumbers()
	{
		while (true)
		{
			await Task.Delay(Random.Shared.Next(100, 1000));
			if (Random.Shared.Next() % 100 == 0)
				yield break;
			yield return Random.Shared.Next();
		}
	}
}
