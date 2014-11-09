namespace FindYourMakeUp.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ProductType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        public int PurposeId { get; set; }

        public virtual Purpose Purpose { get; set; }
    }
}