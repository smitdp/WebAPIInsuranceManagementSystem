using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIInsuranceManagementSystem.Services.DTOs
{
    public class UserDTO
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        public int RoleId { get; set; }

        public int? IsApproved { get; set; }

        public bool IsActive { get; set; }

    }
}


// Request Body
//{
//  "userId": 0,
//  "policyId": 0,
//  "agentId": 0,
//  "enrollmentDate": "2024-04-06",
//  "endDate" : "2025-04-06"
//}