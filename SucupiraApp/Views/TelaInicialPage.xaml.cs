using SucupiraApp.Models;

namespace SucupiraApp.Views;

public partial class TelaInicialPage : ContentPage
{
	public TelaInicialPage()
	{
		InitializeComponent();
	}

    private async void OnCriarContaClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CadastroProfessorPage());
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LoginPage());
    }
    private async void OnContinuarAnonimoClicked(object sender, EventArgs e)
    {
        // Cria uma sess�o an�nima com permiss�o de visualiza��o
        Preferences.Default.Set("UsuarioLogadoId", 0); // 0 pode indicar 'usu�rio an�nimo'
        Preferences.Default.Set("NivelPermissao", NivelPermissao.Usuario.ToString());

        await Navigation.PushAsync(new MainPage());
    }
}