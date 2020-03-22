﻿using CalculatorLib.BLL;
using CalculatorLib.BLL.Contracts;
using System;
using System.ComponentModel;

namespace CalculatorLib.ViewModels
{
    public class CommandExecutor : INotifyPropertyChanged, ICommandExecutor
    {
        private ICommandAcceptor _commandAcceptor;

        public CommandExecutor(ICommandAcceptorFactory factory)
        {
            _commandAcceptor = factory.CreateLiteral(0);
        }

        public string Expression => _commandAcceptor.ExpressionString;

        public string Value => _commandAcceptor.Value.ToString();

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (!(parameter is Command command)) return true;
            return _commandAcceptor?.CanAccept(command) ?? false;
        }

        public void Execute(object parameter)
        {
            if (!(parameter is Command command)) return;
            _commandAcceptor = _commandAcceptor?.Accept(command);
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            OnPropertyChanged(nameof(Expression));
            OnPropertyChanged(nameof(Value));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // Create the OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(string name)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}