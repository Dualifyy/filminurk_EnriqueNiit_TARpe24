namespace Filminurk.Models.Movies
{
    public class MoviesCreateUpdateViewModel
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateOnly FirstPublished { get; set; }
        public string Director { get; set; }
        public List<string>? Actor { get; set; }
        public double CurrentRating { get; set; }
        //public List<UserComment>? Reviews { get; set; }

        /* Kaasasolevate piltide andmeomadused*/
        public List<IFormFile> Files { get; set; }
        public List<ImageViewModel> Images { get; set; } = new List<ImageViewModel>();

        /*kolm omal valikul andmetüüpi yo*/

        public double? UserRating { get; set; }
        public string? BuyPrice { get; set; }
        public int? MovieLength { get; set; }
        public DateTime? EntryCreatedAt { get; set; }
        public DateTime? EntryModifiedAt { get; set; }
    }
}
