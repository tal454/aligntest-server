

namespace align_test.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string? message)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad request you have made",
                404 => "Resource not found",
                500 => "Internal error",
                _ => "NA"
            };
        }
    }
}
