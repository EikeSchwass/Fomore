using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using Core;
using Fomore.UI.ViewModel.Commands;
using Fomore.UI.ViewModel.Helper;
using Brushes = System.Drawing.Brushes;
using Environment = Core.Environment;

namespace Fomore.UI.ViewModel.Data
{
    /// <summary>
    ///     The View Model that encapsulates the Environment Class
    /// </summary>
    public class EnvironmentVM : ViewModelBase<Environment>
    {
        private bool isRenderingImage;
        private double previewHeight;
        private double terrainGenerationStepSize = 1;
        private ImageSource terrainPreviewImage;

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

        public double Gravity
        {
            get => Model.Gravity;
            set
            {
                Model.Gravity = value;
                OnPropertyChanged();
            }
        }

        public double Friction
        {
            get => Model.Friction;
            set
            {
                Model.Friction = value;
                OnPropertyChanged();
            }
        }

        public DateTime CreationDate => Model.CreationDate;

        public ImageSource TerrainPreviewImage
        {
            get => terrainPreviewImage;
            private set
            {
                if (Equals(value, terrainPreviewImage)) return;
                terrainPreviewImage = value;
                OnPropertyChanged();
            }
        }

        public DelegateCommand AddTerrainGeneratorCommand { get; }
        public DelegateCommand DeleteTerrainGeneratorCommand { get; }

        public ObservableCollection<TerrainGeneratorVM> TerrainGenerators { get; } = new ObservableCollection<TerrainGeneratorVM>();

        public double PreviewHeight
        {
            get => previewHeight;
            private set
            {
                if (value.Equals(previewHeight)) return;
                previewHeight = value;
                OnPropertyChanged();
            }
        }

        public bool IsRenderingImage
        {
            get => isRenderingImage;
            set
            {
                if (value == isRenderingImage) return;
                isRenderingImage = value;
                OnPropertyChanged();
            }
        }

        public double TerrainGenerationStepSize
        {
            get => terrainGenerationStepSize;
            set
            {
                if (value.Equals(terrainGenerationStepSize) || value < 1) return;
                terrainGenerationStepSize = value;
                foreach (var terrainGeneratorVM in TerrainGenerators) terrainGeneratorVM.StepSize = value;
                OnPropertyChanged();
            }
        }

        public EnvironmentVM(Environment model) : base(model)
        {
            AddTerrainGeneratorCommand = new DelegateCommand(o => AddTerrainGenerator(), o => true);
            DeleteTerrainGeneratorCommand = new DelegateCommand(o => TerrainGenerators.Remove((TerrainGeneratorVM)o), o => true);
            foreach (var modelTerrainGenerator in model.TerrainGenerators)
            {
                TerrainGenerators.Add(new TerrainGeneratorVM { Model = modelTerrainGenerator });
            }
            TerrainGenerators.CollectionChanged += TerrainGeneratorsChanged;
            TerrainGeneratorsChanged(null, null);
        }

        private void TerrainGeneratorsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            foreach (var terrainGeneratorVM in TerrainGenerators)
            {
                Model.TerrainGenerators.Add(terrainGeneratorVM.Model);
                terrainGeneratorVM.PropertyChanged -= TerrainGeneratorChanged;
                terrainGeneratorVM.PropertyChanged += TerrainGeneratorChanged;
                terrainGeneratorVM.StepSize = TerrainGenerationStepSize;
            }

            TerrainGeneratorChanged(sender, new PropertyChangedEventArgs(""));
        }


        private async void TerrainGeneratorChanged(object sender, PropertyChangedEventArgs e)
        {
            Model.TerrainGenerators.Clear();
            Model.TerrainGenerators.AddRange(TerrainGenerators.Select(tg => tg.Model));
            IsRenderingImage = true;
            var previewImageTask = RenderPreviewImageAsync();
            var previewImage = (await previewImageTask).GetImageSource();
            IsRenderingImage = false;
            TerrainPreviewImage = previewImage;
        }

        private Task<Bitmap> RenderPreviewImageAsync()
        {
            return Task.Run(() =>
                            {
                                try
                                {
                                    const int width = 230;
                                    const int height = 30;
                                    var bitmap = new Bitmap(width, height);

                                    using (var g = Graphics.FromImage(bitmap))
                                    {
                                        g.FillRectangle(Brushes.CornflowerBlue, 0, 0, width, height);

                                        if (TerrainGenerators.Any())
                                        {
                                            var vertices = TerrainGenerators
                                                          .Select(t => t.Model)
                                                          .GenerateAggregated()
                                                          .TakeWhile(vector => vector.X <= width + TerrainGenerationStepSize)
                                                          .ToList();
                                            double min = vertices.Min(v => v.Y);
                                            double max = vertices.Max(v => v.Y);
                                            PreviewHeight = max - min;
                                            PreviewHeight = PreviewHeight < 1 ? 10 : PreviewHeight;
                                            vertices = vertices.Select(v => new Vector2(v.X, v.Y - min))
                                                               .Select(v =>
                                                                       {
                                                                           if (max - min > 0.001)
                                                                               return new Vector2(v.X,
                                                                                                  height -
                                                                                                  v.Y / ((max - min) / height));
                                                                           return new Vector2(v.X, height - v.Y - height / 2.0);
                                                                       })
                                                               .ToList();

                                            for (int i = 0; i < vertices.Count - 1; i++)
                                            {
                                                var current = vertices[i];
                                                var next = vertices[i + 1];
                                                var corners = new[]
                                                {
                                                        new Point((int)current.X, (int)current.Y),
                                                        new Point((int)next.X, (int)next.Y),
                                                        new Point((int)next.X, height),
                                                        new Point((int)current.X, height)
                                                };
                                                g.FillPolygon(Brushes.SaddleBrown, corners);
                                            }
                                        }

                                        g.Flush();
                                    }

                                    return bitmap;
                                }
                                catch (OperationCanceledException)
                                {
                                    return null;
                                }
                            });

        }

        private void AddTerrainGenerator()
        {
            TerrainGenerators.Add(new TerrainGeneratorVM());
        }
    }
}