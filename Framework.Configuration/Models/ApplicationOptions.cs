namespace Framework.Configuration.Models
{
    using Framework.Configuration;
    using System;

    /// <summary>
    /// Defines the <see cref="ApplicationOptions" />.
    /// </summary>
    public class ApplicationOptions : ConfigurationOptions
    {

        /// <summary>
        /// Gets or sets the awsConfigurationOptions.
        /// </summary>
        public AWSConfigurationOptions awsConfigurationOptions { get; set; }
    }
}
