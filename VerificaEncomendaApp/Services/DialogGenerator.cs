using System;
using System.ComponentModel;
using System.Windows;
using ControleFrota.ViewModels.DialogWindows;
using ControleFrota.Views.DialogWindows;

namespace ControleFrota.Services
{
    public class DialogGenerator : IDialogGenerator
    {
        private readonly DialogMainViewModel _dialogMainViewModel;
        private DialogResult resultado = DialogResult.Closed;

        public DialogGenerator(DialogMainViewModel dialogMainViewModel)
        {
            _dialogMainViewModel = dialogMainViewModel;
        }

        public int IDSelecionado { get; set; }
        public DialogMainView janela { get; set; }
        public DialogContentViewModelBase ViewModelExibido { get; set; }
        public string Teste { get; set; } = "Esse nem preenchido foi...";
        public DialogResult Resultado { get => resultado; set => resultado = value; }
        public event Action DialogClosed;
        public void ShowDialog(string windowTitle = "Título Não Fornecido")
        {
            Resultado = DialogResult.Cancel;
            _dialogMainViewModel.CurrentViewModel = ViewModelExibido;
            janela = new DialogMainView(_dialogMainViewModel) { Title = windowTitle };
            janela.Closing += ModalClosing;
            janela.ShowDialog();
        }

        public void Show(string windowTitle = "Título Não Fornecido")
        {
            _dialogMainViewModel.CurrentViewModel = ViewModelExibido;
            janela = new DialogMainView(_dialogMainViewModel) { Title = windowTitle };
            janela.Closing += ModalClosing;
            janela.Show();
        }

        private void AsyncClosing(object sender, CancelEventArgs e)
        {
            MessageBox.Show("Por favor aguarde a tarefa atual");
            e.Cancel = true;
        }

        private void ModalClosing(object sender, CancelEventArgs e)
        {
            DialogClosed?.Invoke();
            DialogClosed = null;
        }

        public void Close()
        {
            janela.Close();
            DialogClosed = null;
            //DialogClosed?.Invoke();
        }
    }
}
