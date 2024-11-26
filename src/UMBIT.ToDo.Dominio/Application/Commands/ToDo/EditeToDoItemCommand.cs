using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMBIT.ToDo.Core.Messages.Messagem.Applications.Commands;

namespace UMBIT.ToDo.Dominio.Application.Commands.ToDo
{
    internal class EditeToDoItemCommand : UMBITCommand<EditeToDoItemCommand>
    {
        public Guid Id { get; set; }
        public Guid? IdToDoList { get; set; }
        public int Index { get; set; }
        public string Nome { get; set; }
        public string? Descricao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public int Status { get; set; }

        protected override void Validadors(ValidatorCommand<EditeToDoItemCommand> validator)
        {
            throw new NotImplementedException();
        }
    }
}
