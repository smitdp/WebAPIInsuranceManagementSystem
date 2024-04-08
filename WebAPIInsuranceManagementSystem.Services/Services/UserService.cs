using WebAPIInsuranceManagementSystem.DataAccess.Models;
using WebAPIInsuranceManagementSystem.DataAccess.Repositories.IRepositories;
using WebAPIInsuranceManagementSystem.Services.DTOs;
using WebAPIInsuranceManagementSystem.Services.Services.IServices;


namespace WebAPIInsuranceManagementSystem.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<int> BuyPolicy(UserPolicyDTO userPolicyDTO)
        {
            try
            {
                if (userPolicyDTO == null)
                {
                    return -1;
                }

                UserPolicy userPolicy = ConvertToUserPolicy(userPolicyDTO);
              
                return await _userRepository.AddUserPolicy(userPolicy);
              
            }
            catch (Exception ex)
            {
                return -2; 
            }
        }

        public async Task<List<MyPolicyDTO>> GetBoughtPolicies(int userId)
        {
            if (userId <= 0)
            {
                return new List<MyPolicyDTO>();
            }

            List<UserPolicy> mypolicies =  await _userRepository.GetBoughtPolicies(userId);

            List<MyPolicyDTO> convertedMyPolicies = mypolicies.Select(policy => ConvertToMyPolicyDTO(policy)).ToList();
            return convertedMyPolicies;

        }

        public async Task<List<UserDTO>> GetApprovedAgents()
        {
            try
            {
                List<User> approvedAgents = await _userRepository.GetApprovedAgents();
                List<UserDTO> convertedAgents = approvedAgents.Select(agent => convertToUserDTO(agent)).ToList();
                return convertedAgents;
            }
            catch (Exception ex)
            { 
                throw new Exception("Error occurred while retrieving agents " + ex.Message);
            }
        }

        public async Task<UserDTO> GetUserInfo(int id)
        {
            try
            {
                User user = await _userRepository.GetUserInfo(id);

                UserDTO userDTO = convertToUserDTO(user);

                return userDTO;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while retrieving agents " + ex.Message);
            }
        }



        //********************************************************** Convert Model Methods *************************************************************
        private UserPolicy ConvertToUserPolicy(UserPolicyDTO userPolicyDTO)
        {
            UserPolicy userPolicy = new UserPolicy
            {
                UserId = userPolicyDTO.UserId,
                PolicyId = userPolicyDTO.PolicyId,
                AgentId = userPolicyDTO.AgentId,
                EnrollmentDate = userPolicyDTO.EnrollmentDate,
                EndDate = userPolicyDTO.EndDate
            };

            return userPolicy;
        }

        private MyPolicyDTO ConvertToMyPolicyDTO(UserPolicy userPolicy)
        {
            MyPolicyDTO policy = new MyPolicyDTO
            {
                Id = userPolicy.Id,
                UserId = userPolicy.UserId,
                PolicyId = userPolicy.PolicyId,
                EnrollmentDate = userPolicy.EnrollmentDate,
                EndDate = userPolicy.EndDate,
                PolicyNumber = userPolicy.Policy.PolicyNumber,
                policyTypeName = userPolicy.Policy.PolicyType.PolicyTypeName,
                PolicyName = userPolicy.Policy.PolicyName,
                Duration = userPolicy.Policy.Duration,
                Description = userPolicy.Policy.Description,
                Installment = userPolicy.Policy.Installment,
                PremiumAmount = userPolicy.Policy.PremiumAmount,
                AgentName = $"{userPolicy.Agent.FirstName} {userPolicy.Agent.LastName}",
                AgentPhoneNumber = userPolicy.Agent.PhoneNumber,

            };

            return policy;
        }

        private UserDTO convertToUserDTO(User user)
        {
            UserDTO userDTO = new UserDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };

            return userDTO;

        }

       
    }
}

