using Framework.Constant;
using Framework.Service.DTO.Error;
using Framework.Service.Enums;
using Framework.Service.Validators;
using User.Models;

namespace User.Validators
{
    public class RegisterRequestModelValidator : IRequestDataValidator
    {
        public List<Error> Validate<T>(T data)
        {
            var result = new List<Error>();
            if (data == null)
            {
                result.Add(new Error() { DevMessage = ErrorConstant.InvalidRequestParameter });
                return result;
            }
            var requestModel = data as RegistrationRequestModel;

            if (!requestModel.Email.IsValidEmail())
                result.Add(new Error() { UserMessage = ErrorConstant.InvalidEmail, ErrorCode = (int)ErrorCode.InvalidEmail });
            else if (string.IsNullOrWhiteSpace(requestModel.UserName))
                result.Add(new Error() { UserMessage = ErrorConstant.InvalidUserName, ErrorCode = (int)ErrorCode.InvalidUserName });
            return result;
        }
    }
}
