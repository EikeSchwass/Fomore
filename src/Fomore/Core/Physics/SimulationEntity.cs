using System.Collections.Generic;
using System.Linq;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Factories;
using static System.Math;

namespace Core.Physics
{
    public class SimulationEntity
    {
        private World World { get; }
        private CreatureMovementPattern CreatureMovementPattern { get; }

        public IReadOnlyCollection<BoneBody> BoneBodies { get; private set; }
        public IReadOnlyCollection<JointPhysicsJoint> JointPhysicsJoints { get; private set; }


        public SimulationEntity(World world, CreatureMovementPattern creatureMovementPattern)
        {
            World = world;
            CreatureMovementPattern = creatureMovementPattern;
            CreateBody();
        }

        private void CreateBody()
        {
            var boneBodies = new List<BoneBody>();
            var jointPhysicsJoints = new List<JointPhysicsJoint>();
            var creatureStructure = CreatureMovementPattern.Creature.CreatureStructure;
            foreach (var bone in creatureStructure.Bones)
            {
                float length = (float)(bone.FirstJoint.Position - bone.SecondJoint.Position).Length;
                var body = BodyFactory.CreateRectangle(World, length, bone.Width, bone.Density, bone.FirstJoint.Position.ToXna());
                body.Rotation = (float)bone.FirstJoint.Position.GetAngleTowards(bone.SecondJoint.Position);
                body.BodyType = BodyType.Dynamic;
                body.Enabled = true;
                body.FixtureList.First().OnCollision += FixtureCollision;
                body.FixtureList.First().OnSeparation += FixtureSeperation;
                body.UserData = this;
                var boneBody = new BoneBody(body, bone);
                boneBodies.Add(boneBody);
            }

            foreach (var joint in creatureStructure.Joints)
            {
                var first = creatureStructure.Bones.First(b => ReferenceEquals(b.FirstJoint, joint));
                var second = creatureStructure.Bones.First(b => ReferenceEquals(b.SecondJoint, joint));

                var firstBoneBody = boneBodies.First(b => b.Bone == first);
                var secondBoneBody = boneBodies.First(b => b.Bone == second);

                var firstJointAnchor =
                    new Microsoft.Xna.Framework.Vector2((float)((first.FirstJoint.Position - first.SecondJoint.Position).X / 2),
                                                        first.Width / 2);
                var secondJointAnchor =
                    new Microsoft.Xna.Framework.Vector2((float)((first.SecondJoint.Position - first.FirstJoint.Position).X / 2),
                                                        first.Width / 2);

                var revoluteJoint = JointFactory.CreateRevoluteJoint(World, secondBoneBody.Body, secondJointAnchor, firstBoneBody.Body, firstJointAnchor);
                revoluteJoint.UserData = this;
                revoluteJoint.CollideConnected = false;
                var jointPhysicsJoint = new JointPhysicsJoint(revoluteJoint, joint);
                jointPhysicsJoints.Add(jointPhysicsJoint);
            }

            foreach (var connectorInformation in creatureStructure.Bones.Select(b => b.ConnectorInformation).Where(c => c.CanControl || c.HasLimits))
            {
                var controlledFrom = connectorInformation.ControlledFrom;
                var jointPhysicsJoint = jointPhysicsJoints.First(j => ReferenceEquals(j.Joint, controlledFrom));
                jointPhysicsJoint.ConnectorInformation = connectorInformation;
                if (connectorInformation.HasLimits)
                {
                    jointPhysicsJoint.PhysicsJoint.LimitEnabled = true;
                    jointPhysicsJoint.PhysicsJoint.LowerLimit = connectorInformation.LowerLimit;
                    jointPhysicsJoint.PhysicsJoint.UpperLimit = connectorInformation.UpperLimit;
                }

                if (connectorInformation.CanControl)
                {
                    jointPhysicsJoint.PhysicsJoint.MotorEnabled = true;
                    jointPhysicsJoint.PhysicsJoint.MaxMotorTorque = connectorInformation.Strength;
                }
            }

            JointPhysicsJoints = jointPhysicsJoints.AsReadOnly();
            BoneBodies = boneBodies.AsReadOnly();
        }

        private void FixtureSeperation(Fixture fixtureA, Fixture fixtureB)
        {
            var fixtureABody = fixtureA.Body;
            BoneBodies.First(b => b.Body == fixtureABody).IsColliding = false;
        }

        private bool FixtureCollision(Fixture fixtureA, Fixture fixtureB, Contact contact)
        {
            var fixtureABody = fixtureA.Body;
            var fixtureBBody = fixtureB.Body;

            if (fixtureBBody.UserData is SimulationEntity se && se != this)
                return false;
            BoneBodies.First(b => b.Body == fixtureABody).IsColliding = true;
            return true;

        }

        public void PreWorldStep()
        {
            var inputs = GetNeuralInputs().ToArray();
            var outputs = CreatureMovementPattern.MovementPattern.NeuralNetwork.CalculateNetworkOutput(inputs);
            int currentIndex = 0;
            foreach (var jointPhysicsJoint in JointPhysicsJoints.Where(j => j.PhysicsJoint.MotorEnabled))
            {
                float currentValue = outputs[currentIndex] * 2 - 1;
                float strength = jointPhysicsJoint.ConnectorInformation.Strength;
                strength *= currentValue * currentValue;
                float targetSpeed = (float)(PI * 2 * currentValue);
                jointPhysicsJoint.PhysicsJoint.MaxMotorTorque = strength;
                jointPhysicsJoint.PhysicsJoint.MotorSpeed = targetSpeed;
            }
        }

        private IEnumerable<float> GetNeuralInputs()
        {
            foreach (var boneBody in BoneBodies)
            {
                float result = boneBody.IsColliding ? 1 : 0;
                yield return result;
            }
        }
    }
}