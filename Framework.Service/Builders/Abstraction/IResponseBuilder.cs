using Framework.Service.DTO;
using Framework.Service.DTO.Error;

namespace Framework.Service.Builders.Abstraction
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IResponseBuilder<T>
    {
        /// <summary>
        /// Adds the HTTP status.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <returns></returns>
        ResponseBuilder<T> AddHttpStatus(int? status);

        /// <summary>
        /// Adds the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        ResponseBuilder<T> AddMessage(string message);

        /// <summary>
        /// Adds the success data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        ResponseBuilder<T> AddSuccessData(T data);

        /// <summary>
        /// Adds the error.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns></returns>
        ResponseBuilder<T> AddError(Error error);

        /// <summary>
        /// Adds the error.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        ResponseBuilder<T> AddError(object error, int code = 000);

        /// <summary>
        /// Adds the errors.
        /// </summary>
        /// <param name="errors">The errors.</param>
        /// <returns></returns>
        ResponseBuilder<T> AddErrors(List<Error> errors);

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        APIResponse<T> Build();
    }
}
