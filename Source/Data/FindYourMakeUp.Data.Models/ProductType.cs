namespace FindYourMakeUp.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ProductType
    {
        public ProductType()
        {
            this.Categories = new HashSet<Category>(); // TODO: Refactor!
            this.Products = new HashSet<Product>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}