using Pirates.Engine.Models;
using Pirates.Telegram.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Pirates.Telegram.Views
{
    internal enum WorldNavigationActions
    {
        Up,
        Down,
        Left,
        Right
    }
    public class WorldView : IView
    {

        public string Name { get { return "simpleworldview"; } }
        private Player _player;
        private WorldViewModel _viewModel;

        public WorldView(Player player)
        {
            _player = player;
            _viewModel = new WorldViewModel(player);
        }
        public void ApplyAction(string action)
        {
            WorldNavigationActions navigationAction;
            if (!Enum.TryParse(action, out navigationAction)) return;

            switch (navigationAction)
            {
                case WorldNavigationActions.Down:
                    {
                        _viewModel.Move(Engine.Enums.LocationDirections.Down);
                        break;
                    }
                case WorldNavigationActions.Up:
                    {
                        _viewModel.Move(Engine.Enums.LocationDirections.Up);
                        break;
                    }
                case WorldNavigationActions.Left:
                    {
                        _viewModel.Move(Engine.Enums.LocationDirections.Left);
                        break;
                    }
                case WorldNavigationActions.Right:
                    {
                        _viewModel.Move(Engine.Enums.LocationDirections.Right);
                        break;
                    }
            }
        }

        public TelegramView Update()
        {
            var blankButton = "\U000025AB";
            var message = new StringBuilder();

            var nearestArea = _viewModel.GetNeareastArea();
            if (nearestArea != null)
            {
                for (int y = 0; y < nearestArea.GetLength(1); y++)
                {
                    for (int x = 0; x < nearestArea.GetLength(0); x++)
                    {
                        var cell = nearestArea[x, y] as Pirates.Engine.Locations.WorldCell;
                        if (cell != null)
                        {
                            if (_player.Location == nearestArea[x, y])
                            {
                                message.Append("\U000026F5 ");
                            }
                            else if (cell.Players.Count() > 0)
                            {
                                message.Append("\U0001F6A2 ");

                            }
                            else
                            {
                                message.Append("\U00003030 ");
                            }
                        }
                    }

                    message.AppendLine();
                }
            }


            var keyboards = new InlineKeyboardButton[3][];
            // first row
            keyboards[0] = new InlineKeyboardButton[3];
            keyboards[0][0] = new InlineKeyboardButton(blankButton);
            if (_viewModel.CanMoveUp())
            {
                keyboards[0][1] = new InlineKeyboardButton("\U00002B06", WorldNavigationActions.Up.ToString());
            }
            else
            {
                keyboards[0][1] = new InlineKeyboardButton(blankButton);
            }
            keyboards[0][2] = new InlineKeyboardButton(blankButton);

            // second row 
            keyboards[1] = new InlineKeyboardButton[3];
            if (_viewModel.CanMoveLeft())
            {
                keyboards[1][0] = new InlineKeyboardButton("\U00002B05 ", WorldNavigationActions.Left.ToString());
            }
            else
            {
                keyboards[1][0] = new InlineKeyboardButton(blankButton);
            }

            keyboards[1][1] = new InlineKeyboardButton(blankButton);
            if (_viewModel.CanMoveRight())
            {
                keyboards[1][2] = new InlineKeyboardButton("\U000027A1 ", WorldNavigationActions.Right.ToString());
            }
            else
            {
                keyboards[1][2] = new InlineKeyboardButton(blankButton);
            }


            // third row
            keyboards[2] = new InlineKeyboardButton[3];
            keyboards[2][0] = new InlineKeyboardButton(blankButton);
            if (_viewModel.CanMoveDown())
            {
                keyboards[2][1] = new InlineKeyboardButton("\U00002B07", WorldNavigationActions.Down.ToString());
            }
            else
            {
                keyboards[2][1] = new InlineKeyboardButton(blankButton);
            }

            keyboards[2][2] = new InlineKeyboardButton(blankButton);

            return new TelegramView
            {
                Message = message.ToString(),
                Keyboard = new InlineKeyboardMarkup(keyboards)
            };
        }
    }
}
