
namespace SharedLibrary.RequestModels.CoreRequests
{
	public class FacultyFilter
	{
		public string? QueryFacultyName { get; set; }
		public double? MinimumScoreForFreeTraining { get; set; }//naxord tarva amenacacr gnahatakan@
		public int? MaxTuitionFee { get; set; } //usman vardz
		public List<string>? EntranceExams { get; set; } = new List<string>(); //qnnutyunneri anunner	
	}
}
