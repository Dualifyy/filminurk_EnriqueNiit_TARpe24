using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filminurk.Core.Domain;
using Filminurk.Core.Dto;

namespace Filminurk.Core.ServiceInterface
{
    public interface IFilesServices
    {
        void FileToAPI(MoviesDTO dto, Movie Domain);
        Task<FileToAPI> RemoveImageFromAPI(FileToAPIDTO dto);
        Task<FileToAPI> RemoveImagesFromAPI(FileToAPIDTO[] dtos);
    }
}
