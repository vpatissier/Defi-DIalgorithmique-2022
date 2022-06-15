public class Solution
{
	public int CoinChange(int[] coins, int amount)
	{
		if (amount == 0) return 0;

		Array.Sort(coins);
		int[,] array = new int[coins.Length, amount + 1];

		for (int i = 0; i < coins.Length; i++)
		{
			for (int j = 0; j < amount + 1; j++)
			{
				array[i, j] = int.MaxValue - 200;
			}
		}

		for (int j = 0; j < amount + 1; j++)
		{
			if (j % coins[0] == 0)
				array[0, j] = j / coins[0];
		}

		for (int i = 1; i < coins.Length; i++)
		{
			for (int j = 0; j < amount + 1; j++)
			{
				//int current = j / coins[i];
				if (coins[i] > j)
				{
					array[i, j] = array[i - 1, j];
				}
				else
				{
					array[i, j] = Math.Min(array[i - 1, j], 1 + array[i, j - coins[i]]);
				}
			}
		}

		int output = array[coins.Length - 1, amount];

		return output >= int.MaxValue - 200 ? -1 : output;
	}
}