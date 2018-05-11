// Eike Stein: Fomore/Core/CreatureBehavior.cs (2018/05/11)

using System;

namespace Core.Creatures
{
    /// <summary>
    ///     This class represents the behavior for a single creature. As the number of input and output neurons are dependend
    ///     on the structure of the creature, instances of this class shouldn't be shared between different creatures.
    /// </summary>
    [Serializable]
    public class CreatureBehavior
    {
        /// <summary>
        ///     The physiological structure of the creature.
        /// </summary>
        public CreatureStructure CreatureStructure { get; }

        /// <summary>
        ///     Creates a new instance of the <see cref="CreatureBehavior" /> class, which will be based on the passed
        ///     <see cref="CreatureStructure" />.
        /// </summary>
        /// <param name="creatureStructure">The structure the behavior should be based on</param>
        public CreatureBehavior(CreatureStructure creatureStructure)
        {
            CreatureStructure = creatureStructure;
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Creates a copy of this behavior and tries to apply it to a different structure. If the new structures is too much
        ///     unlike the original strucute the operation may fail with an <see cref="InvalidOperationException" />.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the new strucute is too different from the original structure
        ///     this behavior was created from.
        /// </exception>
        /// <param name="newStructure"></param>
        /// <returns>A copy of this behavior that operates on the new structure</returns>
        public CreatureBehavior CopyToDifferentCreatureStructure(CreatureStructure newStructure) =>
            throw new NotImplementedException();
    }
}