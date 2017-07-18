using System;
using System.Collections.Generic;
using System.Text;

namespace Pirates.Telegram.Events
{
    internal class TelegramCallbackEventArgs : EventArgs
    {
        private readonly string _from, _data;

        public TelegramCallbackEventArgs(string from, string data)
        {
            _from = from;
            _data = data;
        }

        public string From { get { return _from; } }
        public string Data { get { return _data; } }
    }
}
