using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows;
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
        private const int PreviewImageBorder = 30;

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

        public DelegateCommand DeleteMovementPatternCommand { get; }

        // ------------------------------------------------------------

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

        private void CreatureStructureChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(CreaturePreview));
        }

        private Bitmap GenerateCreaturePreview()
        {
            var bones = CreatureStructureVM.BoneCollectionVM;
            var joints = CreatureStructureVM.JointCollectionVM;

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
            DeleteMovementPatternCommand = new DelegateCommand(o => Console.WriteLine("Hallo"), o => true);
        }

        /// <inheritdoc />
        public CreatureVM Clone()
        {
            return new CreatureVM(Model.Clone());
        }
    }
}