using AutoMapper;
using BusinessLayer.Helper;
using BusinessLayer.Helper.ModelHelper;
using RepositoryLayer;
using SharedLibrary.RequestModels.CoreRequests;
using SharedLibrary.ResponseModels.CoreResponse;

namespace BusinessLayer.Services.CoreServices
{
	public class SearchEngineService : ISearchEngineService
	{
		private readonly  ISearchEngineRepository searchEngineRepository;
		private readonly  IMapper  mapper;
        public SearchEngineService(
			ISearchEngineRepository searchEngineRepository,
			IMapper mapper)
        {
            this.searchEngineRepository = searchEngineRepository;
			this.mapper = mapper;	
        }


		public async Task<Result<List<FavoriteResponse>?>> GetFavorites(int userId)
		{
			try
			{
				var favorites = await searchEngineRepository.GetFavorites(userId);

				if (favorites is null || !favorites.Any())
				{
					return Result<List<FavoriteResponse>?>.Success(null);
				}

				var favoriteListResponse = new List<FavoriteResponse>();

				foreach (var favorite in favorites)
				{
					var faculty = await GetFaculty(favorite.FacultyId);

					if(!faculty.IsSuccess)
					{
						return Result<List<FavoriteResponse>>.Failure(Message.FavoriteError);
					}

					favoriteListResponse.Add(new FavoriteResponse() { Id = favorite.Id, Faculty = faculty.Value });
				}


				if (favoriteListResponse == null || !favoriteListResponse.Any())
				{
					return Result<List<FavoriteResponse>>.Failure(Message.EmptyFavorites);
				}

				return Result<List<FavoriteResponse>>.Success(favoriteListResponse);
			}
			catch (Exception ex)
			{
				return Result<List<FavoriteResponse>>.Failure(Message.SystemError);
			}
		}

		public async Task<Result<bool>> AddFavorites(int userId,FavoriteRequest favoriteRequest)
		{
			try
			{
				await searchEngineRepository.AddFavorite(userId, favoriteRequest.FacultyId.Value);

				return Result<bool>.Success(true);
			}
			catch (Exception ex) 
			{
				return Result<bool>.Failure(Message.SystemError);
			}
		}

		public async Task<Result<bool>> RemoveFavorite(int favoriteId)
		{
			try
			{
				await searchEngineRepository.RemoveFavorite(favoriteId);

				return Result<bool>.Success(true);
			}
			catch (Exception ex)
			{
				return Result<bool>.Failure(Message.SystemError);
			}
		}

		public async Task<Result<List<UniversityResponse>>> GetUniversities(UniversitiesSearchingRequest universitiesSearchingRequest)
		{
			try
			{
				var universities = await searchEngineRepository.GetUniversities();

				var filterUniversity = universitiesSearchingRequest.UniversityFilter;

				if (filterUniversity is not null)
				{
					var query = filterUniversity.QueryUniversityName;
					var city = filterUniversity.City; 

					if(!string.IsNullOrEmpty(query) && query != "string")
					{
						universities =  universities
							.Where(u => u.Name.ToLower().Contains(query.ToLower(), StringComparison.OrdinalIgnoreCase) 
							          || FilterHelper.IsAbbreviationMatch(u.Name,query))
							.ToList();
					}

					if (city is not null)
					{
						universities = universities.Where(u => u.CityId == (int)city).ToList();
					}
				}

				var page = universitiesSearchingRequest.Page!.Value;
				var perPage = universitiesSearchingRequest.PerPage!.Value;
				var dataStartPoint = (page - 1) * perPage;

                if (universities.Count > perPage)
                {
                    universities = universities.Skip(dataStartPoint).Take(perPage).ToList();
                }
               
				var universitiesResponse = new List<UniversityResponse>();

				foreach (var university in universities)
				{
					universitiesResponse.Add(mapper.Map<UniversityResponse>(university));
				}

				return Result<List<UniversityResponse>>.Success(universitiesResponse);

			}
			catch (Exception ex)
			{
				return Result<List<UniversityResponse>>.Failure(Message.SystemError);
			}		
		}


