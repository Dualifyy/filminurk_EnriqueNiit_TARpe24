namespace Filminurk.Models.Movies
{
    public class MoviesIndexViewModel
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public DateOnly FirstPublished { get; set; }
        public decimal CurrentRating { get; set; }
        //public List<UserComment>? Reviews { get; set; }

        /*2 omal valikul andmetüüpi yo*/

        public double UserRating { get; set; }
        public int MovieLength { get; set; }
    }
}
