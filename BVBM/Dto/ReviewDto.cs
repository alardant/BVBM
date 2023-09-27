using System.ComponentModel.DataAnnotations;

namespace BVBM.API.Dto
{
    public class ReviewDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public Package Package { get; set; }
        [Required]
        public string UserId { get; set; }
        
    }
}
