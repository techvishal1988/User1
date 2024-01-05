using Framework.Service.DTO.Error;
using Framework.Service.Validators;

namespace User.Validators.Base
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="SMARTS2.Framework.Validators.IRequestDataValidator" />
    public class BaseRequestDataValidator : IRequestDataValidator
    {
        /// <summary>
        /// Validates the specified data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual List<Error> Validate<T>(T data)
        {
            var result = new List<Error>();
            if (data == null)
            {
                result.Add(new Error() { DevMessage = "Params are null" });
            }
            return result;
        }
    }
}
