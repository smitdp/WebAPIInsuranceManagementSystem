using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIInsuranceManagementSystem.DataAccess.Models;
using WebAPIInsuranceManagementSystem.Services.DTOs;

namespace WebAPIInsuranceManagementSystem.Services.Services.IServices
{
    public interface IUserService
    {
        Task<int> BuyPolicy(UserPolicyDTO userPolicyDTO);

        Task<List<MyPolicyDTO>> GetBoughtPolicies(int userId);

        Task<List<UserDTO>> GetApprovedAgents();
        Task<UserDTO> GetUserInfo(int id);


    }

}
