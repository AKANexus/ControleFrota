using System.Collections.Generic;

namespace ControleFrota.Services
{
    public class DialogsStore : IDialogsStore
    {
        public List<IDialogGenerator> OpenDialogs { get; set; } = new();

        //Depois de 16 horas, é que eu consegui fazer isso funcionar. Não mexa nesse código
        //a não ser que eu tenha explicitamente pedido pra você mexer nele.

        public void CloseDialog(DialogResult dialogResult)
        {
            IDialogGenerator lastDialogGenerator = OpenDialogs[OpenDialogs.Count - 1];
            lastDialogGenerator.Resultado = dialogResult;
            lastDialogGenerator.Close();
        }

        public int RegisterDialog(IDialogGenerator dialog, string windowTitle = "Título Não Fornecido", bool IsModal = true)
        {
            OpenDialogs.Add(dialog);
            dialog.DialogClosed += Dialog_DialogClosed;
            if (IsModal) dialog.ShowDialog(windowTitle);
            else dialog.Show(windowTitle);
            return OpenDialogs.Count - 1;
        }

        private void Dialog_DialogClosed()
        {
            OpenDialogs.RemoveAt(OpenDialogs.Count - 1);
        }
    }
}
