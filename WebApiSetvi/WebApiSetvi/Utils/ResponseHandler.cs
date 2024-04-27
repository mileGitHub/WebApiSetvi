
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApiSetvi.Model;


namespace WebApiSetvi.Utils
{
	public static class ResponseHandler
	{
		public static ObjectResult CreateResponse<T>(Result<T> content)
		{
			return content.StatusCode switch
			{
				HttpStatusCode.BadRequest => new BadRequestObjectResult(content),
				HttpStatusCode.InternalServerError => new InternalServerErrorObjectResult(content),
				_ => new OkObjectResult(content),
			};
		}
	}
}
