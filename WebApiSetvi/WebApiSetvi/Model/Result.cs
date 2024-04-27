using System.Net;

namespace WebApiSetvi.Model
{

	public class Result<T>
	{
		public Result(T data)
		{
			Data = data;
		}
		public Result(HttpStatusCode statuscode, string errorMessage)
		{
			StatusCode = statuscode;
			ErrorMessage = errorMessage;
		}
		public T? Data { get; set; }
		public string? ErrorMessage { get; set; }
		public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
	}
}
