using System;

namespace Core
{
    public class Environment
    {
        private string name;
        private string description;
        private double gravity;
        private double friction;

        public Environment()
        {
            OnAccess();
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnAccess();
            }
        }

        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnAccess();
            }
        }

        public double Gravity
        {
            get => gravity;
            set
            {
                gravity = value;
                OnAccess();
            }
        }

        public double Friction
        {
            get => friction;
            set
            {
                friction = value;
                OnAccess();
            }
        }

        public DateTime LastAccess { get; private set; }

        private void OnAccess()
        {
            LastAccess = DateTime.Now;
        }
    }
}