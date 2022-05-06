using BlazorApp.Client.Services.Abstractions;
using BlazorApp.Shared.Dtos;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorApp.Client.Services.Implementations
{
    public class MovieApi : IMovieApi
    {
        private readonly string _endpoint = "/api/movies";
        private readonly HttpClient _httpClient;

        public MovieApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> Add(MovieDto model)
        {
            var response = await _httpClient.PostAsJsonAsync(_endpoint, model);

            if (!response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();

            return null;
        }

        public async Task<List<MovieDto>> Get()
        {
            return await _httpClient.GetFromJsonAsync<List<MovieDto>>(_endpoint);
        }

        public async Task<MovieDto> GetById(int id)
        {
            return await _httpClient.GetFromJsonAsync<MovieDto>($"{_endpoint}/{id}");
        }

        public async Task<string> Update(MovieDto model)
        {
            var response = await _httpClient.PutAsJsonAsync(_endpoint, model);

            if (!response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();

            return null;
        }
    }
}
