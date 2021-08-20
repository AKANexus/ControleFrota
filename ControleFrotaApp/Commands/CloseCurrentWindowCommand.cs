using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ControleFrota.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ControleFrota.Commands
{
    public class CloseCurrentWindowCommand : ICommand
    {
        private IServiceProvider serviços;
        private readonly IDialogsStore dialogStore;

        public CloseCurrentWindowCommand(IServiceProvider serviços)
        {
            this.serviços = serviços;
            dialogStore = serviços.GetRequiredService<IDialogsStore>();
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            dialogStore.CloseDialog(DialogResult.Escape);
        }
    }
}
