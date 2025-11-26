using Filminurk.Core.Domain;
using Filminurk.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Filminurk.Core.ServiceInterface
{
    public interface IActorServices
    {
        Task<Actor> Create(ActorsDTO dto);
        Task<Actor> DetailsAsync(Guid id);
        Task<Actor> Delete(Guid id);
        Task<Actor> Update(ActorsDTO dto);
    }
}