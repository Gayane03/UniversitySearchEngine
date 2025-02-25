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

					if(query is not null)
					{
						universities =  universities
							.Where(u => u.Name.Contains(query, StringComparison.OrdinalIgnoreCase) 
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

				universities = universities.Skip(dataStartPoint).Take(perPage).ToList();


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

				foreach (var exam in exams)
				{
					var faculty = facultiesResponse.FirstOrDefault(f => f.FacultyId == exam.FacultyId);
					if (faculty != null)
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

					if (query is not null)
					{
						facultiesResponse = facultiesResponse
							.Where(f => f.FacultyName.Contains(query, StringComparison.OrdinalIgnoreCase)
									  || FilterHelper.IsAbbreviationMatch(f.FacultyName, query))
							.ToList();
					}

					if (minimumScoreForFreeTraining is not null)
					{
						facultiesResponse = facultiesResponse
							.Where(u => u.LastYearMinScoreForFreeTrain == minimumScoreForFreeTraining)
							.ToList();
					}

					if ( maxTuitionFee is not null)
					{
						facultiesResponse = facultiesResponse
							.Where(f => maxTuitionFee >= f.TuitionFee)
							.ToList();
					}

					if(entranceExams is not null && entranceExams.Any())
					{
						facultiesResponse = facultiesResponse
							.Where(faculty => entranceExams.Any(exam => faculty.EntranceExam.Contains(exam)))
							.ToList();
					
					}
				}

				var page = facultiesSearchingRequest.Page!.Value;
				var perPage = facultiesSearchingRequest.PerPage!.Value;
				var dataStartPoint = (page - 1) * perPage;

				facultiesResponse = facultiesResponse.Skip(dataStartPoint).Take(perPage).ToList();

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

				foreach (var exam in exams.Where(e => e.FacultyId == faculty.FacultyId))
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

	

		public Task<Result<List<FacultyResponse>>> GetFavorites(int userId)
		{
			throw new NotImplementedException();
		}

		public Task AddFavorite(int userId, int facultyId)
		{
			throw new NotImplementedException();
		}

		public Task RemoveFavorite(int facultyId)
		{
			throw new NotImplementedException();
		}
	}
}
