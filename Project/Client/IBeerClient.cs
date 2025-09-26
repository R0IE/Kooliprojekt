using System.Collections.Generic;
using System.Threading.Tasks;
using KooliProjekt.Models;

namespace KooliProjekt.Services
{
    public interface IBeerClient
    {
        Task<List<Beer>> GetBreweriesAsync(int perPage = 20);
    }
}
