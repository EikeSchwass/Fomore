using System.Windows;
using System.Windows.Input;
using Fomore.UI.ViewModel.Commands;

namespace Fomore.UI.Views.Controls
{
    public class MessageBoxParameters : DependencyObject
    {
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content",
                                        typeof(object),
                                        typeof(MessageBoxParameters),
                                        new PropertyMetadata(default(object)));

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title",
                                        typeof(string),
                                        typeof(MessageBoxParameters),
                                        new PropertyMetadata(default(string)));

        public static readonly DependencyProperty IsOpenedProperty =
            DependencyProperty.Register("IsOpened",
                                        typeof(bool),
                                        typeof(MessageBoxParameters),
                                        new PropertyMetadata(default(bool)));

        public static readonly DependencyProperty OptionsPanelProperty =
            DependencyProperty.Register("OptionsPanel",
                                        typeof(object),
                                        typeof(MessageBoxParameters),
                                        new PropertyMetadata(default(object)));
        
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public object OptionsPanel
        {
            get => GetValue(OptionsPanelProperty);
            set => SetValue(OptionsPanelProperty, value);
        }

        public object Content
        {
            get => GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }

        public bool IsOpened
        {
            get => (bool)GetValue(IsOpenedProperty);
            set => SetValue(IsOpenedProperty, value);
        }

        public ICommand CloseCommand { get; }

        public MessageBoxParameters()
        {
            CloseCommand = new DelegateCommand(o => IsOpened = false, o => true);
        }
    }
}