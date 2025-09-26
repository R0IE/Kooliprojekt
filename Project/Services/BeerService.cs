using System.Collections.Generic;
using System.Threading.Tasks;
using KooliProjekt.Models;

namespace KooliProjekt.Services
{
    public class BeerService
    {
        private readonly IBeerClient _beerClient;

        public BeerService(IBeerClient beerClient)
        {
            _beerClient = beerClient;
        }

        public async Task<List<Beer>> GetBreweriesAsync(int perPage = 20)
        {
            try
            {
                var list = await _beerClient.GetBreweriesAsync(perPage);
                return list ?? new List<Beer>();
            }
            catch
            {
                // On any client/network error return empty list so the UI still renders
                // and the view will display sample breweries populated by the client if needed
                return new List<Beer>();
            }
        }
    }
}
