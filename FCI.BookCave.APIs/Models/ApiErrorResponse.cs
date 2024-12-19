namespace FCI.BookCave.APIs.Models
{
	public class ApiErrorResponse
	{
		public int StatusCode { get; set; }
		public string ErrorMessage { get; set; }

		public ApiErrorResponse(int statusCode, string errorMessage)
		{
			StatusCode = statusCode;
			ErrorMessage = errorMessage;
		}
	}
}
