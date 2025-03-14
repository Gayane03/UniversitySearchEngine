﻿using SharedLibrary.DbModels.Response;

namespace RepositoryLayer
{
	public interface ISearchEngineRepository
	{

		Task<List<UniversityResponseDB>> GetUniversities();
		Task<List<FacultyResponseDB>> GetFaculties(int? universityId);
		Task<List<ExamResponseDB>> GetExams(int? facultyId = null);
		Task<FacultyResponseDB> GetFaculty(int facultyId);
	}
}
