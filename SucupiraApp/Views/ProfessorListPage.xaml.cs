using SucupiraApp.Models;
using SucupiraApp.Services;
using System.Security.Cryptography;
using System.Text;

namespace SucupiraApp.Views;

public partial class ProfessorListPage : ContentPage
{
    private readonly ProfessorService _service = new();
    private List<Professor> _professores = new();

    public ProfessorListPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CarregarProfessoresAsync();
    }

    private async Task CarregarProfessoresAsync()
    {
        _professores = await _service.ListarProfessoresAsync();
        ProfessoresCollection.ItemsSource = _professores;
    }

    private async void OnEditarClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var professor = (Professor)button.BindingContext;

        // Aqui você criará depois a tela de edição
        await Navigation.PushAsync(new CadastroProfessorPage(professor));
    }

    private async void OnExcluirClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var professor = (Professor)button.BindingContext;

        var confirmar = await DisplayAlert("Confirmar", $"Deseja excluir {professor.Nome}?", "Sim", "Não");
        if (!confirmar) return;

        await _service.ExcluirProfessorAsync(professor.Id);
        await DisplayAlert("Sucesso", "Professor excluído.", "OK");
        await CarregarProfessoresAsync();
    }
}
