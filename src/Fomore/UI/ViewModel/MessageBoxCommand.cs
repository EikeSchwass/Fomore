using System.Windows;
using System.Windows.Input;
using Fomore.UI.ViewModel.Commands;

namespace Fomore.UI.ViewModel
{
    public class MessageBoxCommand : DependencyObject
    {
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text",
                                        typeof(string),
                                        typeof(MessageBoxCommand),
                                        new PropertyMetadata(default(string)));

        public static readonly DependencyProperty InvokeCommandProperty =
            DependencyProperty.Register("InvokeCommand",
                                        typeof(ICommand),
                                        typeof(MessageBoxCommand),
                                        new PropertyMetadata(default(ICommand)));

        public static readonly DependencyProperty ParameterProperty =
            DependencyProperty.Register("Parameter",
                                        typeof(object),
                                        typeof(MessageBoxCommand),
                                        new PropertyMetadata(default(object)));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public object Parameter
        {
            get => GetValue(ParameterProperty);
            set => SetValue(ParameterProperty, value);
        }

        public ICommand InvokeCommand
        {
            get => (ICommand)GetValue(InvokeCommandProperty);
            set
            {
                value = new CompositeCommand(AppStateMessageBox?.CloseMessageBoxCommand, value);
                SetValue(InvokeCommandProperty, value);
            }
        }

        public MessageBoxVM AppStateMessageBox { get; }

        public MessageBoxCommand()
        {
            AppStateMessageBox = App.Instance?.AppState.MessageBox;
            InvokeCommand = new DelegateCommand(o => { }, o => true);
        }
    }
}