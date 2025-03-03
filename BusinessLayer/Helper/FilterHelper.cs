
using SharedLibrary.RequestModels.CoreRequests;

namespace BusinessLayer.Helper
{
	public static class FilterHelper
	{
		public static bool IsAbbreviationMatch(string fullName, string abbreviation)
		{
			fullName =  fullName.ToLower();
			abbreviation = abbreviation.ToLower();

			var splitFullName =  fullName.Split(' ');

			var newAbbreviation = "";

			var firstLetters = splitFullName.Select(x => x[0]).ToArray();
			foreach (var item in firstLetters)
			{
				newAbbreviation += item;
			}

			return newAbbreviation == abbreviation;
		}

	}
}
