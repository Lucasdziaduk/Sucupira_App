using SucupiraApp.Services;
using System.Security.Cryptography;
using System.Text;
using SucupiraApp.Models;

namespace SucupiraApp.Views;

public partial class LoginPage : ContentPage
{
    private readonly ProfessorService _service = new();

    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var email = EmailEntry.Text?.Trim();
        var senha = SenhaEntry.Text;

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
        {
            await DisplayAlert("Erro", "Preencha todos os campos.", "OK");
            return;
        }

        var professor = await _service.BuscarPorEmailAsync(email);

        if (professor == null || professor.SenhaHash != CalcularHash(senha))
        {
            await DisplayAlert("Erro", "Credenciais inválidas.", "OK");
            return;
        }

        // Salvar sessão local (Preferences)
        Preferences.Default.Set("UsuarioLogadoId", professor.Id);
        Preferences.Default.Set("UsuarioNome", professor.Nome);
        Preferences.Default.Set("NivelPermissao", professor.NivelPermissao.ToString());

        await Navigation.PushAsync(new MainPage());
    }

    private static string CalcularHash(string texto)
    {
        using var sha = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(texto);
        var hash = sha.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}
