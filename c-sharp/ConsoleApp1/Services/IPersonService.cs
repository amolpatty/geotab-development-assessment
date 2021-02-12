using JokeGenerator.Models;
using System.Threading.Tasks;

namespace JokeGenerator.Services
{
    public interface IPersonService
    {
        Task<IPerson> GetNamesAsync();
    }
}
