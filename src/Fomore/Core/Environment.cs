namespace Core
{
    public class Environment : Entitiy
    {
        public string EnvironmentName;
        public string EnvironmentDescription;
        public bool isOpen { get; set; }

        public Environment()
        {

        }

        public string EnvironName
        {
            get => EnvironmentName;
            set
            {
                if (value == EnvironmentName) return;
                EnvironmentName = value;
            }
        }

        public string EnvironDescription
        {
            get => EnvironmentDescription;
            set
            {
                if (value == EnvironmentDescription) return;
                EnvironmentDescription = value;
            }
        }
    }
}