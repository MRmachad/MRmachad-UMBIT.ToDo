using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMBIT.ToDo.Core.Messages.Messagem.Applications.Commands;

namespace UMBIT.ToDo.Dominio.Application.Commands.ToDo
{
    public class DeleteToDoItemCommand : UMBITCommand<DeleteToDoItemCommand>
    {
        public Guid Id { get; set; }
        protected override void Validadors(ValidatorCommand<DeleteToDoItemCommand> validator)
        {
            throw new NotImplementedException();
        }
    }
}
