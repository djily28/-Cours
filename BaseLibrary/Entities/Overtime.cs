using System.ComponentModel.DataAnnotations;
namespace BaseLibrary.Entities
{
    public class Overtime : OtherBaseEntity
    {
        [Required]
        public DateTime StartDate { get; set; } = DateTime.Now;
        [Required]
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(30);
        public int NumberOfDays => (EndDate - StartDate).Days;

        // Many to one relationship with Vacation Type
        public OvertimeType? OvertimeType { get; set; }
        [Required]
        public int OvertimeTypeId { get; set; }
    }
}