		public async Task<Result<List<FacultyResponse>>> GetFaculties(FacultiesSearchingRequest facultiesSearchingRequest)
		{
			try
			{
				var faculties = await searchEngineRepository.GetFaculties(facultiesSearchingRequest.UniversityId);
				var exams = await searchEngineRepository.GetExams();

				var facultiesResponse = new List<FacultyResponse>();

				foreach (var faculty in faculties)
				{
					var currentFaculty = mapper.Map<FacultyResponse>(faculty);
					currentFaculty.EntranceExam = new List<string>(); 
					currentFaculty.LastTwoYearFeeTrends = new List<int> { faculty.TwoYearsAgoFee,faculty.OneYearsAgoFee };

					facultiesResponse.Add(currentFaculty);
				}

				foreach (var faculty in facultiesResponse)
				{
					var currentFacultyExams = exams.Where(exams => exams.FacultyId == faculty.FacultyId);

                    foreach (var exam in currentFacultyExams)
					{
						faculty.EntranceExam.Add(exam.Name);	
					}
				}

				var filterFaculty = facultiesSearchingRequest.FacultyFilter;

				if (filterFaculty is not null)
				{
					var query = filterFaculty.QueryFacultyName;
					var minimumScoreForFreeTraining = filterFaculty.MinimumScoreForFreeTraining;
					var entranceExams = filterFaculty.EntranceExams;
					var maxTuitionFee = filterFaculty.MaxTuitionFee;

                    if (!string.IsNullOrEmpty(query) && query != "string")
                    {
						facultiesResponse = facultiesResponse
							.Where(f => f.FacultyName.Contains(query, StringComparison.OrdinalIgnoreCase)
									  || FilterHelper.IsAbbreviationMatch(f.FacultyName, query))
							.ToList();
					}

					if (minimumScoreForFreeTraining is not null && minimumScoreForFreeTraining != default(double))
					{
						facultiesResponse = facultiesResponse
							.Where(u => u.LastYearMinScoreForFreeTrain <= minimumScoreForFreeTraining)
							.ToList();
					}

					if (maxTuitionFee is not null && maxTuitionFee != default(int))
					{
						facultiesResponse = facultiesResponse
							.Where(f => maxTuitionFee >= f.TuitionFee)
							.ToList();
					}

					if(entranceExams is not null && entranceExams.Any())
					{
						entranceExams = entranceExams.Where(e => !string.IsNullOrEmpty(e)).ToList();

						if(entranceExams is not null && entranceExams.Any())
						{
							foreach (var faculty in facultiesResponse)
							{
								faculty.EntranceExam = faculty.EntranceExam.Select(e => e.ToLower()).ToList();
							}

							facultiesResponse = facultiesResponse
							.Where(faculty => entranceExams.Any(exam => faculty.EntranceExam.Contains(exam.ToLower())))
							.ToList();
						}
					
					}
				}

				var page = facultiesSearchingRequest.Page!.Value;
				var perPage = facultiesSearchingRequest.PerPage!.Value;
				var dataStartPoint = (page - 1) * perPage;

				if(facultiesResponse.Count > perPage)
				{
                    facultiesResponse = facultiesResponse.Skip(dataStartPoint).Take(perPage).ToList();
                }

				return Result<List<FacultyResponse>>.Success(facultiesResponse);
			}
			catch (Exception)
			{
				return Result<List<FacultyResponse>>.Failure(Message.SystemError);
			}
		}


		public async  Task<Result<FacultyResponse>> GetFaculty(int facultyId)
		{
			try
			{
				var faculty = await searchEngineRepository.GetFaculty(facultyId);
				var exams = await searchEngineRepository.GetExams();

				var facultyResponse = mapper.Map<FacultyResponse>(faculty);
				facultyResponse.LastTwoYearFeeTrends = new List<int> { faculty.TwoYearsAgoFee, faculty.OneYearsAgoFee };

				facultyResponse.EntranceExam = new List<string>();

				foreach (var exam in exams.Where(e => e.FacultyId == faculty.Id))
				{
					facultyResponse.EntranceExam.Add(exam.Name);	
				}

				return  Result<FacultyResponse>.Success(facultyResponse);	
			}
			catch (Exception)
			{
				return Result<FacultyResponse>.Failure(Message.SystemError);
			}			
		}

		
	}
}
