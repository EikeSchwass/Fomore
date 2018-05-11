// Eike Stein: Fomore/Core/Creature.cs (2018/05/11)

using System;
using System.Collections.Generic;

namespace Core.Creatures
{
    /// <summary>
    ///     This class represents a single creature composing of a physiological structure, behavior and additional
    ///     information.
    /// </summary>
    [Serializable]
    public class Creature
    {
        /// <summary>
        ///     <inheritdoc cref="Creatures.CreatureInformation" />
        /// </summary>
        public CreatureInformation CreatureInformation { get; internal set; }

        /// <summary>
        ///     <inheritdoc cref="Creatures.CreatureStructure" />
        /// </summary>
        public CreatureStructure CreatureStructure { get; internal set; }

        /// <summary>
        ///     <inheritdoc cref="Creatures.CreatureBehavior" />
        /// </summary>
        public CreatureBehavior CreatureBehavior { get; internal set; }

        public List<Bone> BoneLibrary { get; } = new List<Bone>();
    }
}