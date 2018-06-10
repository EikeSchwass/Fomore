using System;
using System.ComponentModel;
using System.Windows;

namespace Fomore.UI.Views.Controls
{
    /// <summary>
    /// Interaction logic for CreatureTabControl.xaml
    /// </summary>
    public partial class CreatureTabControl
    {
        public CreatureTabControl()
        {
            InitializeComponent();
        }

        private void CreateNewButton_Click(object sender, RoutedEventArgs e)
        {
            // CoolButton Clicked! Let's show our InputBox.
            CreatureNewBox.Visibility = System.Windows.Visibility.Visible;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // YesButton Clicked! Let's hide our InputBox and handle the input text.
            CreatureNewBox.Visibility = System.Windows.Visibility.Collapsed;

            // Do something with the Input
            String input = InputNameTextBox.Text;
            MyListBox.Items.Add(input); // Add Input to our ListBox.

            // Clear InputBox.
            InputNameTextBox.Text = String.Empty;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // NoButton Clicked! Let's hide our InputBox.
            CreatureNewBox.Visibility = System.Windows.Visibility.Collapsed;

            // Clear InputBox.
            InputNameTextBox.Text = String.Empty;
        }

    }
}
