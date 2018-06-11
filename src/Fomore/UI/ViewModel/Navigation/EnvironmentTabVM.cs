using Fomore.UI.ViewModel.Application;

namespace Fomore.UI.ViewModel.Navigation
{
    public class EnvironmentTabVM : TabPageVM
    {
        public EntityStorageVM EntitiesStorage { get; }

        /// <inheritdoc />
        public override string Header => "Environment";

        public EnvironmentTabVM(EntityStorageVM entitiesStorage)
        {
            EntitiesStorage = entitiesStorage;
        }
    }
}