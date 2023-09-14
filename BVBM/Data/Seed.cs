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

        public async Task SeedJwt()
        {
            if (!_context.JwtSecrets.Any())
            {
                string originalSecretKey = "E2C42061-7964-43B0-B936-0E072BF03553";
                string hashedSecretKey = HashSecretKey(originalSecretKey);

                _context.JwtSecrets.Add(new JwtSecret { SecretKeyHash = hashedSecretKey });
                _context.SaveChanges();
            }
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

