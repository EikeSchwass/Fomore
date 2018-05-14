namespace Core.Creatures
{
    public class Bone
    {
        public double Length => (Joint1.Position - Joint2.Position).Length;
        public double Densitiy { get; set; }
        public Joint Joint1 { get; set; }
        public Joint Joint2 { get; set; }
    }
}