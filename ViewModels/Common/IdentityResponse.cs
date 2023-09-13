namespace Erfa.IdentityService.ViewModels.Common
{
    public class IdentityResponse
    {
        public string Message { get; set; } = string.Empty;
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
    }
}
