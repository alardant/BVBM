using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BVBM.API.Dto
{
    public class ContactDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
