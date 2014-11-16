namespace FindYourMakeUp.Data.Models
{
    using FindYourMakeUp.Data.Contracts.Models;
    using System.ComponentModel.DataAnnotations;

    public class Review : DeletableEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(100)]
        public string Content { get; set; }

        [Required]
        [Range(-10, 10)]
        public int Rate { get; set; }

        [Required]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
