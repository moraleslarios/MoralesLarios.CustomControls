using GalaSoft.MvvmLight;
using MoralesLarios.WPFClientTests.Model;
using System.Collections.ObjectModel;

namespace MoralesLarios.WPFClientTests.ViewModel
{

    public class SearchAllTestsViewModel : ViewModelBase
    {

        public SearchAllTestsViewModel()
        {
            Customers = new ObservableCollection<Customer>(Customer.GetData());

            Fields = new ObservableCollection<string>() { "ID", "Name" };
        }


        private ObservableCollection<Customer> _customers;

        public ObservableCollection<Customer> Customers
        {
            get
            {
                return _customers;
            }
            set
            {
                Set(nameof(Customers), ref _customers, value);
            }
        }



        private ObservableCollection<string> _fields;

        public ObservableCollection<string> Fields
        {
            get
            {
                return _fields;
            }
            set
            {
                Set(nameof(Fields), ref _fields, value);
            }
        }




    }
}