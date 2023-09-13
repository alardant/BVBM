namespace BVBM.Dto
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public Package Package { get; set; }
        public int UserId { get; set; }
    }
}
