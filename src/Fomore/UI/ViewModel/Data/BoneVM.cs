﻿using Core;

namespace Fomore.UI.ViewModel.Data
{
    public class BoneVM : ViewModelBase<Bone>
    {
        private JointVM firstJoint;
        private JointVM secondJoint;
        private ConnectorInformationVM connectorInformation;

        public float Density
        {
            get => Model.Density;
            set
            {
                if (value.Equals(Model.Density)) return;
                Model.Density = value;
                OnPropertyChanged();
            }
        }

        public JointVM FirstJoint
        {
            get => firstJoint = firstJoint ?? new JointVM(Model.FirstJoint);
            set
            {
                if (Equals(value, FirstJoint)) return;
                firstJoint = value;
                Model.FirstJoint = value.Model;
                OnPropertyChanged();
            }
        }

        public JointVM SecondJoint
        {
            get => secondJoint = secondJoint ?? new JointVM(Model.SecondJoint);
            set
            {
                if (Equals(value, SecondJoint)) return;
                secondJoint = value;
                Model.SecondJoint = value.Model;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => Model.Name;
            set
            {
                if (value == Model.Name) return;
                Model.Name = value;
                OnPropertyChanged();
            }
        }

        public ConnectorInformationVM ConnectorInformation => connectorInformation = (connectorInformation ?? new ConnectorInformationVM(Model.ConnectorInformation, this));

        public BoneVM(Bone model) : base(model)
        {
        }
    }
}