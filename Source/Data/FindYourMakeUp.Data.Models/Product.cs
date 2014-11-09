namespace FindYourMakeUp.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Product
    {
        private ICollection<Review> reviews;

        public Product()
        {
            this.reviews = new HashSet<Review>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [Required]
        public int ManufacturerId { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        public string Description { get; set; }

        public int ProductTypeId { get; set; }

        public virtual ProductType ProductType { get; set; }

        public virtual ICollection<Review> Reviews
        {
            get
            {
                return this.reviews;
            }

            private set
            {
                this.reviews = value;
            }
        }
    }
}
