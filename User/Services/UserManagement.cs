using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;
using Amazon.Runtime;
using Framework.Constant;
using Framework.Service.Builders.Factory;
using Framework.Service.DTO;
using Framework.Service.DTO.Error;
using Framework.Service.Enums;
using Framework.Service.Validators;
using Microsoft.Extensions.Options;
using User.Configurations;
using User.Models;
using User.Validators;

namespace User.Services
{
    public class UserManagement : IUserManagement
    {
        private readonly AmazonCognitoIdentityProviderClient adminAmazonCognitoIdentityProviderClient;
        private readonly IOptions<AWSConfigurationOptions> _options;

        private readonly IResponseBuilderFactory _responseBuilderFactory;
        readonly IRequestDataValidatorFactory _requestDataValidatorFactory;
        public UserManagement(IOptions<AWSConfigurationOptions> options, IResponseBuilderFactory responseBuilderFactory, IRequestDataValidatorFactory requestDataValidatorFactory)
        {
            adminAmazonCognitoIdentityProviderClient = new AmazonCognitoIdentityProviderClient(
                  options.Value?.ClientId,
                  options.Value?.ClientSecret);
            _options = options;
            _responseBuilderFactory = responseBuilderFactory;
            _requestDataValidatorFactory = requestDataValidatorFactory;
        }

        public async Task<APIResponse<RegistrationResponseModel>> AdminCreateUserAsync(RegistrationRequestModel model)
        {
            var responseBuilder = _responseBuilderFactory.GetBuilder<RegistrationResponseModel>();
           
            var errors = _requestDataValidatorFactory.GetValidator<RegistrationRequestModel>().Validate(model);
            if (errors.Count.IsValid())
                return responseBuilder.AddErrors(errors).Build();

            var ifExist = await CreateUserIfNotExist(model.UserName, model.Password, _options.Value.UserPoolId, default);
            if (!ifExist)
            {
                return responseBuilder.AddError(new Error() { DevMessage = ErrorConstant.UserAlreadyExist, ErrorCode = (int)ErrorCode.DuplicateFound }).Build();
            }

            AdminUpdateUserAttributesRequest adminUpdateUserAttributesRequest = new AdminUpdateUserAttributesRequest
            {
                Username = model.UserName,
                UserPoolId = _options.Value.UserPoolId,
                UserAttributes = new List<AttributeType>
                    {
                        new AttributeType()
                        {
                            Name = "email_verified",
                            Value = "true"
                        },
                        new AttributeType()
                        {
                            Name = "email",
                            Value = model.Email
                        }
                    }
            };

            AdminUpdateUserAttributesResponse adminUpdateUserAttributesResponse = adminAmazonCognitoIdentityProviderClient
                .AdminUpdateUserAttributesAsync(adminUpdateUserAttributesRequest)
                .Result;


            AdminInitiateAuthRequest adminInitiateAuthRequest = new AdminInitiateAuthRequest
            {
                UserPoolId = _options.Value.UserPoolId,
                ClientId = _options.Value.UserPoolClientId,
                AuthFlow = AuthFlowType.ADMIN_NO_SRP_AUTH,
                AuthParameters = new Dictionary<string, string>
                {
                    { "USERNAME", model.UserName},
                    { "PASSWORD", model.Password},
                }
            };

            AdminInitiateAuthResponse adminInitiateAuthResponse = await adminAmazonCognitoIdentityProviderClient
                .AdminInitiateAuthAsync(adminInitiateAuthRequest)
                .ConfigureAwait(false);

            AdminRespondToAuthChallengeRequest adminRespondToAuthChallengeRequest = new AdminRespondToAuthChallengeRequest
            {
                ChallengeName = ChallengeNameType.NEW_PASSWORD_REQUIRED,
                ClientId = _options.Value.UserPoolClientId,
                UserPoolId = _options.Value.UserPoolId,
                ChallengeResponses = new Dictionary<string, string>
                    {
                        { "USERNAME", model.UserName },
                        { "NEW_PASSWORD", model.Password },                        
                    },
                Session = adminInitiateAuthResponse.Session
            };

            AdminRespondToAuthChallengeResponse adminRespondToAuthChallengeResponse = adminAmazonCognitoIdentityProviderClient
                .AdminRespondToAuthChallengeAsync(adminRespondToAuthChallengeRequest)
                .Result;

            var result = new RegistrationResponseModel()
            {
                Message = ErrorConstant.UserCreated
            };            
            return responseBuilder.AddSuccessData(result).Build();
        }

