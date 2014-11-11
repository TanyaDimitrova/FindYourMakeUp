namespace FindYourMakeUp.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Category
    {
        public Category()
        {
            //TODO: Refactor!
            this.ProductTypes = new HashSet<ProductType>();
        }

        [Key]
        public int Id { get; set; }

        [Index]
        [Required]
        [StringLength(100, MinimumLength=3)]
        public string Name { get; set; }

        public int? ParentCategoryId { get; set; }

        public Category ParentCategory { get; set; }

        public ICollection<ProductType> ProductTypes { get; set; }
    }
}
