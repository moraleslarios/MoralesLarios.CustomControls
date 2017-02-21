using MoralesLarios.Utilities.Excel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace MoralesLarios.Utilities.ClientTests
{
    public partial class MainWindow : Window
    {

        public ObservableCollection<Customer> Datos { get; set; }


        public MainWindow()
        {
            InitializeComponent();


            Datos = new ObservableCollection<Customer>(Customer.GetData());
            dgDatos.ItemsSource = Datos;
            lstDatos.ItemsSource = new ObservableCollection<Customer>(Customer.GetData());
            lswDatos.ItemsSource = new ObservableCollection<Customer>(Customer.GetData());
        }

        private void bt_Click(object sender, RoutedEventArgs e)
        {
            ExcelActions.SetColorFlash(dgDatos, Brushes.Yellow);
        }
    }
}
