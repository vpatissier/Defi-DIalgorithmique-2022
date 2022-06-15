public class Solution {
    public string LongestCommonPrefix(string[] strs)
	{
		int index = 0;
		while (index < strs[0].Length)
		{
			char? previousChar = null;
			for (int i = 0; i < strs.Length; i++)
			{
				char currentChar = strs[i][index];
				if (previousChar != null)
				{
					if (currentChar != previousChar)
						return strs[0].Substring(0, index);
				}
				previousChar = currentChar;
			}
			index++;
		}
		return String.Empty;
	}
}