using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ControleFrota.Services;
using ControleFrota.State;
using Microsoft.Extensions.DependencyInjection;

namespace ControleFrota.Commands
{
    public class CloseCurrentWindowCommand : ICommand
    {
        private IServiceProvider serviços;
        private readonly IDialogsStore dialogStore;
        private readonly BusyStateStore _busyStateStore;

        public CloseCurrentWindowCommand(IServiceProvider serviços)
        {
            this.serviços = serviços;
            dialogStore = serviços.GetRequiredService<IDialogsStore>();
            _busyStateStore = serviços.GetRequiredService<BusyStateStore>();
            _busyStateStore.PropertyChanged += _busyStateStore_PropertyChanged;
        }

        private void _busyStateStore_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, e);
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return !_busyStateStore.IsBusy;
        }

        public void Execute(object parameter)
        {
            dialogStore.CloseDialog(DialogResult.Escape);
        }
    }
}
