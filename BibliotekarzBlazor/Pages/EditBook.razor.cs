using BibliotekarzBlazor.Services;
using BibliotekarzBlazor.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace BibliotekarzBlazor.Pages;

public partial class EditBook : ComponentBase
{
    [Parameter]
    public int Id { get; set; }

    [Inject] 
    public NavigationManager Navigation { get; set; }

    [Inject] 
    public ISnackbar Snackbar { get; set; }

    [Inject]
    public IBookProxyService BookService { get; set; }

    public BookDto Book { get; set; } = new BookDto(); // Initialize to avoid null reference

    private async Task GetData()
    {
        Book = await BookService.GetById(Id);
        if (Book == null)
        {
            Snackbar.Add("Nie znaleziono książki.", Severity.Error);
            Navigation.NavigateTo("/books");
        }
    }
    protected override async Task OnInitializedAsync()
    {

        await GetData();
    }

    private async Task OnSaveClick(MouseEventArgs arg)
    {
        try
        {
            await BookService.Update(Book);
            Navigation.NavigateTo("/");
            Snackbar.Add("Zmiany zostały zapisane.", Severity.Success);
        }
        catch (Exception ex)
        {
            Navigation.NavigateTo("/");
            Snackbar.Add("Wystąpił błąd:" + ex.ToString(), Severity.Error);
        }
        
    }

    private void OnCancelClick(MouseEventArgs obj)
    {
        Navigation.NavigateTo("/");
    }
}