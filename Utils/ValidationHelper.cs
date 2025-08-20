using Avalonia.Controls;
using MsBox.Avalonia;
using System.Threading.Tasks;

namespace InventoryApp.Utils
{
    public static class ValidationHelper
    {
        // Show a simple alert
         // Show a simple alert
        public static async Task ShowError(string message, string title = "Validation Error")
        {
            var messageBox = MessageBoxManager
                .GetMessageBoxStandard(title, message);
            await messageBox.ShowAsync();
        }

        // Validate a required text field
        public static async Task<bool> ValidateRequired(string? value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                await ShowError($"{fieldName} cannot be empty.");
                return false;
            }
            return true;
        }

        // Validate an int field with optional min/max
        public static async Task<(bool IsValid, int Result)> ValidateInt(string value, string fieldName, int? min = null, int? max = null)
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

        // Validate a decimal field with optional min/max
        public static async Task<(bool IsValid, decimal Result)> ValidateDecimal(string value, string fieldName, decimal? min = null, decimal? max = null)
        {
            if (!decimal.TryParse(value, out decimal result))
            {
                await ShowError($"{fieldName} must be a decimal number.");
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
    }
}