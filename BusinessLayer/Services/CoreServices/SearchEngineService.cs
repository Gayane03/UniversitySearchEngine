using BusinessLayer.Helper.ModelHelper;
using SharedLibrary.RequestModels.CoreRequests;
using SharedLibrary.ResponseModels.CoreResponse;

namespace BusinessLayer.Services.CoreServices
{
	public class SearchEngineService : ISearchEngineService
	{
		public async Task<Result<IEnumerable<FacultyResponse>>> GetFaculties(FacultiesSearchingRequest facultiesSearchingRequest)
		{
			throw new NotImplementedException();
		}

		public async Task<Result<IEnumerable<UniversityResponse>>> GetUniversities(UniversitiesSearchingRequest universitiesSearchingRequest)
		{
			throw new NotImplementedException();
		}

		public Task<Result<FacultyResponse>> GetFaculty(int facultyId)
		{
			throw new NotImplementedException();
		}

		public Task<Result<IEnumerable<FavoriteResponse>>> GetFavorites(int userId)
		{
			throw new NotImplementedException();
		}

		public Task AddFavorite(int userId, FavoriteRequest favoriteRequest)
		{
			throw new NotImplementedException();
		}

		public Task RemoveFavorite(int favoriteId)
		{
			throw new NotImplementedException();
		}
	}
}
