using Microsoft.Extensions.Configuration;
using Pirates.Server;
using Pirates.Telegram.Managers;
using System;
using System.IO;


namespace Pirates.Telegram
{
    class Program
    {
        private static IConfigurationRoot Configuration { get; set; }
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("settings.json");

            Configuration = builder.Build();

            // Initialize server, telegram application for wrap all request and callback and connect with user manager
            var server = new BasicServer(Int32.Parse(Configuration["width"]), Int32.Parse(Configuration["height"]));

            var telegramApp = new TelegramApp(Configuration["telegramToken"], server);
            var userManager = new TelegramUserManager(telegramApp);

            // Run listener
            telegramApp.Start();
            Console.ReadLine();
            telegramApp.Stop();
        }
    }
}