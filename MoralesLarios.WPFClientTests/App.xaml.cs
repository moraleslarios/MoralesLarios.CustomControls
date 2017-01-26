using System.Windows;
using GalaSoft.MvvmLight.Threading;

namespace MoralesLarios.WPFClientTests
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            DispatcherHelper.Initialize();
        }
    }
}
