using SucupiraApp.Views;
using SucupiraApp.Database;
using Npgsql;
using SucupiraApp.Services;

namespace SucupiraApp;

public partial class MainPage : ContentPage
{
    private readonly ProfessorService _professorService = new();

    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnCadastrarProfessorClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CadastroProfessorPage());
    }

    private async void OnListarProfessorClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProfessorListPage());
    }

    private async void OnTestarConexaoClicked(object sender, EventArgs e)
    {
        try
        {
            var db = new DatabaseService();
            using var conn = db.GetConnection();
            await conn.OpenAsync();

            using var cmd = new NpgsqlCommand("SELECT COUNT(*) FROM Professor", conn);
            var resultado = await cmd.ExecuteScalarAsync();

            await DisplayAlert("Sucesso", $"Total de professores: {resultado}", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
    }
}
