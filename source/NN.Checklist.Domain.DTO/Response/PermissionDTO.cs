using Newtonsoft.Json;

namespace NN.Checklist.Domain.DTO.Response
{
    public class PermissionDTO
    {
        [JsonProperty("IdPermissao")]
        public long PermissionId { get; set; }

        [JsonProperty("Tag")]
        public string Tag { get; set; }

        [JsonProperty("Descricao")]
        public string Description { get; set; }
    }
}
