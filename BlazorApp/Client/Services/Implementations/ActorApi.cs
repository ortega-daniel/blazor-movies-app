using BlazorApp.Client.Services.Abstractions;
using BlazorApp.Shared.Dtos;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorApp.Client.Services.Implementations
{
    public class ActorApi : IActorApi
    {
        private readonly string _endpoint = "/api/actors";
        private readonly HttpClient _httpClient;

        public ActorApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> Add(ActorDto model)
        {
            var response = await _httpClient.PostAsJsonAsync(_endpoint, model);

            if (!response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();

            return null;
        }

        public async Task<List<ActorDto>> Get()
        {
            return await _httpClient.GetFromJsonAsync<List<ActorDto>>(_endpoint);
        }

        public async Task<ActorDto> GetById(int id)
        {
            return await _httpClient.GetFromJsonAsync<ActorDto>($"{_endpoint}/{id}");
        }

        public async Task<string> Update(ActorDto model)
        {
            var response = await _httpClient.PutAsJsonAsync(_endpoint, model);

            if (!response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();

            return null;
        }
    }
}
