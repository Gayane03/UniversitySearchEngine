using SharedLibrary.RequestModels;
using SharedLibrary.RequestModels.CoreRequests;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace SearchUniversityUI
{
	public class ApiController 
	{
		private readonly HttpClient httpClient;
        public ApiController(HttpClient httpClient)
        {
		    this.httpClient = httpClient;	
        }

		public async Task<HttpResponseMessage?> RegisterUser(RegistrationRequest registrationRequest)
		{
			return await httpClient.PostAsJsonAsync("User/register", registrationRequest);
		}

		public async Task<HttpResponseMessage?> VerifyUserEmail(EmailVerificationCodeRequest emailVerificationCodeRequest,string? token)
		{
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			return await httpClient.PostAsJsonAsync("User/verifyEmail", emailVerificationCodeRequest);
		}

		public async Task<HttpResponseMessage> LoginUser(LoginRequest loginRequest)
		{
			return await httpClient.PostAsJsonAsync("User/login", loginRequest);
		}


		//search engine controller	

		public async Task<HttpResponseMessage?> GetUniversitiesWithFilter(string? token, UniversitiesSearchingRequest universitiesSearchingRequest)
		{
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			return await httpClient.PostAsJsonAsync("SearchEngine/getUniversitiesWithFilter", universitiesSearchingRequest);
		}
		public async Task<HttpResponseMessage?> GetFacultiesWithFilter(string? token, FacultiesSearchingRequest facultiesSearchingRequest)
		{
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			return await httpClient.PostAsJsonAsync("SearchEngine/getFacultiesWithFilter", facultiesSearchingRequest);
		}

		public async Task<HttpResponseMessage?> GetFaculty(string? token,int facultyId)
		{
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			string url = $"SearchEngine/getFaculty/{facultyId}";
			return await httpClient.GetAsync(url);
		}
		public async Task<HttpResponseMessage?> ValidateToken(string token)
		{
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			return await httpClient.GetAsync("SearchEngine/validateToken");
		}



		//favorite controller
		public async Task<HttpResponseMessage?> GetFavorites(string? token)
		{
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			return await httpClient.GetAsync("SearchEngine/getFavorites");
		}

		public async Task<HttpResponseMessage?> AddFavorite(string? token, FavoriteRequest favoriteRequest)
		{
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			return await httpClient.PostAsJsonAsync("SearchEngine/addFavorite", favoriteRequest);
		}

		public async Task<HttpResponseMessage?> RemoveFavorite(string? token, int favoriteId)
		{
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			string url = $"SearchEngine/removeFavorite?favoriteId={favoriteId}";
			return await httpClient.DeleteAsync(url);
		}

	}
}
