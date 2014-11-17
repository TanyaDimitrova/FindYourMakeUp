namespace FindYourMakeUp.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using FindYourMakeUp.Data.Contracts.Models;

    public class Category : DeletableEntity
    {
        public Category()
        {
            // TODO: Refactor!
            this.ProductTypes = new HashSet<ProductType>();
            this.Children = new HashSet<Category>();
        }

        [Key]
        public int Id { get; set; }

        [Index]
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        public int? ParentCategoryId { get; set; }

        public virtual Category ParentCategory { get; set; }

        [InverseProperty("ParentCategory")]
        public ICollection<Category> Children { get; set; }

        public ICollection<ProductType> ProductTypes { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
