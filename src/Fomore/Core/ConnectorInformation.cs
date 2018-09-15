using System;

namespace Core
{
    [Serializable]
    public class ConnectorInformation
    {
        public Bone Bone { get; set; }
        public float LowerLimit { get; set; }
        public float UpperLimit { get; set; }
        public float Strength { get; set; }
        public bool CanControl { get; set; }
        public bool HasLimits { get; set; }
        public bool IsFlipped { get; set; }

        public ConnectorInformation Clone(Bone newBone)
        {
            var connectorInformation = new ConnectorInformation
            {
                Bone = newBone,
                LowerLimit = LowerLimit,
                UpperLimit = UpperLimit,
                Strength = Strength,
                CanControl = CanControl,
                HasLimits = HasLimits,
                IsFlipped = IsFlipped
            };
            return connectorInformation;
        }
    }
}