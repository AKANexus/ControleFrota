using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ControleFrota.Auxiliares;
using ControleFrota.Commands;
using ControleFrota.Domain;
using ControleFrota.Modal;
using ControleFrota.Services;
using ControleFrota.Services.DataServices;
using ControleFrota.State;
using ControleFrota.Views.DialogWindows;
using FastReport;
using FastReport.Export.Image;
using FastReport.Export.PdfSimple;
using FastReport.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace ControleFrota.ViewModels.DialogWindows
{
    public class FiltroRelatórioVeículoViewModel : DialogContentViewModelBase
    {
        public ICommand CloseCurrentWindow { get; set; }
        public ICommand ImprimirRelatório { get; set; }
        private readonly BusyStateStore _busyStateStore;
        public bool IsHitTestVisible { get; set; }
        public bool AbastecimentosChecked { get; set; } = true;
        public bool ManutençõesChecked { get; set; } = true;
        public bool ViagensChecked { get; set; } = true;

        public DateTime DataInicial { get; set; } = DateTime.Now.AddMonths(-1);
        public DateTime DataFinal { get; set; } = DateTime.Now;


        public FiltroRelatórioVeículoViewModel(IServiceProvider serviceProvider)
        {
            CloseCurrentWindow = new CloseCurrentWindowCommand(serviceProvider);
            ImprimirRelatório = new ImprimirRelatórioVeículoCommand(this, serviceProvider, x => MessageBox.Show(x.Message));
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

    public class ImprimirRelatórioVeículoCommand : AsyncCommandBase
    {
        private readonly FiltroRelatórioVeículoViewModel _filtroRelatórioVeículoViewModel;
        private readonly VeículoDataService _veículoDataService;
        private readonly IMessaging<int> _intMessaging;
        private readonly IDialogsStore _dialogStore;
        private readonly BusyStateStore _busyStateStore;

        public ImprimirRelatórioVeículoCommand(FiltroRelatórioVeículoViewModel filtroRelatórioVeículoViewModel, IServiceProvider serviceProvider, Action<Exception> onException) : base(onException)
        {
            _filtroRelatórioVeículoViewModel = filtroRelatórioVeículoViewModel;
            var currentScope = serviceProvider.GetRequiredService<CurrentScopeStore>();
            _veículoDataService = currentScope.PegaEscopoAtual().GetRequiredService<VeículoDataService>();
            _intMessaging = serviceProvider.GetRequiredService<IMessaging<int>>();
            _dialogStore = serviceProvider.GetRequiredService<IDialogsStore>();
            _busyStateStore = serviceProvider.GetRequiredService<BusyStateStore>();
        }

        protected override async Task ExecuteAsync(object parameter)
        {
            _busyStateStore.IsBusy = true;
            try
            {
                Veículo veículoTotal = await _veículoDataService.GetFullVeículoAsNoTracking(_intMessaging.Mensagem);
                ReportObjectVeículo rov = new(veículoTotal, _filtroRelatórioVeículoViewModel.DataInicial,
                    _filtroRelatórioVeículoViewModel.DataFinal);
                if (!_filtroRelatórioVeículoViewModel.AbastecimentosChecked) rov.Abastecimentos.Clear();
                if (!_filtroRelatórioVeículoViewModel.ManutençõesChecked) rov.Manutencoes.Clear();
                if (!_filtroRelatórioVeículoViewModel.ViagensChecked) rov.Viagens.Clear();

                var z = new List<ReportObjectVeículo> { rov };
                Report report = new Report();

                //ReportObjectManutenções rom = new(new(), DateTime.Now, DateTime.Now);
                //var nike = new List<ReportObjectManutenções> { rom };

                //report.Load("ReportFrameworkM.frx");
                //report.Report.Dictionary.RegisterBusinessObject(nike, "Manutenções", 3, true);
                //report.Save("ReportFrameworkM.frx");
                report.Load("ReportFramework.frx");
                report.RegisterData(z, "Veiculo");
                report.Prepare();
                //var reportExport = new PDFSimpleExport();
                //report.Export(reportExport, @"Report.pdf");
                var reportExport = new ImageExport();
                string tempFile = Path.GetTempFileName();
                report.Export(reportExport, tempFile);
                //MessageBox.Show("Clique \"OK\" para abrir o relatório!");
                ReportViewWindow rvv = new ReportViewWindow(tempFile, report);
                _busyStateStore.IsBusy = false;
                rvv.ShowDialog();
                //ProcessStartInfo pi = new ProcessStartInfo
                //{
                //    FileName = "explorer",
                //    Arguments = "\"Report.pdf\""
                //};
                //Process.Start(pi);
                _dialogStore.CloseDialog(DialogResult.OK);
                //Debug.WriteLine("OK!");
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
