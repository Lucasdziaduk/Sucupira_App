using SucupiraApp.Views;

namespace SucupiraApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new TelaInicialPage());
        }
    }
}