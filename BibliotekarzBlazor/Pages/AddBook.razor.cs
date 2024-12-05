using BibliotekarzBlazor.Services;
using BibliotekarzBlazor.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace BibliotekarzBlazor.Pages
{
    public partial class AddBook : ComponentBase
    {
        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        [Inject]
        public IBookProxyService BookService { get; set; }

        public BookDto Book { get; set; } = new BookDto(); 
    
      private async Task OnAddBook(MouseEventArgs arg)
        {
            try
            {
                await BookService.Add(Book);
                Navigation.NavigateTo("/");
                Snackbar.Add("Dodano nową książke.", Severity.Success);
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
    } }
