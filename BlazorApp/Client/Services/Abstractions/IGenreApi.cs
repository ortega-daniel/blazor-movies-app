using BlazorApp.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp.Client.Services.Abstractions
{
    public interface IGenreApi
    {
        Task<string> Add(GenreDto model);
        Task<List<GenreDto>> Get();
        Task<GenreDto> GetById(int id);
        Task<string> Update(GenreDto model);
    }
}
