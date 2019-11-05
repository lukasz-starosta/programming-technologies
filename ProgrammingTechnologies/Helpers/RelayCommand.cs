using System;
using System.Windows.Input;

namespace ProgrammingTechnologies.Helpers
{
    /// <summary>
    /// Encapsulates a command.
    /// </summary>
    class RelayCommand : ICommand
    {
        private Func<bool> _canExecute;
        private Action _execute;

        /// <summary>
        /// Instanciates a new command.
        /// <para>
        /// Takes in execute lambda function to be executed as ICommands Execute implementation and 
        /// canExecute lambda function to be executed as ICommand CanExecute implementation.
        /// </para>
        /// <para>
        /// canExecute parameter can be ommited if command is to be executed without any conditions.
        /// </para>
        /// </summary>
        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public void Execute(object parameter)
        {
            _execute();
        }
    }
}
