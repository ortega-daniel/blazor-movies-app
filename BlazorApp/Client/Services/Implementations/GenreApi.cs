using BlazorApp.Client.Services.Abstractions;
using BlazorApp.Shared.Dtos;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorApp.Client.Services.Implementations
{
    public class GenreApi : IGenreApi
    {
        private readonly string _endpoint = "/api/genres";
        private readonly HttpClient _httpClient;

        public GenreApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> Add(GenreDto model) 
        {
            var response = await _httpClient.PostAsJsonAsync(_endpoint, model);

            if (!response.IsSuccessStatusCode) 
                return await response.Content.ReadAsStringAsync();

            return null;
        }

        public async Task<List<GenreDto>> Get() 
        { 
            return await _httpClient.GetFromJsonAsync<List<GenreDto>>(_endpoint);
        }

        public async Task<GenreDto> GetById(int id) 
        {
            return await _httpClient.GetFromJsonAsync<GenreDto>($"{_endpoint}/{id}");
        }

        public async Task<string> Update(GenreDto model) 
        {
            var response = await _httpClient.PutAsJsonAsync(_endpoint, model);
            
            if (!response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();

            return null;
        }
    }
}
