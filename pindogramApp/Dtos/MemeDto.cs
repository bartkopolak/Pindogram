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
    }
}
