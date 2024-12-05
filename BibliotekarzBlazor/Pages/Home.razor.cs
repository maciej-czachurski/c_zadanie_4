using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Web;
using BibliotekarzBlazor.Model;
using BibliotekarzBlazor.Services;
using BibliotekarzBlazor.Shared.DTOs;
using Microsoft.AspNetCore.Components.Forms;
using static System.Reflection.Metadata.BlobBuilder;

namespace BibliotekarzBlazor.Pages;

public partial class Home
{
    public string? filterText { get; set; } = "";

    public List<BookDto> BookList { get; set; }
    private List<BookDto> filteredBooks = new List<BookDto>();
    [Inject]
    public NavigationManager Navigation { get; set; }

    [Inject]
    public IBookProxyService BookService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await GetData();
        filteredBooks = BookList;
    }
    private void FilterData(string text)
    {
        filteredBooks = BookList
            .Where(b => string.IsNullOrEmpty(text) ||
                        b.Title.Contains(text, StringComparison.OrdinalIgnoreCase) ||
                        b.Author.Contains(text, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
    private async Task GetData()
    {
        BookList = (await BookService.GetAll())?.ToList() ?? [];
    }

    private void OnEditClick(int bookId)
    {
        Navigation.NavigateTo($"edit-book/{bookId}");
    }

    private async Task OnDeleteClick(int bookId)
    {
        
    }
}