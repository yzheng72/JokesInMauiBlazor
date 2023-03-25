using Newtonsoft.Json.Linq;
using System.Web;

namespace JokesInMAUI.Data;

public class JokeFetchingService : IJokeFetchingService
{
    private static readonly string UrlBase = @"https://v2.jokeapi.dev";
    public async Task<Joke> GetJoke()
    {
        Joke joke = new Joke { Setup = "what's up", Delivery = "not much" };
        await Task.Delay(200);
        return joke;
    }

    public async Task<Joke> GetDarkJoke()
    {
        var builder = new UriBuilder(UrlBase);
        builder.Path = "joke/Dark";
        builder.Port = -1;
        var query = HttpUtility.ParseQueryString("");
        query["type"] = "twopart";
        builder.Query = query.ToString();
        string url = builder.ToString();

        using var client = new HttpClient();
        var response = await client.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException("Not able to get a joke at this time");
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        JObject result = JObject.Parse(responseContent);
        Joke joke = result.ToObject<Joke>();

        return joke;
    }
}
