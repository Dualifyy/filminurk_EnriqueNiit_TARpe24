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

namespace Filminurk.ApplicationServices.Services
{
    public class ActorServices : IActorServices
    {
        private readonly FilminurkTARpe24Context _context;

        public ActorServices(FilminurkTARpe24Context context)
        {
            _context = context;
        }

        public async Task<Actors> Create(ActorsDTO dto)
        {
            Actors actor = new Actors();
            actor.ActorID = Guid.NewGuid();
            actor.FirstName = dto.FirstName;
            actor.LastName = dto.LastName;
            actor.NickName = dto.NickName;
            actor.MoviesActedFor = dto.MoviesActedFor;
            actor.MostPopularMovie = dto.MostPopularMovie;
            actor.Age = (int)dto.Age;
            actor.Nationality = dto.Nationality;
            actor.EntryCreatedAt = DateTime.Now;
            actor.EntryModifiedAt = DateTime.Now;

            await _context.Actors.AddAsync(actor);
            await _context.SaveChangesAsync();

            return actor;
        }

        public async Task<Actors> DetailsAsync(Guid id)
        {
            var result = await _context.Actors.FirstOrDefaultAsync(x => x.ActorID == id);

            return result;
        }

        public async Task<Actors> Delete(Guid id)
        {
            var result = await _context.Actors.FirstOrDefaultAsync(x => x.ActorID == id);

            _context.Actors.Remove(result);
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<Actors> Update(ActorsDTO dto)
        {
            Actors actor = new Actors();
            actor.ActorID = (Guid)dto.ActorID;
            actor.FirstName = dto.FirstName;
            actor.LastName = dto.LastName;
            actor.NickName = dto.NickName;
            actor.MoviesActedFor = dto.MoviesActedFor;
            actor.MostPopularMovie = dto.MostPopularMovie;
            actor.Age = (int)dto.Age;
            actor.Nationality = dto.Nationality;
            actor.EntryCreatedAt = dto.EntryCreatedAt;
            actor.EntryModifiedAt = DateTime.Now;

            _context.Actors.Update(actor);
            await _context.SaveChangesAsync();
            return actor;
        }
    }
}