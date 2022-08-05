namespace WebHangHoa.Models
{
    public class APIResponse
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public string message { get; set; }
        public object Data { get; set; }
    }
}
