using BaseMigrationApi.Helpers;
using BusinessLayer.Autho;
using BusinessLayer.Helper;
using BusinessLayer.Services.CoreServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.RequestModels.CoreRequests;

namespace SearchUniversityAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize(Roles = Role.Admin, AuthenticationSchemes = TokenSchemeType.UserAccess)]
	public class SearchEngineController:ControllerBase
	{
		private readonly ISearchEngineService searchEngineService;
		private readonly IJwtTokenHandlerService jwtTokenHandlerService;	
        public SearchEngineController(
			ISearchEngineService searchEngineService,
			IJwtTokenHandlerService jwtTokenHandlerService)
        {
			this.searchEngineService = searchEngineService;
			this.jwtTokenHandlerService = jwtTokenHandlerService;	
        }

		[HttpGet("validateToken")]
		public async Task<IActionResult> ValidateToken()
		{
			return Ok(true);
		}


		[HttpPost("getUniversitiesWithFilter")]
		public async Task<IActionResult> GetUniversitiesWithFilter([FromBody]UniversitiesSearchingRequest universitiesSearchingRequest)
		{
            var result = await searchEngineService.GetUniversities(universitiesSearchingRequest);

			if(!result.IsSuccess)
			{
				return BadRequest(result.Error);
			}

			return Ok(result.Value);
		}


		[HttpPost("getFacultiesWithFilter")]
		public async Task<IActionResult> GetFacultiesWithFilter([FromBody]FacultiesSearchingRequest facultiesSearchingRequest)
		{
			var result = await searchEngineService.GetFaculties(facultiesSearchingRequest);

			if (!result.IsSuccess)
			{
				return BadRequest(result.Error);
			}

			return Ok(result.Value);
		}


		[HttpGet("getFaculty/{facultyId}")]
		public async Task<IActionResult> GetFaculty(int facultyId)
		{
			var result = await searchEngineService.GetFaculty(facultyId);

			if (!result.IsSuccess)
			{
				return BadRequest(result.Error);
			}

			return Ok(result.Value);
		}

	}
}
