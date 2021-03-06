﻿namespace FindYourMakeUp.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using FindYourMakeUp.Data.Contracts.Models;

    public class Product : DeletableEntity
    {
        private const string DefaultImageUrl = "https://cdn1.iconfinder.com/data/icons/DarkGlass_Reworked/128x128/apps/sodipodi.png";
        private ICollection<Review> reviews;

        public Product()
        {
            this.ImageUrl = DefaultImageUrl;
            this.reviews = new HashSet<Review>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        public int ManufacturerId { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 3)]
        public string Description { get; set; }

        [DefaultValue(DefaultImageUrl)]
        public string ImageUrl { get; set; }

        [Required]
        public int ProductTypeId { get; set; }

        public virtual ProductType ProductType { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        [DefaultValue(0)]
        public double Rating
        {
            get
            {
             var allreviews = this.Reviews;
                if (allreviews.Count > 0)
                {
                    int allRates = 0;
                    foreach (var review in allreviews)
                    {
                        allRates += review.Rate;
                    }

                    return allRates / allreviews.Count;
                }

                return 0;
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
