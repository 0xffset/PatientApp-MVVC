using PatientApp.Core.Application.Helpers;
using PatientApp.Core.Application.ViewModerls.User;

namespace PatientApp.Middlewares
{
    public class UserSessionValidation
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserSessionValidation(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public bool HasUser()
        {
            UserViewModel userViewModel = httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");

            if (userViewModel == null)
            {
                return false;
            }
            return true;
        }
    }
}
