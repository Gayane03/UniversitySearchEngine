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



		Task<Result<List<FacultyResponse>>> GetFavorites(int userId);
		Task AddFavorite(int userId, int facultyId);
		Task RemoveFavorite(int facultyId);


	}
}
