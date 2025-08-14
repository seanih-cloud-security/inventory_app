using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;

using System;

namespace InventoryApp.Views
{
    public partial class AddProductView : UserControl
    {
        public event EventHandler? CancelClicked;
        public event EventHandler? SaveClicked;
        // Constructor
        public AddProductView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        // add event handlers here
        private void ModifySaveButton_Click(object? sender, RoutedEventArgs e)
        {
            SaveClicked?.Invoke(this, EventArgs.Empty);
            // TODO: Add saving logic here
        }

        private void ModifyCancelButton_Click(object? sender, RoutedEventArgs e)
        {
            CancelClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}