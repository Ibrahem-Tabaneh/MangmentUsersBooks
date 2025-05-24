namespace WebBook.Models
{
    public class ApiResponse
    {
        // data coming from api 
        public object Data { get; set; }
        // list of api errors
        public object Errors { get; set; }
        //status code 200=success 502=failed
        public string StatusCode { get; set; }
    }
}
