using Microsoft.Data.SqlClient;
using SharedLibrary.DbModels.Response;
using SharedLibrary.ResponseModels.CoreResponse;

namespace RepositoryLayer.Helper
{
	internal static class ResponseModelGenerator
	{
		public static UserActivityDataResponse GetUserActivityData(SqlDataReader reader)
		{
			return new UserActivityDataResponse()
			{
				Id = reader.GetInt32(reader.GetOrdinal("Id")),
				IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
			};
		}
		public static LoginResponseDB GenerateLoginResponse(SqlDataReader reader)
		{
			return new() { Id = reader.GetInt32(reader.GetOrdinal("Id")) };
		}

		public static List<UniversityResponseDB> GetUniversities(SqlDataReader reader)
		{
			var universities = new List<UniversityResponseDB>();

			do
			{
				var university = new UniversityResponseDB
				{
					Id = reader.GetInt32(reader.GetOrdinal("Id")),
					Name = reader.GetString(reader.GetOrdinal("Name")),
					//LogoUrl = reader.GetString(reader.GetOrdinal("LogoUrl")),
					CityId = reader.GetInt32(reader.GetOrdinal("CityId")),
				};

				universities.Add(university);

			} while (reader.Read());

			return universities;
		}
		public static List<FacultyResponseDB> GetFaculties(SqlDataReader reader)
		{
			var faculties = new List<FacultyResponseDB>();

			do
			{
				var faculty = GetFaculty(reader);

				faculties.Add(faculty);

			} while (reader.Read());

			return faculties;
		}

		public static FacultyResponseDB GetFaculty(SqlDataReader reader)
		{
			var faculty = new FacultyResponseDB
			{
				Id = reader.GetInt32(reader.GetOrdinal("Id")),
				UniversityId = reader.GetInt32(reader.GetOrdinal("UniversityId")),
				Name = reader.GetString(reader.GetOrdinal("Name")),
				Description = reader.GetString(reader.GetOrdinal("Description")),
				FreeSpots = reader.GetInt32(reader.GetOrdinal("FreeSpots")),
				PaidSpots = reader.GetInt32(reader.GetOrdinal("PaidSpots")),
				TuitionFee = reader.GetInt32(reader.GetOrdinal("TuitionFee")),
				LastYearMinScoreForFreeTrain = reader.GetInt32(reader.GetOrdinal("TuitionFee")),
				UniversityName = reader.GetString(reader.GetOrdinal("UniversityName")),
				//IsFavorite = reader.GetBoolean(reader.GetOrdinal("IsFavorite")),
				OneYearsAgoFee = reader.GetInt32(reader.GetOrdinal("OneYearsAgoFee")),
				TwoYearsAgoFee = reader.GetInt32(reader.GetOrdinal("TwoYearsAgoFee"))
			};

			return faculty;
		}
 


		public static List<ExamResponseDB> GetExams(SqlDataReader reader)
		{
			var exams = new List<ExamResponseDB>();

			do
			{
				var exam = new ExamResponseDB
				{
					FacultyId = reader.GetInt32(reader.GetOrdinal("FacultyId")),
					Name = reader.GetString(reader.GetOrdinal("Name")),
				};

				exams.Add(exam);

			} while (reader.Read());

			return exams;
		}

	}
}
