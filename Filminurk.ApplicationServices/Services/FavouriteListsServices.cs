using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filminurk.Core.Domain;
using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;

namespace Filminurk.ApplicationServices.Services
{
    public class FavouriteListsServices : IFavouriteListsServices
    {
        private readonly FilminurkTARpe24Context _context;

        public Task<FavouriteList> Create(FavouriteListDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<FavouriteList> DetailsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<FavouriteList> Update(FavouriteListDTO updatedList, string typeOfMethod)
        {
            
            
            FavouriteList updatedListInDB = new();

            updatedListInDB.FavouriteListID = updatedList.FavouriteListID;
            updatedListInDB.ListBelongsToUser = updatedList.ListBelongsToUser;
            updatedListInDB.IsMovieOrActor = updatedList.IsMovieOrActor;
            updatedListInDB.ListName = updatedList.ListName;
            updatedListInDB.ListDescription = updatedList.ListDescription;
            updatedListInDB.IsPrivate = updatedList.IsPrivate;
            updatedListInDB.ListOfMovies = updatedList.ListOfMovies;
            updatedListInDB.ListCreatedAt = updatedList.ListCreatedAt;
            updatedListInDB.ListDeletedAt = updatedList.ListDeletedAt;
            updatedListInDB.ListModifiedAt = updatedList.ListModifiedAt;

            if (typeOfMethod == "Delete")
            {
                _context.FavouriteLists.Attach(updatedListInDB);
                _context.Entry(updatedListInDB).Property(l => l.ListDeletedAt).IsModified = true;

            }
            else if (typeOfMethod == "Private")
            {
                _context.FavouriteLists.Attach(updatedListInDB);
                _context.Entry(updatedListInDB).Property(l => l.IsPrivate).IsModified = true;
                
            }
            _context.Entry(updatedListInDB).Property(l => l.ListModifiedAt).IsModified = true;
            await _context.SaveChangesAsync();
            return updatedListInDB;
        }

    }
}
