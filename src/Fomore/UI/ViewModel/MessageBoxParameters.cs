using System.Collections.ObjectModel;
using System.Windows;

namespace Fomore.UI.ViewModel
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

        public static readonly DependencyProperty OptionsPanelProperty =
            DependencyProperty.Register("OptionsPanel",
                                        typeof(object),
                                        typeof(MessageBoxParameters),
                                        new PropertyMetadata(default(object)));

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon",
                                        typeof(MessageBoxImage),
                                        typeof(MessageBoxParameters),
                                        new PropertyMetadata(MessageBoxImage.None));

        public MessageBoxImage Icon
        {
            get => (MessageBoxImage)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public ObservableCollection<MessageBoxCommand> MessageBoxCommands { get; } =
            new ObservableCollection<MessageBoxCommand>();

        public object Content
        {
            get => GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }
    }
}