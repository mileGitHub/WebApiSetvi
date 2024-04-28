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
			try
			{
				//companyId must be an integer value (companyId:int), so we will check is companyId > 0
				if (!(companyId > 0))
				{
					return ResponseHandler.CreateResponse(new Result<object>(HttpStatusCode.BadRequest, "CompanyId must be > 0"));
				}

				var response = await _userService.GetUsersAsync(companyId);
				return ResponseHandler.CreateResponse(response);
			}
			catch (Exception ex)
			{
				return ResponseHandler.CreateResponse(new Result<object>(HttpStatusCode.InternalServerError, "Exception: " + ex.Message));	
			}
		}

		//ValidationFilterAttribute is used for User model validation (FirstName, LastName and Email)
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		[HttpPost("{companyId}/user")]
		public async Task<IActionResult> AddUser(string companyId, [FromBody] User user)
		{
			try
			{
				//received companyId can be string value, in that case responsed with status code - 400
				if (!int.TryParse(companyId, out int companyIdInt))
				{
					return ResponseHandler.CreateResponse(new Result<object>(HttpStatusCode.BadRequest, "CompanyId must be a int value"));
				}
				if (!(companyIdInt > 0))
				{
					return ResponseHandler.CreateResponse(new Result<object>(HttpStatusCode.BadRequest, "CompanyId must be > 0"));
				}

				user.CompanyId = companyIdInt;
				var response = await _userService.AddUser(user);
				return ResponseHandler.CreateResponse(response);
			}
			catch (Exception ex)
			{
				return ResponseHandler.CreateResponse(new Result<object>(HttpStatusCode.InternalServerError, "Exception: " + ex.Message)); 
			}
		}
	}
}
