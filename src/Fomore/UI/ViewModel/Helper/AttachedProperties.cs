using System;
using System.Collections;
using System.Linq;
using System.Windows;
using Fomore.UI.ViewModel.CreatureEditor;

namespace Fomore.UI.ViewModel.Helper
{
    public static class AttachedProperties
    {
        public static readonly DependencyProperty InputBindingsSourceProperty =
            DependencyProperty.RegisterAttached("InputBindingsSource",
                                                typeof(IEnumerable),
                                                typeof(AttachedProperties),
                                                new UIPropertyMetadata(null, InputBindingsSource_Changed));

        public static IEnumerable GetInputBindingsSource(DependencyObject obj) => (IEnumerable)obj.GetValue(InputBindingsSourceProperty);

        public static void SetInputBindingsSource(DependencyObject obj, IEnumerable value)
        {
            obj.SetValue(InputBindingsSourceProperty, value);
        }

        private static void InputBindingsSource_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (!(obj is FrameworkElement uiElement))
                throw new Exception($"Object of type '{obj.GetType()}' does not support InputBindings");

            var creatureEditorVM = (CreatureEditorVM)uiElement.DataContext;

            uiElement.InputBindings.Clear();
            if (e.NewValue == null)
                return;

            var bindings = (IEnumerable)e.NewValue;
            var inputBindings = from hasInputBindings in bindings.OfType<IHasInputBinding>()
                                let binding = hasInputBindings.GetInputBinding()
                                where binding?.Gesture != null
                                select binding;
            foreach (var binding in inputBindings)
            {
                binding.CommandParameter = creatureEditorVM.CreatureEditorPanelVM;
                uiElement.InputBindings.Add(binding);
            }
        }
    }
}