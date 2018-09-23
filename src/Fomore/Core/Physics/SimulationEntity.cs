using System.Collections.Generic;
using System.Linq;
using Core.Training;
using Core.Training.Neuro;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Dynamics.Joints;
using FarseerPhysics.Factories;
using static System.Math;

namespace Core.Physics
{
    public class SimulationEntity
    {
        private const float MovementLimit = (float)(15 * MathExtensions.DegreesToRadiansFactor);
        private float MotorTorque { get; }
        private World World { get; }
        public CreatureMovementPattern CreatureMovementPattern { get; }

        private Dictionary<Joint, List<RevoluteJoint>> JointRevoluteJoints { get; } = new Dictionary<Joint, List<RevoluteJoint>>();
        private Dictionary<Body, Bone> BodyBones { get; } = new Dictionary<Body, Bone>();
        private Dictionary<Bone, Body> BoneBodies { get; } = new Dictionary<Bone, Body>();
        public List<Body> Bodies { get; } = new List<Body>();
        private Dictionary<Bone, bool> BoneCollisions { get; } = new Dictionary<Bone, bool>();

        public int RevoluteJointsCount => JointRevoluteJoints.Keys.Count;

        public SimulationEntity(World world, CreatureMovementPattern creatureMovementPattern, bool useRandomness)
        {
            World = world;
            CreatureMovementPattern = creatureMovementPattern;
            CreateBody();
            MotorTorque = 0.1f * 2.2f;
            if (useRandomness)
                Bodies.OrderBy(b => b.Position.X).First().ApplyLinearImpulse(Microsoft.Xna.Framework.Vector2.UnitY * ((float)AdvancedRandom.Random.NextDouble() - 0.5f) * 0.01f);
        }

        private void CreateBody()
        {
            var creatureStructure = CreatureMovementPattern.Creature.CreatureStructure;
            CreateBoneBodies(creatureStructure);

            foreach (var joint in creatureStructure.Joints)
            {
                var connectedBones = creatureStructure
                                    .Bones.Where(b => ReferenceEquals(b.FirstJoint.Tracker, joint.Tracker) || ReferenceEquals(b.SecondJoint.Tracker, joint.Tracker)).OrderByDescending(b => GetAngleOf(b, joint))
                                    .ToList();
                var bonePairs = new List<BoneTuple>();

                foreach (var connectedBone1 in connectedBones)
                {
                    foreach (var connectedBone2 in connectedBones)
                    {
                        if (ReferenceEquals(connectedBone1, connectedBone2))
                            continue;
                        var boneTuple = new BoneTuple(connectedBone1, connectedBone2);
                        if (bonePairs.Contains(boneTuple))
                            continue;
                        bonePairs.Add(boneTuple);
                    }
                }

                foreach (var bonePair in bonePairs) CreateJoint(bonePair, joint);
            }
        }

        private void CreateJoint(BoneTuple bonePair, Joint joint)
        {
            float anchorXBone1 = (float)((bonePair.Bone1.FirstJoint.Position - bonePair.Bone1.SecondJoint.Position).Length / 2 + 7 * CreatureMovementPattern.Creature.CreatureStructure.Scale);
            if (ReferenceEquals(bonePair.Bone1.FirstJoint.Tracker, joint.Tracker))
                anchorXBone1 *= -1;
            float anchorXBone2 = (float)((bonePair.Bone2.FirstJoint.Position - bonePair.Bone2.SecondJoint.Position).Length / 2 + 7 * CreatureMovementPattern.Creature.CreatureStructure.Scale);
            if (ReferenceEquals(bonePair.Bone2.FirstJoint.Tracker, joint.Tracker))
                anchorXBone2 *= -1;

            double angleBetweenBones = GetAngleBetweenBones(bonePair.Bone1, bonePair.Bone2, joint);
            RevoluteJoint revoluteJoint;
            if (angleBetweenBones > 0)
            {
                revoluteJoint = JointFactory.CreateRevoluteJoint(World,
                                                                 BoneBodies[bonePair.Bone1],
                                                                 new Microsoft.Xna.Framework.Vector2(anchorXBone1, 0),
                                                                 BoneBodies[bonePair.Bone2],
                                                                 new Microsoft.Xna.Framework.Vector2(anchorXBone2, 0));
            }
            else
            {
                revoluteJoint = JointFactory.CreateRevoluteJoint(World,
                                                                 BoneBodies[bonePair.Bone2],
                                                                 new Microsoft.Xna.Framework.Vector2(anchorXBone2, 0),
                                                                 BoneBodies[bonePair.Bone1],
                                                                 new Microsoft.Xna.Framework.Vector2(anchorXBone1, 0));
            }

            revoluteJoint.CollideConnected = false;
            revoluteJoint.MotorEnabled = true;
            revoluteJoint.LimitEnabled = true;
            revoluteJoint.MaxMotorTorque = MotorTorque;
            revoluteJoint.LowerLimit = -MovementLimit;
            revoluteJoint.UpperLimit = MovementLimit;
            if (!JointRevoluteJoints.ContainsKey(joint))
                JointRevoluteJoints.Add(joint, new List<RevoluteJoint>());
            JointRevoluteJoints[joint].Add(revoluteJoint);
        }

