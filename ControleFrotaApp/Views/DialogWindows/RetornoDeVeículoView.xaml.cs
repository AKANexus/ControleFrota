using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ControleFrota.Domain;
using ControleFrota.ViewModels.DialogWindows;
using MessageBox = System.Windows.MessageBox;

namespace ControleFrota.Views.DialogWindows
{
    /// <summary>
    /// Interaction logic for RetornoDeVeículo.xaml
    /// </summary>
    public partial class RetornoDeVeículoView : UserControl
    {
        public RetornoDeVeículoView()
        {
            InitializeComponent();
        }

        private void EventSetter_OnHandler(object sender, SelectionChangedEventArgs e)
        {
            //if (e.AddedItems[0] is TipoGasto tipoGasto && tipoGasto.ID == 4 && DataContext is RetornoDeVeículoViewModel retornoDeVeículoViewModel)
            //{
            //    retornoDeVeículoViewModel.AcrescentaAbastecimento.Execute(null);
            //}
        }
    }
}
