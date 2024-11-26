using FluentValidation;
using UMBIT.ToDo.Core.Messages.Messagem.Applications.Commands;
using UMBIT.ToDo.Dominio.Utilitarios;

namespace UMBIT.ToDo.Dominio.Application.Commands.Autenticacao
{
    public class AdicionarAdministradorCommand : UMBITCommand<AdicionarAdministradorCommand>
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
        public string ConfirmarSenha { get; private set; }

        protected AdicionarAdministradorCommand() { }

        public AdicionarAdministradorCommand(
            string nome,
            string email,
            string senha,
            string confirmarSenha)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            ConfirmarSenha = confirmarSenha;
        }

        protected override void Validadors(ValidatorCommand<AdicionarAdministradorCommand> validator)
        {
            validator
                .RuleFor(cmd => cmd.Nome)
                .NotEmpty().WithMessage("Nome é obrigatório")
                .Must(nome => !string.IsNullOrEmpty(nome) && nome.Split(' ').Length >= 2).WithMessage("Nome deve conter ao menos nome e sobrenome.");

            validator
                .RuleFor(cmd => cmd.Email)
                .SetValidator(new EmailValidator());

            validator
                .RuleFor(cmd => cmd.Senha)
                .SetValidator(new PasswordValidator());

            validator
                .RuleFor(cmd => cmd.ConfirmarSenha)
                .Equal(cmd => cmd.Senha).WithMessage("O campo 'Confirmar Senha' e 'Senha' devem ser iguais.");
        }
    }
}
