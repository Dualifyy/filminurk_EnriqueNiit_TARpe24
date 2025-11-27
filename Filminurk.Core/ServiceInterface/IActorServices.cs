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
        Task<Actors> Create(ActorsDTO dto);
        Task<Actors> DetailsAsync(Guid id);
        Task<Actors> Delete(Guid id);
        Task<Actors> Update(ActorsDTO dto);
    }
}