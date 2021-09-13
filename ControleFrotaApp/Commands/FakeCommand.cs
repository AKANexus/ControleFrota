using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ControleFrota.Commands
{
    public class FakeCommand : ICommand
    {
        public bool CanExecute(object? parameter)
        {
            return false;
        }

        public void Execute(object? parameter)
        {
        }

        public event EventHandler? CanExecuteChanged;
    }
}
