

namespace BrightMinds.Services.ServiceResponse
{
    public class GenericResponse<T>
    {

        public bool Success {  get; set; } 
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public T Data {  get; set; }
    }
}
