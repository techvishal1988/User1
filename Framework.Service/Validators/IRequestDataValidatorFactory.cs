namespace Framework.Service.Validators
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRequestDataValidatorFactory
    {
        /// <summary>
        /// Gets the validator.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IRequestDataValidator GetValidator<T>() where T : class;
    }
}
