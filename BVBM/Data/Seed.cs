using BVBM.API.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.Metrics;
using System.Security.Cryptography;
using System.Text;

// *** To delete upon Deployment ***

namespace BVBM.API.Data
{
    public class Seed
    {
        private readonly DataContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public Seed(DataContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task SeedDataContext()
        {
            if (!_context.Users.Any())
            {
                string adminUserEmail = "bienvivrebienmanger@gmail.com";
                var adminUser = await _userManager.FindByEmailAsync(adminUserEmail);

                if (adminUser == null)
                {
                    var newAdminUser = new IdentityUser()
                    {
                        UserName = "bienvivrebienmanger@gmail.com",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                    };

                    await _userManager.CreateAsync(newAdminUser, "Happy2share-6");
                }
            }

            if (!_context.Reviews.Any())
                {
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


