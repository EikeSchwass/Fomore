// Eike Stein: Fomore/UI/CommandInvocation.cs (2018/05/15)

using System.Windows;
using System.Windows.Input;

namespace Fomore.UI.ViewModel
{
    public class CommandInvocation : DependencyObject
    {
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command",
                                        typeof(ICommand),
                                        typeof(CommandInvocation),
                                        new PropertyMetadata(default(ICommand)));

        public static readonly DependencyProperty ParameterProperty =
            DependencyProperty.Register("Parameter",
                                        typeof(object),
                                        typeof(CommandInvocation),
                                        new PropertyMetadata(default(object)));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object Parameter
        {
            get => GetValue(ParameterProperty);
            set => SetValue(ParameterProperty, value);
        }
    }
}