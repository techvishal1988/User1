using Framework.Service.DTO.Error;

namespace Framework.Service.Validators
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRequestDataValidator
    {
        /// <summary>
        /// Validates the specified data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        List<Error> Validate<T>(T data);
    }
}
