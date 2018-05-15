using System;

namespace Core.Creatures
{
    public class CreatureInformation
    {
        public string Name { get; set; } = $"Creature #{Guid.NewGuid().ToString().Substring(0, 8)}";
        public DateTime LastUpdateDateTime { get; set; }
        public DateTime CreateDateTime { get; set; } = DateTime.Now;
    }
}