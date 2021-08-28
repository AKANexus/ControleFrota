using System;
using ControleFrota.ViewModels.DialogWindows;
using ControleFrota.Views.DialogWindows;

namespace ControleFrota.Services
{

    public enum DialogResult
    {
        Yes,
        No,
        OK,
        Cancel,
        Escape,
        Closed
    }
    public enum TipoDialogue
    {
        WIP,
        CadastroDeVeículos,
        CadastroDeMotoristas,
        CadastroDeViagens,
        NovaViagem,
        RetornoDeViatura,
        CadastroDeAbastecimento,
        Filtros,
        CadastroDeManutenção
    }
    public interface IDialogGenerator
    {
        public DialogContentViewModelBase ViewModelExibido { get; set; }
        public void ShowDialog(string windowTitle = "Título Não Fornecido");
        public void Show(string windowTitle = "Título Não Fornecido");

        public string Teste { get; set; }
        public DialogResult Resultado { get; set; }
        public DialogMainView janela { get; set; }
        public event Action DialogClosed;
        public void Close();

    }
}
