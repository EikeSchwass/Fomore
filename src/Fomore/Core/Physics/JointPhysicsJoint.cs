namespace Core.Physics
{
    public class JointPhysicsJoint
    {
        public FarseerPhysics.Dynamics.Joints.RevoluteJoint PhysicsJoint { get; }
        public Joint Joint { get; }
        public ConnectorInformation ConnectorInformation { get; internal set; }

        public JointPhysicsJoint(FarseerPhysics.Dynamics.Joints.RevoluteJoint physicsJoint, Joint joint, ConnectorInformation connectorInformation)
        {
            PhysicsJoint = physicsJoint;
            Joint = joint;
            ConnectorInformation = connectorInformation;
        }
    }
}