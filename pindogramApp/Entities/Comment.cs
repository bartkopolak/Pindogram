using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public virtual User Author { get; set; }
        public int? AuthorId { get; set; }
        [ForeignKey("MemeId")]
        public virtual Meme Meme { get; set; }
        public int? MemeId { get; set; }

    }
}
