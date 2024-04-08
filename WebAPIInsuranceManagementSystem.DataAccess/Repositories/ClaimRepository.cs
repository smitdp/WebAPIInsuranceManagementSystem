using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIInsuranceManagementSystem.DataAccess.Models;
using WebAPIInsuranceManagementSystem.DataAccess.Repositories.IRepositories;

namespace WebAPIInsuranceManagementSystem.DataAccess.Repositories
{
    public class ClaimRepository : IClaimRepository
    {
        private readonly InsuranceAndClaimManagementDbContext _context;

        public ClaimRepository(InsuranceAndClaimManagementDbContext context)
        {
            _context = context;
            
        }

        public async Task<int> CreateClaim(Claim claim)
        {
            _context.Claims.Add(claim);
            await _context.SaveChangesAsync();
            return claim.Id;
        }

        public async Task AddClaimDocument(int claimId, string documentPath)
        {
            ClaimDocument document = new ClaimDocument
            {
                ClaimId = claimId,
                DocumentPath = documentPath
            };
            _context.ClaimDocuments.Add(document);
            await _context.SaveChangesAsync();
        }


        public async Task<Claim> GetClaimById(int claimId)
        {
            return await _context.Claims.FindAsync(claimId);
        }
    }
}
