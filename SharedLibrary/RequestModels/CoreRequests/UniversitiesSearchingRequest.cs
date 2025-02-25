namespace SharedLibrary.RequestModels.CoreRequests
{
	public class UniversitiesSearchingRequest
	{
		public UniversityFilter? UniversityFilter { get; set; } 
		public int? Page { get; set; } = 1;
		public int? PerPage { get; set; } = 10;

	}
}
