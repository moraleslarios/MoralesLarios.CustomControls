using System.Linq;
using System.Windows;
using System.Collections.ObjectModel;

namespace WpfSressTests
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private ObservableCollection<EMIR_REP_VALUATIONS_HISTORICO_ARCHIVADAS> datos;

        public MainWindow()
        {
            InitializeComponent();

            CargarDatos();

            dgDatos.ItemsSource = datos;
            searchAll.ItemsSource = datos;
            searchAll.FieldsSearch = new string[] { "Branch", "MessageId", "PartyId", "MarkToMarketCurrency", "DelegPartyId" };
            searchAll.FieldsSugerenciesSearch = new string[] { "Deleg_PartyId", "MarkToMarketCurrency", "Branch", "MessageId", "PartyId" };

            var properties = typeof(EMIR_REP_VALUATIONS_HISTORICO_ARCHIVADAS).GetProperties();


            //searchAll.FieldsSearch = new string[] { "MarkToMarketCurrency", "DelegPartyId" };
            //searchAll.FieldsSugerenciesSearch = new string[] { "MarkToMarketCurrency", "DelegPartyId" };
        }


        private void CargarDatos()
        {
            using(var context = new Model1())
            {
                datos = new ObservableCollection<EMIR_REP_VALUATIONS_HISTORICO_ARCHIVADAS>(context.EMIR_REP_VALUATIONS_HISTORICO_ARCHIVADAS.Take(10000).ToList());
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

            var colecTransformada = searchAll.FilteredItemdSource.OfType<EMIR_REP_VALUATIONS_HISTORICO_ARCHIVADAS>().ToList();

            searchAll.Text = "cambiado";
        }

        private void searchAll_Filtered(object sender, RoutedEventArgs e)
        {

        }
    }
}
