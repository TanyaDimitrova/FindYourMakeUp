namespace FindYourMakeUp.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Purpose
    {
        private ICollection<ProductType> productTypes;

        public Purpose()
        {
            this.productTypes = new HashSet<ProductType>();
        }


        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<ProductType> ProductTypes
        {
            get
            {
                return this.productTypes;
            }

            private set
            {
                this.productTypes = value;
            }
        }
    }
}
