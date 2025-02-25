using SharedLibrary.Enum;

namespace SharedLibrary.RequestModels.CoreRequests
{
	public class UniversityFilter
	{
		public string? QueryUniversityName { get; set; } = null;
		public City? City { get; set; } = null;
	}
}
