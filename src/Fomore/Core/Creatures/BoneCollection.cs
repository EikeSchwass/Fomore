using System.Collections;
using System.Collections.Generic;

namespace Core.Creatures
{
    /// <summary>
    /// This class represents a collection of bones for a single creature.
    /// </summary>
    public class BoneCollection : ICollection<BoneModel>
    {
        private List<BoneModel> BoneModels { get; } = new List<BoneModel>();

        /// <summary>
        /// The name of the Collection for later reference.
        /// </summary>
        public string Name { get; set; }

        /// <inheritdoc />
        public IEnumerator<BoneModel> GetEnumerator() => BoneModels.GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <inheritdoc />
        public void Add(BoneModel boneModel) => BoneModels.Add(boneModel);

        /// <inheritdoc />
        public void Clear() => BoneModels.Clear();

        /// <inheritdoc />
        public bool Contains(BoneModel boneModel) => BoneModels.Contains(boneModel);

        /// <inheritdoc />
        public void CopyTo(BoneModel[] array, int arrayIndex)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public bool Remove(BoneModel boneModel) => BoneModels.Remove(boneModel);

        /// <inheritdoc />
        public int Count => BoneModels.Count;

        /// <inheritdoc />
        public bool IsReadOnly => false;
    }
}