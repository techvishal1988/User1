﻿using Framework.Configuration.Models;
using Framework.Constant;
using Framework.Service;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using UserManagement.Models;

namespace UserManagement.Extensions
{
    /// <summary>
    /// Defines the <see cref="ServicesConfiguration" />.
    /// </summary>
    public static class ServicesConfiguration
    {
        /// <summary>
        /// The ConfigureClientServices.
        /// </summary>
        /// <param name="services">The services<see cref="IServiceCollection"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection ConfigureClientServices(this IServiceCollection services)
        {
            var v = string.Empty;
            var serviceProvider = services.BuildServiceProvider();
            var applicationOptions = serviceProvider.GetRequiredService<ApplicationOptions>();
            return services;
        }

        /// <summary>
        /// The ConfigureSwagger.
        /// </summary>
        /// <param name = "services" > The services<see cref="IServiceCollection"/>.</param>
        /// <returns>The<see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            var swaggerAssemblies = new[] { typeof(RegistrationRequestModel).Assembly, typeof(Model).Assembly };
            services.AddSwaggerWithComments(ApiConstants.ApiName, ApiConstants.ApiVersion, swaggerAssemblies);
            services.AddSwaggerWithComments(ApiConstants.JobsApiName, ApiConstants.JobsApiVersion, swaggerAssemblies);
            return services;
        }

        /// <summary>
        /// The ConfigureJwtSecurity.
        /// </summary>
        /// <param name="services">The services<see cref="IServiceCollection"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        //public static IServiceCollection ConfigureJwtSecurity(this IServiceCollection services)
        //{
        //    services.ConfigureSecurityServices();
        //    var serviceProvider = services.BuildServiceProvider();
        //    var securityOptions = serviceProvider.GetRequiredService<SecurityOptions>();
        //    AuthenticationTechniqueFactory.GetAuthentication(AuthenticationTechnique.JWTToken, services, securityOptions.JwtIssuerOptions, securityOptions.AuthSettings);
        //    return services;
        //}
    }
}
