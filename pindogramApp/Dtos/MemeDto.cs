using pindogramApp.Entities;
using System;

namespace pindogramApp.Dtos
{
    public class MemeDto
    {
        public int? Id { get; set; } = null;
        public string Title { get; set; }
        public DateTime DateAdded { get; set; }
        public string Image { get; set; }
        public int Likes { get; set; }
        public AuthorDto Author { get; set; }
        public bool ActiveUp { get; set; }
        public bool ActiveDown { get; set; }
    }
}
