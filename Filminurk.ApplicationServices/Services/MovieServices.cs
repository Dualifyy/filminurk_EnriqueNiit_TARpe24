using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Filminurk.Core.Domain;
using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Microsoft.EntityFrameworkCore;

namespace Filminurk.ApplicationServices.Services
{
    public class MovieServices : IMovieServices
    {
        private readonly FilminurkTARpe24Context _context;
        private readonly IFilesServices _filesServices;

        public MovieServices(FilminurkTARpe24Context context, IFilesServices filesServices)
        {
            _context = context;
            _filesServices = filesServices;
        }
        public async Task<Movie> Create(MoviesDTO dto)
        {
            Movie movie = new Movie();
            movie.ID = Guid.NewGuid();
            movie.Title = dto.Title;
            movie.Description = dto.Description;
            movie.CurrentRating = (double)dto.CurrentRating;
            movie.FirstPublished = (DateOnly)dto.FirstPublished;
            movie.Actor = dto.Actor;
            movie.Director = dto.Director;
            movie.UserRating = (double)dto.UserRating;
            movie.BuyPrice = dto.BuyPrice;
            movie.MovieLength = (int)dto.MovieLength;
            //movie.EntryCreatedAt = DateTime.Now;
            //movie.EntryModifiedAt = DateTime.Now;
            _filesServices.FileToAPI(dto, movie);

            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
            
            return movie;

        }
        public async Task<Movie> DetailsAsync(Guid id)
        {
            var result = await _context.Movies.FirstOrDefaultAsync(x => x.ID == id);
            return result;
        }
        public async Task<Movie> Delete(Guid id)
        {
            var result = await _context.Movies
                .FirstOrDefaultAsync(m => m.ID == id);

            var images = await _context.FilesToAPI
                .Where(x => x.MovieID == id)
                .Select(y => new FileToAPIDTO
                {
                    ImageID = y.ImageID,
                    MovieID = y.MovieID,
                    FilePath = y.ExistingFilePath,
                }).ToArrayAsync();

            await _filesServices.RemoveImagesFromAPI(images);
            _context.Movies.Remove(result);
            await _context.SaveChangesAsync();

            return result;

        }

        public async Task<Movie> Update(MoviesDTO dto)
        {
            Movie movie = new Movie();

            movie.ID = (Guid)dto.ID;
            movie.Title = dto.Title;
            movie.Description = dto.Description;
            movie.CurrentRating = (double)dto.CurrentRating;
            movie.UserRating = (double)dto.UserRating;
            movie.FirstPublished = (DateOnly)dto.FirstPublished;
            movie.Actor = dto.Actor;
            movie.Director = dto.Director;
            movie.BuyPrice = dto.BuyPrice;
            movie.MovieLength = (int)dto.MovieLength;
            _filesServices.FileToAPI(dto, movie);
  

            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();
            return movie;
        }
    }
}
