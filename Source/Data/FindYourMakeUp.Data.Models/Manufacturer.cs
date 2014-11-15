namespace FindYourMakeUp.Data.Models
{
    using FindYourMakeUp.Data.Contracts.Models;
    using System.ComponentModel.DataAnnotations;

    public class Manufacturer: AuditInfo
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
