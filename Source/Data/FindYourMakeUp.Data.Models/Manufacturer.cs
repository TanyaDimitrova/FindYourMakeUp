namespace FindYourMakeUp.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Manufacturer
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

    }
}
