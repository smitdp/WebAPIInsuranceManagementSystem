using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIInsuranceManagementSystem.DataAccess.Models;
using WebAPIInsuranceManagementSystem.DataAccess.Repositories.IRepositories;
using WebAPIInsuranceManagementSystem.Services.DTOs;
using WebAPIInsuranceManagementSystem.Services.Services.IServices;

namespace WebAPIInsuranceManagementSystem.Services.Services
{
    public class ClaimService : IClaimService
    {
        private readonly IClaimRepository _claimRepository;

        public ClaimService(IClaimRepository claimRepository)
        {
            _claimRepository = claimRepository;
        }

        public async Task<int> CreateClaim(ClaimDTO claimDTO)
        {
            try
            {
                Claim claim = ConvertToClaimModel(claimDTO);

                int claimId = await _claimRepository.CreateClaim(claim);

                if (claimId > 0 && claimDTO.Documents != null && claimDTO.Documents.Any())
                {
                    foreach (var documentPath in claimDTO.Documents)
                    {
                        await _claimRepository.AddClaimDocument(claimId, documentPath);
                    }
                }

                return claimId;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create claim", ex);
            }
        }


      
        private Claim ConvertToClaimModel(ClaimDTO claimDTO)
        {
            return new Claim
            {
                PolicyId = claimDTO.PolicyId,
                UserId = claimDTO.UserId,
                IncidentDate = claimDTO.IncidentDate,
                IncidentLocation = claimDTO.IncidentLocation,
                Address = claimDTO.Address,
                Description = claimDTO.Description,
                Status = claimDTO.Status
            };
        }
    }
}
