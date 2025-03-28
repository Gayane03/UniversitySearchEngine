﻿namespace SharedLibrary.DbModels.Response
{
	public class FacultyResponseDB
	{
		public int UniversityId { get; set; }
		public int Id { get; set; }

		public string Name { get; set; }
		public string Description { get; set; }

		public int FreeSpots { get; set; } // anvcari texer
		public int PaidSpots { get; set; }//  vcaropvii texer
		public int TuitionFee { get; set; } //  vardz

		public double LastYearMinScoreForFreeTrain { get; set; }

		public int TwoYearsAgoFee { get; set; }
		public int OneYearsAgoFee { get; set; }

		// for favourite
		public string? UniversityName { get; set; } //+ masnacux

	}
}
