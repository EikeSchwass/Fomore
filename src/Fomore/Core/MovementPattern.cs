using System;

namespace Core
{
    [Serializable]
    public class MovementPattern
    {
        private string name;
        private int iterations;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnAccess();
            }
        }

        public int Iterations
        {
            get => iterations;
            set
            {
                iterations = value;
                OnAccess();
            }
        }

        public DateTime LastAccess { get; private set; }

        private void OnAccess()
        {
            LastAccess = DateTime.Now;
        }

        public MovementPattern Clone()
        {
            return new MovementPattern();
            // TODO actually implement this once the class has functionality
        }
    }
}