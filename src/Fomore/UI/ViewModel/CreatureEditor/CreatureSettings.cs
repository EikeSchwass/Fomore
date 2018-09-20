using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Fomore.UI.ViewModel.Data;
using Fomore.UI.ViewModel.Helper;

namespace Fomore.UI.ViewModel.CreatureEditor
{
    [SuppressMessage("ReSharper", "ImplicitlyCapturedClosure")]
    public class CreatureSettings : ViewModelBase
    {
        public CreatureEditorPanelVM CreatureEditorPanel { get; }
        public CreatureVM Creature => CreatureEditorPanel.Creature;
        public HistoryStackVM<CreatureStructureEditorCanvasVM> HistoryStack => CreatureEditorPanel.HistoryStack;

        public float Scale
        {
            get => Creature.CreatureStructureVM.Scale;
            set
            {
                float current = Scale;
                var changeOperation = new ChangeOperation(c =>
                                                          {
                                                              c.Creature.CreatureStructureVM.Scale = value;
                                                              OnPropertyChanged();
                                                          },
                                                          c =>
                                                          {
                                                              c.Creature.CreatureStructureVM.Scale = current;
                                                              OnPropertyChanged();
                                                          });
                HistoryStack.AddOperation(changeOperation);
            }
        }

        public string BoneName
        {
            get =>
                CreatureEditorPanel.CreatureStructureEditorCanvasVM.SelectedBones.Count > 1 ?
                    null :
                    CreatureEditorPanel.CreatureStructureEditorCanvasVM.SelectedBones.SingleOrDefault()?.Name;
            set
            {
                var bone = CreatureEditorPanel.CreatureStructureEditorCanvasVM.SelectedBones.Count > 1 ?
                               null :
                               CreatureEditorPanel.CreatureStructureEditorCanvasVM.SelectedBones.SingleOrDefault();
                if (bone == null)
                    return;
                string current = bone.Name;
                var changeOperation = new ChangeOperation(c =>
                                                          {
                                                              bone.Name = value;
                                                              OnPropertyChanged();
                                                          },
                                                          c =>
                                                          {
                                                              bone.Name = current;
                                                              OnPropertyChanged();
                                                          });
                HistoryStack.AddOperation(changeOperation);
            }
        }

        public string BoneDensity
        {
            get
            {
                var selectedBones = CreatureEditorPanel.CreatureStructureEditorCanvasVM.SelectedBones;
                var distinctDensities = selectedBones.Select(b => b.Density).Distinct().ToList();
                if (distinctDensities.Count > 1)
                    return null;
                return distinctDensities.FirstOrDefault().ToString("#####0.0#####");
            }
            set
            {
                if (value.EndsWith(",") || value.EndsWith(".") && float.TryParse(value.Substring(0, value.Length - 1), out _))
                    value += "0";
                var selectedBones = CreatureEditorPanel.CreatureStructureEditorCanvasVM.SelectedBones.ToList();

                var current = selectedBones.ToDictionary(b => b, b => b.Density);
                var changeOperation = new ChangeOperation(c =>
                                                          {
                                                              float boneDensity = Convert.ToSingle(value);
                                                              foreach (var selectedBone in selectedBones)
                                                                  selectedBone.Density = boneDensity;
                                                              OnPropertyChanged();
                                                          },
                                                          c =>
                                                          {
                                                              foreach (var kvp in current) kvp.Key.Density = kvp.Value;
                                                              OnPropertyChanged();
                                                          });
                HistoryStack.AddOperation(changeOperation);
            }
        }

        private ConnectorInformationVM ConnectorInformation =>
            CreatureEditorPanel.CreatureStructureEditorCanvasVM.SelectedBones.SaveSingleOrDefault()?.ConnectorInformation;

        public bool? IsMovementLimitationEnabled
        {
            get => ConnectorInformation?.HasLimits;
            set
            {
                var currentConnectorInformation = ConnectorInformation;
                if (currentConnectorInformation == null)
                    return;
                bool current = ConnectorInformation.HasLimits;
                var changeOperation = new ChangeOperation(c =>
                                                          {
                                                              currentConnectorInformation.HasLimits = value ?? false;
                                                              OnPropertyChanged();
                                                              OnPropertyChanged(nameof(IsFirstJointBasisForMovementLimitation));
                                                              OnPropertyChanged(nameof(IsSecondJointBasisForMovementLimitation));
                                                          },
                                                          c =>
                                                          {
                                                              currentConnectorInformation.HasLimits = current;
                                                              OnPropertyChanged();
                                                              OnPropertyChanged(nameof(IsFirstJointBasisForMovementLimitation));
                                                              OnPropertyChanged(nameof(IsSecondJointBasisForMovementLimitation));
                                                          });
                HistoryStack.AddOperation(changeOperation);
            }
        }

        public float LowerMovementLimit
        {
            get => ConnectorInformation?.LowerLimit ?? 0;
            set
            {
                if (ConnectorInformation == null)
                    return;
                value = Math.Min(value, (float)(Math.PI * 2 - UpperMovementLimit));
                var currentConnectorInformation = ConnectorInformation;
                float currentLimit = ConnectorInformation.LowerLimit;
                var changeOperation = new ChangeOperation(c =>
                                                          {
                                                              currentConnectorInformation.LowerLimit = value;
                                                              OnPropertyChanged();
                                                          },
                                                          c =>
                                                          {
                                                              currentConnectorInformation.LowerLimit = currentLimit;
                                                              OnPropertyChanged();
                                                          });
                HistoryStack.AddOperation(changeOperation);
            }
        }

