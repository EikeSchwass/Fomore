namespace Fomore.UI.ViewModel.Navigation
{
    public class EnvironmentTabVM : TabPageVM
    {
        /// <inheritdoc />
        public override string Header => "Environment";

        public string EnterName => "Enter Name*:";
        public string Description => "Description";
        public string Terrain => "Terrain";
        public string GroundType => "Ground Type:";
        public string Gravity => "Gravity:";
        public string Friction => "Friction:";

        public string CreateButton => "Create";
        public string CancelButton => "Cancel";

        public string EnvironmentName { get; set; }
        public string EnterDescription { get; set; }
    }
}