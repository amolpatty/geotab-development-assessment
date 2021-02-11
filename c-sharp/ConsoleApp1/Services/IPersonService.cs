using System.Threading.Tasks;

namespace JokeGenerator.Services
{
    public interface IPersonService
    {
        Task<dynamic> GetNamesAsync();
    }
}