        public async Task AdminAddUserToGroupAsync(
            string username,
            string userPoolId,
            string groupName)
        {
            AdminAddUserToGroupRequest adminAddUserToGroupRequest = new AdminAddUserToGroupRequest
            {
                Username = username,
                UserPoolId = userPoolId,
                GroupName = groupName
            };

            AdminAddUserToGroupResponse adminAddUserToGroupResponse = await adminAmazonCognitoIdentityProviderClient
                .AdminAddUserToGroupAsync(adminAddUserToGroupRequest)
                .ConfigureAwait(false);
        }

        public async Task<AdminInitiateAuthResponse> AdminAuthenticateUserAsync(
            string username,
            string password,
            string userPoolId,
            string appClientId)
        {
            AdminInitiateAuthRequest adminInitiateAuthRequest = new AdminInitiateAuthRequest
            {
                UserPoolId = userPoolId,
                ClientId = appClientId,
                AuthFlow = AuthFlowType.ADMIN_NO_SRP_AUTH,
                AuthParameters = new Dictionary<string, string>
            {
                { "USERNAME", username},
                { "PASSWORD", password}
            }
            };
            return await adminAmazonCognitoIdentityProviderClient
                .AdminInitiateAuthAsync(adminInitiateAuthRequest)
                .ConfigureAwait(false);
        }

        public async Task AdminRemoveUserFromGroupAsync(
            string username,
            string userPoolId,
            string groupName)
        {
            AdminRemoveUserFromGroupRequest adminRemoveUserFromGroupRequest = new AdminRemoveUserFromGroupRequest
            {
                Username = username,
                UserPoolId = userPoolId,
                GroupName = groupName
            };

            await adminAmazonCognitoIdentityProviderClient
                .AdminRemoveUserFromGroupAsync(adminRemoveUserFromGroupRequest)
                .ConfigureAwait(false);
        }

        public async Task AdminDisableUserAsync(
            string username,
            string userPoolId)
        {
            AdminDisableUserRequest adminDisableUserRequest = new AdminDisableUserRequest
            {
                Username = username,
                UserPoolId = userPoolId
            };

            await adminAmazonCognitoIdentityProviderClient
                .AdminDisableUserAsync(adminDisableUserRequest)
                .ConfigureAwait(false);
        }

        public async Task AdminDeleteUserAsync(
            string username,
            string userPoolId)
        {
            AdminDeleteUserRequest deleteUserRequest = new AdminDeleteUserRequest
            {
                Username = username,
                UserPoolId = userPoolId
            };

            await adminAmazonCognitoIdentityProviderClient
                .AdminDeleteUserAsync(deleteUserRequest)
                .ConfigureAwait(false);
        }

        public async Task<APIResponse<LoginResponseModel>> SignIn(LoginRequestModel model)
        {
            var responseBuilder = _responseBuilderFactory.GetBuilder<LoginResponseModel>();

            AmazonCognitoIdentityProviderClient provider = new AmazonCognitoIdentityProviderClient(new AnonymousAWSCredentials());
            CognitoUserPool userPool = new CognitoUserPool(_options.Value?.UserPoolId, _options.Value?.UserPoolClientId, provider);
            CognitoUser user = new CognitoUser(model.UserName, _options.Value?.UserPoolClientId, userPool, provider);
            InitiateSrpAuthRequest authRequest = new InitiateSrpAuthRequest()
            {
                Password = model.Password
            };

            AuthFlowResponse authResponse = await user.StartWithSrpAuthAsync(authRequest).ConfigureAwait(false);

            if (authResponse.ChallengeName == ChallengeNameType.NEW_PASSWORD_REQUIRED)
            {
                return responseBuilder.AddError(new Error() { DevMessage = ErrorConstant.NewPasswordRequired, ErrorCode = (int)ErrorCode.NewPasswordRequired }).Build();
            }

            LoginResponseModel responseModel = new LoginResponseModel()
            {
                AccessToken = authResponse.AuthenticationResult.AccessToken,
                ExpiresIn = authResponse.AuthenticationResult.ExpiresIn,
                RefreshToken = authResponse.AuthenticationResult.RefreshToken
            };
            return responseBuilder.AddSuccessData(responseModel).Build(); ;
        }

        public async Task<bool> CreateUserIfNotExist(string username, string password, string userPoolId, List<AttributeType> attributeTypes)
        {
            try
            {
                AdminCreateUserRequest adminCreateUserRequest = new AdminCreateUserRequest
                {
                    Username = username,
                    TemporaryPassword = password,
                    UserPoolId = userPoolId,
                    UserAttributes = attributeTypes,
                };
                AdminCreateUserResponse adminCreateUserResponse = await adminAmazonCognitoIdentityProviderClient
                    .AdminCreateUserAsync(adminCreateUserRequest)
                    .ConfigureAwait(false);

                return true;
            }
            catch (UsernameExistsException ex)
            {
                return false;
            }
        }
    }
}
