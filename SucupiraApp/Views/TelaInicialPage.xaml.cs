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
        // Cria uma sessão anônima com permissão de visualização
        Preferences.Default.Set("UsuarioLogadoId", 0); // 0 pode indicar 'usuário anônimo'
        Preferences.Default.Set("NivelPermissao", NivelPermissao.Usuario.ToString());

        await Navigation.PushAsync(new MainPage());
    }
}