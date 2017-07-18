using Pirates.Engine.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types;

namespace Pirates.Telegram.Managers
{
    class TelegramUser
    {
        public string Username { get; set; }
        public Message Message { get; set; }
        public Player Player { get; set; }
        public string ViewName { get; set; }

        public TelegramUser(string username, Player player, Message message)
        {
            Username = username;
            Player = player;
            Message = message;
        }
    }
}
