using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIInsuranceManagementSystem.DataAccess.Models;

namespace WebAPIInsuranceManagementSystem.DataAccess.Repositories.IRepositories
{
    public interface IUserRepository
    {

        public Task<int> AddUserPolicy(UserPolicy userPolicy);

        public Task<List<UserPolicy>> GetBoughtPolicies(int userId);
        public Task<List<User>> GetApprovedAgents();

        Task<User> GetUserInfo(int id);


    }
}
