using System.Threading.Tasks;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;

namespace Core.Physics
{
    public class Simulation
    {
        private World World { get; }

        public Simulation(Microsoft.Xna.Framework.Vector2 gravity)
        {
            World = new World(gravity);
        }

        public Simulation() : this(new Microsoft.Xna.Framework.Vector2(0, 9.81f)) { }

        public async Task<double> GetBoneWeightAsync(Bone bone, float sclae)
        {
            return await new Task<double>(() => GetBoneWeight(bone, sclae));
        }

        public double GetBoneWeight(Bone bone, float sclae)
        {
            var tempWorld = new World(World.Gravity);
            float length = (float)(bone.FirstJoint.Position - bone.SecondJoint.Position).Length;
            var rectangle = BodyFactory.CreateRectangle(tempWorld, length * sclae, bone.Width, bone.Density);
            tempWorld.ProcessChanges();
            tempWorld.Step(1);
            return rectangle.Mass;
        }
    }
}