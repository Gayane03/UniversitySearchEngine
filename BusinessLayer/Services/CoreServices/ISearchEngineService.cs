using BusinessLayer.Helper.ModelHelper;
using SharedLibrary.RequestModels.CoreRequests;
using SharedLibrary.ResponseModels.CoreResponse;

namespace BusinessLayer.Services.CoreServices
{
	public interface ISearchEngineService
	{
		Task<Result<List<UniversityResponse>>> GetUniversities(UniversitiesSearchingRequest universitiesSearchingRequest);
		Task<Result<List<FacultyResponse>>> GetFaculties(FacultiesSearchingRequest facultiesSearchingRequest);
		Task<Result<FacultyResponse>> GetFaculty(int facultyId);	



		Task<Result<List<FavoriteResponse>?>> GetFavorites(int userId);
		Task<Result<bool>> AddFavorites(int userId, FavoriteRequest favoriteRequest);
		Task<Result<bool>> RemoveFavorite(int favoritesId);


	}
}
