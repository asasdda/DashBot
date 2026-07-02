using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // 1. Render'daki "GIF_BOT_TOKEN" ortam değişkenini okur
        string? token = Environment.GetEnvironmentVariable("GIF_BOT_TOKEN");

        if (string.IsNullOrEmpty(token))
        {
            Console.WriteLine("❌ HATA: GIF_BOT_TOKEN bulunamadı!");
            return;
        }

        var client = new DiscordSocketClient(new DiscordSocketConfig 
        { 
            GatewayIntents = GatewayIntents.None 
        });

        client.Ready += async () => {
            // 2. 7/24 "Dash Shop Aktif!" yayını (Twitch linki mor ikon için şart)
            await client.SetGameAsync("Dash Shop Aktif!", "https://www.twitch.tv/monstercat", ActivityType.Streaming);
            Console.WriteLine("✅ BOT 7/24 AKTİF VE YAYINDA!");
        };

        await client.LoginAsync(TokenType.Bot, token);
        await client.StartAsync();

        // 3. Botu ayakta tutan sonsuz döngü (Uyaran mekanizması)
        await Task.Delay(-1);
    }
}
