using System.Windows.Input;

namespace SfRadialMenuTest.WPF.Models
{
    public sealed class RadialMenuItem
    {
        public required string Name { get; init; }
        public required string ImagePath { get; init; }
        public ICommand? Command { get; init; }
        public object? CommandParameter { get; init; }
        public List<RadialMenuItem> Items { get; init; } = new();
    }
}
