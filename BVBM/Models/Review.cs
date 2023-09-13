using System.ComponentModel.DataAnnotations.Schema;

namespace BVBM.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public  DateTime CreatedDate { get; set; }
        public Package Package { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
