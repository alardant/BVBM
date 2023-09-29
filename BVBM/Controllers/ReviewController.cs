using BVBM.API.Dto;
using BVBM.API.Interface;
using BVBM.API.Models;
using BVBM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace BVBM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IUserRepository _userRepository;
        public ReviewController(IReviewRepository reviewRepository, IUserRepository userRepository)
        {
            _reviewRepository = reviewRepository;
            _userRepository = userRepository;
        }

        //Get all reviews
        [HttpGet]
        public async Task<IActionResult> getAllReviews()
        {
            var reviews = await _reviewRepository.GetAllReviewsAsync();
            return Ok(reviews);
        }

        [HttpGet("id")]
        public async Task<IActionResult> getReviewbyId(int id)
        {
                var review = await _reviewRepository.GetReviewByIdAsync(id);
                return Ok(review);
        }

        //Create a review
        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateReview([FromBody] ReviewDto reviewCreate)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest("Échec de la création du commentaire. Veuillez réessayer.");
            }

            try
            {
                var review = new Review
                {
                    Description = reviewCreate.Description,
                    Name = reviewCreate.Name,
                    CreatedDate = DateTime.Now,
                    Package = reviewCreate.Package,
                    UserId = _userRepository.GetFirstUser().Id
                };

                _reviewRepository.CreateReview(review);
            }
            catch (Exception ex)
            {
                return BadRequest("Échec de la création du commentaire. Veuillez réessayer.");
            }

            return Ok("Le commentaire a bien été crée !");
        }


        //Update a review
        [Authorize]
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateReview(int id, [FromBody] ReviewDto reviewUpdate)
        {
            if (!_reviewRepository.ReviewExists(id) || reviewUpdate == null)
                return NotFound("Échec de la modification du commentaire, le commentaire n'a pas été trouvé");

            if (!ModelState.IsValid)
                return BadRequest("Échec de la modification du commentaire, veuillez réessayer.");

            var review = await _reviewRepository.GetReviewByIdAsync(id);

            try
            {
                review.Description = reviewUpdate.Description;
                review.Name = reviewUpdate.Name;
                review.CreatedDate = DateTime.Now;
                review.Package = reviewUpdate.Package;
                review.UserId = _userRepository.GetFirstUser().Id;

                _reviewRepository.UpdateReview(review);
            }
            catch
            {
                return BadRequest("Échec de la modification du commentaire. Veuillez réessayer.");
            }

            return Ok("Le commentaire a bien été modifié !");
        }

        //Delete de Review
        [Authorize]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            if (!_reviewRepository.ReviewExists(id))
            {
                return NotFound("Échec de la suppression du commentaire, le commentaire n'a pas été trouvé");
            }

            if (!ModelState.IsValid)
            {
                return NotFound("Échec de la suppression du commentaire, veuillez réessayer.");
            }

            var reviewToDelete = await _reviewRepository.GetReviewByIdAsync(id);

            try
            {
                _reviewRepository.DeleteReview(reviewToDelete);
            }
            catch (Exception ex)
            {
                return NotFound("Échec de la suppression du commentaire, veuillez réessayer.");
            }

            return Ok("Le commentaire a bien été supprimé !");

        }

    }
}