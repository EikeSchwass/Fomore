// Eike Stein: Fomore/Core/Bone.cs (2018/05/11)

using System;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Core.Creatures
{
    /// <summary>
    /// This class represents a bone that is defined by a <see cref="BoneModel"/>.
    /// </summary>
    [Serializable]
    public class Bone
    {
        /// <summary>
        /// The model this bone defined by.
        /// </summary>
        public BoneModel Model { get; }

        /// <summary>
        /// The position of the center in world coordinates.
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// Creates a new Bone based on a <see cref="BoneModel"/>.
        /// </summary>
        /// <param name="boneModel">The <see cref="BoneModel"/> this instance is based on.</param>
        public Bone(BoneModel boneModel)
        {
            Model = new BoneModel(boneModel.Shape, boneModel.Density)
            {
                AnchorPoints = boneModel.AnchorPoints.Select(ap => new AnchorPoint
                                         {
                                             LocalPosition = ap.LocalPosition
                                         })
                                        .ToList()
            };
        }
    }
}