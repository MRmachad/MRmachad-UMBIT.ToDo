using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMBIT.ToDo.Core.Messages.Messagem.Applications.Commands;

namespace UMBIT.ToDo.Dominio.Application.Commands.ToDo
{
    public class EditeToDoListItemCommand : UMBITCommand<EditeToDoListItemCommand>
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        protected override void Validadors(ValidatorCommand<EditeToDoListItemCommand> validator)
        {
            throw new NotImplementedException();
        }
    }
}
