using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksApi.Models
{
    [Table("BOOK")]
    public class Book
    {
        [Column("ID")]
        [Key]
        public int Id { get; set; }

        [Column("TITLE")]
        [Required]
        public string Title { get; set; }

        [Column("DESCRIPTION")]
        public string Description { get; set; }

        public Book(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }
}
