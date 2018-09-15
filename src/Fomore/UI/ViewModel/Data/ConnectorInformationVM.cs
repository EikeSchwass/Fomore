using Core;

namespace Fomore.UI.ViewModel.Data
{
    public class ConnectorInformationVM : ViewModelBase<ConnectorInformation>
    {
        public BoneVM Bone { get; }

        public bool HasLimits
        {
            get => Model.HasLimits;
            set
            {
                if (value == Model.HasLimits) return;
                Model.HasLimits = value;
                OnPropertyChanged();
            }
        }

        public bool CanControl
        {
            get => Model.CanControl;
            set
            {
                if (value == Model.CanControl) return;
                Model.CanControl = value;
                OnPropertyChanged();
            }
        }

        public JointVM PivotJoint => Model.IsFlipped ? Bone.SecondJoint : Bone.FirstJoint;
        public JointVM NonPivotJoint => Model.IsFlipped ? Bone.FirstJoint : Bone.SecondJoint;

        public Vector2 LowerLimitEndPoint => NonPivotJoint.Position.RotateAround(PivotJoint.Position, -LowerLimit);
        public Vector2 UpperLimitEndPoint => NonPivotJoint.Position.RotateAround(PivotJoint.Position, UpperLimit);

        public bool IsFlipped
        {
            get => Model.IsFlipped;
            set
            {
                if (Model.IsFlipped == value)
                    return;
                Model.IsFlipped = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PivotJoint));
                OnPropertyChanged(nameof(LowerLimitEndPoint));
                OnPropertyChanged(nameof(UpperLimitEndPoint));
            }
        }

        public float LowerLimit
        {
            get => Model.LowerLimit;
            set
            {
                if (Equals(Model.LowerLimit, value))
                    return;
                Model.LowerLimit = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LowerLimitEndPoint));
            }
        }

        public float UpperLimit
        {
            get => Model.UpperLimit;
            set
            {
                if (Equals(Model.UpperLimit, value))
                    return;
                Model.UpperLimit = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(UpperLimitEndPoint));
            }
        }

        /// <inheritdoc />
        public ConnectorInformationVM(ConnectorInformation model, BoneVM bone) : base(model)
        {
            Bone = bone;
            Bone.FirstJoint.PropertyChanged += (o, e) => NotifyAll();
            Bone.SecondJoint.PropertyChanged += (o, e) => NotifyAll();
        }

        private void NotifyAll()
        {
            OnPropertyChanged("");
        }
    }
}