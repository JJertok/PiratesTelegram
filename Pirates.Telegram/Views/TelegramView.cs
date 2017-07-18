using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types.ReplyMarkups;

namespace Pirates.Telegram.Views
{
    /// <summary>
    /// Simple telegram view should contain message and keyboard layout
    /// </summary>
    public class TelegramView
    {
        public string Message { get; set; }
        public IReplyMarkup Keyboard { get; set; }
    }
}
