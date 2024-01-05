using Framework.Service.Builders.Abstraction;

namespace Framework.Service.Builders.Factory
{
    /// <summary>
    /// 
    /// </summary>
    public interface IResponseBuilderFactory
    {
        /// <summary>
        /// Gets the builder.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IResponseBuilder<T> GetBuilder<T>();
    }
}