        public float UpperMovementLimit
        {
            get => ConnectorInformation?.UpperLimit ?? 0;
            set
            {
                if (ConnectorInformation == null)
                    return;
                value = Math.Min(value, (float)(Math.PI * 2 - LowerMovementLimit));
                var currentConnectorInformation = ConnectorInformation;
                float currentLimit = ConnectorInformation.UpperLimit;
                var changeOperation = new ChangeOperation(c =>
                                                          {
                                                              currentConnectorInformation.UpperLimit = value;
                                                              OnPropertyChanged();
                                                          },
                                                          c =>
                                                          {
                                                              currentConnectorInformation.UpperLimit = currentLimit;
                                                              OnPropertyChanged();
                                                          });
                HistoryStack.AddOperation(changeOperation);
                OnPropertyChanged();
            }
        }

        public bool? IsFirstJointBasisForMovementLimitation
        {
            get => (IsMovementLimitationEnabled ?? false) && !(ConnectorInformation?.IsFlipped ?? false);
            set
            {
                if (value == true)
                {
                    var currentConnectorInformation = ConnectorInformation;
                    if (currentConnectorInformation == null)
                        return;
                    bool currentHasLimits = ConnectorInformation.HasLimits;
                    bool currentIsFlipped = ConnectorInformation.IsFlipped;
                    var changeOperation = new ChangeOperation(c =>
                                                              {
                                                                  currentConnectorInformation.HasLimits = true;
                                                                  currentConnectorInformation.IsFlipped = false;
                                                                  OnPropertyChanged();
                                                                  OnPropertyChanged(nameof(IsFirstJointBasisForMovementLimitation));
                                                                  OnPropertyChanged(nameof(IsSecondJointBasisForMovementLimitation));
                                                              },
                                                              c =>
                                                              {
                                                                  currentConnectorInformation.HasLimits = currentHasLimits;
                                                                  currentConnectorInformation.IsFlipped = currentIsFlipped;
                                                                  OnPropertyChanged();
                                                                  OnPropertyChanged(nameof(IsFirstJointBasisForMovementLimitation));
                                                                  OnPropertyChanged(nameof(IsSecondJointBasisForMovementLimitation));
                                                              });
                    HistoryStack.AddOperation(changeOperation);
                }
            }
        }

        public bool? IsSecondJointBasisForMovementLimitation
        {
            get => (IsMovementLimitationEnabled ?? false) && (ConnectorInformation?.IsFlipped ?? false);
            set
            {
                if (value == true)
                {
                    var currentConnectorInformation = ConnectorInformation;
                    if (currentConnectorInformation == null)
                        return;
                    bool currentHasLimits = ConnectorInformation.HasLimits;
                    bool currentIsFlipped = ConnectorInformation.IsFlipped;
                    var changeOperation = new ChangeOperation(c =>
                                                              {
                                                                  currentConnectorInformation.HasLimits = true;
                                                                  currentConnectorInformation.IsFlipped = true;
                                                                  OnPropertyChanged();
                                                                  OnPropertyChanged(nameof(IsFirstJointBasisForMovementLimitation));
                                                                  OnPropertyChanged(nameof(IsSecondJointBasisForMovementLimitation));
                                                              },
                                                              c =>
                                                              {
                                                                  currentConnectorInformation.HasLimits = currentHasLimits;
                                                                  currentConnectorInformation.IsFlipped = currentIsFlipped;
                                                                  OnPropertyChanged();
                                                                  OnPropertyChanged(nameof(IsFirstJointBasisForMovementLimitation));
                                                                  OnPropertyChanged(nameof(IsSecondJointBasisForMovementLimitation));
                                                              });
                    HistoryStack.AddOperation(changeOperation);
                }
            }
        }

        public bool? IsSensor
        {
            get => ConnectorInformation?.IsSensor;
            set
            {
                if (value == ConnectorInformation?.IsSensor || ConnectorInformation == null)
                    return;
                var connectorInformationVM = ConnectorInformation;
                bool isSensor = ConnectorInformation.IsSensor;
                var changeOperation = new ChangeOperation(c =>
                                                          {
                                                              connectorInformationVM.IsSensor = value == true;
                                                              OnPropertyChanged();
                                                          },
                                                          c =>
                                                          {
                                                              connectorInformationVM.IsSensor = isSensor;
                                                              OnPropertyChanged();
                                                          });
                HistoryStack.AddOperation(changeOperation);
            }
        }

        public CreatureSettings(CreatureEditorPanelVM creatureEditorPanel)
        {
            CreatureEditorPanel = creatureEditorPanel;
            CreatureEditorPanel.CreatureStructureEditorCanvasVM.SelectedBones.CollectionChanged += CollectionChanged;
        }

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var newItems = e.NewItems?.Cast<ViewModelBase>() ?? Enumerable.Empty<ViewModelBase>();
            var oldItems = e.OldItems?.Cast<ViewModelBase>() ?? Enumerable.Empty<ViewModelBase>();

            foreach (var oldItem in oldItems) oldItem.PropertyChanged -= ItemChanged;
            foreach (var newItem in newItems) newItem.PropertyChanged += ItemChanged;
            ResetAll();
        }

        private void ItemChanged(object sender, PropertyChangedEventArgs e)
        {
            ItemChanged((ViewModelBase)sender);
        }

        private void ItemChanged(ViewModelBase item)
        {
            switch (item)
            {
                case JointVM _:
                    OnPropertyChanged(nameof(ConnectorInformation));
                    OnPropertyChanged(nameof(LowerMovementLimit));
                    OnPropertyChanged(nameof(UpperMovementLimit));
                    break;
                case BoneVM _:
                    OnPropertyChanged(nameof(BoneName));
                    OnPropertyChanged(nameof(BoneDensity));
                    break;
            }
        }

        private void ResetAll()
        {
            OnPropertyChanged("");
        }
    }
}