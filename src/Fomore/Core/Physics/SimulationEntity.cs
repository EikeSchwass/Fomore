using System.Collections.Generic;
using System.Linq;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Dynamics.Joints;
using FarseerPhysics.Factories;
using static System.Math;

namespace Core.Physics
{
    public class SimulationEntity
    {
        private World World { get; }
        private CreatureMovementPattern CreatureMovementPattern { get; }

        private Dictionary<Bone, bool> BoneCollisions { get; set; }
        private Dictionary<Body, Bone> BodyBones { get; set; }
        public IReadOnlyCollection<JointPhysicsJoint> JointPhysicsJoints { get; private set; }

        public IReadOnlyCollection<Body> Bones { get; private set; }
        public IReadOnlyCollection<Body> Joints { get; private set; }


        public SimulationEntity(World world, CreatureMovementPattern creatureMovementPattern)
        {
            World = world;
            CreatureMovementPattern = creatureMovementPattern;
            CreateBody();
        }

        private void CreateBody()
        {
            var jointPhysicsJoints = new List<JointPhysicsJoint>();
            var creatureStructure = CreatureMovementPattern.Creature.CreatureStructure;

            // Bones Added
            var boneBodies = CreateBoneBodies(creatureStructure);

            var jointBodies = CreateJointBodies(creatureStructure);

            foreach (var joint in jointBodies)
            {
                var jointBody = joint.Value;

                foreach (var bone in creatureStructure.Bones)
                {
                    var boneBody = boneBodies.Single(b => b.Bone == bone);
                    RevoluteJoint revoluteJoint;
                    float length = (float)(bone.FirstJoint.Position - bone.SecondJoint.Position).Length;
                    if (ReferenceEquals(bone.FirstJoint.Tracker, joint.Key.Tracker))
                    {
                        var anchor = new Microsoft.Xna.Framework.Vector2(-length / 2 - 10 * creatureStructure.Scale, 0);
                        revoluteJoint =
                            JointFactory.CreateRevoluteJoint(World, jointBody, Microsoft.Xna.Framework.Vector2.Zero, boneBody.Body, anchor);
                    }
                    else if (ReferenceEquals(bone.SecondJoint.Tracker, joint.Key.Tracker))
                    {

                        var anchor = new Microsoft.Xna.Framework.Vector2(length / 2 + 10 * creatureStructure.Scale, 0);
                        revoluteJoint =
                            JointFactory.CreateRevoluteJoint(World, jointBody, Microsoft.Xna.Framework.Vector2.Zero, boneBody.Body, anchor);
                    }
                    else
                    {
                        continue;
                    }
                    revoluteJoint.CollideConnected = false;
                    var connectorInformation = bone.ConnectorInformation;
                    if (ReferenceEquals(connectorInformation.ControlledFrom.Tracker, joint.Key.Tracker))
                    {
                        if (connectorInformation.HasLimits || true)
                        {
                            revoluteJoint.LimitEnabled = true;
                            revoluteJoint.LowerLimit = -connectorInformation.LowerLimit / 100;
                            revoluteJoint.UpperLimit = connectorInformation.UpperLimit / 100;
                        }

                        if (connectorInformation.CanControl || true)
                        {
                            revoluteJoint.MotorEnabled = true;
                            revoluteJoint.MaxMotorTorque = connectorInformation.Strength;
                            jointPhysicsJoints.Add(new JointPhysicsJoint(revoluteJoint, joint.Key, connectorInformation));
                        }
                    }
                }
            }

            JointPhysicsJoints = jointPhysicsJoints.AsReadOnly();
            BoneCollisions = boneBodies.ToDictionary(b => b.Bone, b => false);
            BodyBones = boneBodies.ToDictionary(b => b.Body, b => b.Bone);

            Joints = jointBodies.Select(kvp => kvp.Value).ToList().AsReadOnly();
            Bones = boneBodies.Select(b => b.Body).ToList().AsReadOnly();
        }

        private Dictionary<Joint, Body> CreateJointBodies(CreatureStructure creatureStructure)
        {
            var jointBodies = new Dictionary<Joint, Body>();
            foreach (var joint in creatureStructure.Joints)
            {
                if (creatureStructure.Bones.Count(b => b.FirstJoint.Tracker == joint.Tracker || b.SecondJoint.Tracker == joint.Tracker) >
                    1 ||
                    creatureStructure.Bones.Any(b => b.ConnectorInformation.ControlledFrom.Tracker == joint.Tracker))
                {
                    if (jointBodies.Keys.All(j => j.Tracker != joint.Tracker))
                    {
                        var jointBody = BodyFactory.CreateRectangle(World, 0.01f, 0.01f, 10f, joint.Position.ToXna(), joint);
                        jointBody.Enabled = true;
                        jointBody.BodyType = BodyType.Dynamic;
                        jointBody.OnCollision += FixtureCollision;
                        jointBodies.Add(joint, jointBody);
                    }
                }
            }

            return jointBodies;
        }

        private List<BoneBody> CreateBoneBodies(CreatureStructure creatureStructure)
        {
            var boneBodies = new List<BoneBody>();
            foreach (var connectorInformation in creatureStructure.Bones.Select(b => b.ConnectorInformation))
            {
                float orientation =
                    (float)connectorInformation.Bone.FirstJoint.Position.GetAngleTowards(connectorInformation.Bone.SecondJoint.Position);
                float width = (float)(connectorInformation.Bone.FirstJoint.Position - connectorInformation.Bone.SecondJoint.Position).Length;
                float height = connectorInformation.Bone.Width;
                float density = connectorInformation.Bone.Density;

                var body = BodyFactory.CreateRectangle(World, width, height, density / 100, connectorInformation.Bone.Position.ToXna(), this);
                body.Rotation = orientation;
                body.Enabled = true;
                body.BodyType = BodyType.Dynamic;
                body.OnCollision += FixtureCollision;
                body.OnSeparation += FixtureSeperation;
                boneBodies.Add(new BoneBody(body, connectorInformation.Bone));
            }

            return boneBodies;
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
            foreach (var jointPhysicsJoint in JointPhysicsJoints.Where(j => j.PhysicsJoint.MotorEnabled))
            {
                float currentValue = outputs[currentIndex] * 2 - 1;
                float targetSpeed = (float)(PI * 128 * currentValue);
                jointPhysicsJoint.PhysicsJoint.MaxMotorTorque = 1000;
                jointPhysicsJoint.PhysicsJoint.MotorSpeed = 5000;
                jointPhysicsJoint.PhysicsJoint.MotorEnabled = true;
                currentIndex++;
            }
        }

        private IEnumerable<float> GetNeuralInputs(float totalTime)
        {
            foreach (var boneBody in Bones)
            {
                float result = BoneCollisions[BodyBones[boneBody]] ? 1 : 0;
                yield return result;
            }
            yield return (float)Sin(totalTime * 2);
        }
    }
}