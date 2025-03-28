using Microsoft.Extensions.Configuration;
using RepositoryLayer.Helper;
using SharedLibrary.DbModels.Response;


namespace RepositoryLayer
{
	public class SearchEngineRepository : CoreBaseRepository, ISearchEngineRepository
	{
		public SearchEngineRepository(IConfiguration configuration) : base(configuration) { }

		public async Task<List<ExamResponseDB>> GetExams(int? facultyId)
		{
            Dictionary<string, object>? parameters = null;
            string? whereConditionBody = null;

            if (facultyId is not null)
            {
                parameters = new Dictionary<string, object>();
                parameters.Add($"facultyId", facultyId);

                whereConditionBody = $"{nameof(FacultyResponseDB.Id)} = @facultyId";
            }


            var getExams = await GetAll<Exam, ExamResponseDB>(ResponseModelGenerator.GetExams, parameters, whereConditionBody);
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

			var whereConditionBody = $"{nameof(FacultyResponseDB.Id)} = @facultyId";
			var faculty = await Get<Faculty, FacultyResponseDB>(ResponseModelGenerator.GetFaculty, parameters, whereConditionBody);

			return faculty;
		}

		public async Task<List<FavoriteResponseDB>> GetFavorites(int userId)
		{
			Dictionary<string, object>? parameters = null;
			string? whereConditionBody = null;

			parameters = new Dictionary<string, object>();
			parameters.Add($"userId", userId);

			whereConditionBody = $"UserId = @userId";

			var favorites = await GetAll<Favorite, FavoriteResponseDB>(ResponseModelGenerator.GetFavorites, parameters, whereConditionBody);
			return favorites;
		}

		public async Task AddFavorite(int userId, int facultyId)
		{
            var parameters = new Dictionary<string, object>();
            parameters.Add($"@{nameof(Favorite.UserId)}", userId);
            parameters.Add($"@{nameof(Favorite.FacultyId)}", facultyId);
            await Insert<Favorite>(parameters);
        }

		public async Task RemoveFavorite(int favoriteId)
		{
            var whereConditionBody = " Id = @favoriteId";

            var parameters = new Dictionary<string, object>();
            parameters.Add($"@favoriteId", favoriteId);
            await Delete<Favorite>(parameters, whereConditionBody);

        }
	}

	class University { }
	class Faculty { }
	class Exam { }
	public class Favorite {
      
		public int UserId { get; set; }	
        public int FacultyId { get; set; }
    }

	public class FavoriteResponseDB
	{
		public int Id { get; set; }
		public int FacultyId { get; set; }
	}
}
