using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UMBIT.ToDo.Web.Models
{
    public class TaskDTO
    {
        [JsonPropertyName("id")]    
        public Guid Id { get; set; }

        [JsonPropertyName("idToDoList")]

        [Display(Name = "Lista")]
        public Guid? IdToDoList { get; set; }

        [JsonPropertyName("index")]
        public int Index { get; set; }

        [JsonPropertyName("nome")]
        public string Titulo { get; set; }

        [JsonPropertyName("descricao")]
        public string? Descricao { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; }


        [JsonPropertyName("dataInicio")]
        [Display(Name = "Data de Inicio")]
        public DateTime DataInicio { get; set; }

        [JsonPropertyName("dataFim")]
        [Display(Name = "Data de Fim")]
        public DateTime DataFim { get; set; }
    }
}
