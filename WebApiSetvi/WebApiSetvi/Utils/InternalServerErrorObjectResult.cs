
using Microsoft.AspNetCore.Mvc;
namespace WebApiSetvi.Utils
{
	public class InternalServerErrorObjectResult : ObjectResult
	{
		public InternalServerErrorObjectResult(object value) : base(value)
		{
			StatusCode = 500;
		}
	}
}
