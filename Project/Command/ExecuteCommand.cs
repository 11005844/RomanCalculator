using System;
using System.Windows.Input;
using Project.ViewModel;

namespace Project.Command
{
    public class ExecuteCommand:ICommand
    {
        private ConversionVM _VM;
        public event EventHandler CanExecuteChanged;

        public ExecuteCommand(ConversionVM _conversionVM)
        {
            _VM = _conversionVM;
        }

        public void DisposeCmd()
        {
            _VM = null;
        }

        public bool CanExecute(object param)
        {
            return true;
        }

        public void Execute(object param)
        {
            _VM.Calculate();
        }
    }
}
