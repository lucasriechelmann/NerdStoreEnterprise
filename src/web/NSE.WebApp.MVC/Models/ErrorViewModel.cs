namespace NSE.WebApp.MVC.Models
{
    public class ErrorViewModel
    {
        public ErrorViewModel()
        {                
        }
        public ErrorViewModel(int errorCode)
        {
            ErrorCode = errorCode;
        }
        public string Message { get; set; }
        public string Title { get; set; }
        public int ErrorCode { get; set; }
    }
}