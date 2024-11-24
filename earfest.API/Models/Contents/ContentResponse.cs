using earfest.API.Domain.Entities;
using System.ComponentModel;

namespace earfest.API.Models.Contents
{
    public class ContentResponse : BaseResponse
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? AudioUrl { get; set; }
        public string? VideoUrl { get; set; }
        public string? Lyrics { get; set; }
     
        //public List<AppUser> Artists { get; set; }
        //public List<Category> Categories { get; set; }
        //public List<Mood> Moods { get; set; }

    }
}
