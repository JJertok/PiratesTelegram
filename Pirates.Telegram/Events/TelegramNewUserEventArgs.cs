using Pirates.Telegram.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pirates.Telegram.Events
{
    internal class TelegramNewUserEventArgs : EventArgs
    {
        private TelegramUser _telegramUser;

        public TelegramNewUserEventArgs(TelegramUser telegramUser)
        {
            _telegramUser = telegramUser;
        }

        public TelegramUser TelegramUser { get { return _telegramUser; } }
    }
}
