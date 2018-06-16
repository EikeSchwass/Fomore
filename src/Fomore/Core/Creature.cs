namespace Core
{
    public class Creature : Entitiy
    {
        private string creatureDescription;
        private string creatureName;
        public bool isOpen { get; set; }

        public  Creature()
        {

        }

        public string CreatureName
        {
            get => creatureName;
            set
            {
                if (value == creatureName) return;
                creatureName = value;
            }
        }

        public string CreatureDescription
        {
            get => creatureDescription;
            set
            {
                if (value == creatureDescription) return;
                creatureDescription = value;
            }
        }
    }
}