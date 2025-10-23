using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filminurk.Core.Domain;
using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Filminurk.ApplicationServices.Services
{
    public class FilesServices : IFilesServices
    {
        private readonly IHostEnvironment _webHost;
        private readonly FilminurkTARpe24Context _context;

        public FilesServices(IHostEnvironment webHost, FilminurkTARpe24Context context)
        {
            _webHost = webHost;
            _context = context;
        }

        public void FileToAPI(MoviesDTO dto, Movie Domain)
        {
            if (dto.Files != null && dto.Files.Count > 0)
            {
                if (!Directory.Exists(_webHost.ContentRootPath + "\\wwwroot\\multipleFileUpload\\"))
                {
                    Directory.CreateDirectory(_webHost.ContentRootPath + "\\wwwroot\\multipleFileUpload\\");
                }

                foreach (var file in dto.Files)
                {
                    string uploadsFolder = Path.Combine(_webHost.ContentRootPath, "wwwroot", "multipleFileUpload");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath,FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                        FileToAPI path = new FileToAPI
                        {
                            ImageID = Guid.NewGuid(),
                            ExistingFilePath = uniqueFileName,
                            MovieID = Domain.ID,
                        };

                        _context.FilesToAPI.AddAsync(path);
                    }

                }

            }
        }

        public async Task<FileToAPI> RemoveImageFromAPI(FileToAPIDTO dto)
        {
            var ImageID = await _context.FilesToAPI.FirstOrDefaultAsync(x => x.ImageID == dto.ImageID);
            var filePath = _webHost.ContentRootPath + "\\wwwroot\\multipleFileUpload\\" + ImageID.ExistingFilePath;

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            _context.FilesToAPI.Remove(ImageID);
            await _context.SaveChangesAsync();

            return null;
        }

        public async Task<List<FileToAPI>> RemoveImagesFromAPI(FileToAPIDTO[] dtos)
        {
            foreach (var dto in dtos)
            {
                RemoveImageFromAPI(dto);
            }
            return null;
        }
    }
}
