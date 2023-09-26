using BVBM.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Security.Cryptography;
using System.Text;

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

        private string HashSecretKey(string secretKey)
        {
            using (var sha256 = SHA256.Create()) // Use a secure hashing algorithm
            {
                byte[] secretBytes = Encoding.UTF8.GetBytes(secretKey);
                byte[] hashedBytes = sha256.ComputeHash(secretBytes);

                // Convert the hashed bytes to a hexadecimal string
                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashedBytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }
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

