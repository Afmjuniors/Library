using Newtonsoft.Json;
using AptaCore.Core;
using System.Collections.Generic;

namespace Novo.Checklist.Domain.DTO.Response
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
