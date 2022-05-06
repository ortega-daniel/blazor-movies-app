using BlazorApp.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp.Client.Services.Abstractions
{
    public interface IActorApi
    {
        Task<string> Add(ActorDto model);
        Task<List<ActorDto>> Get();
        Task<ActorDto> GetById(int id);
        Task<string> Update(ActorDto model);
    }
}
