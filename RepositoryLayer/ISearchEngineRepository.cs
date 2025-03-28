using SharedLibrary.DbModels.Response;
using SharedLibrary.ResponseModels.CoreResponse;

namespace RepositoryLayer
{
	public interface ISearchEngineRepository
	{

		Task<List<UniversityResponseDB>> GetUniversities();
		Task<List<FacultyResponseDB>> GetFaculties(int? universityId);
		Task<List<ExamResponseDB>> GetExams(int? facultyId = null);
		Task<FacultyResponseDB> GetFaculty(int facultyId);


		Task<List<FavoriteResponseDB>> GetFavorites(int userId);
		Task AddFavorite(int userId, int facultyId);
		Task RemoveFavorite(int favoriteId);
	}
}
