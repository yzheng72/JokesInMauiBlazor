namespace JokesInMAUI.Data;

public interface IJokeFetchingService
{
    public Task<Joke> GetJoke();

    public Task<Joke> GetDarkJoke();
}

