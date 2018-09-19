namespace Core.Physics
{
    public class CreatureMovementPattern
    {
        public Creature Creature { get; }
        public MovementPattern MovementPattern { get; }

        public CreatureMovementPattern(Creature creature, MovementPattern movementPattern)
        {
            Creature = creature.Clone();
            MovementPattern = movementPattern.Clone();
        }
    }
}