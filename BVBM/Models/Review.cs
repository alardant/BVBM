using System.ComponentModel.DataAnnotations.Schema;

namespace BVBM.API.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public  DateTime CreatedDate { get; set; }
        public Package Package { get; set; }
        public bool IsValidated { get; set; } = false;
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }


    }
}
