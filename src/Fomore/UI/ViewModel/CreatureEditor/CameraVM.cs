using System;
using System.Windows;
using System.Windows.Controls;

namespace Fomore.UI.ViewModel.CreatureEditor
{
    public class CameraVM : ViewBase
    {
        public const double MinZoomFactor = 0.5;
        public const double MaxZoomFactor = 10;

        public static readonly DependencyProperty OffsetXProperty =
            DependencyProperty.Register("OffsetX", typeof(double), typeof(CameraVM), new PropertyMetadata(default(double), OffsetXChanged));

        public static readonly DependencyProperty OffsetYProperty =
            DependencyProperty.Register("OffsetY", typeof(double), typeof(CameraVM), new PropertyMetadata(default(double), OffsetYChanged));

        public static readonly DependencyProperty ZoomFactorProperty =
            DependencyProperty.Register("ZoomFactor", typeof(double), typeof(CameraVM), new PropertyMetadata(1.0, ZoomFactorChanged));

        public static readonly DependencyProperty MaxOffsetXProperty =
            DependencyProperty.Register("MaxOffsetX", typeof(double), typeof(CameraVM), new PropertyMetadata(default(double)));

        public static readonly DependencyProperty MaxOffsetYProperty =
            DependencyProperty.Register("MaxOffsetY", typeof(double), typeof(CameraVM), new PropertyMetadata(default(double)));

        public double MaxOffsetX
        {
            get => (double)GetValue(MaxOffsetXProperty);
            set => SetValue(MaxOffsetXProperty, value);
        }

        public double MaxOffsetY
        {
            get => (double)GetValue(MaxOffsetYProperty);
            set => SetValue(MaxOffsetYProperty, value);
        }

        public double OffsetX
        {
            get => (double)GetValue(OffsetXProperty);
            set
            {
                double min = -CreatureStructureEditorCanvasVM.CanvasWidth * ZoomFactor + MaxOffsetX - 10;
                double max = 10 * ZoomFactor;
                value = Math.Min(max, Math.Max(min, value));
                SetValue(OffsetXProperty, value);
            }
        }

        public double OffsetY
        {
            get => (double)GetValue(OffsetYProperty);
            set
            {
                double min = -CreatureStructureEditorCanvasVM.CanvasHeight * ZoomFactor + MaxOffsetY - 10;
                double max = 10 * ZoomFactor;
                value = Math.Min(max, Math.Max(min, value));
                SetValue(OffsetYProperty, value);
            }
        }

        public double ZoomFactor
        {
            get => (double)GetValue(ZoomFactorProperty);
            set
            {
                value = Math.Min(value, MaxZoomFactor);
                value = Math.Max(value, MinZoomFactor);
                SetValue(ZoomFactorProperty, value);
                UpdateBoundaries();
            }
        }

        private static void ZoomFactorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var camera = (CameraVM)d;
            camera.UpdateBoundaries();
        }

        private static void OffsetXChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var camera = (CameraVM)d;
            camera.UpdateBoundaries();
        }

        private static void OffsetYChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var camera = (CameraVM)d;
            camera.UpdateBoundaries();
        }

        public void UpdateBoundaries()
        {
            MaxOffsetX = MaxOffsetX;
            MaxOffsetY = MaxOffsetY;
            OffsetX = OffsetX;
            OffsetY = OffsetY;
        }
    }
}