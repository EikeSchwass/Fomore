using System;
namespace Core.Creatures
{
    public class CreatureBehavior
    {
        public Creature Creature { get; }

        private CreatureBehavior(Creature creature)
        {
            Creature = creature;
        }

        public static CreatureBehavior CreateFromCreature(Creature creature)
        {
            var creatureBehavior = new CreatureBehavior(creature);
            throw new NotImplementedException();
        }
    }
}