using Core.Creatures;

namespace Core.Simulations
{
    public class Scenario : ICloneable<Scenario>
    {
        public Creature Creature { get; set; } = new Creature();
        public Environment Environment { get; set; } = new Environment();
        public Scenario Clone() => new Scenario();
    }
}