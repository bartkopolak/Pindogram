using pindogramApp.Entities;
using System;


namespace pindogramApp.Dtos
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime DateAdded { get; set; }
        public AuthorDto Author { get; set; }
        public int MemeId { get; set; }
    }
}
