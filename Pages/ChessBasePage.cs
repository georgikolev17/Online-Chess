using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChessClient_Blazor.Pages
{
    public class ChessBasePage : ComponentBase
    {
        protected HttpClient Http = new HttpClient();

        [Inject]
        protected NavigationManager NavManager { get; set; }
        [Inject]

        protected ISessionStorageService SessionStorage { get; set; }
        protected string PlayerId { get; set; }
        protected string GameCode { get; set; }
        protected bool PlayerIsWhite { get; set; }
        protected int Time { get; set; }
        protected int Increment { get; set; }

        protected async override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                await LoadGameState();
            }
        }

        protected async Task LoadGameState()
        {
            this.PlayerId = await SessionStorage.GetItemAsync<string>("PlayerId");
            this.GameCode = await SessionStorage.GetItemAsync<string>("GameCode");
            this.PlayerIsWhite = await SessionStorage.GetItemAsync<bool>("PlayerIsWhite");
            this.Time = await SessionStorage.GetItemAsync<int>("Time");
            this.Increment = await SessionStorage.GetItemAsync<int>("Increment");
        }

        protected async Task SaveGameState()
        {
            await SessionStorage.SetItemAsync("PlayerId", this.PlayerId);
            await SessionStorage.SetItemAsync("GameCode", this.GameCode);
            await SessionStorage.SetItemAsync("PlayerIsWhite", this.PlayerIsWhite);
            await SessionStorage.SetItemAsync("Time", this.Time);
            await SessionStorage.SetItemAsync("Increment", this.Increment);
        }
    }
}
