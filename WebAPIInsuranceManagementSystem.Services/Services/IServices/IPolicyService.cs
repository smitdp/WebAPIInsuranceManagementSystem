using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIInsuranceManagementSystem.Services.DTOs;

namespace WebAPIInsuranceManagementSystem.Services.Services.IServices
{
    public interface IPolicyService
    {
        Task<List<PolicyDTO>> GetAllPolicies();
    }
}
