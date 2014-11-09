namespace FindYourMakeUp.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        private ICollection<Purpose> purposes;

        public Category()
        {
            this.purposes = new HashSet<Purpose>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        public virtual ICollection<Purpose> Purposes
        {
            get
            {
                return this.purposes;
            }

            private set
            {
                this.purposes = value;
            }
        }
    }
}
