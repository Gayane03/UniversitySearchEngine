using MudBlazor;
using SharedLibrary.RequestModels.CoreRequests;
using SharedLibrary.ResponseModels.CoreResponse;

namespace SearchUniversityUI.Helper
{
	public class FavoriteHelper
	{

		public LocalStorageHelper localStorageHelper;


		public ISnackbar Snackbar;


		public ApiController apiController;


		public ResponseMessageUtile responseMessageUtile;

		public FavoriteHelper(LocalStorageHelper localStorageHelper,
			ISnackbar Snackbar,
			ApiController apiController,
			ResponseMessageUtile responseMessageUtile)
		{
			this.localStorageHelper = localStorageHelper;
			this.Snackbar = Snackbar;
			this.apiController = apiController;
			this.responseMessageUtile = responseMessageUtile;

		}


		public List<FavoriteResponse> FavoritesResponseList = null;
		public List<int> FavoriteIds = new();
		public List<int> FacultyIds = new();
		public string token;


		public async Task ToggleFavorite(int favoriteId, int? facultyId = null)
		{
			try
			{
				FavoriteResponse currentFavorite;


				if (facultyId is not null)
				{
					if(FavoritesResponseList is  null || !FavoritesResponseList.Any())
					{
						await apiController.AddFavorite(token, new FavoriteRequest() { FacultyId = facultyId.Value });
						await RefreshFavorites();
						return;
					}

					currentFavorite = FavoritesResponseList.FirstOrDefault(f => f.Faculty.FacultyId == facultyId.Value);
					if(currentFavorite is null)
					{
						await apiController.AddFavorite(token, new FavoriteRequest() { FacultyId = facultyId.Value });
						await RefreshFavorites();
						return;
					}

					favoriteId = currentFavorite.Id;
				}
				else
				{
					//currentFavorite = FavoritesResponseList.FirstOrDefault(f => f.Id == favoriteId);
					facultyId = FavoritesResponseList.FirstOrDefault(f => f.Id == favoriteId).Faculty.FacultyId;
				}


				if (FavoriteIds.Contains(favoriteId))
				{
					//FavoriteIds.Remove(favoriteId);
					////FavoritesResponseList.Remove(currentFavorite);
					//FacultyIds.Remove(facultyId.Value);
					await apiController.RemoveFavorite(token, favoriteId);
				}
				else
				{
					//FavoriteIds.Add(favoriteId);
					//FavoritesResponseList.Add(currentFavorite);
					//FacultyIds.Add(facultyId.Value);
					await apiController.AddFavorite(token, new FavoriteRequest() { FacultyId = facultyId.Value });
				}

				await RefreshFavorites();
				return;

			}
			catch (Exception)
			{
				Snackbar.Add("Please contact with support team", MudBlazor.Severity.Error);
			}

		}


		public async Task RefreshFavorites()
		{
			try
			{
				

				var result = await apiController.GetFavorites(token);

				FavoritesResponseList = await responseMessageUtile.HandleResponse<List<FavoriteResponse>?>(result);

				if (FavoritesResponseList is not null && FavoritesResponseList.Any())
				{
					FavoriteIds = FavoritesResponseList.Select(f => f.Id).ToList();
					FacultyIds = FavoritesResponseList.Select(f => f.Faculty.FacultyId).ToList();
				}

							
			}
			catch (Exception ex)
			{
				Snackbar.Add(ex.Message, MudBlazor.Severity.Normal);

			}
		}

		public bool IsFavorite(int favoriteId, int? facultyId = null)
		{
			if (facultyId is not null)
			{
				return FacultyIds != null && FacultyIds.Contains(facultyId.Value);
			}

			return FavoriteIds != null && FavoriteIds.Contains(favoriteId);
		}

	}
}
