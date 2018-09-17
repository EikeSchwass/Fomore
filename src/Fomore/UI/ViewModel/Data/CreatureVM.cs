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
using Fomore.UI.ViewModel.Commands;
using Fomore.UI.ViewModel.Helper;
using Color = System.Drawing.Color;
using Pen = System.Drawing.Pen;
using Point = System.Drawing.Point;

namespace Fomore.UI.ViewModel.Data
{
    /// <summary>
    /// The Viewmodel that encapsulates the Creature class
    /// </summary>
    public class CreatureVM : ViewModelBase<Creature>, ICloneable<CreatureVM>
    {
        private const int PreviewDimension = 500;
        private const int PreviewSmallDimension = 48;
        private const double PreviewThickness = 0.01;
        private const double PreviewSmallThickness = 0.05;
        private const double PreviewImageBorder = 0.1;
        private Color PreviewJointColor = Color.FromArgb(85, 85, 85);
        private Color PreviewSmallJointColor = Color.FromArgb(255, 255, 255);
        private Color PreviewBoneColor = Color.FromArgb(119, 119, 119);
        private Color PreviewSmallBoneColor = Color.FromArgb(180, 180, 180);


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

        public string Description
        {
            get => Model.Description;
            set
            {
                if (value == Model.Description) return;
                Model.Description = value;
                OnPropertyChanged();
            }
        }

        public DateTime CreationDate => Model.CreationDate;

        public DelegateCommand<MovementPatternVM> DeleteMovementPatternCommand { get; }

        private void DeleteMovementPatternAction(MovementPatternVM obj)
        {
            MovementPatternCollectionVM.RemoveItemCommand.Execute(obj);
        }

        // ------------------------------------------------------------

        public ImageSource CreaturePreview
        {
            get
            {
                var bitmap = GenerateCreaturePreview(PreviewDimension, PreviewThickness, PreviewJointColor, PreviewBoneColor);

                return Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(),
                                                             IntPtr.Zero,
                                                             Int32Rect.Empty,
                                                             BitmapSizeOptions.FromEmptyOptions());
            }
        }

        public ImageSource CreatureSmallPreview
        {
            get
            {
                var bitmap = GenerateCreaturePreview(PreviewSmallDimension, PreviewSmallThickness, PreviewSmallJointColor, PreviewSmallBoneColor);

                return Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(),
                                                             IntPtr.Zero,
                                                             Int32Rect.Empty,
                                                             BitmapSizeOptions.FromEmptyOptions());
            }
        }

        private void CreatureStructureChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(CreaturePreview));
        }

        private Bitmap GenerateCreaturePreview(int size, double thickness, Color jointColor, Color boneColor)
        {
            var bones = CreatureStructureVM.BoneCollectionVM;
            var joints = CreatureStructureVM.JointCollectionVM;

            float brushThickness = (float)(size * thickness);

            var bonePen = new Pen(boneColor) {Width = brushThickness };
            var jointPen = new Pen(jointColor);

            if (joints == null || joints.Count == 0 || bones == null)
            {
                return new Bitmap(size, size);
            }

            // Weird special case..
            if (joints.Count == 1)
            {
                var specialOneDotBitmap = new Bitmap(size, size);
                using (var g = Graphics.FromImage(specialOneDotBitmap))
                {
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.FillEllipse(jointPen.Brush, size / 2 - brushThickness, size / 2 - brushThickness, brushThickness * 2, brushThickness * 2);
                }

                return specialOneDotBitmap;
            }

            var min = new Vector2(joints.Min(j => j.Position.X), joints.Min(j => j.Position.Y));
            var max = new Vector2(joints.Max(j => j.Position.X), joints.Max(j => j.Position.Y));

            var bitmap = new Bitmap(size, size);

            using (var g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = SmoothingMode.HighQuality;

                foreach (var bone in bones)
                {
                    g.DrawLine(bonePen,
                               NormalizePoint(bone.FirstJoint.Position, min, max, size),
                               NormalizePoint(bone.SecondJoint.Position, min, max, size));
                }

                foreach (var joint in joints)
                {
                    var n = NormalizePoint(joint.Position, min, max, size);
                    g.FillEllipse(jointPen.Brush, n.X - brushThickness, n.Y - brushThickness, brushThickness * 2, brushThickness * 2);
                }
            }

            return bitmap;
        }

        private Point NormalizePoint(Vector2 v, Vector2 min, Vector2 max, int size)
        {
            double xDiff = max.X - min.X;
            double yDiff = max.Y - min.Y;
            double scale;
            double xOffset = 0, yOffset = 0;

            if (xDiff > yDiff)
            {
                scale = (size - size * PreviewImageBorder * 2) / xDiff;
                yOffset = (xDiff - yDiff) / 2;
            }
            else
            {
                scale = (size - size * PreviewImageBorder * 2) / yDiff;
                xOffset = (yDiff - xDiff) / 2;
            }

            v -= min;

            int x = (int)((v.X + xOffset) * scale + size * PreviewImageBorder);
            int y = (int)((v.Y + yOffset) * scale + size * PreviewImageBorder);

            return new Point(x, y);
        }

        // ------------------------------------------------------------

        public EncapsulatingObservableCollection<MovementPatternVM, MovementPattern> MovementPatternCollectionVM { get; }

        public CreatureStructureVM CreatureStructureVM { get; }

        /// <inheritdoc />
        public CreatureVM(Creature creature) : base(creature)
        {
            MovementPatternCollectionVM =
                new EncapsulatingObservableCollection<MovementPatternVM, MovementPattern>(creature.MovementPatterns,
                                                                                          creature.MovementPatterns
                                                                                                  .Select(m => new MovementPatternVM(m))
                                                                                                  .ToList());
            CreatureStructureVM = new CreatureStructureVM(Model.CreatureStructure);
            CreatureStructureVM.BoneCollectionVM.CollectionChanged += CreatureStructureChanged;
            CreatureStructureVM.JointCollectionVM.CollectionChanged += CreatureStructureChanged;
            DeleteMovementPatternCommand = new DelegateCommand<MovementPatternVM>(DeleteMovementPatternAction, o => true);
        }

        /// <inheritdoc />
        public CreatureVM Clone()
        {
            return new CreatureVM(Model.Clone());
        }
    }
}