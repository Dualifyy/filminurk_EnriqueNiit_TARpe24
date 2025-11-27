using Filminurk.Core.Domain;
using System.ComponentModel.DataAnnotations;

namespace Filminurk.Models.Actors
{
    public class ActorsIndexViewModel
    {
        public Guid ActorID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public List<string>? MoviesActedFor { get; set; }
        // public Guid PortraitID { get; set; }
        // 3 õpilase andmetüübi
        public string MostPopularMovie { get; set; }
        public int Age { get; set; }
        public string Nationality { get; set; }


    }
}