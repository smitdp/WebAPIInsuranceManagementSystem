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
    public class PolicyRepository : IPolicyRepository
    {
        private readonly InsuranceAndClaimManagementDbContext _context;
        public PolicyRepository(InsuranceAndClaimManagementDbContext context)
        {
            _context = context;
        }

        public async Task<List<Policy>> GetAllPolicies()
        {
            try
            {
                List<Policy> policies = await _context.Policies.Include(p => p.PolicyType).ToListAsync();
                return policies;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve policies from the database", ex);
            }
        }
    }
}
