using System;

namespace Core
{
    [Serializable]
    public class MovementPattern
    {
        public MovementPattern Parent { get; }

        public string Name { get; set; }

        public int Iterations => Parent?.Iterations + 1 ?? 1;
        
        public DateTime CreationDate { get; private set; }
        
        public MovementPattern Clone()
        {
            return new MovementPattern(Parent?.Clone()) {Name = Name};
            // TODO actually implement this once the class has functionality
        }

        public MovementPattern(MovementPattern parent)
        {
            Parent = parent;
            OnCreate();
        }

        private void OnCreate()
        {
            CreationDate = DateTime.Now;
        }
    }
}
