using Pirates.Engine.Enums;
using Pirates.Engine.Infrastructures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;

namespace Pirates.Engine.Models
{
    public class Player : INotifyPropertyChanged
    {
        public string Name { get; private set; }

        public Player(string name, ILocation location)
        {
            Name = name;
            Location = location;
            Location.Add(this);
        }

        #region Movement and location
        private ILocation _location;
        public ILocation Location
        {
            get
            {
                return _location;
            }
            set
            {
                SetField(ref _location, value, "Location");
            }
        }
        public bool CanMoveTo(LocationDirections direction)
        {
            return Location.LinkedLocations.ContainsKey(direction);
        }
        public void MoveTo(LocationDirections direction)
        {
            if (!CanMoveTo(direction))
            {
                return;
            }

            Location.Remove(this);
            Location.LinkedLocations[direction].Add(this);
            Location = Location.LinkedLocations[direction];
            
        }
        #endregion

        #region Notify property changed 

        private DateTime _changes = DateTime.Now;
        public DateTime LastUpdated
        {
            get
            {
                return _changes;
            }
            set
            {
                SetField(ref _changes, value, "Changes");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool SetField<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        #endregion

    }
}
