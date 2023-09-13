using BVBM.API.Models;
using System.Diagnostics.Metrics;

// *** To delete upon Deployment ***

namespace BVBM.API.Data
{
    public class Seed
    {
        private readonly DataContext _context;
        public Seed(DataContext context)
        {
            _context = context;
        }


        public async Task SeedDataContext()
        {

            if (!_context.Reviews.Any()) {
                var reviews = new List<Review>()
                    {
                        new Review { Name="Pikachu",Description = "Pickahu is the best pokemon, because it is electric", CreatedDate = DateTime.Now, Package=Package.ConsultationIndividuelle, UserId = _context.Users.FirstOrDefault().Id},
                        new Review { Name="Pikachu", Description = "Pickachu is the best a killing rocks", CreatedDate = DateTime.Now, Package=Package.ConsultationIndividuelle, UserId = _context.Users.FirstOrDefault().Id},
                        new Review { Name="Pikachu",Description = "Pickchu, pickachu, pikachu", CreatedDate = DateTime.Now , Package = Package.Pack3mois, UserId = _context.Users.FirstOrDefault().Id},
                    };
                _context.Reviews.AddRange(reviews);
                _context.SaveChanges();
            }
        }
     }
}

