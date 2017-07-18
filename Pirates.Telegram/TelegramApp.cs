using Pirates.Server;
using Pirates.Telegram.Events;
using Pirates.Telegram.Managers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Pirates.Telegram
{
    /// <summary>
    /// Telegram wrapper
    /// </summary>
    class TelegramApp
    {
        //private 
        private readonly TelegramBotClient _bot;
        private readonly IDictionary<string, TelegramUser> _users;
        private readonly BasicServer _server;

        public event EventHandler<TelegramCallbackEventArgs> CallbackReceived;
        public event EventHandler<TelegramNewUserEventArgs> NewUser;
        public TelegramApp(string token, BasicServer server)
        {
            _users = new Dictionary<string, TelegramUser>();
            _server = server;
            _bot = new TelegramBotClient(token);

            _bot.OnCallbackQuery += receiveCallback;
            _bot.OnMessage += receiveMessage;
        }

        public void Start()
        {
            _bot.StartReceiving();
            Console.WriteLine("Started..");
        }

        public void Stop()
        {
            _bot.StopReceiving();
        }

        public void SendMessage(string username, string message, IReplyMarkup keyboard)
        {
            if (!_users.ContainsKey(username))
            {
                return;
            }
            var dialog = _users[username].Message;

            var newDialog = _bot.SendTextMessageAsync(dialog.Chat.Id, message, replyMarkup: keyboard).Result;
            _users[username].Message = newDialog;
        }

        public void UpdateLastMessage(string username, string message, IReplyMarkup keyboard)
        {
            if (!_users.ContainsKey(username))
            {
                return;
            }
            var dialog = _users[username].Message;


            //if (dialog.Text.Equals(message)) return;
            // TODO: nonce generator
            //var random = new Random();
            //message += "\n" + random.NextDouble();


            var newDialog = _bot.EditMessageTextAsync(dialog.Chat.Id, dialog.MessageId, message, replyMarkup: keyboard).Result;
            _users[username].Message = newDialog;
        }


        private async void receiveMessage(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (message == null || message.Type != MessageType.TextMessage) return;


            var messageText = message.Text;

            var newDialog = await _bot.SendTextMessageAsync(message.Chat.Id, string.Format("Hello {0}", e.Message.From.Username),
              replyMarkup: new ReplyKeyboardHide());

            if (!_users.ContainsKey(message.From.Username))
            {
                var newUser = _server.GetOrCreateUser(message.From.Username);
                var telegramUser = new TelegramUser(message.From.Username, newUser, newDialog);
                _users.Add(message.From.Username, telegramUser);
                NewUser?.Invoke(this, new TelegramNewUserEventArgs(telegramUser));
            }
        }

        private void receiveCallback(object sender, CallbackQueryEventArgs e)
        {
            CallbackReceived?.Invoke(this, new TelegramCallbackEventArgs(e.CallbackQuery.From.Username, e.CallbackQuery.Data));
        }
    }
}
