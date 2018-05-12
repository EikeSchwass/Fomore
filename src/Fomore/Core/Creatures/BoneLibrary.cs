// Eike Stein: Fomore/Core/BoneLibrary.cs (2018/05/12)

using System.Collections;
using System.Collections.Generic;

namespace Core.Creatures
{
    /// <summary>
    /// This class represents a collection of all <see cref="BoneCollection"/>.
    /// </summary>
    public class BoneLibrary : IEnumerable<BoneCollection>
    {
        /// <summary>
        /// List of all <see cref="BoneCollection"/>.
        /// </summary>
        public List<BoneCollection> BoneCollections { get; } = new List<BoneCollection>();

        public void Add(BoneCollection boneCollection)
        {
            BoneCollections.Add(boneCollection);
        }

        public IEnumerator<BoneCollection> GetEnumerator() => BoneCollections.GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}