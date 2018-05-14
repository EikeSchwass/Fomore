namespace Core.Simulations
{
    public struct EvaluationResult
    {
        public float Rating { get; }

        public EvaluationResult(float rating)
        {
            Rating = rating;
        }
    }
}