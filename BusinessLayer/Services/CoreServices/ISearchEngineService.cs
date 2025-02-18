using BusinessLayer.Helper.ModelHelper;
using SharedLibrary.RequestModels.CoreRequests;
using SharedLibrary.ResponseModels.CoreResponse;

namespace BusinessLayer.Services.CoreServices
{
	public interface ISearchEngineService
	{

		Task<Result<IEnumerable<UniversityResponse>>> GetUniversities(UniversitiesSearchingRequest universitiesSearchingRequest);
		Task<Result<IEnumerable<FacultyResponse>>> GetFaculties(FacultiesSearchingRequest facultiesSearchingRequest);
		Task<Result<FacultyResponse>> GetFaculty(int facultyId);	



		Task<Result<IEnumerable<FavoriteResponse>>> GetFavorites(int userId);
		Task AddFavorite(int userId, FavoriteRequest favoriteRequest);
		Task RemoveFavorite(int favoriteId);


	}
}
