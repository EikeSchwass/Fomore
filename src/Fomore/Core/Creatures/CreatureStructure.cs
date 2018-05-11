// Eike Stein: Fomore/Core/CreatureStructure.cs (2018/05/11)

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Core.Creatures
{
    /// <summary>
    ///     This class describes the physiological structure of the creature (bones and joints).
    /// </summary>
    [Serializable]
    public class CreatureStructure
    {
        private List<Bone> Bones { get; } = new List<Bone>();

        /// <summary>
        ///     The collections of bones of this creature.
        /// </summary>
        public IReadOnlyCollection<Bone> BoneCollection => Bones.AsReadOnly();

        /// <summary>
        ///     Adds a bone to the creature model at a certain position.
        /// </summary>
        /// <param name="boneModel">The model of the bone to add.</param>
        /// <param name="position">The position where the bone should be added.</param>
        public Bone AddBone(BoneModel boneModel, Vector2 position)
        {
            var bone = new Bone(boneModel)
            {
                Position = position
            };
            Bones.Add(bone);
            return bone;
        }

        /// <summary>
        ///     <inheritdoc cref="AddBone(Core.Creatures.BoneModel,Microsoft.Xna.Framework.Vector2)" />
        /// </summary>
        /// <param name="boneModel">The model of the bone to add.</param>
        public Bone AddBone(BoneModel boneModel) => AddBone(boneModel, Vector2.Zero);
        
    }
}