using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ControleFrota.Auxiliares;
using ControleFrota.Commands;
using ControleFrota.Domain;
using ControleFrota.Extensions;
using ControleFrota.Modal;
using ControleFrota.Services;
using ControleFrota.Services.DataServices;
using ControleFrota.State;
using FastReport;
using FastReport.Export.Image;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.DependencyInjection;

namespace ControleFrota.ViewModels.DialogWindows
{
    public class FiltroRelatórioManutençõesViewModel : DialogContentViewModelBase
    {
        public ICommand CloseCurrentWindow { get; set; }
        public ICommand ImprimirRelatório { get; set; }
        private readonly BusyStateStore _busyStateStore;
        public bool IsHitTestVisible { get; set; }
        public bool CarrosChecked { get; set; } = true;
        public bool MotosChecked { get; set; } = true;

        public DateTime DataInicial { get; set; } = DateTime.Now.AddMonths(-1);
        public DateTime DataFinal { get; set; } = DateTime.Now;


        public FiltroRelatórioManutençõesViewModel(IServiceProvider serviceProvider)
        {
            CloseCurrentWindow = new CloseCurrentWindowCommand(serviceProvider);
            ImprimirRelatório = new ImprimirRelatórioManutençõesCommand(this, serviceProvider, x => MessageBox.Show(x.Message));
            _busyStateStore = serviceProvider.GetRequiredService<BusyStateStore>();
            _busyStateStore.PropertyChanged += _busyStateStore_PropertyChanged;
        }

        private void _busyStateStore_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_busyStateStore.IsBusy))
            {
                if (_busyStateStore.IsBusy)
                {
                    IsHitTestVisible = false;
                    Mouse.OverrideCursor = Cursors.Wait;
                    Application.Current.Dispatcher.Invoke(() => { Mouse.OverrideCursor = Cursors.Wait; });
                }
                else
                {
                    IsHitTestVisible = true;
                    Application.Current.Dispatcher.Invoke(() => { Mouse.OverrideCursor = null; });
                }
                OnPropertyChanged(nameof(IsHitTestVisible));
            }
        }
    }

    public class ImprimirRelatórioManutençõesCommand : AsyncCommandBase
    {
        private readonly FiltroRelatórioManutençõesViewModel _filtroRelatórioManutençõesViewModel;
        private readonly ManutençãoDataService _manutençãoDataService;
        private readonly IDialogsStore _dialogStore;
        private readonly BusyStateStore _busyStateStore;

        public ImprimirRelatórioManutençõesCommand(FiltroRelatórioManutençõesViewModel filtroRelatórioManutençõesViewModel, IServiceProvider serviceProvider, Action<Exception> onException) : base(onException)
        {
            _filtroRelatórioManutençõesViewModel = filtroRelatórioManutençõesViewModel;
            var currentScope = serviceProvider.GetRequiredService<CurrentScopeStore>();
            _manutençãoDataService = currentScope.PegaEscopoAtual().GetRequiredService<ManutençãoDataService>();
            _dialogStore = serviceProvider.GetRequiredService<IDialogsStore>();
            _busyStateStore = serviceProvider.GetRequiredService<BusyStateStore>();
        }

        protected override async Task ExecuteAsync(object parameter)
        {
            _busyStateStore.IsBusy = true;
            try
            {
                List<Manutenção> manutenções = new();
                foreach (Manutenção manutenção in (await _manutençãoDataService.GetAllAsNoTracking()).Where(x =>
                    x.DataHora.IsBetween(_filtroRelatórioManutençõesViewModel.DataInicial,
                        _filtroRelatórioManutençõesViewModel.DataFinal)))
                {
                    if (manutenção.Veículo.TipoVeículo == TipoVeículo.Carro &&
                        _filtroRelatórioManutençõesViewModel.MotosChecked)
                    {
                        manutenções.Add(manutenção);
                    }

                    if (manutenção.Veículo.TipoVeículo == TipoVeículo.Moto &&
                        _filtroRelatórioManutençõesViewModel.CarrosChecked)
                    {
                        manutenções.Add(manutenção);
                    }
                }
                ReportObjectManutenções rom = new(manutenções, _filtroRelatórioManutençõesViewModel.DataInicial,
                    _filtroRelatórioManutençõesViewModel.DataFinal);

                var z = new List<ReportObjectManutenções> { rom };
                Report report = new Report();

                ReportObjectManutenções rom1 = new(new(), DateTime.Now, DateTime.Now);
                var nike = new List<ReportObjectManutenções> { rom1 };

                report.Load("ReportFrameworkM.frx");
                report.Report.Dictionary.RegisterBusinessObject(nike, "Manutenções", 3, true);
                report.Save("ReportFrameworkM.frx");


                report.Load("ReportFrameworkM.frx");
                report.RegisterData(z, "Manutenções");
                report.Prepare();
                var reportExport = new ImageExport();
                string tempFile = Path.GetTempFileName();
                report.Export(reportExport, tempFile);
                ReportViewWindow rvv = new ReportViewWindow(tempFile, report);
                _busyStateStore.IsBusy = false;
                rvv.ShowDialog();
                _dialogStore.CloseDialog(DialogResult.OK);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            finally
            {
                _busyStateStore.IsBusy = false;
            }

        }
    }
}

