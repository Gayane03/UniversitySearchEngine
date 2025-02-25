using Microsoft.Extensions.Configuration;
using RepositoryLayer.Helper;
using SharedLibrary.DbModels.Response;

namespace RepositoryLayer
{
	public class SearchEngineRepository : CoreBaseRepository, ISearchEngineRepository
	{
		public SearchEngineRepository(IConfiguration configuration) : base(configuration) { }

		public async Task<List<ExamResponseDB>> GetExams()
		{
			var getExams = await GetAll<Exam, ExamResponseDB>(ResponseModelGenerator.GetExams);
			return getExams;
		}

		public async Task<List<FacultyResponseDB>> GetFaculties(int? universityId)
		{
			Dictionary<string, object>? parameters = null;
			string? whereConditionBody = null;

			if (universityId is not null)
			{
				parameters = new Dictionary<string, object>();
				parameters.Add($"universityId", universityId);

				whereConditionBody = $"{nameof(FacultyResponseDB.UniversityId)} = @universityId";
			}

			var faculties = await GetAll<Faculty, FacultyResponseDB>(ResponseModelGenerator.GetFaculties, parameters, whereConditionBody);
			return faculties;
		}

		public async Task<List<UniversityResponseDB>> GetUniversities()
		{
			var universities = await GetAll<University, UniversityResponseDB>(ResponseModelGenerator.GetUniversities);
			return universities;
		}

		public async Task<FacultyResponseDB> GetFaculty(int facultyId)
		{

			var parameters = new Dictionary<string, object>();
			parameters.Add($"facultyId", facultyId);

			var whereConditionBody = $"{nameof(FacultyResponseDB.FacultyId)} = @facultyId";
			var faculty = await Get<Faculty, FacultyResponseDB>(ResponseModelGenerator.GetFaculty, parameters, whereConditionBody);

			return faculty;
		}
	}

	class University { }
	class Faculty { }
	class Exam { }
}
