// Eike Stein: Fomore/Core/CreatureInformation.cs (2018/05/11)

using System;

namespace Core.Creatures
{
    /// <summary>
    ///     This class is used to store information about a creature, such as learning progress, author, etc.
    /// </summary>
    [Serializable]
    public class CreatureInformation
    {
        /// <summary>
        ///     The Author of the creature
        /// </summary>
        public User Author { get; }

        /// <summary>
        ///     The <see cref="DateTime" /> the creature was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        ///     The <see cref="DateTime" /> the creature's structure was changed last.
        /// </summary>
        public DateTime LastStructuralChangeDate { get; set; }

        /// <summary>
        ///     The <see cref="DateTime" /> the creature was trained last.
        /// </summary>
        public DateTime LastTrainingDate { get; set; }

        /// <summary>
        ///     Creates a new <see cref="CreatureInformation" /> instance, that is used to keep track of the development of the
        ///     creature.
        /// </summary>
        /// <param name="author">The author of the creature.</param>
        public CreatureInformation(User author)
        {
            Author = author;
        }
    }
}