using System.Text.Json.Serialization;

namespace UMBIT.ToDo.Web.Models
{
    public class AuthStatusDTO
    {
        [JsonPropertyName("configured")]
        public bool Configured { get; set; }
    }
}
