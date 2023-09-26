using BVBM.API.Dto;
using BVBM.API.Interface;
using BVBM.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace BVBM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;
        public ReviewController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        //Get all reviews
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        public async Task<IActionResult> getAllReviews()
        {
            var reviews = await _reviewRepository.GetAllReviewsAsync();
            return Ok(reviews);
        }

        //Create a review
        [Authorize]
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateReview([FromBody] ReviewDto reviewCreate)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var review = new Review
                {
                    Description = reviewCreate.Description,
                    Name = reviewCreate.Name,
                    CreatedDate = DateTime.Now,
                    Package = reviewCreate.Package,
                    UserId = reviewCreate.UserId,
                    IsValidated = reviewCreate.IsValidated
                };

                _reviewRepository.CreateReview(review);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Échec de la création du commentaire. Veuillez réessayer");
                return StatusCode(500, ModelState);
            }

            return Ok("Le commentaire a bien été crée !");
        }


        //Update a review
        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateReview(int id, [FromBody] ReviewDto reviewUpdate)
        {
            if (reviewUpdate == null)
                return BadRequest(ModelState);

            if (id != reviewUpdate.Id)
                return BadRequest(ModelState);

            if (!_reviewRepository.ReviewExists(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var review = await _reviewRepository.GetReviewByIdAsync(id);

            try
            {
                review.Description = reviewUpdate.Description;
                review.Name = reviewUpdate.Name;
                review.CreatedDate = reviewUpdate.CreatedDate;
                review.Package = reviewUpdate.Package;
                review.UserId = reviewUpdate.UserId;

                _reviewRepository.UpdateReview(review);
            } catch
            {
                ModelState.AddModelError("", "Échec de la modification du commentaire. Veuillez réessayer");
                return StatusCode(500, ModelState);
            }

            return Ok("Le commentaire a bien été modifié !");
        }

        //Delete de Review
        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteReview(int id)
        {
            if (!_reviewRepository.ReviewExists(id))
            {
                return NotFound();
                }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reviewToDelete = await _reviewRepository.GetReviewByIdAsync(id);

            try
            {
                _reviewRepository.DeleteReview(reviewToDelete);
            } catch (Exception ex) 
            {
                ModelState.AddModelError("", "Échec de la suppression du commentaire. Veuillez réessayer");
            }

            return Ok("Le commentaire a bien été supprimé !");

        }

    }
}
