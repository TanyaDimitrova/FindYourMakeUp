namespace FindYourMakeUp.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FindYourMakeUp.Data.Contracts.Models;

    public class Product : AuditInfo
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

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public double Rating
        {
            get
            {
                if (this.reviews.Count == 0)
                {
                    return 0;
                }

                int allRates = 0;
                foreach (var review in this.Reviews)
                {
                    allRates += review.Rate;
                }

                return allRates / this.Reviews.Count;
            }
        }

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
