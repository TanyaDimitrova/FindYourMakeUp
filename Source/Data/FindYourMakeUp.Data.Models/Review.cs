namespace FindYourMakeUp.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(100)]
        public string Content { get; set; }

        [Required]
        [Range(0,10)]
        public int Rating { get; set; }

        [Required]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
