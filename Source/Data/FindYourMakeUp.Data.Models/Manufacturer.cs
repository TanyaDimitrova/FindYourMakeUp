namespace FindYourMakeUp.Data.Models
{
    using FindYourMakeUp.Data.Contracts.Models;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Manufacturer : DeletableEntity
    {
        [Key]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string Name { get; set; }
    }
}
