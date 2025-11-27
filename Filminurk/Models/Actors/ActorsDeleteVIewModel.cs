using Filminurk.Core.Domain;
using Filminurk.Models.Movies;

namespace Filminurk.Models.Actors
{
    public class ActorsDeleteViewModel
    {
        public Guid? ActorID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? NickName { get; set; }
        public List<string>? MoviesActedFor { get; set; } = new List<string>();

        // public Guid PortraitID { get; set; }
        public string MostPopularMovie { get; set; }
        public int Age { get; set; }
        public string Nationality { get; set; }
        // andmebaasi jaoks
        public DateTime? EntryCreatedAt { get; set; }
        public DateTime? EntryModifiedAt { get; set; }
    }
}