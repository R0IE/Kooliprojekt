using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.Generic;
using KooliProjekt.Models;

namespace KooliProjekt.Services
{
    public class BeerClient : IBeerClient
    {
    private readonly HttpClient _httpClient;

    private const string openBreweryUrl = "https://api.openbrewerydb.org/v1/breweries";

        public BeerClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Beer>> GetBreweriesAsync(int perPage = 20)
        {
            try
            {
                var url = openBreweryUrl + "?per_page=" + perPage;
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var responseJson = await response.Content.ReadAsStringAsync();

                var list = new List<Beer>();
                try
                {
                    using var doc = JsonDocument.Parse(responseJson);
                    var arr = doc.RootElement.EnumerateArray();
                    foreach (var el in arr)
                    {
                        var beer = new Beer
                        {
                            Id = el.TryGetProperty("id", out var idp) ? idp.GetString() ?? "" : "",
                            Name = el.TryGetProperty("name", out var np) ? np.GetString() : null,
                            BreweryType = el.TryGetProperty("brewery_type", out var bt) ? bt.GetString() : null,
                            City = el.TryGetProperty("city", out var cp) ? cp.GetString() : null,
                            State = el.TryGetProperty("state", out var sp) ? sp.GetString() : null,
                            Country = el.TryGetProperty("country", out var counp) ? counp.GetString() : null,
                            WebsiteUrl = el.TryGetProperty("website_url", out var wp) ? wp.GetString() : null,
                            ImageUrl = null
                        };
                        
                        list.Add(beer);
                    }
                }
                catch (JsonException)
                {
                }

                if (list.Count == 0)
                {
                    list = new List<Beer>
                    {
                        new Beer { Id = "sample-1", Name = "Copper Kettle Brewery", BreweryType = "micro", City = "Tallinn", State = "Harjumaa", Country = "Estonia", WebsiteUrl = "https://example.com/copper-kettle", ImageUrl = "/assets/img/sample-beer-1.jpg" },
                        new Beer { Id = "sample-2", Name = "Old Harbor Ales", BreweryType = "regional", City = "Tartu", State = "Tartumaa", Country = "Estonia", WebsiteUrl = "https://example.com/old-harbor", ImageUrl = "/assets/img/sample-beer-2.jpg" },
                        new Beer { Id = "sample-3", Name = "Forest Road Brewing", BreweryType = "brewpub", City = "Pärnu", State = "Pärnumaa", Country = "Estonia", WebsiteUrl = "https://example.com/forest-road", ImageUrl = "/assets/img/sample-beer-3.jpg" }
                    };
                }

                return list;
            }
            catch
            {
                return new List<Beer>
                {
                    new Beer { Id = "sample-1", Name = "Copper Kettle Brewery", BreweryType = "micro", City = "Tallinn", State = "Harjumaa", Country = "Estonia", WebsiteUrl = "https://example.com/copper-kettle", ImageUrl = "/assets/img/sample-beer-1.jpg" }
                };
            }
        }
    }
}
