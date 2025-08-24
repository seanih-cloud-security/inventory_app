using System.Linq;
using MsBox.Avalonia;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Layout;

namespace InventoryApp.Utils
{
    public static class ValidationHelper
    {
        public static async Task ShowError(string message, string title = "Validation Error")
        {
            var messageBox = MessageBoxManager
                .GetMessageBoxStandard(title, message);
            await messageBox.ShowAsync();
        }

        // Validate a text field
        public static async Task<bool> ValidateRequired(string? value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                await ShowError($"{fieldName} cannot be empty.");
                return false;
            }

            return true;
        }

        // Validate an int field with min/max
        public static async Task<(bool IsValid, int Result)> ValidateInt(string value, string fieldName,
            int? min = null, int? max = null)
        {
            if (!int.TryParse(value, out int result))
            {
                await ShowError($"{fieldName} must be an integer.");
                return (false, 0);
            }

            if (min.HasValue && result < min.Value)
            {
                await ShowError($"{fieldName} must be ≥ {min.Value}.");
                return (false, 0);
            }

            if (max.HasValue && result > max.Value)
            {
                await ShowError($"{fieldName} must be ≤ {max.Value}.");
                return (false, 0);
            }

            return (true, result);
        }

        // Validate a decimal field
        public static async Task<(bool IsValid, decimal Result)> ValidateDecimal(string value, string fieldName,
            decimal? min = null, decimal? max = null)
        {
            if (!decimal.TryParse(value, out decimal result))
            {
                await ShowError($"{fieldName} must be a decimal number.");
                return (false, 0);
            }

            return (true, result);
        }

        // CONFIRM DELETE PART
        public static async Task<bool> ShowConfirmation(string message, string title = "Confirm Delete")
        {
            var dialog = new Window
            {
                Title = title,
                Width = 400,
                Height = 150,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Content = new StackPanel
                {
                    Margin = new Thickness(20),
                    Spacing = 10,
                    Children =
                    {
                        new TextBlock { Text = message, TextWrapping = Avalonia.Media.TextWrapping.Wrap },
                        new StackPanel
                        {
                            Orientation = Orientation.Horizontal,
                            HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                            Spacing = 20,
                            Children =
                            {
                                new Button { Content = "Yes", Width = 80, IsDefault = true, Name = "YesBtn" },
                                new Button { Content = "No", Width = 80, IsCancel = true, Name = "NoBtn" }
                            }
                        }
                    }
                }
            };

            var tcs = new TaskCompletionSource<bool>();

            // Hook up buttons
            (dialog.Content as StackPanel)!.Children.OfType<StackPanel>()
                .First().Children.OfType<Button>().First(b => b.Name == "YesBtn").Click += (_, _) =>
                {
                    tcs.SetResult(true);
                    dialog.Close();
                };
            (dialog.Content as StackPanel)!.Children.OfType<StackPanel>()
                .First().Children.OfType<Button>().First(b => b.Name == "NoBtn").Click += (_, _) =>
                {
                    tcs.SetResult(false);
                    dialog.Close();
                };

            await dialog.ShowDialog(
                (Application.Current!.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)!.MainWindow!);
            return await tcs.Task;
        }
    }
}