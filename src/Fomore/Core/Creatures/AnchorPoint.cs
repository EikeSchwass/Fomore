// Eike Stein: Fomore/Core/AnchorPoint.cs (2018/05/11)

using System;
using Microsoft.Xna.Framework;

namespace Core.Creatures
{
    /// <summary>
    /// This class represents a point at which two or more bones may be connected together.
    /// </summary>
    [Serializable]
    public class AnchorPoint
    {
        /// <summary>
        /// The Position in local coordinates in reference to the center of the <see cref="BoneModel"/>.
        /// </summary>
        public Vector2 LocalPosition { get; set; }

        /// <summary>
        /// Gets the world coordinate based on the current position of the <see cref="Bone"/>.
        /// </summary>
        /// <param name="bone">The bone this anchor point belongs to.</param>
        /// <returns>The position of this anchor point in world coordinates.</returns>
        public Vector2 GetWorldCoordinates(Bone bone) => bone.Position + LocalPosition;
    }
}