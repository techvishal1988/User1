using Amazon.CognitoIdentityProvider.Model;
using Framework.Service.DTO;
using User.Models;

namespace User.Services
{
    public interface IUserManagement
    {
        public Task<APIResponse<RegistrationResponseModel>> AdminCreateUserAsync(RegistrationRequestModel model);

        public Task AdminAddUserToGroupAsync(string username, string userPoolId, string groupName);

        public Task<AdminInitiateAuthResponse> AdminAuthenticateUserAsync(string username, string password, string userPoolId, string appClientId);

        public Task AdminRemoveUserFromGroupAsync(string username, string userPoolId, string groupName);

        public Task AdminDisableUserAsync(string username, string userPoolId);

        public Task AdminDeleteUserAsync(string username, string userPoolId);

        public Task<APIResponse<LoginResponseModel>> SignIn(LoginRequestModel model);
    }
}
