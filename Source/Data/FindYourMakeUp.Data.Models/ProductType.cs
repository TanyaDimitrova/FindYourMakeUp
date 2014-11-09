namespace FindYourMakeUp.Data.Models
{
    using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

    public class ProductType
    {
        public ProductType()
        {
            this.Purposes = new HashSet<Purpose>();// TODO: Refactor!
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        public virtual ICollection<Purpose> Purposes { get; set; }
    }
}