        private double GetAngleBetweenBones(Bone bone1, Bone bone2, Joint joint)
        {
            var angleBone1 = GetAngleOf(bone1, joint);
            var angleBone2 = GetAngleOf(bone2, joint);
            return angleBone2 - angleBone1;
        }

        private double GetAngleOf(Bone bone, Joint joint)
        {
            var from = joint.Position;
            var to = ReferenceEquals(bone.FirstJoint, joint) ? bone.SecondJoint.Position : bone.FirstJoint.Position;

            return from.GetAngleTowards(to);
        }

        private void CreateBoneBodies(CreatureStructure creatureStructure)
        {
            var bodies = new List<Body>();
            foreach (var bone in creatureStructure.Bones)
            {
                float orientation = (float)bone.FirstJoint.Position.GetAngleTowards(bone.SecondJoint.Position);
                float width = (float)(bone.FirstJoint.Position - bone.SecondJoint.Position).Length;
                float height = bone.Width * 2;
                float density = bone.Density;

                var body = BodyFactory.CreateRectangle(World, width, height, density, bone.Position.ToXna() * 1.05f, this);
                foreach (var fixture in body.FixtureList)
                {
                    fixture.Friction = 1f;
                }
                body.Rotation = orientation;
                body.Enabled = true;
                body.BodyType = BodyType.Dynamic;
                if (bone.ConnectorInformation.IsSensor)
                {
                    body.OnCollision += FixtureCollision;
                    body.OnSeparation += FixtureSeperation;
                    BoneCollisions.Add(bone, false);
                }

                BoneBodies.Add(bone, body);
                BodyBones.Add(body, bone);
                bodies.Add(body);
            }

            Bodies.AddRange(bodies.OrderBy(b => b.Position.X).ThenBy(b => b.Position.Y));
        }

        private void FixtureSeperation(Fixture fixtureA, Fixture fixtureB)
        {
            var fixtureABody = fixtureA.Body;
            if (BodyBones.TryGetValue(fixtureABody, out var bodyBone))
                BoneCollisions[bodyBone] = false;
        }

        private bool FixtureCollision(Fixture fixtureA, Fixture fixtureB, Contact contact)
        {
            var fixtureABody = fixtureA.Body;
            var fixtureBBody = fixtureB.Body;

            if (fixtureBBody.UserData is SimulationEntity se)
            {
                if (se != this)
                    return false;
                return false;
            }
            else
            {
                if (BodyBones.TryGetValue(fixtureABody, out var bodyBone))
                    BoneCollisions[bodyBone] = true;
            }

            return true;
        }

        public void PreWorldStep(float dt, float totalTime)
        {
            var inputs = GetNeuralInputs(totalTime).ToArray();
            var outputs = CreatureMovementPattern.MovementPattern.NeuralNetwork.CalculateNetworkOutput(inputs);
            int currentIndex = 0;
            foreach (var joint in CreatureMovementPattern.Creature.CreatureStructure.Joints)
            {
                if (!JointRevoluteJoints.TryGetValue(joint, out var revoluteJoints))
                    continue;
                double currentValue = outputs[currentIndex] * 2 - 1;
                double targetSpeed = (PI * 2 * currentValue);

                //float strength = currentValue * currentValue * MotorTorque;

                foreach (var revoluteJoint in revoluteJoints)
                {
                    revoluteJoint.MotorSpeed = (float)targetSpeed;
                    revoluteJoint.MaxMotorTorque = MotorTorque;
                }
                currentIndex++;
            }
        }

        public IEnumerable<double> GetNeuralInputs(float totalTime)
        {
            foreach (var kvp in BoneCollisions.OrderByDescending(kvp => kvp.Key.Position.X))
            {
                float result = kvp.Value ? 1 : -1;
                yield return result;
            }

            float averageXVelocity = Bodies.Average(b => b.LinearVelocity.X);
            float averageYVelocity = Bodies.Average(b => b.LinearVelocity.Y);

            yield return NeuralNetwork.Sigmoid(averageXVelocity / 2) * 2 - 1;
            yield return NeuralNetwork.Sigmoid(averageYVelocity / 2) * 2 - 1;

            float rotation = Bodies.Average(b => b.Rotation);

            yield return (float)Cos(rotation);

            yield return (float)Sin(rotation);
        }
    }
}