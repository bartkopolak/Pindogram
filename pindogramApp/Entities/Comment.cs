using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;

namespace pindogramApp.Entities

{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime DateAdded { get; set; }
        [ForeignKey("AuthorId")]
        public User Author { get; set; }
        public int AuthorId { get; set; }
        [ForeignKey("MemeId")]
        public virtual Meme Meme { get; set; }
        public int? MemeId { get; set; }

    }
}
