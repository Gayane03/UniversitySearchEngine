
using SharedLibrary.RequestModels.CoreRequests;

namespace BusinessLayer.Helper
{
	public static class FilterHelper
	{
		public static bool IsAbbreviationMatch(string fullName, string abbreviation)
		{
			int index = 0;
			foreach (char ch in fullName.ToLower()) // Convert to lowercase for case-insensitivity
			{
				if (index < abbreviation.Length && ch == abbreviation[index])
				{
					index++;
				}
			}
			return index == abbreviation.Length;
		}

	}
}
