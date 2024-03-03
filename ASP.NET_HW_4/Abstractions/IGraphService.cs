namespace AppAuthLesson4.Abstractions
{
    public interface IGraphService
    {
        Task<T?> Get<T>(string url);
        Task<string> Post<T>(string url, T request);
    }
}
