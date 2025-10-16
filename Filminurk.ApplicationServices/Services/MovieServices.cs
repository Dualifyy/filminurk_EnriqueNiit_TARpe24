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

        public MovieServices(FilminurkTARpe24Context context)
        {
            _context = context;
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


            _context.Movies.Remove(result);
            await _context.SaveChangesAsync();

            return result;

        }
    }
}
