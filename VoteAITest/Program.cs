using Newtonsoft.Json;
using VoteAITest;
using VoteAITest.Models;

public class Program
{
    static async Task Main(string[] args)
    {
        // Init data
        Console.WriteLine("Enter word to create: ");
        var word = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(word))
            word = Constants.Apple;

        // Create word
        await CreateWordAsync(word);

        Console.WriteLine("Enter word to guess: ");
        var guess = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(guess))
            guess = Console.ReadLine();

        // Guess word
        await GuessWordAsync(word, guess);

        Console.ReadKey();
    }

    // Create word
    static async Task CreateWordAsync(string word)
    {
        var pairs = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("text", word)
    };
        using var httpClient = new HttpClient();
        using var req = new HttpRequestMessage(HttpMethod.Post, Constants.ApiUrl + "/wordseg") { Content = new FormUrlEncodedContent(pairs) };
        using var res = await httpClient.SendAsync(req);

        if (res.IsSuccessStatusCode)
            Console.WriteLine("Word created");
        else
            Console.WriteLine("Failed to create word");
    }


    // Guess word
    static async Task GuessWordAsync(string targetWord, string? guess)
    {
        using var httpClient = new HttpClient();
        var response = await httpClient.GetAsync($"{Constants.ApiUrl}/word/{targetWord}?guess={guess}");

        var json = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<List<GuessResult>>(json);

        if (result != null && result.Any())
            foreach (var item in result)
            {
                Console.WriteLine($"position {item.slot} - char {item.guess} - result {item.result}");
            }

    }
}