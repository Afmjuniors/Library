using Newtonsoft.Json;
using TDCore.Core;
using NN.Checklist.Domain.Common;
using System.Collections.Generic;
using NN.Checklist.Domain.DTO.Request.User;

namespace NN.Checklist.Domain.DTO.Response
{
    public class AuthenticatedUserDTO
    {
        
        public long UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public EnumAuthenticationType AuthenticationType { get; set; }

        public string Email { get; set; }

        [NoMap]
        public string Password { get; set; }
        [Map("Initials")]
        public string UserAD { get; set; }

        
        public long? DomainAdId { get; set; }

        public bool Deactivated { get; set; }

        public bool Active { get; set; }


        public string UserProfile { get; set; }
        
        public long UserProfileId { get; set; }

        
        public string Token { get; set; }

        [Map("Language.Code")]
        public string CultureInfo { get; set; }

        public long InactivityTimeLimit { get; set; }
        [NoMap]
        public List<PermissionDTO> Permissions { get; set; }
        [NoMap]
        public List<string> PhonesNumbers { get; set; }
        [NoMap]
        public List<string> GroupsAreas { get; set; }
        [NoMap]
        public List<string> MemberOf { get; set; }
        [NoMap]
        public List<AdGroupDTO> AdGroups { get; set; }
        public List<AdGroupUserDTO> AdGroupsUser { get; set; }
        [NoMap]
        public MenuDTO Menu { get; set; }
    }
}
