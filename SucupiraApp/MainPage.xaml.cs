using Npgsql;
using SucupiraApp.Database;

namespace SucupiraApp;

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);

        // Chama o teste de conexão ao clicar no botão
        await TestarConexaoAsync();
    }

    private async Task TestarConexaoAsync()
    {
        try
        {
            var db = new DatabaseService();
            using var conn = db.GetConnection();
            await conn.OpenAsync();

            using var cmd = new NpgsqlCommand("SELECT COUNT(*) FROM Professor", conn);
            var resultado = await cmd.ExecuteScalarAsync();

            await Application.Current.MainPage.DisplayAlert("Sucesso", $"Total de professores: {resultado}", "OK");
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "OK");
        }
    }
}
