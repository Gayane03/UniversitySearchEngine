using SharedLibrary.ResponseModels.CoreResponse;
using System.Net.Http.Json;

namespace SearchUniversityUI.Helper
{
    public class ResponseMessageUtile
    {
        public async Task<T?> HandleResponse<T>(HttpResponseMessage? response)
        {

            if (response == null)
            {
                throw new SystemException("Response message is null.");
            }

            if (!response.IsSuccessStatusCode)
            {
                string errorMessage = await response.Content.ReadAsStringAsync();
                throw new SystemException(errorMessage);
            }

			var content = await response.Content.ReadAsStringAsync();
			if (string.IsNullOrWhiteSpace(content))
			{
                return default(T); // Return empty list if API returns no data
			}

			var result = await response!.Content.ReadFromJsonAsync<T>();

            //if (result == null)
            //{
            //    throw new SystemException("Response model is null.");
            //}

            return result;
        }
    }
}
