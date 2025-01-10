using MealOrderingApp.Client.Pages.Users;
using MealOrderingApp.Shared.DTOs;
using MealOrderingApp.Shared.ResponseModels;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MealOrderingApp.Client.Pages.PageProcess
{
    public class UserListProcess : ComponentBase
    {
        [Inject]
        public HttpClient httpClient { get; set; }
        protected List<UserDTO> users = new List<UserDTO>();
        protected override async Task OnInitializedAsync()
        {
            await LoadList();
        }
        protected async Task LoadList()
        {
            var response = await httpClient.GetFromJsonAsync<ServiceResponse<List<UserDTO>>>("api/User/Users");
            if (response.Success)
            {
                users = response.Value;

            }
        }
    }
}
