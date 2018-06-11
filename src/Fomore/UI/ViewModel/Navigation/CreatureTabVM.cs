using Fomore.UI.ViewModel.Application;

namespace Fomore.UI.ViewModel.Navigation
{
    public class CreatureTabVM : TabPageVM
    {
        public EntityStorageVM EntitiesStorage { get; }

        /// <inheritdoc />
        public override string Header => "New Creature";

        public string EnterName => "Enter Name*:";
        public string Description => "Description";
        public string CreateButton => "Create";
        public string CancelButton => "Cancel";

        public string CreatureName { get; set; }
        public string EnterDescription { get; set; }

        public CreatureTabVM(EntityStorageVM entitiesStorage)
        {
            EntitiesStorage = entitiesStorage;
        }
    }
}