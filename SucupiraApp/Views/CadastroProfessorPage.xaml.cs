using SucupiraApp.Models;
using SucupiraApp.Services;
using System.Security.Cryptography;
using System.Text;

namespace SucupiraApp.Views;

public partial class CadastroProfessorPage : ContentPage
{
    private readonly ProfessorService _service = new();

    public CadastroProfessorPage()
    {
        InitializeComponent();
        var permissoes = Enum.GetValues(typeof(NivelPermissao))
                         .Cast<NivelPermissao>()
                         .Where(p => p != NivelPermissao.Usuario)
                         .Select(p => p.ToString())
                         .ToList();

        PermissaoPicker.ItemsSource = permissoes;
    }

    private async void OnSalvarClicked(object sender, EventArgs e)
    {
        try
        {
            var nome = NomeEntry.Text;
            var email = EmailEntry.Text;
            var senha = SenhaEntry.Text;
            var senhaHash = CalcularHash(senha);
            var lattes = LattesEntry.Text;

            var permissaoSelecionada = PermissaoPicker.SelectedItem?.ToString()?.Trim() ?? "Professor";

            // Conversão segura do Picker para o enum
            if (!Enum.TryParse(permissaoSelecionada, ignoreCase: true, out NivelPermissao permissaoConvertida))
            {
                permissaoConvertida = NivelPermissao.Professor; // fallback de segurança
            }

            var professor = new Professor
            {
                Nome = nome,
                Email = email,
                SenhaHash = senhaHash,
                LattesUrl = lattes,
                NivelPermissao = permissaoConvertida,
            };

            await _service.CadastrarProfessorAsync(professor);
            await DisplayAlert("Sucesso", "Professor cadastrado com sucesso!", "OK");

            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
    }

    private static string CalcularHash(string texto)
    {
        using var sha = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(texto);
        var hash = sha.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}
