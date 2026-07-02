using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

class Program
{
    private static DiscordSocketClient _client = null!;

    static async Task Main(string[] args)
    {
        // Token'ı Render'dan çekiyoruz
        string? token = Environment.GetEnvironmentVariable("GIF_BOT_TOKEN");

        if (string.IsNullOrEmpty(token))
        {
            Console.WriteLine("❌ KRİTİK HATA: 'GIF_BOT_TOKEN' ortam değişkeni bulunamadı!");
            return;
        }

        await RunBotAsync(token);
    }

    public static async Task RunBotAsync(string token)
    {
        var config = new DiscordSocketConfig { GatewayIntents = GatewayIntents.All | GatewayIntents.MessageContent };
        _client = new DiscordSocketClient(config);

        _client.Ready += async () => {
            // Yayında modu ve Twitch linki
            await _client.SetGameAsync("Dash Shop Aktif!", "https://www.twitch.tv/monstercat", ActivityType.Streaming);
            Console.WriteLine("✅ BOT BAĞLANDI VE YAYINA GİRDİ!");
        };

        _client.MessageReceived += async (message) =>
        {
            if (message.Author.IsBot || !message.Content.ToLower().StartsWith("!intro ")) return;

            string tur = message.Content.ToLower().Replace("!intro ", "").Trim();
            await message.Channel.SendMessageAsync($"🎬 **{tur.ToUpper()}** stili için talebin alındı reis!");
        };

        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();

        await Task.Delay(-1);
    }
}
