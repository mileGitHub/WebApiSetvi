using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApiSetvi.Model;
using WebApiSetvi.Model.Validation;
using WebApiSetvi.Service.Interfaces;
using WebApiSetvi.Utils;

namespace WebApiSetvi.Controllers
{
    [Route("[controller]")]
	[ApiController]
	public class CompanyController : ControllerBase
	{
		private readonly ICompanyUserService _userService;
		public CompanyController(ICompanyUserService userService)
		{
			_userService = userService;
		}

		[HttpGet("{companyId:int}/users")]
		public async Task<IActionResult> GetCompanyUsers(int companyId)
		{
			if (!(companyId > 0))
			{
				//return StatusCode(400, new Result<List<User>>(HttpStatusCode.BadRequest, "Value must be > 0"));
				return ResponseHandler.CreateResponse(new Result<object>(HttpStatusCode.BadRequest, "Value must be > 0"));
			}
			try
			{
				var response = await _userService.GetUsersAsync(companyId);
				return ResponseHandler.CreateResponse(response);
			}
			catch (Exception ex)
			{
				//return StatusCode(500, new Result<List<User>>(HttpStatusCode.InternalServerError, "Exception: " + ex.Message));
				return ResponseHandler.CreateResponse(new Result<object>(HttpStatusCode.InternalServerError, "Exception: " + ex.Message));
				
			}
		}

		[ServiceFilter(typeof(ValidationFilterAttribute))]
		[HttpPost("{companyId}/user")]
		public async Task<IActionResult> AddUser(string companyId, [FromBody] User user)
		{
			if (!int.TryParse(companyId, out int companyIdInt))
			{
				return ResponseHandler.CreateResponse(new Result<object>(HttpStatusCode.BadRequest, "CompanyId must be a int value"));
			}
			if (!(companyIdInt > 0))
			{
				return ResponseHandler.CreateResponse(new Result<object>(HttpStatusCode.BadRequest, "Value must be > 0"));
			}
			//ValidationFilterAttribute is used for User model validation
			try
			{
				user.CompanyId = companyIdInt;
				var response = await _userService.AddUser(user);
				return ResponseHandler.CreateResponse(response);
			}
			catch (Exception ex)
			{
				return ResponseHandler.CreateResponse(new Result<List<User>>(HttpStatusCode.InternalServerError, "Exception: " + ex.Message)); 
			}
		}
	}
}
