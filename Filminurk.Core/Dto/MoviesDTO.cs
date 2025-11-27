using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Filminurk.Core.Dto
{
    public class MoviesDTO
    {
        public Guid? ID { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateOnly? FirstPublished { get; set; }
        public string? Director { get; set; }
        public List<string>? Actors { get; set; }
        public double? CurrentRating { get; set; }
        //public List<UserComment>? Reviews { get; set; }

        public List<IFormFile> Files { get; set; }
        public IEnumerable<FileToAPIDTO> FileToAPIDTOs { get; set; } = new List<FileToAPIDTO>();

        /*kolm omal valikul andmetüüpi yo*/

        public double? UserRating { get; set; }
        public string? BuyPrice { get; set; }
        public int? MovieLength { get; set; }
        public DateTime? EntryCreatedAt { get; set; }
        public DateTime? EntryModifiedAt { get; set; }
    }
}
