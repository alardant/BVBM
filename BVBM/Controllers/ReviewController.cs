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
            try
            {
                var reviews = await _reviewRepository.GetAllReviewsAsync();
                return Ok(reviews);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while fetching reviews.");
            }
        }

        [HttpGet("id")]
        public async Task<IActionResult> getReviewbyId(int id)
        {
            try
            {
                var review = await _reviewRepository.GetReviewByIdAsync(id);
                if (review == null)
                {
                    return NotFound("Review not found.");
                }
                return Ok(review);
            }
            catch (Exception)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, "An error occurred while fetching the review.");
            }
        }

        //Create a review
        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateReview([FromBody] ReviewDto reviewCreate)
        {
            try
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
                catch (Exception)
                {
                    return BadRequest("Échec de la création du commentaire. Veuillez réessayer.");
                }

                return Ok("Le commentaire a bien été crée !");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while creating the review.");
            }
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

            try
            {
                var review = await _reviewRepository.GetReviewByIdAsync(id);
                review.Description = reviewUpdate.Description;
                review.Name = reviewUpdate.Name;
                review.CreatedDate = DateTime.Now;
                review.Package = reviewUpdate.Package;
                review.UserId = _userRepository.GetFirstUser().Id;

                _reviewRepository.UpdateReview(review);
                return Ok("Le commentaire a bien été modifié !");
            }
            catch
            {
                return StatusCode(500, "An error occurred while updating the review.");
            }

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


            try
            {
                var reviewToDelete = await _reviewRepository.GetReviewByIdAsync(id);
                _reviewRepository.DeleteReview(reviewToDelete);
                return Ok("Le commentaire a bien été supprimé !");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while deleting the review.");
            }


        }

    }
}