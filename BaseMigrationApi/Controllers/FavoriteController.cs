using BaseMigrationApi.Helpers;
using BusinessLayer.Autho;
using BusinessLayer.Helper;
using BusinessLayer.Services.CoreServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SearchUniversityAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize(Roles = Role.Admin, AuthenticationSchemes = TokenSchemeType.UserAccess)]
	public class FavoriteController:ControllerBase
	{
		private readonly ISearchEngineService searchEngineService;
		private readonly IJwtTokenHandlerService jwtTokenHandlerService;

		private readonly int UserId;
		public FavoriteController(ISearchEngineService searchEngineService, IJwtTokenHandlerService jwtTokenHandlerService)
		{
			this.searchEngineService = searchEngineService;
			this.jwtTokenHandlerService = jwtTokenHandlerService;

			var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
			UserId = jwtTokenHandlerService.GetUserIdFromToken(token);

		}

		[HttpGet("getFavorites")]
		public async Task<IActionResult> GetFavorites()
		{
			var result = await searchEngineService.GetFavorites(UserId);

			if (!result.IsSuccess)
			{
				return BadRequest(result.Error);
			}

			return Ok(result.Value);
		}


		[HttpGet("addFavorite")]
		public async Task<IActionResult> AddFavorite(int facultyId)
		{
		   await searchEngineService.AddFavorite(UserId, facultyId); // is favorite to true

			//var result = await searchEngineService.GetFavorites(UserId);

			//if (!result.IsSuccess)
			//{
			//	return BadRequest(result.Error);
			//}

			return Ok();
		}


		[HttpDelete("removeFavorite")]
		public async Task<IActionResult> RemoveFavorite(int facultyId)
		{
			await searchEngineService.RemoveFavorite(facultyId);

			////var result = await searchEngineService.GetFavorites(UserId);

			//if (!result.IsSuccess)
			//{
			//	return BadRequest(result.Error);
			//}

			return Ok();
		}
	}
}
