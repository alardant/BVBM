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

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        public async Task<IActionResult> GetAllReviews()
        {
            var reviews = await _reviewRepository.GetAllReviewsAsync();
            return Ok(reviews);
        }

        [HttpGet("Validated")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        public async Task<IActionResult> GetReviewsValidated()
        {
            var reviews = await _reviewRepository.GetAllReviewsValidatedAsync();
            return Ok(reviews);
        }

        [HttpGet("NotValidated")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        public async Task<IActionResult> GetReviewsNotValidatedAsync()
        {
            var reviews = await _reviewRepository.GetAllReviewsNotValidatedAsync();
            return Ok(reviews);
        }

        [HttpPut("Validate/{id}")]
        public async Task<IActionResult> ValidateReview(int id)
        {
            var review = await _reviewRepository.GetReviewByIdAsync(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(!_reviewRepository.ReviewExists(id))
            {
                throw new Exception("Commentaire non trouvé");
            }

            try
            {
                review.IsValidated = true;
                _reviewRepository.UpdateReview(review);
                return Ok(new { message = "Le commentaire a bien été validé !" });
            } catch (Exception ex)
           {
                return Ok(new { message = "Erreur lors de la validation du commentaire. Veuillez ressayer" });
            }
        }


            ////Update a review
            //[Authorize]
            //[HttpPut("{id}")]
            //[ProducesResponseType(400)]
            //[ProducesResponseType(204)]
            //[ProducesResponseType(404)]
            //public async Task<IActionResult> UpdateReview(int id, [FromBody] ReviewDto reviewUpdate)
            //{
            //    if (reviewUpdate == null)
            //        return BadRequest(ModelState);

            //    if (id != reviewUpdate.Id)
            //        return BadRequest(ModelState);

            //    if (!_reviewRepository.ReviewExists(id))
            //        return NotFound();

            //    if (!ModelState.IsValid)
            //        return BadRequest();

            //    var review = await _reviewRepository.GetReviewByIdAsync(id);

            //    try
            //    {
            //        review.Description = reviewUpdate.Description;
            //        review.Name = reviewUpdate.Name;
            //        review.CreatedDate = reviewUpdate.CreatedDate;
            //        review.Package = reviewUpdate.Package;
            //        review.UserId = reviewUpdate.UserId;

            //        _reviewRepository.UpdateReview(review);
            //    } catch
            //    {
            //        ModelState.AddModelError("", "Échec de la modification du commentaire. Veuillez réessayer");
            //        return StatusCode(500, ModelState);
            //    }

            //    return Ok("Le commentaire a bien été modifié !");
            //}

            //Delete de Review
        [HttpDelete("delete/{id}")]
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
                return Ok(new { message = "Le commentaire a bien été validé !" });

            }
            catch (Exception ex) 
            {
                return Ok(new { message = "Erreur lors de la suppression du commentare. Veuillez ressayer" });
            }
        }

    }
}
