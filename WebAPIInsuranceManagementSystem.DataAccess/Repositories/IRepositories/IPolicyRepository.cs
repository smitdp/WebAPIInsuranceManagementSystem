using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIInsuranceManagementSystem.DataAccess.Models;
namespace WebAPIInsuranceManagementSystem.DataAccess.Repositories.IRepositories
{
    public interface IPolicyRepository
    {
        Task<List<Policy>> GetAllPolicies();
    }
}
