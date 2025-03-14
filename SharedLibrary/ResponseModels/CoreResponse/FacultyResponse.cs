﻿namespace SharedLibrary.ResponseModels.CoreResponse
{
	public class FacultyResponse
	{
		public int UniversityId { get; set; }	
		public int FacultyId { get; set; }

		public string FacultyName { get; set; }
		public string FacultyDescription { get; set; }

		public int FreeSpots { get; set; } // anvcari texer
		public int PaidSpots { get; set; }//  vcaropvii texer
		public int TuitionFee { get; set; } //  vardz

		public double LastYearMinScoreForFreeTrain { get; set; }	

		public List<string> EntranceExam { get; set; }
		public List<int> LastTwoYearFeeTrends {  get; set; }	// naxord 2 tarva  vardzeri chap@


		// for favourite
		public string? UniversityName { get; set; } //+ masnacux
		public bool? IsFavorite { get; set; }
		
	}
}
