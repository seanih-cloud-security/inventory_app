using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;
using System;

namespace InventoryApp.Views
{
    public partial class ModifyProductView : UserControl
    {
        public event EventHandler? CancelClicked;

        public event EventHandler? SaveClicked;

        // Constructor
        public ModifyProductView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        // add event handlers here
        private void ModifyProductSaveButton_Click(object? sender, RoutedEventArgs e)
        {
            SaveClicked?.Invoke(this, EventArgs.Empty);
            // TODO: Modify saving logic here
        }

        private void ModifyProductCancelButton_Click(object? sender, RoutedEventArgs e)
        {
            CancelClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}