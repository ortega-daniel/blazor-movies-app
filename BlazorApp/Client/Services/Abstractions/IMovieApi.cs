using BlazorApp.Shared.Dtos;
using Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp.Client.Services.Abstractions
{
    public interface IMovieApi
    {
        Task<string> Add(MovieDto model);
        Task<List<MovieDto>> Get();
        Task<MovieDto> GetById(int id);
        Task<string> Update(MovieDto model);
    }
}
