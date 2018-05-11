// Eike Stein: Fomore/Core/BoneModel.cs (2018/05/11)

using System;
using System.Collections.Generic;
using FarseerPhysics.Collision.Shapes;

namespace Core.Creatures
{
    /// <summary>
    ///     This class represents the model if a bone (density, shape and anchor points).
    /// </summary>
    [Serializable]
    public class BoneModel
    {
        /// <summary>
        ///     <inheritdoc cref="FarseerPhysics.Collision.Shapes.Shape" />
        /// </summary>
        public Shape Shape { get; set; }

        /// <summary>
        ///     The density of the bone. Don't confuse this with the total weight.
        /// </summary>
        public double Density { get; set; }

        /// <summary>
        /// Anchor points in local coordinates that may be used to connect bones together.
        /// </summary>
        public List<AnchorPoint> AnchorPoints { get; set; } = new List<AnchorPoint>();

        /// <summary>
        ///     Creates a new bone, with a given shape and density.
        /// </summary>
        /// <param name="shape">
        ///     The shape of the bone. See also:
        ///     <seealso cref="CircleShape" />, 
        ///     <seealso cref="ChainShape" />, 
        ///     <seealso cref="EdgeShape" />, 
        ///     <seealso cref="PolygonShape" />
        /// </param>
        /// <param name="density"></param>
        public BoneModel(Shape shape, double density)
        {
            Shape = shape;
            Density = density;
        }
    }
}