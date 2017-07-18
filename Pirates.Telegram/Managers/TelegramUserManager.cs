using Pirates.Engine.Models;
using Pirates.Telegram.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pirates.Telegram.Managers
{
    /// <summary>
    /// Store and manage all telegram users
    /// </summary>
    class TelegramUserManager
    {
        #region Private fields
        private IList<TelegramUser> _users;
        private TelegramApp _app;
        #endregion

        public TelegramUserManager(TelegramApp app)
        {
            _users = new List<TelegramUser>();
            _app = app;

            // Add event handlers for events from telegram wrapper
            _app.CallbackReceived += userCallbackAction;
            _app.NewUser += newUser;
        }

        private void newUser(object sender, Events.TelegramNewUserEventArgs e)
        {
            if (e.TelegramUser == null)
            {
                return;
            }

            Add(e.TelegramUser);
            var view = ViewBuilder.Build(e.TelegramUser.Player);

            UpdateView(e.TelegramUser, view);
        }

        private void userCallbackAction(object sender, Events.TelegramCallbackEventArgs e)
        {
            var user = _users.FirstOrDefault(x => x.Username == e.From);

            if (user == null)
            {
                return;
            }

            var view = ViewBuilder.Build(user.Player);
            view.ApplyAction(e.Data);

            //UpdateView(user, view);
        }

        /// <summary>
        /// Add new telegram user to user manager
        /// </summary>
        /// <param name="telegramUser"></param>
        public void Add(TelegramUser telegramUser)
        {
            _users.Add(telegramUser);
            telegramUser.Player.PropertyChanged += PropertyChanged;
        }

        /// <summary>
        /// Remove user from user manager
        /// </summary>
        /// <param name="telegramUser"></param>
        public void Remove(TelegramUser telegramUser)
        {
            _users.Remove(telegramUser);
        }

        /// <summary>
        /// Build a suitable view for current user and send or update message
        /// </summary>
        /// <param name="user"></param>
        /// <param name="view"></param>
        private void UpdateView(TelegramUser user, IView view)
        {
            var telegramView = view.Update();

            if (string.IsNullOrEmpty(user.ViewName) || user.ViewName != view.Name)
            {
                _app.SendMessage(user.Username, telegramView.Message, telegramView.Keyboard);
                user.ViewName = view.Name;
            }
            else if (user.ViewName == view.Name)
            {
                _app.UpdateLastMessage(user.Username, telegramView.Message, telegramView.Keyboard);
            }
        }

        #region Property changed 

        // If something changed in player properties than assosiatied telegram user should take a new view
        private void PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var player = sender as Player;
            if (player == null)
            {
                return;
            }

            var view = ViewBuilder.Build(player);
            var user = _users.FirstOrDefault(x => x.Player == player);
            if (user == null)
            {
                return;
            }

            UpdateView(user, view);
        }
        #endregion
    }
}
