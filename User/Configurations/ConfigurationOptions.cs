namespace User.Configurations
{
    public abstract class ConfigurationOptions : IConfigurationOptions
    {
        /// <summary>
        /// Defines the Suffix.
        /// </summary>
        private const string Suffix = ConfigurationConstant.ConfigurationOptionsSuffix;

        /// <summary>
        /// Defines the _sectionName.
        /// </summary>
        private string _sectionName;

        /// <summary>
        /// Gets or sets the SectionName.
        /// </summary>
        public string SectionName { get => GetSectionName(); set => _sectionName = value; }

        /// <summary>
        /// The GetSectionName.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        private string GetSectionName()
        {
            if (string.IsNullOrWhiteSpace(_sectionName))
            {
                var className = GetType().Name;

                if (className.EndsWith(Suffix, StringComparison.OrdinalIgnoreCase))
                {
                    _sectionName = className.Substring(0, className.Length - Suffix.Length);
                }
            }

            return _sectionName;
        }
    }
}
