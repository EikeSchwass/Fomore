using System;

namespace Core
{
    [Serializable]
    public class MovementPattern
    {
        public MovementPattern Clone()
        {
            return new MovementPattern();
            // TODO actually implement this once the class has functionality
        }
    }
}