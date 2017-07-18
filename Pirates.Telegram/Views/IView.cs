using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types.ReplyMarkups;

namespace Pirates.Telegram.Views
{
    /// <summary>
    /// Telegram view must be able to return TelegramView and apply action
    /// each view should has unique name
    /// </summary>
    interface IView
    {
        string Name { get; }
        void ApplyAction(string action);
        TelegramView Update();
    }
}
