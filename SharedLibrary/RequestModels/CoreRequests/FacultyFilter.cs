
namespace SharedLibrary.RequestModels.CoreRequests
{
	public class FacultyFilter
	{
		public int? UniversityId { get; set; }
		public int? MinimumScoreForFreeTraining { get; set; }
		public int? TuituionFee { get; set; }
		public List<string>? EntranceExams { get; set; }	
	}
}
