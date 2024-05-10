using System.ComponentModel.DataAnnotations;

namespace UMBIT.ToDo.Web.Models
{
    public class ListTaskDTO
    {
        public Guid Id { get; set; }

        [Display(Name = "Nome"), Required]
        public string Nome { get; set; }
    }
}
