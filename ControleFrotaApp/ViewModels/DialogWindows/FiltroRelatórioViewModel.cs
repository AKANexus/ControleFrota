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
//using DinkToPdf;
using Microsoft.Extensions.DependencyInjection;

namespace ControleFrota.ViewModels.DialogWindows
{
    public class FiltroRelatórioViewModel : DialogContentViewModelBase
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


        public FiltroRelatórioViewModel(IServiceProvider serviceProvider)
        {
            CloseCurrentWindow = new CloseCurrentWindowCommand(serviceProvider);
            ImprimirRelatório = new ImprimirRelatórioCommand(this, serviceProvider, x => MessageBox.Show(x.Message));
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

    public class ImprimirRelatórioCommand : AsyncCommandBase
    {
        private readonly FiltroRelatórioViewModel _filtroRelatórioViewModel;
        private readonly VeículoDataService _veículoDataService;
        private readonly IMessaging<int> _intMessaging;
        private readonly IDialogsStore _dialogStore;
        private readonly BusyStateStore _busyStateStore;

        public ImprimirRelatórioCommand(FiltroRelatórioViewModel filtroRelatórioViewModel, IServiceProvider serviceProvider, Action<Exception> onException) : base(onException)
        {
            _filtroRelatórioViewModel = filtroRelatórioViewModel;
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
                ReportObject ro = new(veículoTotal, _filtroRelatórioViewModel.DataInicial,
                    _filtroRelatórioViewModel.DataFinal);
                if (!_filtroRelatórioViewModel.AbastecimentosChecked) ro.Abastecimentos.Clear();
                if (!_filtroRelatórioViewModel.ManutençõesChecked) ro.Manutencoes.Clear();
                if (!_filtroRelatórioViewModel.ViagensChecked) ro.Viagens.Clear();

                var z = new List<ReportObject> { ro };
                Report report = new Report();


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
