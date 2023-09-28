using System.ComponentModel.DataAnnotations;

namespace BVBM.API.Dto
{
    public class ReviewDto
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Package Package { get; set; }
        
    }
}
