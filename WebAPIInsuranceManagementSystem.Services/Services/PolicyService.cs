using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIInsuranceManagementSystem.DataAccess.Models;
using WebAPIInsuranceManagementSystem.DataAccess.Repositories.IRepositories;
using WebAPIInsuranceManagementSystem.Services.DTOs;
using WebAPIInsuranceManagementSystem.Services.Services.IServices;

namespace WebAPIInsuranceManagementSystem.Services.Services
{
    public class PolicyService : IPolicyService
    {
        private readonly IPolicyRepository _policyRepository;

        public PolicyService(IPolicyRepository policyRepository)
        {
            _policyRepository = policyRepository;
        }

        public async Task<List<PolicyDTO>> GetAllPolicies()
        {
            try
            {
                List<Policy> policies = await _policyRepository.GetAllPolicies();

                if (policies == null || !policies.Any())  //check if it contains any elements
                {
                    return new List<PolicyDTO>();
                }

                List<PolicyDTO> convertedPolicies = policies.Select(policy => ConvertToPolicyDTO(policy)).ToList();
                return convertedPolicies;

            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve policies from the database " +  ex.Message);
            }
        }


        //********************************************************** Convert Model Methods *************************************************************
        
        private PolicyDTO ConvertToPolicyDTO(Policy policy)
        {
            PolicyDTO policyDTO = new PolicyDTO
            {
                Id = policy.Id,
                PolicyName = policy.PolicyName,
                PolicyNumber = policy.PolicyNumber,
                PolicyTypeName = policy.PolicyType.PolicyTypeName,
                Duration = policy.Duration,
                Description = policy.Description,
                Installment = policy.Installment,
                PremiumAmount = policy.PremiumAmount,
                IsActive = policy.IsActive,
            };

            return policyDTO;
        }

       
    }


}
