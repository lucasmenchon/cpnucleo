namespace Cpnucleo.Domain.Entities;

public sealed class Impedimento : BaseEntity
{
    public string? Nome { get; private set; }

    public static Impedimento Create(string nome)
    {
        return new Impedimento
        {
            Id = Guid.NewGuid(),
            Nome = nome,
            DataInclusao = DateTime.UtcNow,
            Ativo = true
        };
    }

    public static Impedimento Update(Impedimento impedimento, string nome)
    {
        impedimento.Nome = nome;
        impedimento.DataAlteracao = DateTime.UtcNow;

        return impedimento;
    }

    public static Impedimento Remove(Impedimento impedimento)
    {
        impedimento.Ativo = false;
        impedimento.DataExclusao = DateTime.UtcNow;

        return impedimento;
    }
}
