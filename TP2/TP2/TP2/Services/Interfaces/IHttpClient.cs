using System.Threading.Tasks;

namespace TP2.Services.Interfaces
{
    public interface IHttpClient
    {
        void GetUrl(string url);
        Task<string> GetLocationAsync(string key, string region, string language);
    }
}
