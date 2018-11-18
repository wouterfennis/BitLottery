using System.Net.Http;
using System.Threading.Tasks;

namespace BitLottery.RandomService.HttpClientWrapper
{
    /// <summary>
    /// Wrapper for the System.Net.HttpClient
    /// </summary>
    public interface IHttpClientWrapper
    {
        /// <summary>
        /// Wrapper for the PostAsync method
        /// </summary>
        Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content);
    }
}