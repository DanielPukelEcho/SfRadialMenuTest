using System.Windows.Input;

namespace SfRadialMenuTest.WPF.Commands
{
	public abstract class CommandBase : ICommand
	{
		public event EventHandler? CanExecuteChanged;

		public virtual bool CanExecute(object? parameter)
		{
			return true;
		}
		public abstract void Execute(object? parameter);
		protected virtual void OnCanExecuteChanged()
		{
			CanExecuteChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}
