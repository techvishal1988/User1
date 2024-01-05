using Framework.Service.Validators;
using User.Models;
using User.Validators.Base;

namespace User.Validators.Factory
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
