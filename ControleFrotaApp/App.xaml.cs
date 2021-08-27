using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using ControleFrota.EFCore;
using ControleFrota.Logging;
using ControleFrota.Services;
using ControleFrota.ViewModels.Factories;
using ControleFrota.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ControleFrota.HostBuilders;
using Microsoft.Extensions.Configuration;

namespace ControleFrota
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;
        private readonly Logger log = new("Startup");
        public const int HWND_BROADCAST = 0xffff;
        public static readonly int WM_SHOWME = RegisterWindowMessage("WM_SHOWME");
        [DllImport("user32")]
        public static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);
        [DllImport("user32")]
        public static extern int RegisterWindowMessage(string message);
        public App()
        {
            _host = CreateHostBuilder().Build();
        }

        private static IHostBuilder CreateHostBuilder(string[] args = null)
        {
            return Host.CreateDefaultBuilder(args)
                .AddConfiguration()
                .AddDbContext()
                .AddServices()
                .AddStores()
                .AddViewModels()
                .AddViews()
                ;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Logger.Start(new FileInfo($"./Logs/LOG-{DateTime.Today:dd-MM-yy}.log"));

            _host.Start();

            string mutexName = "AmbiERP_" + System.Security.Principal.WindowsIdentity.GetCurrent().User?.AccountDomainSid;

            if (Mutex.TryOpenExisting(mutexName, out _))
            {
                PostMessage(
                (IntPtr)HWND_BROADCAST,
                WM_SHOWME,
                IntPtr.Zero,
                IntPtr.Zero);
                Shutdown();
                return;
            }


            ActualStartup();


        }


        private async Task ActualStartup()
        {
            log.Info("Preparando serviços");
            IDialogGenerator dialogGenerator = _host.Services.GetRequiredService<IDialogGenerator>();
            IDialogsStore dialogStore = _host.Services.GetRequiredService<IDialogsStore>();
            IDialogViewModelFactory dialogVMFactory = _host.Services.GetRequiredService<IDialogViewModelFactory>();
            IMessaging<(string, string)> messaging = _host.Services.GetRequiredService<IMessaging<(string, string)>>();
            messaging.Mensagem = ("Aguarde...", "Atualizando tabelas...");

            try
            {
                dialogGenerator.ViewModelExibido = dialogVMFactory.CreateDialogContentViewModel(TipoDialogue.WIP);
                dialogStore.RegisterDialog(dialogGenerator, "DISGRAÇA", false);
            }
            catch (Exception e1)
            {
                Debug.WriteLine($">>>{e1.Message}");
                await _host.StopAsync();
                _host.Dispose();
                Environment.Exit(0);
            }

            MainDbContext context;
            try
            {
                MainDbContextFactory contextFactory = _host.Services.GetRequiredService<MainDbContextFactory>();
                context = contextFactory.CreateDbContext();
            }
            catch (Exception ex)
            {
                log.Error("Falha ao registrar Contexts");
                log.Error($"{ex.Message}");
                dialogStore.CloseDialog(DialogResult.OK);
                MessageBox.Show($"Falha ao registrar Contexts: \n {ex.Message}");
                await _host.StopAsync();
                _host.Dispose();
                Environment.Exit(0);
                log.Info("Fechando sistema. Tchau.");
                throw;
            }
            log.Info("Contextos instanciados...");
            Task task =
            Task.Run(() =>
            {
                log.Info("Executando MIGRATE");

                context.Database.Migrate();

                log.Info("Migrate executado");
                context.Dispose();
            }
                );
            try
            {
                log.Info("Iniciando MIGRATE");
                await Task.Run(() => task);
                log.Info("MIGRATE executado com sucesso!");
            }
            catch (Exception ex)
            {
                log.Error("Falha ao executar MIGRATE");
                log.Error($"{ex.Message}");
                dialogStore.CloseDialog(DialogResult.OK);
                MessageBox.Show($"Falha ao atualizar as tabelas: \n {ex.Message}");
                await _host.StopAsync();
                _host.Dispose();
                Environment.Exit(0);
                log.Info("Fechando sistema. Tchau.");
                throw;
            }

            try
            {
                log.Info("Abrindo <MainView>");
                Window janela = _host.Services.GetRequiredService<MainView>();
                janela.Show();
            }
            catch (Exception ex)
            {
                log.Error("Falha ao registrar <MainView>");
                log.Error($"{ex.Message}");
                dialogStore.CloseDialog(DialogResult.OK);
                MessageBox.Show($"Falha ao registrar <MainView>: \n {ex.Message}");
                await _host.StopAsync();
                _host.Dispose();
                Environment.Exit(0);
                log.Info("Fechando sistema. Tchau.");
                throw;
            }
            finally
            {
                log.Info("Fechando tela de load");
                dialogStore.CloseDialog(DialogResult.OK);
            }
        }


        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();
            Logger.ShutDown();
            base.OnExit(e);
        }
    }
}
