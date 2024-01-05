using Framework.Service.Validators;
using UserManagement.Models;
using UserManagement.Validators.Base;

namespace UserManagement.Validators.Factory
{
    public class RequestDataValidatorFactory : IRequestDataValidatorFactory
    {
        public IRequestDataValidator GetValidator<T>() where T : class
        {
            return typeof(T).Name switch
            {
                (nameof(RegistrationRequestModel)) => new RegisterRequestModelValidator(),
                _ => new BaseRequestDataValidator(),

            };
        }
    }
}
