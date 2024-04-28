
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApiSetvi.Model;


namespace WebApiSetvi.Utils
{
	public static class ResponseHandler
	{
		public static ObjectResult CreateResponse<T>(Result<T> content)
		{
			// In the case that we want to always receive a response with a status of 200 and then consider the nested status
			//return new OkObjectResult(content);

			//In case that we want to seperate status 200, 400 or 500
			return content.StatusCode switch
			{
				HttpStatusCode.BadRequest => new BadRequestObjectResult(content),
				//Custom ObjectResult for Internal server error
				HttpStatusCode.InternalServerError => new InternalServerErrorObjectResult(content),
				_ => new OkObjectResult(content),
			};
		}
	}
}
