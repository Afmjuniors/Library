using Newtonsoft.Json;
using AptaCore.Core;
using Novo.Checklist.Domain.Common;
using System.Collections.Generic;

namespace Novo.Checklist.Domain.DTO.Response
{
    public class AuthenticatedUserDTO
    {
        [JsonProperty("IdUsuario")]
        public long UserId { get; set; }

        [JsonProperty("PrimeiroNome")]
        public string FirstName { get; set; }

        [JsonProperty("Sobrenome")]
        public string LastName { get; set; }

        [JsonProperty("TipoAutenticacao")]
        public EnumAuthenticationType AuthenticationType { get; set; }

        public string Email { get; set; }

        [NoMap]
        [JsonProperty("Senha")]
        public string Password { get; set; }

        [JsonProperty("UsuarioAD")]
        public string UserAD { get; set; }

        [JsonProperty("IdDominioAD")]
        [Map("DominioAD.IdDominioAD")]
        public long? DomainAdId { get; set; }

        [JsonProperty("Ativo")]
        public bool Active { get; set; }

        [NoMap]
        [JsonProperty("PerfilUsuario")]
        public string UserProfile { get; set; }

        [NoMap]
        [JsonProperty("IdPerfilUsuario")]
        public long UserProfileId { get; set; }

        [NoMap]
        [JsonProperty("Token")]
        public string Token { get; set; }

        [NoMap]
        [JsonProperty("Permissoes")]
        public List<PermissionDTO> Permissions { get; set; }
        public List<string> MemberOf { get; set; }
    }
}
