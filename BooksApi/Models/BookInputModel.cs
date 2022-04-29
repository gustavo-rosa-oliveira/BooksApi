using System.ComponentModel.DataAnnotations;

namespace BooksApi.Models
{
    public class BookInputModel
    {
        /// <summary>
        /// Book title
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Book description
        /// </summary>
        public string Description { get; set; }
    }
}
