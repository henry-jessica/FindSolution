using DataServices;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataServices; 

namespace RAD30223Week8BlazorClient.S00214115.Pages
{
    public partial class Login
    {
        [Inject]
        public IHttpClientService httpService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; } // Inject NavigationManager

        [Inject]
        public AppState appState { get; set; }



        Token token = null;

        ViewModels.LoginViewModel model = new ViewModels.LoginViewModel();
        private bool loading;

        protected override async Task OnInitializedAsync()
        {
            token = await httpService.GetTokenAsync();
            if (token != null)
            {
                appState.LoggedIn = true;
                NavigationManager.NavigateTo("/");
            }
        }
        private async void OnValidSubmit()
        {
            loading = true;
            try
            {
                
              if (await httpService.login(model.Username, model.Password))
                {
                    appState.LoggedIn = true;
                    NavigationManager.NavigateTo("/");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                loading = false;
                StateHasChanged();
            }
        }
    }
}
