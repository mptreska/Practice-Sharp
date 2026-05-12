using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MedClinic
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> execute;
        private readonly Func<object, bool> canExecute;

        public RelayCommand(Action<object> execute,
                            Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) =>
            canExecute == null || canExecute(parameter);

        public void Execute(object parameter) => execute(parameter);
    }

    // Асинхронная версия команды
    public class AsyncRelayCommand : ICommand
    {
        private readonly Func<object, Task> execute;
        private readonly Func<object, bool> canExecute;
        private bool isExecuting;

        public AsyncRelayCommand(Func<object, Task> execute,
                                 Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) =>
            !isExecuting && (canExecute == null || canExecute(parameter));

        public async void Execute(object parameter)
        {
            isExecuting = true;
            CommandManager.InvalidateRequerySuggested();
            try
            {
                await execute(parameter);
            }
            finally
            {
                isExecuting = false;
                CommandManager.InvalidateRequerySuggested();
            }
        }
    }
}