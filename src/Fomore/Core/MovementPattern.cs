using System;

namespace Core
{
    [Serializable]
    public class MovementPattern
    {
        public string Name { get; set; }


        public int Iterations { get; set; }


        public DateTime CreationDate { get; private set; }
        
        public MovementPattern Clone()
        {
            return new MovementPattern() {Name = Name, Iterations = Iterations};
            // TODO actually implement this once the class has functionality
        }

        public MovementPattern()
        {
            OnCreate();
        }

        private void OnCreate()
        {
            CreationDate = DateTime.Now;
        }
    }
}