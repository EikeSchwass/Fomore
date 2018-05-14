namespace Core.Simulations
{
    public class Environment
    {
        public string Name { get; set; }
        public Vector2 Gravity { get; set; }
        public Terrain Terrain { get; set; }
    }
}