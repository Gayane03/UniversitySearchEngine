using SharedLibrary.Enum;

namespace SharedLibrary.RequestModels.CoreRequests
{
	public class UniversityFilter
	{
		public string? QueryUniversityName { get; set; }
		public City? City { get; set; } 
	}
}
