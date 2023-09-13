using Erfa.IdentityService.ViewModels.Common;

namespace Erfa.IdentityService.ViewModels.Error
{
    public class ErrorResponse : IdentityResponse
    {
        public string[] Errors { get; set; } = new string[0];
        public ErrorResponse()
        {
            IsSuccess = false;
            StatusCode = 400;
        }
    }
}
