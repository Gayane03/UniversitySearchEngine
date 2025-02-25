namespace SharedLibrary.RequestModels.CoreRequests
{
	public class FacultiesSearchingRequest
	{
		public int? UniversityId { get; set; }

		public FacultyFilter? FacultyFilter { get; set; }
		public int? Page { get; set; } = 1;
		public int? PerPage { get; set; } = 10;

	}
}
