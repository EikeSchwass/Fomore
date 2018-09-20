using System;

namespace Core
{
    [Serializable]
    public class ConnectorInformation
    {
        public Bone Bone { get; set; }
        public float LowerLimit { get; set; }
        public float UpperLimit { get; set; }
        public float Strength { get; set; } = 50;
        public bool HasLimits { get; set; }
        public bool IsFlipped { get; set; }
        public bool IsSensor { get; set; }

        public Joint ControlledFrom => !IsFlipped ? Bone.FirstJoint : Bone.SecondJoint;

        public ConnectorInformation Clone(Bone newBone)
        {
            var connectorInformation = new ConnectorInformation
            {
                Bone = newBone,
                LowerLimit = LowerLimit,
                UpperLimit = UpperLimit,
                Strength = Strength,
                IsSensor = IsSensor,
                HasLimits = HasLimits,
                IsFlipped = IsFlipped
            };
            return connectorInformation;
        }
    }
}