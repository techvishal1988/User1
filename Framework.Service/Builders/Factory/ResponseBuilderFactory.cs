using Framework.Service.Builders.Abstraction;

namespace Framework.Service.Builders.Factory
{
    /// <summary>
    /// 
    /// </summary>
    public class ResponseBuilderFactory : IResponseBuilderFactory
    {
        /// <summary>
        /// Gets the builder.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IResponseBuilder<T> GetBuilder<T>()
        {
            return new ResponseBuilder<T>();
        }
    }
}
