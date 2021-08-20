using System.Windows;

namespace ControleFrota.Views.DialogWindows
{
    /// <summary>
    /// Interaction logic for DialogueMainView.xaml
    /// </summary>
    public partial class DialogMainView : Window
    {
        public DialogMainView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
        }
    }
}
