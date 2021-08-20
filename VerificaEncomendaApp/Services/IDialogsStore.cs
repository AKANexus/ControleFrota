using System.Collections.Generic;

namespace ControleFrota.Services
{
    public interface IDialogsStore
    {
        public List<IDialogGenerator> OpenDialogs { get; set; }

        public int RegisterDialog(IDialogGenerator dialog, string windowTitle = "Título Não Fornecido", bool IsModal = true);
        public void CloseDialog(DialogResult dialogResult);
    }
}
