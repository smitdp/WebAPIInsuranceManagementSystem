using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIInsuranceManagementSystem.DataAccess.Models;
using WebAPIInsuranceManagementSystem.DataAccess.Repositories.IRepositories;

namespace WebAPIInsuranceManagementSystem.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly InsuranceAndClaimManagementDbContext _context;

        public UserRepository(InsuranceAndClaimManagementDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddUserPolicy(UserPolicy userPolicy)
        {
            try
            {
                _context.UserPolicies.Add(userPolicy);
                int rowsAffected = await _context.SaveChangesAsync();

                return rowsAffected;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public async Task<List<UserPolicy>> GetBoughtPolicies(int userId)
        {
            try
            {
                if (userId <= 0)
                {
                    Console.WriteLine("Invalid user ID. User ID must be greater than zero.");
                    return new List<UserPolicy>();
                }

                List<UserPolicy> myPolicies = await _context.UserPolicies
                    .Include(up => up.Policy)
                        .ThenInclude(p => p.PolicyType)
                    .Include(up => up.Agent)
                    .Where(up => up.UserId == userId)
                    .ToListAsync();

                

                return myPolicies;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving user policies: {ex.Message}");
                return new List<UserPolicy>();
            }
        }

        public async Task<List<User>> GetApprovedAgents()
        {
            try
            {
                return await _context.Users
               .Where(u => u.RoleId == 2 && u.IsApproved == 1)
               .ToListAsync();
            }
            catch(Exception ex)
            {
                return new List<User>();
            }
        }

        public async Task<User> GetUserInfo(int id)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new User();
            }
        }
    }
}
