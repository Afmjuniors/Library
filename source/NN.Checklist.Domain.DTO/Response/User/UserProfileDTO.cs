using Newtonsoft.Json;
using TDCore.Core;
using System.Collections.Generic;

namespace NN.Checklist.Domain.DTO.Response
{
    public class UserProfileDTO
    {
        
        [JsonProperty("IdPerfilUsuario")]
        public long ProfileUserId { get; set; }
        [JsonProperty("Descricao")]
        public string Description { get; set; }

        [NoMap]
        [JsonProperty("Permissoes")]
        public List<PermissionDTO> Permissions { get; set; }
    }
}
