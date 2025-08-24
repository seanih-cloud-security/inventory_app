using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;
using System;
using InventoryApp.Models;

namespace InventoryApp.Views
{
    public partial class ModifyProductView : UserControl
    {
        public event EventHandler? CancelClicked;

        public event EventHandler? SaveClicked;
        private Product _product;

        // Constructor
        public ModifyProductView(Product selectedProduct)
        {
            InitializeComponent();
            
            _product = selectedProduct;
            DataContext = _product;
            
            // Populate fields
            IdTextBox.Text = selectedProduct.ProductId.ToString();
            NameTextBox.Text = selectedProduct.Name;
            InventoryTextBox.Text = selectedProduct.InStock.ToString();
            PriceTextBox.Text = selectedProduct.Price.ToString();
            MaxTextBox.Text = selectedProduct.Max.ToString();
            MinTextBox.Text = selectedProduct.Min.ToString();
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