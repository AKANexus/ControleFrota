using System.ComponentModel;

namespace ControleFrota.State
{
    public class BusyStateStore : INotifyPropertyChanged
    {
        private bool _isBusy = false;

        public bool IsBusy

        {
            get => _isBusy;
            set { _isBusy = value; OnPropertyChanged(nameof(IsBusy)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
