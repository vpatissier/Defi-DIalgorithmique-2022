public class Solution
{

	public bool ValidPalindrome(string s)
	{
		if (isPalindrome(s)) return true;

		for (int i = 0; i < s.Length; i++)
		{
			string copy = (string)s.Clone();
			string reducedString = copy.Remove(i, 1);
			if (isPalindrome(reducedString))
				return true;
		}
		return false;
	}

	private bool isPalindrome(string s)
	{
		char[] charArray = s.ToCharArray();
		Array.Reverse(charArray);
		string reversed = new string(charArray);

		return s.Equals(reversed);
	}
}