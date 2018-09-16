using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Core;
using Fomore.UI.ViewModel.Application;
using Fomore.UI.ViewModel.Commands;
using Fomore.UI.ViewModel.CreatureEditor;
using Fomore.UI.ViewModel.Data;
using Fomore.UI.Views.Windows;
using Color = System.Drawing.Color;
using Pen = System.Drawing.Pen;
using Point = System.Drawing.Point;

namespace Fomore.UI.ViewModel.Navigation
{
    public class CreatureTabVM : TabPageVM
    {
        /// <inheritdoc />
        public override string Header => "Creature";

        public TabNavigationVM TabNavigationVM { get; }
        public EntityStorageVM EntitiesStorage { get; }

        // ------------------------------------------------------------
        // Properties and private members
        // ------------------------------------------------------------

        private const int PreviewDimension = 500;
        private const int PreviewImageBorder = 30;

        private CreatureVM selectedCreature;
        private MovementPatternVM selectedMovementPattern;

        public CreatureVM SelectedCreature
        {
            get => selectedCreature;
            set
            {
                if (Equals(value, selectedCreature)) return;
                if (selectedCreature != null)
                {
                    selectedCreature.CreatureStructureVM.BoneCollectionVM.CollectionChanged -= CreatureStructureChanged;
                    selectedCreature.CreatureStructureVM.JointCollectionVM.CollectionChanged -= CreatureStructureChanged;
                    selectedCreature.MovementPatternCollectionVM.CollectionChanged -= MovementPatternCollectionChanged;
                }

                selectedCreature = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectedCreatureMovementPattern));
                OnPropertyChanged(nameof(CreaturePreview));
                if (selectedCreature != null)
                {
                    selectedCreature.CreatureStructureVM.BoneCollectionVM.CollectionChanged += CreatureStructureChanged;
                    selectedCreature.CreatureStructureVM.JointCollectionVM.CollectionChanged += CreatureStructureChanged;
                    selectedCreature.MovementPatternCollectionVM.CollectionChanged += MovementPatternCollectionChanged;
                }
                ClearMovementPatternsCommand.OnCanExecuteChanged();
            }
        }

        private void CreatureStructureChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(CreaturePreview));
        }

        private void MovementPatternCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ClearMovementPatternsCommand.OnCanExecuteChanged();
        }

        public MovementPatternVM SelectedMovementPattern
        {
            get => selectedMovementPattern;
            set
            {
                if (Equals(value, selectedMovementPattern)) return;
                selectedMovementPattern = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectedCreatureMovementPattern));
            }
        }

        public CreatureMovementPattern SelectedCreatureMovementPattern =>
            new CreatureMovementPattern(SelectedCreature, SelectedMovementPattern);

        public struct CreatureMovementPattern
        {
            public CreatureVM Creature { get; }
            public MovementPatternVM MovementPattern { get; }

            public CreatureMovementPattern(CreatureVM creature, MovementPatternVM movementPattern)
            {
                Creature = creature;
                MovementPattern = movementPattern;
            }
        }

        public ImageSource CreaturePreview
        {
            get
            {
                var bitmap = GenerateCreaturePreview();

                return Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(),
                                                             IntPtr.Zero,
                                                             Int32Rect.Empty,
                                                             BitmapSizeOptions.FromEmptyOptions());
            }
        }

        // ------------------------------------------------------------
        // Commands and Actions
        // ------------------------------------------------------------

        public DelegateCommand NewCreatureCommand { get; }
        public DelegateCommand TrainCommand { get; }
        public DelegateCommand SimulateCommand { get; }
        public DelegateCommand EditCreatureCommand { get; }
        public DelegateCommand CloneCommand { get; }
        public DelegateCommand ClearMovementPatternsCommand { get; }

        private void NewCreatureAction(object obj)
        {
            var creature = new Creature() {Name = "New Creature", Description = "No description available..."};
            var creatureVM = new CreatureVM(creature);
            EntitiesStorage.AddCreatureCommand.Execute(creatureVM);
            SelectedCreature = creatureVM;
        }

        private void TrainAction(object obj)
        {
            TabNavigationVM.SwitchToTrainingTabCommand.Execute(obj);
        }

        private void SimulateAction(object obj)
        {
            TabNavigationVM.SwitchToSimulationTabCommand.Execute(obj);
        }

        private void EditAction(object obj)
        {
            if (obj is CreatureVM creature)
            {
                var creatureStructureEditor = new CreatureStructureEditor {DataContext = new CreatureEditorVM(creature)};
                creatureStructureEditor.Show();
            }
        }

        private void CloneAction(object obj)
        {
            if (obj is CreatureVM creature)
            {
                var clone = creature.Clone();
                clone.Name = "Clone of " + clone.Name;
                EntitiesStorage.AddCreatureCommand.Execute(clone);
                SelectedCreature = clone;
            }
        }

        private void ClearMovementPatternsAction(object obj)
        {
            SelectedCreature?.MovementPatternCollectionVM.Clear();
        }

        private Bitmap GenerateCreaturePreview()
        {
            var bones = SelectedCreature?.CreatureStructureVM.BoneCollectionVM;
            var joints = SelectedCreature?.CreatureStructureVM.JointCollectionVM;

            var bonePen = new Pen(Color.FromArgb(119, 119, 119)) {Width = 5};
            var jointPen = new Pen(Color.FromArgb(85, 85, 85));
            var backgroundPen = new Pen(Color.FromArgb(40, 40, 40));

            if (joints == null || joints.Count == 0 || bones == null)
            {
                var emptyBitmap = new Bitmap(PreviewDimension, PreviewDimension);
                using (var g = Graphics.FromImage(emptyBitmap))
                {
                    g.FillRectangle(backgroundPen.Brush, -1, -1, PreviewDimension + 2, PreviewDimension + 2);
                }

                return emptyBitmap;
            }

            // Weird special case..
            if (joints.Count == 1)
            {
                var specialOneDotBitmap = new Bitmap(PreviewDimension, PreviewDimension);
                using (var g = Graphics.FromImage(specialOneDotBitmap))
                {
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.FillRectangle(backgroundPen.Brush, -1, -1, PreviewDimension + 2, PreviewDimension + 2);
                    g.FillEllipse(jointPen.Brush, PreviewDimension / 2 - 5, PreviewDimension / 2 - 5, 10, 10);
                }

                return specialOneDotBitmap;
            }

            var min = new Vector2(joints.Min(j => j.Position.X), joints.Min(j => j.Position.Y));
            var max = new Vector2(joints.Max(j => j.Position.X), joints.Max(j => j.Position.Y));

            var bitmap = new Bitmap(PreviewDimension, PreviewDimension);

            using (var g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = SmoothingMode.HighQuality;

                g.FillRectangle(backgroundPen.Brush, -1, -1, PreviewDimension + 2, PreviewDimension + 2);

                foreach (var bone in bones)
                {
                    g.DrawLine(bonePen,
                               NormalizePoint(bone.FirstJoint.Position, min, max),
                               NormalizePoint(bone.SecondJoint.Position, min, max));
                }

                foreach (var joint in joints)
                {
                    var n = NormalizePoint(joint.Position, min, max);
                    g.FillEllipse(jointPen.Brush, n.X - 5, n.Y - 5, 10, 10);
                }
            }

            return bitmap;
        }

        private Point NormalizePoint(Vector2 v, Vector2 min, Vector2 max)
        {
            double xDiff = max.X - min.X;
            double yDiff = max.Y - min.Y;
            double scale;
            double xOffset = 0, yOffset = 0;

            if (xDiff > yDiff)
            {
                scale = (PreviewDimension - PreviewImageBorder * 2) / xDiff;
                yOffset = (xDiff - yDiff) / 2;
            }
            else
            {
                scale = (PreviewDimension - PreviewImageBorder * 2) / yDiff;
                xOffset = (yDiff - xDiff) / 2;
            }

            v -= min;

            int x = (int)((v.X + xOffset) * scale) + PreviewImageBorder;
            int y = (int)((v.Y + yOffset) * scale) + PreviewImageBorder;

            return new Point(x, y);
        }

        // ------------------------------------------------------------
        // Entry point & other methods
        // ------------------------------------------------------------
        public CreatureTabVM(TabNavigationVM tabNavigationVM, EntityStorageVM entitiesStorage)
        {
            TabNavigationVM = tabNavigationVM;
            EntitiesStorage = entitiesStorage;
            NewCreatureCommand = new DelegateCommand(NewCreatureAction, o => true);
            TrainCommand = new DelegateCommand(TrainAction, o => true);
            SimulateCommand = new DelegateCommand(SimulateAction, o => true);
            EditCreatureCommand = new DelegateCommand(EditAction, o => true);
            CloneCommand = new DelegateCommand(CloneAction, o => true);
            ClearMovementPatternsCommand = new DelegateCommand(ClearMovementPatternsAction, o => SelectedCreature?.MovementPatternCollectionVM.Count > 0);
        }
    }
}