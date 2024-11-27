namespace UMBIT.ToDo.Web.Models
{
    public class AdicionarAdministradorRequestDTO
    {

        [System.Text.Json.Serialization.JsonPropertyName("Nome")]
        public string Nome { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("email")]
        public string Email { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("password")]
        public string Password { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("confirmPassword")]
        public string ConfirmPassword { get; set; }
    }
}
