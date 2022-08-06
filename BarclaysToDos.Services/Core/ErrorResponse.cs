namespace BarclaysToDos.Services.Core
{
    public class ErrorResponse
    {
        /// <summary>
        /// It is created to manage the return response to show more than one error condition at the same time.
        /// </summary>
        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
    }
}
