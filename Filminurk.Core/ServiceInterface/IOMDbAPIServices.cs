using Filminurk.Core.Dto.OMDbAPIDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filminurk.Core.ServiceInterface
{
    public interface IOMDbAPIServices
    {
        Task<OMDbAPIMovieSearchRootDTO> OMDbSearchResult(string Title);
    }
}
